Imports SAPbobsCOM
Imports Microsoft.VisualBasic.FileIO
Imports System.Globalization

Public Class UdoFileProcessor

    ' == API PÚBLICA (mismas firmas que ya llamas desde ExecuteProcess) ==
    Public Shared Sub ProcessReal(company As Company,
                                  headerTable As String,
                                  detailTable As String,
                                  headerFilePath As String,
                                  detailFilePath As String)
        ProcessInternal(company, headerTable, detailTable, headerFilePath, detailFilePath, simulate:=False)
    End Sub

    Public Shared Sub ProcessSimulation(company As Company,
                                        headerTable As String,
                                        detailTable As String,
                                        headerFilePath As String,
                                        detailFilePath As String)
        ProcessInternal(company, headerTable, detailTable, headerFilePath, detailFilePath, simulate:=True)
    End Sub

    ' == IMPLEMENTACIÓN ==
    Private Shared Sub ProcessInternal(company As Company,
                                       headerTable As String,
                                       detailTable As String,
                                       headerFilePath As String,
                                       detailFilePath As String,
                                       simulate As Boolean)

        Dim headers As DataTable = LoadFile(headerFilePath)
        Dim details As DataTable = LoadFile(detailFilePath)

        If headers Is Nothing OrElse headers.Rows.Count = 0 Then Exit Sub

        Dim svc As CompanyService = Nothing
        Dim gen As GeneralService = Nothing

        ' UDO code / service name: por compatibilidad asumimos = tabla sin @ (p.ej. @SS_REEMCAB -> "SS_REEMCAB")
        Dim udoCode As String = headerTable.TrimStart("@"c)

        Dim headerUdfs As HashSet(Of String) = GetUdfAliasSet(company, headerTable)
        Dim detailUdfs As HashSet(Of String) = GetUdfAliasSet(company, detailTable)

        ' Alias del child: por defecto el nombre de la tabla sin @
        Dim childAlias As String = detailTable.TrimStart("@"c)

        Dim startedTx As Boolean = False
        Try
            ' Transacción: en simulación SIEMPRE se hace rollback al final
            If Not company.InTransaction Then
                company.StartTransaction()
                startedTx = True
            End If

            svc = company.GetCompanyService()
            gen = svc.GetGeneralService(udoCode)

            Dim i As Integer
            For i = 0 To headers.Rows.Count - 1
                Dim h As DataRow = headers.Rows(i)
                Dim code As String = SafeStr(h, "Code")

                If String.IsNullOrWhiteSpace(code) Then
                    LogError("(sin Code)", "Fila de cabecera sin 'Code'.")
                    Continue For
                End If

                Dim isUpdate As Boolean = False
                Dim data As GeneralData = Nothing

                ' --- Cargar si existe (UPDATE) o crear nuevo (ADD) ---
                Try
                    Dim p As GeneralDataParams = CType(gen.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams), GeneralDataParams)
                    p.SetProperty("Code", code)
                    data = gen.GetByParams(p)     ' existe -> UPDATE
                    isUpdate = True
                Catch
                    data = CType(gen.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData), GeneralData) ' no existe -> ADD
                    ' En ADD hay que setear Code explícitamente
                    data.SetProperty("Code", code)
                End Try

                ' --- CABECERA ---
                For Each col As DataColumn In headers.Columns
                    Dim colName As String = col.ColumnName
                    If colName.Equals("Code", StringComparison.OrdinalIgnoreCase) Then Continue For
                    If colName.Equals("DocEntry", StringComparison.OrdinalIgnoreCase) Then Continue For ' B1 lo maneja

                    Dim value As String = SafeStr(h, colName)
                    If value = "" Then Continue For

                    Try
                        If colName.Equals("Name", StringComparison.OrdinalIgnoreCase) Then
                            ' Name: sólo en ALTAS
                            If Not isUpdate Then data.SetProperty("Name", value)
                            Continue For
                        End If

                        ' Para UDFs: sólo si existen en CUFD (registrados en B1)
                        If colName.StartsWith("U_", StringComparison.OrdinalIgnoreCase) Then
                            If Not headerUdfs.Contains(colName) Then
                                LogError(code, $"Cabecera: UDF '{colName}' no existe en B1 (CUFD). Créalo desde B1 o DI-API.")
                                Continue For
                            End If
                        End If

                        data.SetProperty(colName, ParseValue(value))
                    Catch ex As Exception
                        LogError(code, $"Cabecera: error seteando '{colName}'='{value}': {ex.Message}")
                    End Try
                Next

                ' --- DETALLE ---
                Dim lines As GeneralDataCollection = data.Child(childAlias)

                ' Si es UPDATE: limpiar líneas (no manejamos LineId en archivo)
                If isUpdate Then
                    For idx As Integer = lines.Count - 1 To 0 Step -1
                        lines.Remove(idx)
                    Next
                End If

                ' Agregar líneas del archivo con el mismo Code
                Dim dRows() As DataRow = details.Select("Code = '" & code.Replace("'", "''") & "'")
                For Each d As DataRow In dRows
                    Dim line As GeneralData = lines.Add()
                    For Each dcol As DataColumn In details.Columns
                        Dim dname As String = dcol.ColumnName
                        If dname.Equals("Code", StringComparison.OrdinalIgnoreCase) Then Continue For
                        If dname.Equals("LineId", StringComparison.OrdinalIgnoreCase) Then Continue For ' B1 lo maneja

                        Dim v As String = SafeStr(d, dname)
                        If v = "" Then Continue For

                        Try
                            If dname.StartsWith("U_", StringComparison.OrdinalIgnoreCase) Then
                                If Not detailUdfs.Contains(dname) Then
                                    LogError(code, $"Detalle: UDF '{dname}' no existe en B1 (CUFD). Créalo desde B1 o DI-API.")
                                    Continue For
                                End If
                            End If

                            line.SetProperty(dname, ParseValue(v))
                        Catch ex As Exception
                            LogError(code, $"Detalle: error seteando '{dname}'='{v}': {ex.Message}")
                        End Try
                    Next
                Next

                ' --- ADD / UPDATE ---
                Try
                    If isUpdate Then
                        gen.Update(data)
                    Else
                        gen.Add(data)
                    End If
                Catch ex As Exception
                    LogError(code, $"Error al {(If(isUpdate, "actualizar", "agregar"))} el registro: {ex.Message}")
                End Try
            Next

            ' --- FIN TRANSACCIÓN ---
            If startedTx Then
                If simulate Then
                    company.EndTransaction(BoWfTransOpt.wf_RollBack)
                Else
                    company.EndTransaction(BoWfTransOpt.wf_Commit)
                End If
            End If

        Catch ex As Exception
            ' Falla mayor -> rollback si abrimos la transacción aquí
            If startedTx AndAlso company.InTransaction Then
                Try : company.EndTransaction(BoWfTransOpt.wf_RollBack) : Catch : End Try
            End If
            LogError("(general)", ex.Message)

        Finally
            ' Liberación COM
            If gen IsNot Nothing Then Try : System.Runtime.InteropServices.Marshal.ReleaseComObject(gen) : Catch : End Try
            If svc IsNot Nothing Then Try : System.Runtime.InteropServices.Marshal.ReleaseComObject(svc) : Catch : End Try
        End Try
    End Sub

    ' == UTILIDADES ==
    Private Shared Function LoadFile(path As String) As DataTable
        Dim dt As New DataTable()
        If String.IsNullOrWhiteSpace(path) OrElse Not IO.File.Exists(path) Then Return dt

        Using parser As New TextFieldParser(path, System.Text.Encoding.UTF8)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(vbTab)

            Dim headers = parser.ReadFields()
            If headers Is Nothing Then Return dt
            Dim h As String
            For Each h In headers
                dt.Columns.Add(h)
            Next

            While Not parser.EndOfData
                Dim fields = parser.ReadFields()
                If fields Is Nothing Then Exit While
                dt.Rows.Add(fields)
            End While
        End Using
        Return dt
    End Function

    Private Shared Function SafeStr(r As DataRow, col As String) As String
        If Not r.Table.Columns.Contains(col) Then Return ""
        Dim o As Object = r(col)
        If o Is Nothing OrElse o Is DBNull.Value Then Return ""
        Return Convert.ToString(o).Trim()
    End Function

    Private Shared Function ParseValue(value As String) As Object
        ' Fechas comunes: dd/MM/yyyy, yyyy-MM-dd, dd-MM-yyyy, MM/dd/yyyy
        Dim d As Date
        If Date.TryParseExact(value,
                              New String() {"dd/MM/yyyy", "yyyy-MM-dd", "dd-MM-yyyy", "MM/dd/yyyy"},
                              CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              d) Then
            Return d
        End If

        ' Decimal/integer (usa Invariant; si viene con coma decimal, la normalizamos)
        Dim s As String = value.Replace(","c, "."c)
        Dim decVal As Decimal
        If Decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, decVal) Then
            Return decVal
        End If

        Dim intVal As Integer
        If Integer.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, intVal) Then
            Return intVal
        End If

        ' Texto
        Return value
    End Function

    Private Shared Sub LogError(code As String, message As String)
        Try
            If SubMain.ListadoErrores.ContainsKey(code) Then
                SubMain.ListadoErrores(code) &= Environment.NewLine & message
            Else
                SubMain.ListadoErrores(code) = message
            End If
        Catch
            ' En caso no exista SubMain.ListadoErrores aún
        End Try
    End Sub

    ' --- NUEVO: detectar si es HANA ---
    Private Shared Function IsHana(company As SAPbobsCOM.Company) As Boolean
        Return company.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB
    End Function

    Private Shared Function GetUdfAliasSet(company As SAPbobsCOM.Company, tableId As String) As HashSet(Of String)
        Dim setAliases As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        Dim rs As SAPbobsCOM.Recordset = CType(company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset), SAPbobsCOM.Recordset)
        Try
            Dim q As String
            If IsHana(company) Then
                q = $"SELECT ""AliasID"" FROM ""CUFD"" WHERE ""TableID"" = '{tableId.Replace("'", "''")}'"
            Else
                q = $"SELECT AliasID FROM CUFD WHERE TableID = '{tableId.Replace("'", "''")}'"
            End If
            rs.DoQuery(q)
            While Not rs.EoF
                Dim aliasId As String = Convert.ToString(rs.Fields.Item(0).Value)
                Dim raw As String = Convert.ToString(rs.Fields.Item(0).Value)
                If Not String.IsNullOrWhiteSpace(raw) Then
                    Dim a = raw.Trim()
                    ' Guarda ambas variantes para que coincida tanto "SS_IdProv" como "U_SS_IdProv"
                    If a.StartsWith("U_", StringComparison.OrdinalIgnoreCase) Then
                        setAliases.Add(a)                ' con U_
                        setAliases.Add(a.Substring(2))   ' sin U_
                    Else
                        setAliases.Add(a)                ' sin U_
                        setAliases.Add("U_" & a)         ' con U_
                    End If
                End If
                rs.MoveNext()
            End While
        Finally
            If rs IsNot Nothing Then Try : System.Runtime.InteropServices.Marshal.ReleaseComObject(rs) : Catch : End Try
        End Try
        Return setAliases
    End Function

End Class
