Imports System.Windows.Forms

Public Class ErrorsForm

    Private Sub ErrorsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetupGrid()
        LoadErrors()
    End Sub

    Private Sub SetupGrid()
        DataGridView1.Columns.Clear()
        DataGridView1.AutoGenerateColumns = False
        DataGridView1.RowHeadersVisible = False
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.Name = "No"
        colNo.HeaderText = "No"
        colNo.Width = 50
        colNo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DataGridView1.Columns.Add(colNo)

        Dim colKey As New DataGridViewTextBoxColumn()
        colKey.Name = "Key"
        colKey.HeaderText = "Key"
        colKey.Width = 150
        DataGridView1.Columns.Add(colKey)

        Dim colMsg As New DataGridViewTextBoxColumn()
        colMsg.Name = "Message"
        colMsg.HeaderText = "Error"
        colMsg.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        DataGridView1.Columns.Add(colMsg)
    End Sub

    Private Sub LoadErrors()
        DataGridView1.Rows.Clear()
        Dim index As Integer = 1
        For Each kvp In SubMain.ListadoErrores
            Dim messages = kvp.Value.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each msg In messages
                DataGridView1.Rows.Add(index, kvp.Key, msg)
                index += 1
            Next
        Next
    End Sub

End Class