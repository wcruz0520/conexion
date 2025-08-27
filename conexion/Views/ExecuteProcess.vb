Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class ExecuteProcess
        Inherits UserControl

        Private optRollback As RadioButton
        Private optIgnoreAll As RadioButton
        Private optIgnoreUpTo As RadioButton
        Private numErrors As NumericUpDown

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            Me.SuspendLayout()
            Me.BackColor = Color.LightBlue
            Dim lbl As New Label()
            lbl.Text = $"Ejecución Proceso {SubMain.SelectedOptionNative}/{SubMain.SelectedOptionUDO} - {SubMain.SelectedNativeTable}/{SubMain.SelectedUDOTable} - {SubMain.SelectedNativeTableCode}/{SubMain.SelectedUDOTableCode}"
            lbl.Dock = DockStyle.Top
            lbl.Height = 40
            lbl.TextAlign = ContentAlignment.MiddleCenter
            Me.Controls.Add(lbl)
            Dim grp As New GroupBox()
            grp.Text = "Manejo de errores"
            grp.Location = New Point(20, 60)
            grp.Size = New Size(800, 150)

            optRollback = New RadioButton()
            optRollback.Text = "Cancelar la importación y revertir cuando ocurra un error"
            optRollback.Location = New Point(20, 30)

            optIgnoreAll = New RadioButton()
            optIgnoreAll.Text = "Ignorar todos los errores y procesar registros válidos"
            optIgnoreAll.Location = New Point(20, 60)

            optIgnoreUpTo = New RadioButton()
            optIgnoreUpTo.Text = "Ignorar hasta"
            optIgnoreUpTo.Location = New Point(20, 90)

            numErrors = New NumericUpDown()
            numErrors.Location = New Point(150, 88)
            numErrors.Width = 60
            numErrors.Minimum = 1
            numErrors.Maximum = 1000
            numErrors.Value = 10

            Dim lblEnd As New Label()
            lblEnd.Text = "errores y procesar registros válidos"
            lblEnd.Location = New Point(220, 90)

            grp.Controls.AddRange(New Control() {optRollback, optIgnoreAll, optIgnoreUpTo, numErrors, lblEnd})
            Me.Controls.Add(grp)
            Me.ResumeLayout(False)
        End Sub

        Public Sub RunSimulation()
            MessageBox.Show("Simulación ejecutada", "Simulación", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub
    End Class
End Namespace
