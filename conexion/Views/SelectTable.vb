Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTable
        Inherits UserControl
        Private lbl As Label

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.BackColor = Color.LightBlue
            lbl = New Label()
            lbl.Dock = DockStyle.Fill
            lbl.TextAlign = ContentAlignment.MiddleCenter
            Me.Controls.Add(lbl)
            UpdateLabel()
            Me.ResumeLayout(False)
        End Sub

        Private Sub UpdateLabel()
            lbl.Text = $"Paso 2: Seleccionar tabla tipo {SubMain.SelectedTypeObject}"
        End Sub

        Protected Overrides Sub OnVisibleChanged(e As EventArgs)
            MyBase.OnVisibleChanged(e)
            If Me.Visible Then
                UpdateLabel()
            End If
        End Sub
    End Class
End Namespace
