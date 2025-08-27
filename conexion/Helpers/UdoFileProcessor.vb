Imports SAPbobsCOM
Imports Microsoft.VisualBasic.FileIO
Imports System.Data

Public Class UdoFileProcessor
    ''' <summary>
    ''' Imports UDO records using tab-delimited header and detail files. The first row of each file
    ''' must contain column names. Records are created or updated based on the Code field.
    ''' </summary>
    ''' <param name="company">Connected SAP Business One company object.</param>
    ''' <param name="headerTable">Name of the UDO header table (e.g. @SS_REEMCAB).</param>
    ''' <param name="detailTable">Name of the UDO detail table (e.g. @SS_REEMDET).</param>
    ''' <param name="headerFilePath">Path to the tab-delimited file containing header records.</param>
    ''' <param name="detailFilePath">Path to the tab-delimited file containing detail records.</param>
    Public Shared Sub ProcessReal(company As Company,
                                 headerTable As String,
                                 detailTable As String,
                                 headerFilePath As String,
                                 detailFilePath As String)

        ProcessInternal(company, headerTable, detailTable, headerFilePath, detailFilePath, False)
    End Sub

    Public Shared Sub ProcessSimulation(company As Company,
                                        headerTable As String,
                                        detailTable As String,
                                        headerFilePath As String,
                                        detailFilePath As String)

        ProcessInternal(company, headerTable, detailTable, headerFilePath, detailFilePath, True)
    End Sub

    Private Shared Sub ProcessInternal(company As Company,
                                       headerTable As String,
                                       detailTable As String,
                                       headerFilePath As String,
                                       detailFilePath As String,
                                       simulate As Boolean)

        Dim headers As DataTable = LoadFile(headerFilePath)
        Dim details As DataTable = LoadFile(detailFilePath)

        If simulate Then company.StartTransaction()

        For Each row As DataRow In headers.Rows
            Dim code As String = Convert.ToString(row("Code"))
            If String.IsNullOrWhiteSpace(code) Then Continue For

            Dim service As GeneralService = company.GetCompanyService().GetGeneralService(headerTable.TrimStart("@"c))
            Dim data As GeneralData = service.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData)

            ' Header properties
            For Each col As DataColumn In headers.Columns
                Dim value As String = Convert.ToString(row(col))
                If Not String.IsNullOrWhiteSpace(value) Then
                    data.SetProperty(col.ColumnName, value)
                End If
            Next

            ' Detail lines filtered by Code
            Dim lineCollection As GeneralDataCollection = data.Child(detailTable.TrimStart("@"c))
            Dim childRows() As DataRow = details.Select($"Code = '{code.Replace("'", "''")}'")
            For Each dRow As DataRow In childRows
                Dim line As GeneralData = lineCollection.Add()
                For Each col As DataColumn In details.Columns
                    If col.ColumnName.Equals("Code", StringComparison.OrdinalIgnoreCase) Then Continue For
                    Dim value As String = Convert.ToString(dRow(col))
                    If Not String.IsNullOrWhiteSpace(value) Then
                        line.SetProperty(col.ColumnName, value)
                    End If
                Next
            Next

            If RecordExists(company, headerTable, code) Then
                service.Update(data)
            Else
                service.Add(data)
            End If

            System.Runtime.InteropServices.Marshal.ReleaseComObject(service)
        Next

        If simulate AndAlso company.InTransaction Then
            company.EndTransaction(BoWfTransOpt.wf_RollBack)
        End If
    End Sub

    Private Shared Function LoadFile(path As String) As DataTable
        Dim dt As New DataTable()
        If String.IsNullOrWhiteSpace(path) OrElse Not IO.File.Exists(path) Then Return dt

        Using parser As New TextFieldParser(path, System.Text.Encoding.UTF8)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(vbTab)

            Dim headers = parser.ReadFields()
            If headers Is Nothing Then Return dt
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

    Private Shared Function RecordExists(company As Company, table As String, code As String) As Boolean
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim isHana As Boolean = (company.DbServerType = BoDataServerTypes.dst_HANADB)

        Dim query As String
        If isHana Then
            'query = $"SELECT 1 FROM \"{table.Replace("\"", "\"\"")}\" WHERE \"Code\" = '{code.Replace("'", "''")}'"
            query = ""
        Else
            query = $"SELECT 1 FROM ""{table}"" WHERE Code = '{code.Replace("'", "''")}'"
        End If

        rs.DoQuery(query)
        Dim exists As Boolean = rs.RecordCount > 0
        System.Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        Return exists
    End Function
End Class