Imports SAPbobsCOM

Public Class ReturnTable
    Public Function GetChildTables(company As Company, selectedTable As String) As List(Of String)
        Dim result As New List(Of String)()

        If String.IsNullOrWhiteSpace(selectedTable) Then Return result

        ' 1) Caso UDO (@Tabla)
        If selectedTable.StartsWith("@") Then
            result.AddRange(GetUdoChildTables(company, selectedTable))
            Return result
        End If

        ' 2) Caso objetos nativos tipo documento (cabeceras Oxxx)
        If selectedTable.Length >= 2 AndAlso selectedTable.StartsWith("O") Then
            Dim base As String = selectedTable.Substring(1) ' e.g., OINV -> INV
            ' candidatos comunes: INV1, INV2, INV3...; RDR1, RDR2, etc.
            For i As Integer = 1 To 9
                Dim candidate As String = base & i.ToString()
                If TableExists(company, candidate) Then
                    result.Add(candidate)
                End If
            Next
        End If

        Return result
    End Function

    Private Function GetUdoChildTables(company As Company, udoMainTable As String) As List(Of String)
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim children As New List(Of String)()

        ' Para HANA: comillas dobles; para SQL Server: sin comillas funciona, pero usamos una sola consulta por motor.
        Dim isHana As Boolean = (company.DbServerType = BoDataServerTypes.dst_HANADB)

        Dim q As String
        If isHana Then
            q = "
                SELECT T1.""TableName"" AS ""Child""
                FROM ""OUDO"" T0
                INNER JOIN ""OUDC"" T1 ON T1.""Code"" = T0.""Code""
                WHERE T0.""TableName"" = '" & udoMainTable.Replace("'", "''") & "'"
        Else
            q = "
                SELECT T1.TableName AS Child
                FROM OUDO T0
                INNER JOIN OUDC T1 ON T1.Code = T0.Code
                WHERE T0.TableName = '" & udoMainTable.Replace("'", "''") & "'"
        End If

        rs.DoQuery(q)
        While Not rs.EoF
            Dim child As String = rs.Fields.Item(0).Value.ToString()
            If Not String.IsNullOrWhiteSpace(child) Then children.Add(child)
            rs.MoveNext()
        End While

        System.Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        Return children
    End Function

    Private Function TableExists(company As Company, tableName As String) As Boolean
        Dim rs As Recordset = CType(company.GetBusinessObject(BoObjectTypes.BoRecordset), Recordset)
        Dim exists As Boolean = False
        Dim isHana As Boolean = (company.DbServerType = BoDataServerTypes.dst_HANADB)

        Dim q As String
        If isHana Then
            ' En HANA las tablas están en el esquema de la compañía (CURRENT_SCHEMA)
            q = "
        SELECT COUNT(*) AS ""CNT""
        FROM ""SYS"".""TABLES""
        WHERE ""SCHEMA_NAME"" = CURRENT_SCHEMA
          AND ""TABLE_NAME"" = '" & tableName.Replace("'", "''").ToUpper() & "'"
        Else
            ' SQL Server
            q = "
        SELECT COUNT(*) AS CNT
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_NAME = '" & tableName.Replace("'", "''") & "'"
        End If

        rs.DoQuery(q)
        Dim cnt As Integer = Convert.ToInt32(rs.Fields.Item(0).Value)
        exists = (cnt > 0)

        System.Runtime.InteropServices.Marshal.ReleaseComObject(rs)
        Return exists
    End Function
End Class
