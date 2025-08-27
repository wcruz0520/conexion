Public Class UDOCategories
    Public Property Name As String
    Public ReadOnly Property Objects As List(Of String)

    Public Sub New(name As String, ParamArray objs() As String)
        Me.Name = name
        Me.Objects = New List(Of String)(objs)
    End Sub

End Class

Public Module MappingUdoCategories
    Public Function GetDefault() As IEnumerable(Of UDOCategories)
        Return New List(Of UDOCategories) From {
            New UDOCategories("Tablas de Usuario", "Reembolsos")
        }
    End Function
End Module