Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTypeObject
        Inherits UserControl

        Private rbDatosMaestros As RadioButton
        Private rbDatosTransaccionales As RadioButton
        Private rbDatosConfiguracion As RadioButton
        Public selectoption As String

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.BackColor = Color.White
            Dim lbl As New Label()
            Dim Sublbl As New Label()

            'Titulo 
            lbl.Text = "Paso 1:  Seleccionar Tipo Objeto"
            lbl.Font = New Font("Calibri", 15, FontStyle.Bold)
            'lbl.Dock = DockStyle.Top
            lbl.Location = New Point(20, 20)
            lbl.TextAlign = ContentAlignment.MiddleLeft
            lbl.AutoSize = True
            'lbl.Height = 30
            Me.Controls.Add(lbl)

            'SubTitulo
            Sublbl.Text = "Seleccionar el tipo de data que quiere importar. Para continuar dar clic en ""Siguiente"""
            Sublbl.Font = New Font("Calibri", 11, FontStyle.Regular)
            'Sublbl.Dock = DockStyle.Top
            Sublbl.Location = New Point(20, 40)
            Sublbl.TextAlign = ContentAlignment.MiddleLeft
            Sublbl.AutoSize = True
            'Sublbl.Height = 30
            Me.Controls.Add(Sublbl)

            rbDatosMaestros = New RadioButton()
            rbDatosMaestros.AutoSize = True
            rbDatosMaestros.Text = "Datos Maestros"
            rbDatosMaestros.Font = New Font("Calibri", 12, FontStyle.Regular)
            rbDatosMaestros.Location = New Point(50, 100)
            rbDatosMaestros.Checked = True

            rbDatosTransaccionales = New RadioButton()
            rbDatosTransaccionales.AutoSize = True
            rbDatosTransaccionales.Text = "Datos transaccionales"
            rbDatosTransaccionales.Font = New Font("Calibri", 12, FontStyle.Regular)
            rbDatosTransaccionales.Location = New Point(50, 130)

            rbDatosConfiguracion = New RadioButton()
            rbDatosConfiguracion.AutoSize = True
            rbDatosConfiguracion.Text = "Datos de configuración"
            rbDatosConfiguracion.Font = New Font("Calibri", 12, FontStyle.Regular)
            rbDatosConfiguracion.Location = New Point(50, 160)

            AddHandler rbDatosMaestros.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbDatosTransaccionales.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbDatosConfiguracion.CheckedChanged, AddressOf RadioButton_CheckedChanged

            Me.Controls.Add(rbDatosMaestros)
            Me.Controls.Add(rbDatosTransaccionales)
            Me.Controls.Add(rbDatosConfiguracion)

            If rbDatosConfiguracion.Checked Then
                SubMain.SelectedTypeObject = rbDatosConfiguracion.Text
            End If

            If rbDatosMaestros.Checked Then
                SubMain.SelectedTypeObject = rbDatosMaestros.Text
            End If

            If rbDatosTransaccionales.Checked Then
                SubMain.SelectedTypeObject = rbDatosTransaccionales.Text
            End If

            Me.ResumeLayout(False)

        End Sub

        Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs)
            Dim rb As RadioButton = CType(sender, RadioButton)
            If rb.Checked Then
                SubMain.SelectedTypeObject = rb.Text
            End If
        End Sub
    End Class
End Namespace
