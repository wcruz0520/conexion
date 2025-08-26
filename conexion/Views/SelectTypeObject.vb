Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTypeObject
        Inherits UserControl

        Private rbDatosMaestros As RadioButton
        Private rbDatosTransaccionales As RadioButton
        Private rbDatosConfiguracion As RadioButton
        Private rbUdoDatosMaestros As RadioButton
        Private rbUdoDatosTransaccionales As RadioButton
        Private rbUdoDatosConfiguracion As RadioButton
        Public SelectedOption As String

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
            panelContent.Dock = DockStyle.Top
            panelContent.Padding = New Padding(50, 5, 50, 20)
            panelContent.BackColor = Color.LightGray

            Dim panelContent2 As New Panel()
            panelContent2.Dock = DockStyle.Bottom
            panelContent2.Padding = New Padding(50, 5, 50, 30)
            panelContent2.BackColor = Color.LightGray

            rbDatosMaestros = New RadioButton()
            rbDatosMaestros.AutoSize = True
            rbDatosMaestros.Text = "Datos Maestros"
            rbDatosMaestros.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbDatosMaestros.Location = New Point(20, 40)
            rbDatosMaestros.Checked = True

            rbDatosTransaccionales = New RadioButton()
            rbDatosTransaccionales.AutoSize = True
            rbDatosTransaccionales.Text = "Documentos transaccionales"
            rbDatosTransaccionales.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbDatosTransaccionales.Location = New Point(20, 70)

            rbDatosConfiguracion = New RadioButton()
            rbDatosConfiguracion.AutoSize = True
            rbDatosConfiguracion.Text = "Configuración"
            rbDatosConfiguracion.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbDatosConfiguracion.Location = New Point(20, 100)

            rbUdoDatosMaestros = New RadioButton()
            rbUdoDatosMaestros.AutoSize = True
            rbUdoDatosMaestros.Text = "UDO Datos Maestros"
            rbUdoDatosMaestros.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbUdoDatosMaestros.Location = New Point(20, 40)
            rbUdoDatosMaestros.Checked = True

            rbUdoDatosTransaccionales = New RadioButton()
            rbUdoDatosTransaccionales.AutoSize = True
            rbUdoDatosTransaccionales.Text = "UDO Documento"
            rbUdoDatosTransaccionales.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbUdoDatosTransaccionales.Location = New Point(20, 70)

            rbUdoDatosConfiguracion = New RadioButton()
            rbUdoDatosConfiguracion.AutoSize = True
            rbUdoDatosConfiguracion.Text = "UDO Ningún objeto"
            rbUdoDatosConfiguracion.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbUdoDatosConfiguracion.Location = New Point(20, 100)

            AddHandler rbDatosMaestros.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbDatosTransaccionales.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbDatosConfiguracion.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbUdoDatosMaestros.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbUdoDatosTransaccionales.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbUdoDatosConfiguracion.CheckedChanged, AddressOf RadioButton_CheckedChanged

            panelContent2.Controls.Add(rbUdoDatosMaestros)
            panelContent2.Controls.Add(rbUdoDatosTransaccionales)
            panelContent2.Controls.Add(rbUdoDatosConfiguracion)

            panelContent.Controls.Add(rbDatosMaestros)
            panelContent.Controls.Add(rbDatosTransaccionales)
            panelContent.Controls.Add(rbDatosConfiguracion)

            Me.Controls.Add(panelContent)
            Me.Controls.Add(panelContent2)
            Me.Controls.Add(panelHeader)

            SelectedOption = rbDatosMaestros.Text
            SubMain.SelectedTypeObject = SelectedOption

            Me.ResumeLayout(False)

        End Sub

        Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs)
            Dim rb As RadioButton = CType(sender, RadioButton)
            If rb.Checked Then
                SelectedOption = rb.Text
                SubMain.SelectedTypeObject = SelectedOption
            End If
        End Sub
    End Class
End Namespace
