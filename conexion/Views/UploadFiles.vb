Imports Microsoft.VisualBasic.FileIO

Namespace Views
    Partial Class UploadFiles
        Inherits UserControl

        Public Enum FileKind
            CsvComma
            CsvSemicolon
            TxtTab
        End Enum

        Private ReadOnly _nativeObjects As New List(Of String)()
        Private ReadOnly _udoObjects As New List(Of String)()

        Public Sub New()
            InitializeComponent()

            ' Opciones exactamente como en la UI de referencia
            cboFileType.Items.AddRange(New Object() {
                "csv (Comma delimited)",
                "csv (Semicolon delimited)",
                "txt (Tab delimited)"
            })
            cboFileType.SelectedIndex = 0

            ' Eventos
            AddHandler gridNative.CellContentClick, AddressOf gridNative_CellContentClick
            AddHandler gridUDO.CellContentClick, AddressOf gridUDO_CellContentClick
            AddHandler tvNative.AfterSelect, AddressOf tv_AfterSelect
            AddHandler tvUDO.AfterSelect, AddressOf tv_AfterSelect

            ' Dummy inicial (puedes llenar desde fuera con SetNativeObjects/SetUDOObjects)
            If _nativeObjects.Count = 0 Then
                _nativeObjects.AddRange(New String() {"Documents", "Document_Lines", "SerialNumbers"})
            End If
            If _udoObjects.Count = 0 Then
                _udoObjects.AddRange(New String() {"@UDO_HEADER", "@UDO_LINES"})
            End If

            BuildNative()
            BuildUDO()
        End Sub

        ' ========== Construcción de cada pestaña ==========
        Private Sub BuildNative()
            tvNative.Nodes.Clear()
            Dim root = tvNative.Nodes.Add("Business Objects")
            For Each s In _nativeObjects
                root.Nodes.Add(s)
            Next
            tvNative.ExpandAll()

            gridNative.Rows.Clear()
            For Each s In _nativeObjects
                Dim idx = gridNative.Rows.Add(s, "", "...")
                gridNative.Rows(idx).Tag = s
            Next
        End Sub

        Private Sub BuildUDO()
            tvUDO.Nodes.Clear()
            Dim root = tvUDO.Nodes.Add("Business Objects")
            For Each s In _udoObjects
                root.Nodes.Add(s)
            Next
            tvUDO.ExpandAll()

            gridUDO.Rows.Clear()
            For Each s In _udoObjects
                Dim idx = gridUDO.Rows.Add(s, "", "...")
                gridUDO.Rows(idx).Tag = s
            Next
        End Sub

        ' ========== Navegación/selección ==========
        Private Sub tv_AfterSelect(sender As Object, e As TreeViewEventArgs)
            Dim tv = DirectCast(sender, TreeView)
            Dim grid As DataGridView = If(tv Is tvNative, gridNative, gridUDO)
            If e.Node Is Nothing OrElse e.Node.Parent Is Nothing Then Return
            For Each r As DataGridViewRow In grid.Rows
                If CStr(r.Cells(0).Value) = e.Node.Text Then
                    r.Selected = True
                    grid.CurrentCell = r.Cells(1)
                    Exit For
                End If
            Next
        End Sub

        Private Sub gridNative_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
            If e.RowIndex < 0 OrElse e.ColumnIndex <> gridNative.Columns("colNativeBrowse").Index Then Return
            Dim path = BrowseForSelected()
            If path IsNot Nothing Then gridNative.Rows(e.RowIndex).Cells("colNativePath").Value = path
        End Sub

        Private Sub gridUDO_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
            If e.RowIndex < 0 OrElse e.ColumnIndex <> gridUDO.Columns("colUDOBrowse").Index Then Return
            Dim path = BrowseForSelected()
            If path IsNot Nothing Then gridUDO.Rows(e.RowIndex).Cells("colUDOPath").Value = path
        End Sub

        Private Function BrowseForSelected() As String
            Using dlg As New OpenFileDialog()
                Select Case SelectedKind
                    Case FileKind.CsvComma, FileKind.CsvSemicolon
                        dlg.Filter = "CSV files (*.csv)|*.csv"
                    Case FileKind.TxtTab
                        dlg.Filter = "Text files (*.txt)|*.txt"
                End Select
                dlg.Title = "Seleccione archivo de datos"
                dlg.Multiselect = False
                If dlg.ShowDialog() = DialogResult.OK Then
                    Return dlg.FileName
                End If
            End Using
            Return Nothing
        End Function

        ' ========== API pública del control ==========
        Public ReadOnly Property SelectedKind As FileKind
            Get
                Select Case cboFileType.SelectedIndex
                    Case 0 : Return FileKind.CsvComma
                    Case 1 : Return FileKind.CsvSemicolon
                    Case 2 : Return FileKind.TxtTab
                    Case Else : Return FileKind.CsvComma
                End Select
            End Get
        End Property

        Public ReadOnly Property SelectedDelimiter As String
            Get
                Select Case SelectedKind
                    Case FileKind.CsvComma : Return ","
                    Case FileKind.CsvSemicolon : Return ";"
                    Case FileKind.TxtTab : Return vbTab
                    Case Else : Return ","
                End Select
            End Get
        End Property

        Public Sub SetNativeObjects(objs As IEnumerable(Of String))
            _nativeObjects.Clear()
            _nativeObjects.AddRange(objs)
            BuildNative()
        End Sub

        Public Sub SetUDOObjects(objs As IEnumerable(Of String))
            _udoObjects.Clear()
            _udoObjects.AddRange(objs)
            BuildUDO()
        End Sub

        Public Sub EnableNativeTab(enabled As Boolean)
            tabNative.Enabled = enabled
            If Not enabled AndAlso tabs.SelectedTab Is tabNative Then tabs.SelectedTab = tabUDOs
        End Sub

        Public Sub EnableUDOTab(enabled As Boolean)
            tabUDOs.Enabled = enabled
            If Not enabled AndAlso tabs.SelectedTab Is tabUDOs Then tabs.SelectedTab = tabNative
        End Sub

        ' Devuelven los mapas Objeto -> Ruta
        Public Function GetSelectedFilesNative() As Dictionary(Of String, String)
            Dim map As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            For Each row As DataGridViewRow In gridNative.Rows
                Dim objName = CStr(row.Cells("colNativeObject").Value)
                Dim path = CStr(row.Cells("colNativePath").Value)
                If Not String.IsNullOrWhiteSpace(path) Then map(objName) = path
            Next
            Return map
        End Function

        Public Function GetSelectedFilesUDOs() As Dictionary(Of String, String)
            Dim map As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            For Each row As DataGridViewRow In gridUDO.Rows
                Dim objName = CStr(row.Cells("colUDOObject").Value)
                Dim path = CStr(row.Cells("colUDOPath").Value)
                If Not String.IsNullOrWhiteSpace(path) Then map(objName) = path
            Next
            Return map
        End Function

        Public Function GetAllSelectedFiles() As Dictionary(Of String, String)
            Dim all As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            For Each kv In GetSelectedFilesNative() : all(kv.Key) = kv.Value : Next
            For Each kv In GetSelectedFilesUDOs() : all(kv.Key) = kv.Value : Next
            Return all
        End Function

        ' Preview opcional (elige de qué pestaña leer)
        Public Function PreviewFirstRows(objName As String, fromUDOTab As Boolean, Optional maxRows As Integer = 20) As DataTable
            Dim dt As New DataTable(objName)
            Dim grid = If(fromUDOTab, gridUDO, gridNative)
            Dim path As String = Nothing

            For Each row As DataGridViewRow In grid.Rows
                If String.Equals(CStr(row.Cells(0).Value), objName, StringComparison.OrdinalIgnoreCase) Then
                    path = CStr(row.Cells(1).Value) : Exit For
                End If
            Next
            If String.IsNullOrWhiteSpace(path) OrElse Not IO.File.Exists(path) Then Return dt

            Using parser As New TextFieldParser(path, System.Text.Encoding.UTF8)
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(New String() {SelectedDelimiter})

                Dim headers = parser.ReadFields()
                If headers Is Nothing Then Return dt
                For Each h In headers
                    dt.Columns.Add(If(String.IsNullOrWhiteSpace(h), $"Col{dt.Columns.Count + 1}", h))
                Next

                Dim c As Integer = 0
                While Not parser.EndOfData AndAlso c < maxRows
                    dt.Rows.Add(parser.ReadFields())
                    c += 1
                End While
            End Using
            Return dt
        End Function
    End Class
End Namespace

