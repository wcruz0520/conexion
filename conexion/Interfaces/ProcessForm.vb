Public Class ProcessForm
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

    Private Sub ProcessForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Valores de ejemplo para que se vea como la imagen
        InProcessPercent = 51
        ErrorsDetected = 23
        SetProcessed(23, 45)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class