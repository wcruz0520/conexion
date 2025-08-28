Imports conexion.ImportModels

Public Class ResultsForm
    Inherits Form

    Private ReadOnly grid As DataGridView
    Private ReadOnly panelTop As Panel
    Private ReadOnly cboFilter As ComboBox

    Public Sub New(results As IEnumerable(Of RowResult))
        Me.Text = "Resultados de la importación"
        Me.Width = 880 : Me.Height = 520
        Me.StartPosition = FormStartPosition.CenterParent

        panelTop = New Panel With {.Dock = DockStyle.Top, .Height = 40, .Padding = New Padding(8)}
        cboFilter = New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Width = 180}
        cboFilter.Items.AddRange({"Todos", "Éxito", "Error"})
        cboFilter.SelectedIndex = 0
        panelTop.Controls.Add(New Label With {.Text = "Mostrar:", .AutoSize = True, .Location = New Point(8, 12)})
        cboFilter.Location = New Point(70, 8)
        panelTop.Controls.Add(cboFilter)

        grid = New DataGridView With {.Dock = DockStyle.Fill, .ReadOnly = True, .AllowUserToAddRows = False, .AllowUserToDeleteRows = False, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
        grid.Columns.Add("colIdx", "#")
        grid.Columns.Add("colTable", "Tabla")
        grid.Columns.Add("colKey", "Key")
        grid.Columns.Add("colAction", "Acción")
        grid.Columns.Add("colState", "Estado")
        grid.Columns.Add("colMsg", "Mensaje")

        Controls.Add(grid)
        Controls.Add(panelTop)

        _all = results.ToList()
        Bind()
        AddHandler cboFilter.SelectedIndexChanged, Sub() Bind()
    End Sub

    Private ReadOnly _all As List(Of RowResult)

    Private Sub Bind()
        grid.Rows.Clear()
        Dim filter = cboFilter.SelectedItem?.ToString()
        Dim data = _all.AsEnumerable()
        If filter = "Éxito" Then data = data.Where(Function(r) r.State = RowState.Success)
        If filter = "Error" Then data = data.Where(Function(r) r.State = RowState.Error)

        Dim i = 0
        For Each r In data
            i += 1
            grid.Rows.Add(r.Index, r.TableName, r.Key, r.Action.ToString(), If(r.State = RowState.Success, "Éxito", "Error"), r.Message)
        Next
    End Sub
End Class
