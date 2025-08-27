Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class ExecuteProcess
        Inherits UserControl

        'Public Event HelpClicked()
        'Public Event CancelClicked()
        'Public Event BackClicked()
        'Public Event NextClicked()
        'Public Event RunSimulationClicked()

        Private ReadOnly lblStep As Label
        Private ReadOnly lblDesc As Label
        Private ReadOnly grp As GroupBox
        Private ReadOnly optRollback As RadioButton
        Private ReadOnly optIgnoreAll As RadioButton
        Private ReadOnly optIgnoreUpTo As RadioButton
        Private ReadOnly numErrors As NumericUpDown
        Private ReadOnly lblEnd As Label

        Private ReadOnly pnlButtons As Panel
        'Private ReadOnly btnHelp As Button
        'Private ReadOnly btnCancel As Button
        'Private ReadOnly btnRunSimulation As Button
        'Private ReadOnly btnBack As Button
        'Private ReadOnly btnNext As Button
        'Private ReadOnly btnFinish As Button

        Public Enum ErrorHandlingMode
            RollbackOnAnyError = 0
            IgnoreAllAndProcessValid = 1
            IgnoreUpToNAndProcessValid = 2
        End Enum

        Public Sub New()
            DoubleBuffered = True
            Dock = DockStyle.Fill
            BackColor = SystemColors.Control

            '--- Layout raíz
            Dim root As New TableLayoutPanel() With {
                .Dock = DockStyle.Fill,
                .ColumnCount = 1,
                .RowCount = 5,
                .Padding = New Padding(10)
            }
            root.RowStyles.Add(New RowStyle(SizeType.AutoSize))          ' Step title
            root.RowStyles.Add(New RowStyle(SizeType.AutoSize))          ' Description
            root.RowStyles.Add(New RowStyle(SizeType.Absolute, 180))     ' Group options
            root.RowStyles.Add(New RowStyle(SizeType.Percent, 100))      ' Filler
            root.RowStyles.Add(New RowStyle(SizeType.Absolute, 50))      ' Bottom bar
            Controls.Add(root)

            '--- Step title
            lblStep = New Label() With {
                .AutoSize = True,
                .Text = "Paso 2: Ejecución de carga",
                .Font = New Font("Segoe UI", 11.0F, FontStyle.Bold),
                .Margin = New Padding(0, 0, 0, 6)
            }
            root.Controls.Add(lblStep, 0, 0)

            '--- Description (3 líneas como en el asistente)
            Dim desc As String =
                "Define the options for handling errors that occur during the import process." & Environment.NewLine &
                "To simulate the data import in order to identify possible errors, select an option and choose the ""Run Simulation"" button. This does not affect the company database." & Environment.NewLine &
                "To continue, choose the ""Next"" button."
            lblDesc = New Label() With {
                .AutoSize = True,
                .Text = desc,
                .MaximumSize = New Size(900, 0),
                .Margin = New Padding(0, 0, 0, 8)
            }
            root.Controls.Add(lblDesc, 0, 1)

            '--- Group box (sin título, como el borde de la captura)
            grp = New GroupBox() With {
                .Text = "",
                .Dock = DockStyle.Fill
            }
            root.Controls.Add(grp, 0, 2)

            ' Contenido del group
            Dim inner As New Panel() With {.Dock = DockStyle.Fill, .Padding = New Padding(20, 18, 20, 18)}
            grp.Controls.Add(inner)

            optRollback = New RadioButton() With {
                .Text = "Cancel Import and Perform Rollback When One or More Errors Occur",
                .AutoSize = True,
                .Location = New Point(10, 10)
            }
            inner.Controls.Add(optRollback)

            optIgnoreAll = New RadioButton() With {
                .Text = "Ignore All Errors and Process Valid Records",
                .AutoSize = True,
                .Location = New Point(10, 40),
                .Checked = True
            }
            inner.Controls.Add(optIgnoreAll)

            optIgnoreUpTo = New RadioButton() With {
                .Text = "Ignore Up to",
                .AutoSize = True,
                .Location = New Point(10, 70)
            }
            inner.Controls.Add(optIgnoreUpTo)

            numErrors = New NumericUpDown() With {
                .Location = New Point(optIgnoreUpTo.Right + 8, 68),
                .Minimum = 1,
                .Maximum = 1000,
                .Value = 10,
                .Width = 60
            }
            inner.Controls.Add(numErrors)

            lblEnd = New Label() With {
                .AutoSize = True,
                .Text = "Errors and Process Valid Records",
                .Location = New Point(numErrors.Right + 8, 71)
            }
            inner.Controls.Add(lblEnd)

            ' Recolocar si cambia tamaño
            AddHandler inner.Resize, Sub(sender, e)
                                         numErrors.Location = New Point(optIgnoreUpTo.Left + optIgnoreUpTo.Width + 8, 68)
                                         lblEnd.Location = New Point(numErrors.Right + 8, 71)
                                     End Sub

            '--- Bottom button bar (posiciones como la captura)
            pnlButtons = New Panel() With {.Dock = DockStyle.Fill}
            root.Controls.Add(pnlButtons, 0, 4)

            'btnHelp = New Button() With {.Text = "Help", .Size = New Size(90, 28)}
            'btnCancel = New Button() With {.Text = "Cancel", .Size = New Size(90, 28)}
            'btnRunSimulation = New Button() With {.Text = "Run Simulation", .Size = New Size(120, 28)}
            'btnBack = New Button() With {.Text = "< Back", .Size = New Size(90, 28)}
            'btnNext = New Button() With {.Text = "Next >", .Size = New Size(90, 28)}
            'btnFinish = New Button() With {.Text = "Finish", .Size = New Size(90, 28), .Enabled = False}

            'pnlButtons.Controls.AddRange({btnHelp, btnCancel, btnRunSimulation, btnBack, btnNext, btnFinish})
            'AddHandler pnlButtons.Resize, AddressOf LayoutButtons
            'LayoutButtons(Nothing, EventArgs.Empty)

            '--- Eventos públicos
            'AddHandler btnHelp.Click, Sub() RaiseEvent HelpClicked()
            'AddHandler btnCancel.Click, Sub() RaiseEvent CancelClicked()
            'AddHandler btnBack.Click, Sub() RaiseEvent BackClicked()
            'AddHandler btnNext.Click, Sub() RaiseEvent NextClicked()
            'AddHandler btnRunSimulation.Click, Sub()
            '                                       RaiseEvent RunSimulationClicked()
            '                                       RunSimulation()
            '                                   End Sub
        End Sub

        'Private Sub LayoutButtons(sender As Object, e As EventArgs)
        '    ' Izquierda: Help, Cancel
        '    btnHelp.Location = New Point(0, pnlButtons.Height - btnHelp.Height)
        '    btnCancel.Location = New Point(btnHelp.Right + 8, pnlButtons.Height - btnCancel.Height)

        '    ' Centro: Run Simulation
        '    btnRunSimulation.Location = New Point(
        '        (pnlButtons.Width - btnRunSimulation.Width) \ 2,
        '        pnlButtons.Height - btnRunSimulation.Height
        '    )

        '    ' Derecha: < Back, Next >, Finish
        '    btnFinish.Location = New Point(pnlButtons.Width - btnFinish.Width, pnlButtons.Height - btnFinish.Height)
        '    btnNext.Location = New Point(btnFinish.Left - 8 - btnNext.Width, pnlButtons.Height - btnNext.Height)
        '    btnBack.Location = New Point(btnNext.Left - 8 - btnBack.Width, pnlButtons.Height - btnBack.Height)
        'End Sub

        Public ReadOnly Property SelectedMode As ErrorHandlingMode
            Get
                If optRollback.Checked Then Return ErrorHandlingMode.RollbackOnAnyError
                If optIgnoreUpTo.Checked Then Return ErrorHandlingMode.IgnoreUpToNAndProcessValid
                Return ErrorHandlingMode.IgnoreAllAndProcessValid
            End Get
        End Property

        Public ReadOnly Property IgnoreCount As Integer
            Get
                Return CInt(numErrors.Value)
            End Get
        End Property

        Public Sub RunSimulation()
            ProcessForm.Show()
        End Sub
    End Class
End Namespace
