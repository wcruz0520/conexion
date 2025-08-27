Public Module MappingUploadTables
    Public Function GetNativeTables(objName As String) As IEnumerable(Of String)
        If String.IsNullOrWhiteSpace(objName) Then Return New String() {}
        Select Case objName.Trim().ToLowerInvariant()
            Case "factura de deudores", "factura de ventas"
                Return New String() {"OINV", "INV1"}
            Case Else
                Return New String() {}
        End Select
    End Function

    Public Function GetUDOTables(objName As String) As IEnumerable(Of String)
        If String.IsNullOrWhiteSpace(objName) Then Return New String() {}
        Select Case objName.Trim().ToLowerInvariant()
            Case "reembolsos"
                Return New String() {"@SS_REEMCAB", "@SS_REEMDET"}
            Case Else
                Return New String() {}
        End Select
    End Function
End Module
