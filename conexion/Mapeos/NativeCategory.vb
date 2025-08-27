Public Class NativeCategory
    Public Property Name As String
    Public ReadOnly Property Objects As List(Of String)

    Public Sub New(name As String, ParamArray objs() As String)
        Me.Name = name
        Me.Objects = New List(Of String)(objs)
    End Sub
End Class

Public Module MappingNativeCategories
    Public Function GetDefault() As IEnumerable(Of NativeCategory)
        Return New List(Of NativeCategory) From {
            New NativeCategory("Ventas", "Factura de deudores")
        }
    End Function
End Module