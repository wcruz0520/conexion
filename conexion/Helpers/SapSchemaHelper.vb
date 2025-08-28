Imports SAPbobsCOM
Imports System.Data

Public NotInheritable Class SapSchemaHelper
    Private Sub New()
    End Sub

    Public Shared Function IsHana(company As Company) As Boolean
        Return company.DbServerType = BoDataServerTypes.dst_HANADB
    End Function

    '========================
    ' 1) Esquema de columnas
    '========================
    Public Shared Function GetTableSchema(company As Company, tableName As String, Optional schemaName As String = Nothing) As DataTable
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim dt As New DataTable("TableSchema")
        Try
            Dim q As String
            If IsHana(company) Then
                ' HANA
                ' Si no envías schemaName, usamos CURRENT_SCHEMA
                If String.IsNullOrWhiteSpace(schemaName) Then
                    q = $"SELECT COLUMN_NAME      AS ""ColumnName"",
                                 DATA_TYPE_NAME   AS ""DataType"",
                                 LENGTH           AS ""Length"",
                                 SCALE            AS ""Scale"",
                                 IS_NULLABLE      AS ""IsNullable"",
                                 POSITION         AS ""Ordinal""
                          FROM ""SYS"".""TABLE_COLUMNS""
                         WHERE SCHEMA_NAME = CURRENT_SCHEMA
                           AND TABLE_NAME  = '{tableName.Replace("'", "''")}'
                      ORDER BY POSITION"
                Else
                    q = $"SELECT COLUMN_NAME      AS ""ColumnName"",
                                 DATA_TYPE_NAME   AS ""DataType"",
                                 LENGTH           AS ""Length"",
                                 SCALE            AS ""Scale"",
                                 IS_NULLABLE      AS ""IsNullable"",
                                 POSITION         AS ""Ordinal""
                          FROM ""SYS"".""TABLE_COLUMNS""
                         WHERE SCHEMA_NAME = '{schemaName.Replace("'", "''")}'
                           AND TABLE_NAME  = '{tableName.Replace("'", "''")}'
                      ORDER BY POSITION"
                End If
            Else
                ' SQL Server
                Dim sch As String = If(String.IsNullOrWhiteSpace(schemaName), "dbo", schemaName)
                q = $"SELECT c.COLUMN_NAME   AS [ColumnName],
                             c.DATA_TYPE     AS [DataType],
                             ISNULL(c.CHARACTER_MAXIMUM_LENGTH, 0) AS [Length],
                             ISNULL(c.NUMERIC_SCALE, 0)             AS [Scale],
                             c.IS_NULLABLE  AS [IsNullable],
                             c.ORDINAL_POSITION AS [Ordinal]
                        FROM INFORMATION_SCHEMA.COLUMNS c
                       WHERE c.TABLE_SCHEMA = '{sch.Replace("'", "''")}'
                         AND c.TABLE_NAME   = '{tableName.Replace("'", "''")}'
                    ORDER BY c.ORDINAL_POSITION"
            End If

            rs.DoQuery(q)
            dt = RecordsetToDataTable(rs)
            ' Añadimos flag UDF por conveniencia
            If Not dt.Columns.Contains("IsUdf") Then dt.Columns.Add("IsUdf", GetType(Boolean))
            For Each r As DataRow In dt.Rows
                r("IsUdf") = CStr(r("ColumnName")).StartsWith("U_", StringComparison.OrdinalIgnoreCase)
            Next
            Return dt
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Function

    '========================
    ' 2) Columnas clave (PK)
    '========================
    Public Shared Function GetPrimaryKeyColumns(company As Company, tableName As String, Optional schemaName As String = Nothing) As List(Of String)
        Dim keys As New List(Of String)()
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Try
            Dim q As String
            If IsHana(company) Then
                If String.IsNullOrWhiteSpace(schemaName) Then
                    q = $"SELECT cc.COLUMN_NAME
                           FROM ""SYS"".""CONSTRAINTS"" c
                           JOIN ""SYS"".""CONSTRAINT_COLUMNS"" cc
                             ON c.SCHEMA_NAME = cc.SCHEMA_NAME
                            AND c.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
                          WHERE c.SCHEMA_NAME = CURRENT_SCHEMA
                            AND c.TABLE_NAME  = '{tableName.Replace("'", "''")}'
                            AND c.CONSTRAINT_TYPE = 'PRIMARY KEY'
                       ORDER BY cc.POSITION"
                Else
                    q = $"SELECT cc.COLUMN_NAME
                           FROM ""SYS"".""CONSTRAINTS"" c
                           JOIN ""SYS"".""CONSTRAINT_COLUMNS"" cc
                             ON c.SCHEMA_NAME = cc.SCHEMA_NAME
                            AND c.CONSTRAINT_NAME = cc.CONSTRAINT_NAME
                          WHERE c.SCHEMA_NAME = '{schemaName.Replace("'", "''")}'
                            AND c.TABLE_NAME  = '{tableName.Replace("'", "''")}'
                            AND c.CONSTRAINT_TYPE = 'PRIMARY KEY'
                       ORDER BY cc.POSITION"
                End If
            Else
                Dim sch As String = If(String.IsNullOrWhiteSpace(schemaName), "dbo", schemaName)
                q = $"SELECT KU.COLUMN_NAME
                        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS TC
                        JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KU
                          ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
                         AND TC.TABLE_SCHEMA   = KU.TABLE_SCHEMA
                         AND TC.TABLE_NAME     = KU.TABLE_NAME
                       WHERE TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
                         AND TC.TABLE_SCHEMA    = '{sch.Replace("'", "''")}'
                         AND TC.TABLE_NAME      = '{tableName.Replace("'", "''")}'
                    ORDER BY KU.ORDINAL_POSITION"
            End If

            rs.DoQuery(q)
            While Not rs.EoF
                keys.Add(CStr(rs.Fields.Item(0).Value))
                rs.MoveNext()
            End While
            Return keys
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Function

    '=========================================
    ' 3) UDFs (CUFD) registrados en la tabla
    '=========================================
    Public Shared Function GetUdfs(company As Company, tableName As String) As DataTable
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Try
            Dim q As String
            If IsHana(company) Then
                q = $"SELECT ""FieldID"",
                             ""AliasID"",
                             ""Descr"",
                             ""EditType"",
                             ""Size"",
                             ""Default"",
                             ""NotNull""
                        FROM ""CUFD""
                       WHERE ""TableID"" = '{tableName.Replace("'", "''")}'
                    ORDER BY ""FieldID"""
            Else
                q = $"SELECT FieldID,
                             AliasID,
                             Descr,
                             EditType,
                             Size,
                             [Default],
                             NotNull
                        FROM CUFD
                       WHERE TableID = '{tableName.Replace("'", "''")}'
                    ORDER BY FieldID"
            End If
            rs.DoQuery(q)
            Return RecordsetToDataTable(rs)
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Function

    '===================================
    ' 4) UDT (OUTB): tipo y descripción
    '===================================
    Public Shared Function GetUdtInfo(company As Company, tableName As String) As DataTable
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Try
            Dim q As String
            If IsHana(company) Then
                q = $"SELECT ""TableName"", ""TableDescr"", ""TableType""
                        FROM ""OUTB""
                       WHERE ""TableName"" = '{tableName.Replace("'", "''")}'"
            Else
                q = $"SELECT TableName, TableDescr, TableType
                        FROM OUTB
                       WHERE TableName = '{tableName.Replace("'", "''")}'"
            End If
            rs.DoQuery(q)
            Return RecordsetToDataTable(rs)
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Function

    '=====================================================
    ' 5) UDO (OUDO/UDO1): cabecera y children (alias reales)
    '=====================================================
    Public Shared Function GetUdoDefinition(company As Company, udoCode As String) As (HeaderTable As String, ChildTables As List(Of String))
        Dim header As String = Nothing
        Dim children As New List(Of String)()
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Try
            Dim q1 As String
            If IsHana(company) Then
                q1 = $"SELECT ""TableName""
                         FROM ""OUDO""
                        WHERE ""Code"" = '{udoCode.Replace("'", "''")}'"
            Else
                q1 = $"SELECT TableName
                         FROM OUDO
                        WHERE Code = '{udoCode.Replace("'", "''")}'"
            End If
            rs.DoQuery(q1)
            If rs.RecordCount > 0 Then header = CStr(rs.Fields.Item(0).Value)

            Dim q2 As String
            If IsHana(company) Then
                q2 = $"SELECT ""ChildTable""
                         FROM ""UDO1""
                        WHERE ""Code"" = '{udoCode.Replace("'", "''")}'
                     ORDER BY ""LineId"""
            Else
                q2 = $"SELECT ChildTable
                         FROM UDO1
                        WHERE Code = '{udoCode.Replace("'", "''")}'
                     ORDER BY LineId"
            End If
            rs.DoQuery(q2)
            While Not rs.EoF
                children.Add(CStr(rs.Fields.Item(0).Value))
                rs.MoveNext()
            End While
            Return (header, children)
        Finally
            If rs IsNot Nothing Then Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        End Try
    End Function

    '========================
    ' Utilidad: RS -> DataTable
    '========================
    Private Shared Function RecordsetToDataTable(rs As Recordset) As DataTable
        Dim dt As New DataTable()
        For i As Integer = 0 To rs.Fields.Count - 1
            dt.Columns.Add(rs.Fields.Item(i).Name, GetType(String))
        Next
        While Not rs.EoF
            Dim row = dt.NewRow()
            For i As Integer = 0 To rs.Fields.Count - 1
                Dim v = rs.Fields.Item(i).Value
                row(i) = If(v Is Nothing, Nothing, v.ToString())
            Next
            dt.Rows.Add(row)
            rs.MoveNext()
        End While
        Return dt
    End Function
End Class
