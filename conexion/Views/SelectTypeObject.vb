Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTypeObject
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.BackColor = Color.LightBlue
            Dim lbl As New Label()
            lbl.Text = "Seleccionar Tipo Objeto"
            lbl.Dock = DockStyle.Fill
            lbl.TextAlign = ContentAlignment.MiddleCenter
            Me.Controls.Add(lbl)
            Me.ResumeLayout(False)
        End Sub
    End Class
End Namespace
