Imports System.ComponentModel

Public Class ProgressForm
    Inherits Form

    Private ReadOnly pb As ProgressBar
    Private ReadOnly lblTitle As Label
    Private ReadOnly lblCounts As Label
    Private ReadOnly btnCancel As Button

    Public Event CancelRequested()

    Public Sub New()
        Me.Text = "Progreso"
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.StartPosition = FormStartPosition.CenterParent
        Me.MaximizeBox = False : Me.MinimizeBox = False
        Me.Width = 520 : Me.Height = 180

        lblTitle = New Label With {.AutoSize = False, .Dock = DockStyle.Top, .Height = 28, .TextAlign = ContentAlignment.MiddleLeft}
        pb = New ProgressBar With {.Dock = DockStyle.Top, .Height = 22, .Style = ProgressBarStyle.Continuous}
        lblCounts = New Label With {.AutoSize = False, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleLeft}
        btnCancel = New Button With {.Text = "Cancelar", .Dock = DockStyle.Bottom, .Height = 32}

        AddHandler btnCancel.Click, Sub() RaiseEvent CancelRequested()

        Controls.Add(lblCounts)
        Controls.Add(pb)
        Controls.Add(lblTitle)
        Controls.Add(btnCancel)
    End Sub

    Public Sub Initialize(total As Integer, isSimulation As Boolean)
        lblTitle.Text = If(isSimulation, "Simulación de importación", "Importación real")
        pb.Minimum = 0 : pb.Maximum = Math.Max(1, total) : pb.Value = 0
        UpdateStatus(0, total, 0, 0, Nothing)
    End Sub

    Public Sub UpdateStatus(processed As Integer, total As Integer, success As Integer, failed As Integer, currentKey As String)
        processed = Math.Max(0, Math.Min(processed, Math.Max(1, total)))
        If processed <= pb.Maximum Then pb.Value = processed
        lblCounts.Text =
            $"Registro: {processed} / {total}
            Éxitos: {success}   Fallas: {failed}{If(String.IsNullOrEmpty(currentKey), "", $"{Environment.NewLine}Procesando: {currentKey}")}"
        lblCounts.Refresh()
    End Sub
End Class
