Public Class ImportModels
    Public Enum RowState
        Success = 1
        [Error] = 2
    End Enum

    Public Enum RowAction
        Create = 1
        Update = 2
    End Enum

    Public Class RowResult
        Public Property Index As Integer        ' 1..N
        Public Property Key As String          ' "Code" u otra PK
        Public Property Action As RowAction
        Public Property State As RowState
        Public Property Message As String      ' éxito/stack corto
        Public Property TableName As String
    End Class

End Class
