Imports SAPbobsCOM
Imports Microsoft.VisualBasic.FileIO
Imports System.Globalization
Imports conexion.ImportModels

Public Interface IImporter
    ' simulate: True = rollback; False = commit
    ' onRow(index, key, action, ok, message)
    Sub Process(simulate As Boolean, onRow As Action(Of Integer, String, RowAction, Boolean, String))
    Function CountRows() As Integer
End Interface

' ------------ UDO / UDT ------------
Public Class UdoImporter : Implements IImporter
    Private ReadOnly _company As Company
    Private ReadOnly _headerTable As String
    Private ReadOnly _detailTable As String
    Private ReadOnly _headerFile As String
    Private ReadOnly _detailFile As String
    Private ReadOnly _delimiter As String

    Public Sub New(company As Company, headerTable As String, detailTable As String, headerFile As String, detailFile As String, delimiter As String)
        _company = company : _headerTable = headerTable : _detailTable = detailTable
        _headerFile = headerFile : _detailFile = detailFile : _delimiter = delimiter
    End Sub

    Public Function CountRows() As Integer Implements IImporter.CountRows
        Return CountFileRows(_headerFile, _delimiter)
    End Function

    Public Sub Process(simulate As Boolean, onRow As Action(Of Integer, String, RowAction, Boolean, String)) Implements IImporter.Process
        Dim headers = LoadFile(_headerFile, _delimiter)
        Dim details = LoadFile(_detailFile, _delimiter)

        Dim svc = _company.GetCompanyService()
        ' *** UDO CODE real por OUDO, con fallback ***
        Dim udoCode As String = ResolveUdoCode(_company, _headerTable)
        Dim gen = svc.GetGeneralService(udoCode)

        Dim idx As Integer = 0
        Dim started = False
        If Not _company.InTransaction Then _company.StartTransaction() : started = True

        Try
            For Each h As DataRow In headers.Rows
                idx += 1
                Dim key As String = SafeStr(h, "Code")
                If String.IsNullOrWhiteSpace(key) Then
                    onRow(idx, "(sin Code)", RowAction.Create, False, "Fila sin 'Code'")
                    Continue For
                End If

                Dim action As RowAction = RowAction.Create
                Dim data As GeneralData = Nothing
                Try
                    Dim p As GeneralDataParams = CType(gen.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams), GeneralDataParams)
                    p.SetProperty("Code", key)
                    data = gen.GetByParams(p) : action = RowAction.Update
                Catch
                    data = CType(gen.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData), GeneralData)
                    data.SetProperty("Code", key) : action = RowAction.Create
                End Try

                ' Cabecera (omite DocEntry/Code)
                For Each c As DataColumn In headers.Columns
                    Dim name = c.ColumnName
                    If name.Equals("Code", StringComparison.OrdinalIgnoreCase) Then Continue For
                    If name.Equals("DocEntry", StringComparison.OrdinalIgnoreCase) Then Continue For
                    Dim v = SafeStr(h, name)
                    If v = "" Then Continue For
                    Try : data.SetProperty(name, v)
                    Catch ex As Exception : onRow(idx, key, action, False, $"Cabecera {name}: {ex.Message}") : Continue For
                    End Try
                Next

                ' Child alias exacto desde UDO1, con fallback
                Dim childAlias = ResolveChildAlias(_company, udoCode, _detailTable)

                Dim lines = data.Child(childAlias)
                ' Limpia si update
                If action = RowAction.Update Then
                    For i = lines.Count - 1 To 0 Step -1 : lines.Remove(i) : Next
                End If
                For Each d As DataRow In details.Select($"Code = '{key.Replace("'", "''")}'")
                    Dim line = lines.Add()
                    For Each dc As DataColumn In details.Columns
                        Dim dn = dc.ColumnName
                        If dn.Equals("Code", StringComparison.OrdinalIgnoreCase) Then Continue For
                        If dn.Equals("LineId", StringComparison.OrdinalIgnoreCase) Then Continue For
                        Dim v = SafeStr(d, dn)
                        If v = "" Then Continue For
                        Try : line.SetProperty(dn, ParseValue(v))
                        Catch ex As Exception : onRow(idx, key, action, False, $"Detalle {dn}: {ex.Message}")
                        End Try
                    Next
                Next

                Try
                    If action = RowAction.Create Then gen.Add(data) Else gen.Update(data)
                    onRow(idx, key, action, True, "OK")
                Catch ex As Exception
                    onRow(idx, key, action, False, ex.Message)
                End Try
            Next

            If started Then
                If simulate Then _company.EndTransaction(BoWfTransOpt.wf_RollBack) Else _company.EndTransaction(BoWfTransOpt.wf_Commit)
            End If
        Catch ex As Exception
            If started AndAlso _company.InTransaction Then _company.EndTransaction(BoWfTransOpt.wf_RollBack)
            onRow(idx, "GENERAL", RowAction.Create, False, ex.Message)
        End Try
    End Sub

    ' -------- Helpers UDO --------
    Private Shared Function ResolveUdoCode(company As Company, headerTable As String) As String
        Dim rs = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim q = If(company.DbServerType = BoDataServerTypes.dst_HANADB,
                   $"SELECT ""Code"" FROM ""OUDO"" WHERE ""TableName""='{headerTable.Replace("'", "''")}'",
                   $"SELECT Code FROM OUDO WHERE TableName='{headerTable.Replace("'", "''")}'")
        rs.DoQuery(q)
        Dim code = If(rs.RecordCount > 0, CStr(rs.Fields.Item(0).Value), headerTable.TrimStart("@"c))
        Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        Return code
    End Function

    Private Shared Function ResolveChildAlias(company As Company, udoCode As String, detailTable As String) As String
        Dim rs = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim q = If(company.DbServerType = BoDataServerTypes.dst_HANADB,
                   $"SELECT ""SonName"" FROM ""UDO1"" WHERE ""Code""='{udoCode.Replace("'", "''")}'",
                   $"SELECT SonName FROM UDO1 WHERE Code='{udoCode.Replace("'", "''")}'")
        rs.DoQuery(q)
        Dim aliasName As String = detailTable.TrimStart("@"c)
        While Not rs.EoF
            Dim ct = CStr(rs.Fields.Item(0).Value)
            If String.Equals(ct, detailTable, StringComparison.OrdinalIgnoreCase) Then aliasName = ct.TrimStart("@"c)
            rs.MoveNext()
        End While
        Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        Return aliasName
    End Function

    ' -------- Utilidades comunes --------
    Private Shared Function LoadFile(path As String, delimiter As String) As DataTable
        Dim dt As New DataTable()
        Using p As New TextFieldParser(path, System.Text.Encoding.UTF8)
            p.TextFieldType = FieldType.Delimited
            p.SetDelimiters(delimiter)
            Dim headers = p.ReadFields() : If headers Is Nothing Then Return dt
            For Each h In headers : dt.Columns.Add(h) : Next
            While Not p.EndOfData
                Dim f = p.ReadFields() : If f Is Nothing Then Exit While
                dt.Rows.Add(f)
            End While
        End Using
        Return dt
    End Function

    Public Shared Function CountFileRows(path As String, delimiter As String) As Integer
        Dim c = 0
        Using p As New TextFieldParser(path, System.Text.Encoding.UTF8)
            p.TextFieldType = FieldType.Delimited : p.SetDelimiters(delimiter)
            Dim rf = p.ReadFields()
            While Not p.EndOfData : Dim __ = p.ReadFields() : c += 1 : End While
        End Using
        Return c
    End Function

    Private Shared Function SafeStr(r As DataRow, col As String) As String
        If Not r.Table.Columns.Contains(col) Then Return ""
        Dim o = r(col) : If o Is Nothing OrElse o Is DBNull.Value Then Return "" Else Return o.ToString().Trim()
    End Function

    Private Shared Function ParseValue(value As String) As Object
        Dim d As Date
        If Date.TryParseExact(value, {"dd/MM/yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "MM/dd/yyyy"}, CultureInfo.InvariantCulture, DateTimeStyles.None, d) Then Return d
        Dim s = value.Replace(","c, "."c)
        Dim decVal As Decimal : If Decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, decVal) Then Return decVal
        Dim intVal As Integer : If Integer.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, intVal) Then Return intVal
        Return value
    End Function
End Class

' ------------ Tablas nativas (genéricas) ------------
Public Class NativeTableImporter : Implements IImporter
    Private ReadOnly _company As Company
    Private ReadOnly _table As String
    Private ReadOnly _file As String
    Private ReadOnly _delimiter As String

    Public Sub New(company As Company, table As String, file As String, delimiter As String)
        _company = company : _table = table : _file = file : _delimiter = delimiter
    End Sub

    Public Function CountRows() As Integer Implements IImporter.CountRows
        Return UdoImporter.CountFileRows(_file, _delimiter)
    End Function

    Public Sub Process(simulate As Boolean, onRow As Action(Of Integer, String, RowAction, Boolean, String)) Implements IImporter.Process
        Dim dt = Load(_file, _delimiter)
        Dim pk = GetPrimaryKeys(_company, _table)
        If pk.Count = 0 Then Throw New ApplicationException($"No se encontró PK para la tabla {_table}.")

        Dim rs = CType(_company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim started = False : If Not _company.InTransaction Then _company.StartTransaction() : started = True

        Try
            Dim i = 0
            For Each r As DataRow In dt.Rows
                i += 1
                Dim where = BuildWhere(pk, r)
                Dim _exists As Boolean = Exists(rs, _table, where)

                Dim action = If(_exists, RowAction.Update, RowAction.Create)
                Dim sql As String = If(_exists, BuildUpdate(_table, r, where), BuildInsert(_table, r))

                Try
                    rs.DoQuery(sql)
                    onRow(i, GetKeyDisplay(pk, r), action, True, "OK")
                Catch ex As Exception
                    onRow(i, GetKeyDisplay(pk, r), action, False, ex.Message)
                End Try
            Next

            If started Then
                If simulate Then _company.EndTransaction(BoWfTransOpt.wf_RollBack) Else _company.EndTransaction(BoWfTransOpt.wf_Commit)
            End If
        Catch ex As Exception
            If started AndAlso _company.InTransaction Then _company.EndTransaction(BoWfTransOpt.wf_RollBack)
            onRow(0, "GENERAL", RowAction.Create, False, ex.Message)
        Finally
            Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Sub

    ' ---- helpers SQL/HANA ----
    Private Function Exists(rs As Recordset, table As String, whereClause As String) As Boolean
        Dim q = If(IsHana(_company),
                   $"SELECT 1 FROM ""{table}"" WHERE {whereClause}",
                   $"IF EXISTS(SELECT 1 FROM [{table}] WHERE {whereClause}) SELECT 1 ELSE SELECT 0")
        rs.DoQuery(q) : Return rs.RecordCount > 0 AndAlso CInt(rs.Fields.Item(0).Value) = 1
    End Function

    Private Function BuildInsert(table As String, r As DataRow) As String
        Dim cols = New List(Of String)()
        Dim vals = New List(Of String)()
        For Each c As DataColumn In r.Table.Columns
            cols.Add(Quote(c.ColumnName))
            vals.Add(Lit(r(c), c.DataType))
        Next
        If IsHana(_company) Then
            Return $"INSERT INTO ""{table}"" ({String.Join(",", cols)}) VALUES ({String.Join(",", vals)})"
        Else
            Return $"INSERT INTO [{table}] ({String.Join(",", cols)}) VALUES ({String.Join(",", vals)})"
        End If
    End Function

    Private Function BuildUpdate(table As String, r As DataRow, whereClause As String) As String
        Dim sets = New List(Of String)()
        For Each c As DataColumn In r.Table.Columns
            sets.Add($"{Quote(c.ColumnName)} = {Lit(r(c), c.DataType)}")
        Next
        If IsHana(_company) Then
            Return $"UPDATE ""{table}"" SET {String.Join(",", sets)} WHERE {whereClause}"
        Else
            Return $"UPDATE [{table}] SET {String.Join(",", sets)} WHERE {whereClause}"
        End If
    End Function

    Private Function BuildWhere(pk As List(Of String), r As DataRow) As String
        Dim parts = New List(Of String)()
        For Each k In pk
            Dim v = r(k) : parts.Add($"{Quote(k)} = {Lit(v, v?.GetType())}")
        Next
        Return String.Join(" AND ", parts)
    End Function

    Private Shared Function GetKeyDisplay(pk As List(Of String), r As DataRow) As String
        Return String.Join("|", pk.Select(Function(k) $"{k}:{Convert.ToString(r(k))}"))
    End Function

    Private Function Quote(col As String) As String
        Return If(IsHana(_company), $"""{col}""", $"[{col}]")
    End Function

    Private Shared Function IsHana(company As Company) As Boolean
        Return company.DbServerType = BoDataServerTypes.dst_HANADB
    End Function

    Private Function Lit(v As Object, t As Type) As String
        If v Is Nothing OrElse v Is DBNull.Value Then Return "NULL"
        Dim s = Convert.ToString(v).Replace("'", "''")
        If t Is GetType(Date) OrElse t Is GetType(DateTime) Then
            ' SAP B1 usa formato YYYY-MM-DD
            Dim d As Date = Convert.ToDateTime(v)
            Return $"'{d:yyyy-MM-dd}'"
        End If
        If TypeOf v Is String Then Return $"'{s}'"
        If TypeOf v Is Boolean Then Return If(CType(v, Boolean), "1", "0")
        ' numérico
        Return s.Replace(","c, "."c)
    End Function

    Private Function Load(path As String, delimiter As String) As DataTable
        Dim dt As New DataTable()
        Using p As New TextFieldParser(path, System.Text.Encoding.UTF8)
            p.TextFieldType = FieldType.Delimited : p.SetDelimiters(delimiter)
            Dim headers = p.ReadFields() : If headers Is Nothing Then Return dt
            For Each h In headers : dt.Columns.Add(h) : Next
            While Not p.EndOfData
                Dim f = p.ReadFields() : If f Is Nothing Then Exit While
                dt.Rows.Add(f)
            End While
        End Using
        Return dt
    End Function

    Private Function GetPrimaryKeys(company As Company, table As String) As List(Of String)
        Dim rs = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim list As New List(Of String)()
        Dim q As String
        If IsHana(company) Then
            q = $"SELECT cc.COLUMN_NAME
                   FROM ""SYS"".""CONSTRAINTS"" c
                   JOIN ""SYS"".""CONSTRAINT_COLUMNS"" cc
                     ON c.SCHEMA_NAME = cc.SCHEMA_NAME
                    AND c.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
                  WHERE c.SCHEMA_NAME = CURRENT_SCHEMA
                    AND c.TABLE_NAME  = '{table.Replace("'", "''")}'
                    AND c.CONSTRAINT_TYPE = 'PRIMARY KEY'
               ORDER BY cc.POSITION"
        Else
            q = $"SELECT KU.COLUMN_NAME
                    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
                    JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
                      ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
                     AND TC.TABLE_SCHEMA   = KU.TABLE_SCHEMA
                     AND TC.TABLE_NAME     = KU.TABLE_NAME
                   WHERE TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
                     AND TC.TABLE_SCHEMA    = 'dbo'
                     AND TC.TABLE_NAME      = '{table.Replace("'", "''")}'
                ORDER BY KU.ORDINAL_POSITION"
        End If
        rs.DoQuery(q)
        While Not rs.EoF
            list.Add(CStr(rs.Fields.Item(0).Value))
            rs.MoveNext()
        End While
        Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        Return list
    End Function
End Class
