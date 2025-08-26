Public Class MappingUDOs
    Public Class TableMap
        Public Property Display As String   ' Nombre visible
        Public Property Table As String     ' Nombre real en BD (@SS_XXX)
        Public Sub New(display As String, table As String)
            Me.Display = display : Me.Table = table
        End Sub
        Public Overrides Function ToString() As String
            If String.IsNullOrEmpty(Table) Then Return Display
            Return Display & " (" & Table & ")"
        End Function
    End Class

    ' Pinta el TreeView según la categoría elegida en el Paso 1
    Public Shared Sub Populate(tree As TreeView, selectedCategory As String)
        If tree Is Nothing Then Return
        tree.BeginUpdate()
        Try
            tree.Nodes.Clear()
            Dim cat As String = If(selectedCategory, String.Empty).Trim().ToLowerInvariant()

            Select Case cat
                Case "udo datos maestros", "datos maestros"
                    Dim root = tree.Nodes.Add("UDO - Datos Maestros")
                    AddNodes(root, {
                      New TableMap("Clientes UDO", "@SS_CLIENTES"),
                      New TableMap("Vehículos UDO", "@SS_VEHICULOS")
                    })

                Case "udo documento", "documento"
                    Dim root = tree.Nodes.Add("UDO - Documento")
                    AddNodes(root, {
                      New TableMap("Cotización UDO", "@SS_DOC_COTIZ"),
                      New TableMap("Póliza UDO", "@SS_DOC_POLIZA")
                    })

                Case Else
                    tree.Nodes.Add("No se seleccionó categoría de UDO en el Paso 1.")
            End Select

            For Each n As TreeNode In tree.Nodes
                n.Expand()
            Next
        Finally
            tree.EndUpdate()
        End Try
    End Sub

    Private Shared Sub AddNodes(parent As TreeNode, maps As IEnumerable(Of TableMap))
        For Each m In maps
            Dim node = parent.Nodes.Add(m.ToString())
            node.Tag = m
        Next
    End Sub

    Public Shared Function GetSelectedTableCode(node As TreeNode) As String
        Dim m = TryCast(If(node, Nothing)?.Tag, TableMap)
        Return If(m?.Table, String.Empty)
    End Function
End Class
