<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ProcessForm
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.progress = New System.Windows.Forms.ProgressBar()
        Me.table = New System.Windows.Forms.TableLayoutPanel()
        Me.lblInProcess = New System.Windows.Forms.Label()
        Me.lblInProcessVal = New System.Windows.Forms.Label()
        Me.lblErrors = New System.Windows.Forms.Label()
        Me.lblErrorsVal = New System.Windows.Forms.Label()
        Me.lblProcessed = New System.Windows.Forms.Label()
        Me.lblProcessedVal = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.table.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(238, 28)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Simulación importación"
        '
        'progress
        '
        Me.progress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.progress.Location = New System.Drawing.Point(16, 44)
        Me.progress.Name = "progress"
        Me.progress.Size = New System.Drawing.Size(441, 18)
        Me.progress.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.progress.TabIndex = 1
        Me.progress.Value = 51
        '
        'table
        '
        Me.table.ColumnCount = 2
        Me.table.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.0!))
        Me.table.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.table.Controls.Add(Me.lblInProcess, 0, 0)
        Me.table.Controls.Add(Me.lblInProcessVal, 1, 0)
        Me.table.Controls.Add(Me.lblErrors, 0, 1)
        Me.table.Controls.Add(Me.lblErrorsVal, 1, 1)
        Me.table.Controls.Add(Me.lblProcessed, 0, 2)
        Me.table.Controls.Add(Me.lblProcessedVal, 1, 2)
        Me.table.Location = New System.Drawing.Point(16, 84)
        Me.table.Name = "table"
        Me.table.RowCount = 3
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.table.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26.0!))
        Me.table.Size = New System.Drawing.Size(440, 78)
        Me.table.TabIndex = 2
        '
        'lblInProcess
        '
        Me.lblInProcess.AutoSize = True
        Me.lblInProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInProcess.Location = New System.Drawing.Point(3, 0)
        Me.lblInProcess.Name = "lblInProcess"
        Me.lblInProcess.Size = New System.Drawing.Size(258, 26)
        Me.lblInProcess.TabIndex = 0
        Me.lblInProcess.Text = "En proceso:"
        Me.lblInProcess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblInProcessVal
        '
        Me.lblInProcessVal.AutoSize = True
        Me.lblInProcessVal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblInProcessVal.Location = New System.Drawing.Point(267, 0)
        Me.lblInProcessVal.Name = "lblInProcessVal"
        Me.lblInProcessVal.Size = New System.Drawing.Size(170, 26)
        Me.lblInProcessVal.TabIndex = 1
        Me.lblInProcessVal.Text = "51%"
        Me.lblInProcessVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblErrors
        '
        Me.lblErrors.AutoSize = True
        Me.lblErrors.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblErrors.Location = New System.Drawing.Point(3, 26)
        Me.lblErrors.Name = "lblErrors"
        Me.lblErrors.Size = New System.Drawing.Size(258, 26)
        Me.lblErrors.TabIndex = 2
        Me.lblErrors.Text = "Número errores detectados:"
        Me.lblErrors.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblErrorsVal
        '
        Me.lblErrorsVal.AutoSize = True
        Me.lblErrorsVal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblErrorsVal.Location = New System.Drawing.Point(267, 26)
        Me.lblErrorsVal.Name = "lblErrorsVal"
        Me.lblErrorsVal.Size = New System.Drawing.Size(170, 26)
        Me.lblErrorsVal.TabIndex = 3
        Me.lblErrorsVal.Text = "23"
        Me.lblErrorsVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblProcessed
        '
        Me.lblProcessed.AutoSize = True
        Me.lblProcessed.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProcessed.Location = New System.Drawing.Point(3, 52)
        Me.lblProcessed.Name = "lblProcessed"
        Me.lblProcessed.Size = New System.Drawing.Size(258, 26)
        Me.lblProcessed.TabIndex = 4
        Me.lblProcessed.Text = "Número de procesado:"
        Me.lblProcessed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProcessedVal
        '
        Me.lblProcessedVal.AutoSize = True
        Me.lblProcessedVal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblProcessedVal.Location = New System.Drawing.Point(267, 52)
        Me.lblProcessedVal.Name = "lblProcessedVal"
        Me.lblProcessedVal.Size = New System.Drawing.Size(170, 26)
        Me.lblProcessedVal.TabIndex = 5
        Me.lblProcessedVal.Text = "23 out of 45"
        Me.lblProcessedVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(357, 178)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 28)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'ProcessForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(473, 243)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.table)
        Me.Controls.Add(Me.progress)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "ProcessForm"
        Me.Padding = New System.Windows.Forms.Padding(12)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Progreso"
        Me.table.ResumeLayout(False)
        Me.table.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents progress As System.Windows.Forms.ProgressBar
    Friend WithEvents table As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblInProcess As System.Windows.Forms.Label
    Friend WithEvents lblInProcessVal As System.Windows.Forms.Label
    Friend WithEvents lblErrors As System.Windows.Forms.Label
    Friend WithEvents lblErrorsVal As System.Windows.Forms.Label
    Friend WithEvents lblProcessed As System.Windows.Forms.Label
    Friend WithEvents lblProcessedVal As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
