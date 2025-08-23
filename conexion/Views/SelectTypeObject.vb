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
            'Dim lbl As New Label()
            'Dim Sublbl As New Label()

            ' Panel para título y subtítulo
            Dim panelHeader As New Panel()
            panelHeader.Dock = DockStyle.Top
            panelHeader.Height = 70
            panelHeader.Padding = New Padding(20, 20, 20, 10)
            panelHeader.BackColor = Color.White 'Color.FromArgb(46, 254, 132)

            Dim lbl As New Label()
            lbl.Text = "Paso 1:  Seleccionar Tipo Objeto"
            lbl.Font = New Font("Calibri", 15, FontStyle.Bold)
            lbl.AutoSize = True
            lbl.Location = New Point(20, 15)
            panelHeader.Controls.Add(lbl)

            Dim Sublbl As New Label()
            Sublbl.Text = "Seleccionar el tipo de data que quiere importar. Para continuar dar clic en ""Siguiente"""
            Sublbl.Font = New Font("Calibri", 11, FontStyle.Regular)
            Sublbl.AutoSize = True
            Sublbl.Location = New Point(20, 35)
            panelHeader.Controls.Add(Sublbl)

            ' Panel para el resto de los elementos
            Dim panelContent As New Panel()
            panelContent.Dock = DockStyle.Fill
            panelContent.Padding = New Padding(50, 20, 50, 20)
            panelContent.BackColor = Color.LightGray

            rbDatosMaestros = New RadioButton()
            rbDatosMaestros.AutoSize = True
            rbDatosMaestros.Text = "UDO tipo Datos Maestros"
            rbDatosMaestros.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbDatosMaestros.Location = New Point(20, 40)
            rbDatosMaestros.Checked = True

            rbDatosTransaccionales = New RadioButton()
            rbDatosTransaccionales.AutoSize = True
            rbDatosTransaccionales.Text = "UDO tipo Documento"
            rbDatosTransaccionales.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbDatosTransaccionales.Location = New Point(20, 70)

            rbDatosConfiguracion = New RadioButton()
            rbDatosConfiguracion.AutoSize = True
            rbDatosConfiguracion.Text = "UDO tipo Ningún objeto"
            rbDatosConfiguracion.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbDatosConfiguracion.Location = New Point(20, 100)

            AddHandler rbDatosMaestros.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbDatosTransaccionales.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbDatosConfiguracion.CheckedChanged, AddressOf RadioButton_CheckedChanged

            panelContent.Controls.Add(rbDatosMaestros)
            panelContent.Controls.Add(rbDatosTransaccionales)
            panelContent.Controls.Add(rbDatosConfiguracion)

            Me.Controls.Add(panelContent)
            Me.Controls.Add(panelHeader)

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
