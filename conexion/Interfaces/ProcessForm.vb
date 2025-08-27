Imports System.Threading
Imports System.Threading.Tasks

Public Class ProcessForm

    Private cts As CancellationTokenSource
    'Sólo visual; propiedades para que, si quieres, actualices los valores.
    Public Property InProcessPercent As Integer
        Get
            Return progress.Value
        End Get
        Set(value As Integer)
            progress.Value = Math.Max(progress.Minimum, Math.Min(progress.Maximum, value))
            lblInProcessVal.Text = value.ToString() & "%"
        End Set
    End Property

    Public Property ErrorsDetected As Integer
        Get
            Return CInt(Val(lblErrorsVal.Text))
        End Get
        Set(value As Integer)
            lblErrorsVal.Text = value.ToString()
        End Set
    End Property

    Public Sub SetProcessed(current As Integer, total As Integer)
        lblProcessedVal.Text = $"{current} out of {total}"
    End Sub

    Public Sub StartProcess(Optional total As Integer = 100)
        cts = New CancellationTokenSource()
        InProcessPercent = 0
        ErrorsDetected = 0
        SetProcessed(0, total)
        RunProcessAsync(total, cts.Token)
    End Sub

    Private Async Sub RunProcessAsync(total As Integer, token As CancellationToken)
        Try
            For i As Integer = 1 To total
                Await Task.Delay(50, token)
                InProcessPercent = CInt(i * 100 / total)
                SetProcessed(i, total)
            Next
        Catch ex As OperationCanceledException
        End Try
        If Not token.IsCancellationRequested Then
            Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        cts?.Cancel()
        Me.Close()
    End Sub
End Class