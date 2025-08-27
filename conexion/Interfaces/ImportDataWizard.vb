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
            Dim upload = TryCast(vistas(indiceActual), Views.UploadFiles)
            If upload IsNot Nothing Then
                SubMain.Upload_FilesUDO = upload.GetSelectedFilesUDOs()
            End If
            indiceActual += 1
            MostrarVistaActual()
        Else
            Dim exec = TryCast(vistas(indiceActual), Views.ExecuteProcess)
            If exec IsNot Nothing Then
                Dim result = MessageBox.Show("¿Desea ejecutar la importación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    exec.RunReal()
                End If
            End If
        End If
    End Sub

    Private Sub btnRunSimulation_Click(sender As Object, e As EventArgs) Handles btnRunSimulation.Click
        Dim exec = TryCast(vistas(indiceActual), Views.ExecuteProcess)
        If exec IsNot Nothing Then
            exec.RunSimulation()
            ErrorsForm.Show()
            'OpenMdiChild(Of ProcessForm)()
        End If
    End Sub
    Private Sub MostrarVistaActual()
        panelPrincipal.Controls.Clear()
        Dim vista As UserControl = vistas(indiceActual)
        vista.Dock = DockStyle.Fill
        panelPrincipal.Controls.Add(vista)
        btnAtras.Enabled = indiceActual > 0
        'btnSiguiente.Enabled = indiceActual < vistas.Count - 1
        btnSiguiente.Enabled = True
        btnSiguiente.Text = If(indiceActual < vistas.Count - 1, "Siguiente", "Ejecutar")
        btnRunSimulation.Visible = TypeOf vista Is Views.ExecuteProcess
    End Sub

    Private Sub OpenMdiChild(Of T As {Form, New})()
        ' ¿ya está abierto?
        For Each f As Form In Me.MdiChildren
            If TypeOf f Is T Then
                If f.WindowState = FormWindowState.Minimized Then f.WindowState = FormWindowState.Normal
                f.Activate()
                Return
            End If
        Next

        ' crear y configurar el hijo
        Dim child As New T()
        child.MdiParent = Me
        child.StartPosition = FormStartPosition.CenterParent

        ' Estilo “diálogo” (se mueve dentro del contenedor y no sale de los bordes)
        child.FormBorderStyle = FormBorderStyle.FixedDialog
        child.MaximizeBox = False
        child.MinimizeBox = False
        child.ShowInTaskbar = False
        ' --- Si prefieres que NO se mueva en absoluto (pantalla pegada) ---
        'child.FormBorderStyle = FormBorderStyle.None
        'child.WindowState = FormWindowState.Maximized

        child.Show()
    End Sub

End Class