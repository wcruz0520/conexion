Public Class ImportDataWizard

    Private vistas As List(Of UserControl)
    Private indiceActual As Integer
    Private Sub ImportDataWizard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        vistas = New List(Of UserControl) From {
            New Views.UploadFiles(),
            New Views.ExecuteProcess()
        }
        indiceActual = 0
        MostrarVistaActual()
        Me.Text = "Asistente de importación de datos"
        btnRunSimulation.Visible = False
    End Sub

    Private Sub btnAtras_Click(sender As Object, e As EventArgs) Handles btnAtras.Click

        If indiceActual > 0 Then
            indiceActual -= 1
            MostrarVistaActual()
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        If indiceActual < vistas.Count - 1 Then
            indiceActual += 1
            MostrarVistaActual()
        End If
    End Sub

    Private Sub btnRunSimulation_Click(sender As Object, e As EventArgs) Handles btnRunSimulation.Click
        Dim exec = TryCast(vistas(indiceActual), Views.ExecuteProcess)
        If exec IsNot Nothing Then
            exec.RunSimulation()
        End If
    End Sub
    Private Sub MostrarVistaActual()
        panelPrincipal.Controls.Clear()
        Dim vista As UserControl = vistas(indiceActual)
        vista.Dock = DockStyle.Fill
        panelPrincipal.Controls.Add(vista)
        btnAtras.Enabled = indiceActual > 0
        btnSiguiente.Enabled = indiceActual < vistas.Count - 1
        btnRunSimulation.Visible = TypeOf vista Is Views.ExecuteProcess
    End Sub
End Class