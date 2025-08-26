Namespace Views
    Public Class SelectAction
        Inherits UserControl

        Private rbAddData As RadioButton
        Private rbUpdateData As RadioButton
        Private rbAddUpdateData As RadioButton
        Public SelectedOption2 As String
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
            lbl.Text = "Paso 2:  Seleccionar el tipo de acción que desea realizar"
            lbl.Font = New Font("Calibri", 15, FontStyle.Bold)
            lbl.AutoSize = True
            lbl.Location = New Point(20, 15)
            panelHeader.Controls.Add(lbl)

            Sublbl = New Label()
            'Sublbl.Text = "Seleccionar el tipo de acción que desea realizar. Para continuar dar clic en ""Siguiente"""
            Sublbl.Font = New Font("Calibri", 10, FontStyle.Regular)
            Sublbl.AutoSize = True
            Sublbl.Location = New Point(20, 35)
            panelHeader.Controls.Add(Sublbl)

            ' Panel para el resto de los elementos
            Dim panelContent As New Panel()
            panelContent.Dock = DockStyle.Fill
            panelContent.Padding = New Padding(50, 20, 50, 20)
            panelContent.BackColor = Color.LightGray

            rbAddData = New RadioButton()
            rbAddData.AutoSize = True
            rbAddData.Text = "Añadir nuevos datos"
            rbAddData.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbAddData.Location = New Point(20, 40)
            rbAddData.Checked = True

            rbUpdateData = New RadioButton()
            rbUpdateData.AutoSize = True
            rbUpdateData.Text = "Actualizar datos existentes"
            rbUpdateData.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbUpdateData.Location = New Point(20, 70)

            rbAddUpdateData = New RadioButton()
            rbAddUpdateData.AutoSize = True
            rbAddUpdateData.Text = "Añadir nuevos datos y actualizar datos existentes"
            rbAddUpdateData.Font = New Font("Calibri", 10, FontStyle.Regular)
            rbAddUpdateData.Location = New Point(20, 100)

            AddHandler rbAddData.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbUpdateData.CheckedChanged, AddressOf RadioButton_CheckedChanged
            AddHandler rbAddUpdateData.CheckedChanged, AddressOf RadioButton_CheckedChanged

            panelContent.Controls.Add(rbAddData)
            panelContent.Controls.Add(rbUpdateData)
            panelContent.Controls.Add(rbAddUpdateData)

            Me.Controls.Add(panelContent)
            Me.Controls.Add(panelHeader)

            SelectedOption2 = rbAddData.Text
            SubMain.SelectedTypeObject2 = SelectedOption2
            UpdateLabel()
            Me.ResumeLayout(False)
        End Sub

        Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs)
            Dim rb As RadioButton = CType(sender, RadioButton)
            If rb.Checked Then
                SelectedOption2 = rb.Text
                SubMain.SelectedTypeObject2 = SelectedOption2
            End If
        End Sub
        Private Sub UpdateLabel()
            Sublbl.Text = $"Seleccionar el objeto tipo {SelectedOptionNative}/{SelectedOptionUDO}. Para continuar dar clic en ""Siguiente"""
        End Sub
        Protected Overrides Sub OnVisibleChanged(e As EventArgs)
            MyBase.OnVisibleChanged(e)
            If Me.Visible Then
                UpdateLabel()
            End If
        End Sub

    End Class
End Namespace

