Public Class MappingNative
    Public Class TableMap
        Public Property Display As String   ' Nombre visible
        Public Property Table As String     ' Código real (OCRD, OITM, etc.)
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
                Case "datos maestros"
                    Dim root = tree.Nodes.Add("Datos Maestros")
                    AddNodes(root, {
                      New TableMap("Socios de Negocio", "OCRD"),
                      New TableMap("Direcciones de SN", "CRD1"),
                      New TableMap("Artículos", "OITM"),
                      New TableMap("Listas de Precios", "OPLN")
                    })

                Case "documentos transaccionales"
                    Dim root = tree.Nodes.Add("Documentos")
                    AddNodes(root, {
                      New TableMap("Pedido de Venta", "ORDR"),
                      New TableMap("Entrega", "ODLN"),
                      New TableMap("Factura A/R", "OINV"),
                      New TableMap("Nota de Crédito A/R", "ORIN"),
                      New TableMap("Pedido de Compra", "OPOR"),
                      New TableMap("Entrada de Mercancías", "OPDN"),
                      New TableMap("Factura A/P", "OPCH"),
                      New TableMap("Nota de Crédito A/P", "ORPC")
                    })

                Case "configuración"
                    Dim root = tree.Nodes.Add("Configuración")
                    AddNodes(root, {
                      New TableMap("Bodegas", "OWHS"),
                      New TableMap("Plan de Cuentas", "OACT"),
                      New TableMap("Condiciones de Pago", "OCTG"),
                      New TableMap("Series de Numeración", "NNM1")
                    })

                Case Else
                    tree.Nodes.Add("No se seleccionó categoría de nativas en el Paso 1.")
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
            node.Tag = m ' Guarda el objeto para poder leer el código luego
        Next
    End Sub

    ' Utilidad para leer el código real de la tabla seleccionada
    Public Shared Function GetSelectedTableCode(node As TreeNode) As String
        Dim m = TryCast(If(node, Nothing)?.Tag, TableMap)
        Return If(m?.Table, String.Empty)
    End Function
End Class
