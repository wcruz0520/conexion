<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PrincipalForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PrincipalForm))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblConnStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblServer = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPartner = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLogo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.topStrip = New System.Windows.Forms.ToolStrip()
        Me.btnLogOff = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnImport = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1.SuspendLayout()
        Me.topStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblConnStatus, Me.lblServer, Me.lblUser, Me.lblPartner, Me.lblLogo})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 827)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1282, 26)
        Me.StatusStrip1.TabIndex = 3
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblConnStatus
        '
        Me.lblConnStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblConnStatus.Name = "lblConnStatus"
        Me.lblConnStatus.Size = New System.Drawing.Size(99, 20)
        Me.lblConnStatus.Text = "Disconnected"
        '
        'lblServer
        '
        Me.lblServer.BackColor = System.Drawing.Color.Transparent
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(0, 20)
        '
        'lblUser
        '
        Me.lblUser.BackColor = System.Drawing.Color.Transparent
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(0, 20)
        '
        'lblPartner
        '
        Me.lblPartner.Name = "lblPartner"
        Me.lblPartner.Size = New System.Drawing.Size(86, 20)
        Me.lblPartner.Text = "Powered by"
        '
        'lblLogo
        '
        Me.lblLogo.Name = "lblLogo"
        Me.lblLogo.Size = New System.Drawing.Size(0, 20)
        Me.lblLogo.Visible = False
        '
        'topStrip
        '
        Me.topStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.topStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLogOff, Me.ToolStripSeparator1, Me.btnImport})
        Me.topStrip.Location = New System.Drawing.Point(0, 0)
        Me.topStrip.Name = "topStrip"
        Me.topStrip.Size = New System.Drawing.Size(1282, 27)
        Me.topStrip.TabIndex = 4
        Me.topStrip.Text = "ToolStrip1"
        '
        'btnLogOff
        '
        Me.btnLogOff.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnLogOff.Image = CType(resources.GetObject("btnLogOff.Image"), System.Drawing.Image)
        Me.btnLogOff.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLogOff.Name = "btnLogOff"
        Me.btnLogOff.Size = New System.Drawing.Size(29, 24)
        Me.btnLogOff.Text = "Log"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnImport
        '
        Me.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnImport.Image = CType(resources.GetObject("btnImport.Image"), System.Drawing.Image)
        Me.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(29, 24)
        Me.btnImport.Text = "Importar"
        '
        'PrincipalForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.ClientSize = New System.Drawing.Size(1282, 853)
        Me.Controls.Add(Me.topStrip)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "PrincipalForm"
        Me.Text = "Form1"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.topStrip.ResumeLayout(False)
        Me.topStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents topStrip As ToolStrip
    Friend WithEvents btnLogOff As ToolStripButton
    Friend WithEvents btnImport As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents lblConnStatus As ToolStripStatusLabel
    Friend WithEvents lblServer As ToolStripStatusLabel
    Friend WithEvents lblUser As ToolStripStatusLabel
    Friend WithEvents lblPartner As ToolStripStatusLabel
    Friend WithEvents lblLogo As ToolStripStatusLabel
End Class
