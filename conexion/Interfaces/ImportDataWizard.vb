Public Class ImportDataWizard

    Private vistas As List(Of UserControl)
    Private indiceActual As Integer
    Private Sub ImportDataWizard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        vistas = New List(Of UserControl) From {
            New Views.SelectTypeObject(),
            New Views.SelectAction(),
            New Views.SelectTable(),
            New Views.ExecuteProcess()
        }
        indiceActual = 0
        MostrarVistaActual()
        Me.Text = "Asistente de importación de datos"
    End Sub

    Private Sub btnAtras_Click(sender As Object, e As EventArgs) Handles btnAtras.Click

        If indiceActual > 0 Then
            indiceActual -= 1
            MostrarVistaActual()
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim vistaActual As UserControl = vistas(indiceActual)
        If TypeOf vistaActual Is Views.SelectTypeObject Then
            Dim selectView As Views.SelectTypeObject = DirectCast(vistaActual, Views.SelectTypeObject)
            If Not selectView.HasSelection() Then
                MessageBox.Show("Debe seleccionar al menos un tipo de objeto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If
        ' >>> NUEVO: Validación Paso 3 (SelectTable: pestaña activa debe tener una tabla seleccionada)
        If TypeOf vistaActual Is Views.SelectTable Then
            Dim sel As Views.SelectTable = DirectCast(vistaActual, Views.SelectTable)
            Dim msg As String = Nothing
            If Not sel.HasSelection(msg) Then
                MessageBox.Show(msg, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        End If
        If indiceActual < vistas.Count - 1 Then
            indiceActual += 1
            MostrarVistaActual()
        End If
    End Sub

    Private Sub MostrarVistaActual()
        panelPrincipal.Controls.Clear()
        Dim vista As UserControl = vistas(indiceActual)
        vista.Dock = DockStyle.Fill
        panelPrincipal.Controls.Add(vista)
        btnAtras.Enabled = indiceActual > 0
        btnSiguiente.Enabled = indiceActual < vistas.Count - 1
    End Sub
End Class