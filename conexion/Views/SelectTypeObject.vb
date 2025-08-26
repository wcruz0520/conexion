Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTypeObject
        Inherits UserControl

        Private cbDatosMaestros As CheckBox
        Private cbDatosTransaccionales As CheckBox
        Private cbDatosConfiguracion As CheckBox
        Private cbUdoDatosMaestros As CheckBox
        Private cbUdoDatosTransaccionales As CheckBox
        Private cbUdoDatosConfiguracion As CheckBox

        Public SelectedOption As String

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.AutoScroll = True
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
            panelContent.Height = 130
            panelContent.Padding = New Padding(50, 5, 50, 20)
            panelContent.BackColor = Color.LightGray

            Dim panelContent2 As New Panel()
            panelContent2.Dock = DockStyle.Top
            panelContent2.Height = 130
            panelContent2.Padding = New Padding(50, 5, 50, 30)
            panelContent2.BackColor = Color.LightGray

            cbDatosMaestros = New CheckBox()
            cbDatosMaestros.AutoSize = True
            cbDatosMaestros.Text = "Datos Maestros"
            cbDatosMaestros.Font = New Font("Calibri", 10, FontStyle.Regular)
            cbDatosMaestros.Location = New Point(20, 40)
            AddHandler cbDatosMaestros.CheckedChanged, AddressOf CheckBox_CheckedChanged
            'cbDatosMaestros.Checked = True

            cbDatosTransaccionales = New CheckBox()
            cbDatosTransaccionales.AutoSize = True
            cbDatosTransaccionales.Text = "Documentos transaccionales"
            cbDatosTransaccionales.Font = New Font("Calibri", 10, FontStyle.Regular)
            cbDatosTransaccionales.Location = New Point(20, 70)

            cbDatosConfiguracion = New CheckBox()
            cbDatosConfiguracion.AutoSize = True
            cbDatosConfiguracion.Text = "Configuración"
            cbDatosConfiguracion.Font = New Font("Calibri", 10, FontStyle.Regular)
            cbDatosConfiguracion.Location = New Point(20, 100)

            cbUdoDatosMaestros = New CheckBox()
            cbUdoDatosMaestros.AutoSize = True
            cbUdoDatosMaestros.Text = "UDO Datos Maestros"
            cbUdoDatosMaestros.Font = New Font("Calibri", 10, FontStyle.Regular)
            cbUdoDatosMaestros.Location = New Point(20, 40)
            'cbUdoDatosMaestros.Checked = True

            cbUdoDatosTransaccionales = New CheckBox()
            cbUdoDatosTransaccionales.AutoSize = True
            cbUdoDatosTransaccionales.Text = "UDO Documento"
            cbUdoDatosTransaccionales.Font = New Font("Calibri", 10, FontStyle.Regular)
            cbUdoDatosTransaccionales.Location = New Point(20, 70)

            cbUdoDatosConfiguracion = New CheckBox()
            cbUdoDatosConfiguracion.AutoSize = True
            cbUdoDatosConfiguracion.Text = "UDO Ningún objeto"
            cbUdoDatosConfiguracion.Font = New Font("Calibri", 10, FontStyle.Regular)
            cbUdoDatosConfiguracion.Location = New Point(20, 100)

            AddHandler cbDatosTransaccionales.CheckedChanged, AddressOf CheckBox_CheckedChanged
            AddHandler cbDatosConfiguracion.CheckedChanged, AddressOf CheckBox_CheckedChanged
            AddHandler cbUdoDatosMaestros.CheckedChanged, AddressOf CheckBox_CheckedChanged
            AddHandler cbUdoDatosTransaccionales.CheckedChanged, AddressOf CheckBox_CheckedChanged
            AddHandler cbUdoDatosConfiguracion.CheckedChanged, AddressOf CheckBox_CheckedChanged

            panelContent.Controls.Add(cbDatosMaestros)
            panelContent.Controls.Add(cbDatosTransaccionales)
            panelContent.Controls.Add(cbDatosConfiguracion)
            cbDatosMaestros.Checked = True

            panelContent2.Controls.Add(cbUdoDatosMaestros)
            panelContent2.Controls.Add(cbUdoDatosTransaccionales)
            panelContent2.Controls.Add(cbUdoDatosConfiguracion)
            cbUdoDatosMaestros.Checked = True

            Me.Controls.Add(panelContent2)
            Me.Controls.Add(panelContent)
            Me.Controls.Add(panelHeader)

            SelectedOption = cbDatosMaestros.Text
            SubMain.SelectedTypeObject = SelectedOption

            Me.ResumeLayout(False)

        End Sub

        Private Sub CheckBox_CheckedChanged(sender As Object, e As EventArgs)
            Dim cb As CheckBox = CType(sender, CheckBox)
            If cb.Checked Then
                If cb.Parent IsNot Nothing Then
                    For Each ctrl As Control In cb.Parent.Controls
                        If TypeOf ctrl Is CheckBox AndAlso ctrl IsNot cb Then
                            DirectCast(ctrl, CheckBox).Checked = False
                        End If
                    Next
                End If
                SelectedOption = cb.Text
                SubMain.SelectedTypeObject = SelectedOption
            End If
        End Sub
    End Class
End Namespace
