Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTable
        Inherits UserControl
        Private lbl As Label
        Public Sublbl As Label

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
            lbl.Text = "Paso 3:  Seleccionar Tabla"
            lbl.Font = New Font("Calibri", 15, FontStyle.Bold)
            lbl.AutoSize = True
            lbl.Location = New Point(20, 15)
            panelHeader.Controls.Add(lbl)

            Sublbl = New Label()
            'Sublbl.Text = "Seleccionar la tabla. Para continuar dar clic en ""Siguiente"""
            Sublbl.Font = New Font("Calibri", 11, FontStyle.Regular)
            Sublbl.AutoSize = True
            Sublbl.Location = New Point(20, 35)
            panelHeader.Controls.Add(Sublbl)

            ' Panel para el resto de los elementos
            Dim panelContent As New Panel()
            panelContent.Dock = DockStyle.Fill
            panelContent.Padding = New Padding(50, 20, 50, 20)
            panelContent.BackColor = Color.LightGray

            Me.Controls.Add(panelContent)
            Me.Controls.Add(panelHeader)

            UpdateLabel()
            Me.ResumeLayout(False)
        End Sub

        Private Sub UpdateLabel()
            Sublbl.Text = $"Paso 3: Seleccionar tabla tipo {SubMain.SelectedTypeObject} que se va {SubMain.SelectedTypeObject2}"
        End Sub

        Protected Overrides Sub OnVisibleChanged(e As EventArgs)
            MyBase.OnVisibleChanged(e)
            If Me.Visible Then
                UpdateLabel()
            End If
        End Sub
    End Class
End Namespace
