<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSAPUser = New System.Windows.Forms.TextBox()
        Me.txtSAPPw = New System.Windows.Forms.TextBox()
        Me.cmbCompania = New System.Windows.Forms.ComboBox()
        Me.btnConectar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.Location = New System.Drawing.Point(-2, 2)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(402, 66)
        Me.pnlHeader.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Seleccionar sociedad"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Usuario SAP"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 219)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Contraseña"
        '
        'txtSAPUser
        '
        Me.txtSAPUser.Location = New System.Drawing.Point(18, 178)
        Me.txtSAPUser.Name = "txtSAPUser"
        Me.txtSAPUser.Size = New System.Drawing.Size(191, 22)
        Me.txtSAPUser.TabIndex = 5
        '
        'txtSAPPw
        '
        Me.txtSAPPw.Location = New System.Drawing.Point(18, 239)
        Me.txtSAPPw.Name = "txtSAPPw"
        Me.txtSAPPw.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSAPPw.Size = New System.Drawing.Size(191, 22)
        Me.txtSAPPw.TabIndex = 6
        '
        'cmbCompania
        '
        Me.cmbCompania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompania.FormattingEnabled = True
        Me.cmbCompania.Location = New System.Drawing.Point(18, 118)
        Me.cmbCompania.Name = "cmbCompania"
        Me.cmbCompania.Size = New System.Drawing.Size(349, 24)
        Me.cmbCompania.TabIndex = 7
        '
        'btnConectar
        '
        Me.btnConectar.Location = New System.Drawing.Point(142, 283)
        Me.btnConectar.Name = "btnConectar"
        Me.btnConectar.Size = New System.Drawing.Size(124, 28)
        Me.btnConectar.TabIndex = 8
        Me.btnConectar.Text = "Conectar"
        Me.btnConectar.UseVisualStyleBackColor = True
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 332)
        Me.Controls.Add(Me.btnConectar)
        Me.Controls.Add(Me.cmbCompania)
        Me.Controls.Add(Me.txtSAPPw)
        Me.Controls.Add(Me.txtSAPUser)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "Login"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSAPUser As TextBox
    Friend WithEvents txtSAPPw As TextBox
    Friend WithEvents cmbCompania As ComboBox
    Friend WithEvents btnConectar As Button
End Class
