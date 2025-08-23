Imports System.Windows.Forms

Public Class PrincipalForm
    Private Sub PrincipalForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Icon = My.Resources.LogoV3
        topStrip.Dock = DockStyle.Top
        topStrip.GripStyle = ToolStripGripStyle.Hidden
        topStrip.AutoSize = False
        topStrip.Height = 39
        topStrip.ImageScalingSize = New Size(20, 20)
        topStrip.RenderMode = ToolStripRenderMode.Professional

        btnLogOff.AutoSize = False
        btnLogOff.Size = New Size(90, 36)
        'btnLogOff.Text = "Log on"
        btnLogOff.TextImageRelation = TextImageRelation.ImageAboveText
        btnLogOff.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

        btnImport.AutoSize = False
        btnImport.Size = New Size(90, 36)
        btnImport.Text = "Import"
        btnImport.TextImageRelation = TextImageRelation.ImageAboveText
        btnImport.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText

        Try
            btnLogOff.Image = My.Resources.connect
            btnImport.Image = My.Resources.import
        Catch ex As Exception

        End Try

        Me.IsMdiContainer = True
        UpdateConnectionStatus()

        ' (Opcional) color del área MDI estilo SAP
        For Each ctl As Control In Me.Controls
            Dim mdi = TryCast(ctl, MdiClient)
            If mdi IsNot Nothing Then
                mdi.BackColor = SystemColors.ActiveCaption
            End If
        Next

    End Sub

    Private Sub btnLogOff_Click(sender As Object, e As EventArgs) Handles btnLogOff.Click
        If SubMain.oCompany IsNot Nothing AndAlso SubMain.oCompany.Connected Then
            SubMain.oCompany.Disconnect()
            UpdateConnectionStatus()
        Else
            OpenMdiChild(Of Login)()
        End If
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        'ImportDataWizard.Show()
        OpenMdiChild(Of ImportDataWizard)()
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

    Public Sub UpdateConnectionStatus()
        If SubMain.oCompany IsNot Nothing AndAlso SubMain.oCompany.Connected Then
            btnLogOff.Text = "Log off"
            btnImport.Enabled = True
        Else
            btnLogOff.Text = "Log on"
            btnImport.Enabled = False
        End If
    End Sub

    Private Sub PrincipalForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If SubMain.oCompany IsNot Nothing AndAlso SubMain.oCompany.Connected Then
            SubMain.oCompany.Disconnect()
        End If
    End Sub

End Class
