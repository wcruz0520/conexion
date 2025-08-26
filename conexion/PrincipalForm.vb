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
            lblConnStatus.Image = My.Resources.puntorojo2
            lblServer.Image = My.Resources.server
            lblUser.Image = My.Resources.user
            lblLogo.Image = My.Resources.LogoSS
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

        ' Que el grip no “robe” espacio
        StatusStrip1.SizingGrip = False

        ' 3 columnas elásticas
        'lblConnStatus.Spring = True
        lblServer.Spring = True
        lblUser.Spring = True

        ' Alinear texto e icono
        lblConnStatus.TextAlign = ContentAlignment.MiddleLeft
        lblServer.TextAlign = ContentAlignment.MiddleLeft
        lblUser.TextAlign = ContentAlignment.MiddleLeft

        'lblLogo.AutoSize = False
        'lblLogo.ImageScaling = ToolStripItemImageScaling.None
        'lblLogo.DisplayStyle = ToolStripItemDisplayStyle.Image
        lblLogo.Size = New Size(50, 20)
        'lblLogo.Margin = New Padding(0)

        ' Otra opción para mostrar el logo es alojar un PictureBox dentro del
        ' StatusStrip usando un ToolStripControlHost:
        Dim logoBox As New PictureBox() With {
            .Image = My.Resources.LogoSS,
            .SizeMode = PictureBoxSizeMode.Normal,
            .Size = New Size(30, 20)
        }
        Dim host As New ToolStripControlHost(logoBox)
        StatusStrip1.Items.Add(host)

        lblPartner.TextAlign = ContentAlignment.MiddleRight
        lblPartner.BackColor = Color.Transparent

    End Sub

    Private Sub btnLogOff_Click(sender As Object, e As EventArgs) Handles btnLogOff.Click
        If SubMain.oCompany IsNot Nothing AndAlso SubMain.oCompany.Connected Then
            If Me.ActiveMdiChild IsNot Nothing Then
                MessageBox.Show("Debe cerrar todos los formularios antes de desconectarse.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
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
            lblConnStatus.Text = "Connected"
            lblServer.Text = String.Format("{0}", SubMain.oCompany.Server)
            lblUser.Text = String.Format("{0} ({1})", SubMain.oCompany.UserName, SubMain.oCompany.CompanyDB)
            lblConnStatus.Image = My.Resources.puntoverde2
        Else
            btnLogOff.Text = "Log on"
            btnImport.Enabled = False
            lblConnStatus.Text = "Disconnected"
            lblServer.Text = " "
            lblUser.Text = " "
            lblConnStatus.Image = My.Resources.puntorojo2
        End If
    End Sub

    Private Sub PrincipalForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If SubMain.oCompany IsNot Nothing AndAlso SubMain.oCompany.Connected Then
            SubMain.oCompany.Disconnect()
        End If
    End Sub

End Class
