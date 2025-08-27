Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Namespace Views
    Partial Class UploadFiles
        Inherits UserControl

        Public Enum FileKind
            CsvComma
            CsvSemicolon
            TxtTab
        End Enum

        Private ReadOnly _nativeCategories As New List(Of NativeCategory)()
        Private ReadOnly _udoCategories As New List(Of UDOCategories)()
        Private ReadOnly _nativePaths As New Dictionary(Of String, Dictionary(Of String, String))(StringComparer.OrdinalIgnoreCase)
        Private ReadOnly _udoPaths As New Dictionary(Of String, Dictionary(Of String, String))(StringComparer.OrdinalIgnoreCase)
        Private ReadOnly splitNativeMain As New SplitContainer()
        Private ReadOnly splitUDOMain As New SplitContainer()
        Private ReadOnly tabsNativePreview As New TabControl()
        Private ReadOnly tabsUDOPreview As New TabControl()

        Public Sub New()
            InitializeComponent()
            SetupSplitWithPreview(tabNative, splitNative, tabsNativePreview, splitNativeMain)
            SetupSplitWithPreview(tabUDOs, splitUDO, tabsUDOPreview, splitUDOMain)
            SwapPanels(splitNative, tvNative, gridNative)
            SwapPanels(splitUDO, tvUDO, gridUDO)
            splitNative.SplitterDistance = splitNative.Width \ 2
            splitUDO.SplitterDistance = splitUDO.Width \ 2
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
            If _nativeCategories.Count = 0 Then
                _nativeCategories.AddRange(MappingNativeCategories.GetDefault())
            End If
            If _udoCategories.Count = 0 Then
                _udoCategories.AddRange(MappingUdoCategories.GetDefault())
            End If

            BuildNative()
            BuildUDO()
        End Sub

        Private Sub SwapPanels(split As SplitContainer, left As Control, right As Control)
            split.Panel1.Controls.Clear()
            split.Panel1.Padding = New Padding(0, 4, 8, 0)
            split.Panel1.Controls.Add(left)

            split.Panel2.Controls.Clear()
            split.Panel2.Padding = New Padding(8, 4, 0, 0)
            split.Panel2.Controls.Add(right)
        End Sub

        Private Sub SetupSplitWithPreview(tab As TabPage, existingSplit As SplitContainer, previewTabs As TabControl, wrapper As SplitContainer)
            tab.Controls.Remove(existingSplit)
            wrapper.Dock = DockStyle.Fill
            wrapper.Orientation = Orientation.Horizontal
            wrapper.SplitterDistance = CInt(tab.Height * 0.6)
            wrapper.Panel1.Controls.Add(existingSplit)
            wrapper.Panel2.Padding = New Padding(0, 8, 0, 0)
            previewTabs.Dock = DockStyle.Fill
            wrapper.Panel2.Controls.Add(previewTabs)
            tab.Controls.Add(wrapper)
        End Sub
        ' ========== Construcción de cada pestaña ==========
        Private Sub BuildNative()
            tvNative.Nodes.Clear()
            Dim root = tvNative.Nodes.Add("Business Objects")
            For Each cat In _nativeCategories
                Dim catNode = root.Nodes.Add(cat.Name)
                For Each s In cat.Objects
                    catNode.Nodes.Add(s)
                Next
            Next
            tvNative.ExpandAll()
            gridNative.Rows.Clear()
        End Sub

        Private Sub BuildUDO()
            tvUDO.Nodes.Clear()
            Dim root = tvUDO.Nodes.Add("Business Objects")
            For Each cat In _udoCategories
                Dim catNode = root.Nodes.Add(cat.Name)
                For Each s In cat.Objects
                    catNode.Nodes.Add(s)
                Next
            Next
            tvUDO.ExpandAll()
            gridUDO.Rows.Clear()
        End Sub

        Private Sub ShowNativeTables(objName As String)
            gridNative.Rows.Clear()
            If String.IsNullOrWhiteSpace(objName) Then Return
            Dim paths As Dictionary(Of String, String) = Nothing
            _nativePaths.TryGetValue(objName, paths)
            For Each tbl In MappingUploadTables.GetNativeTables(objName)
                Dim current As String = Nothing
                If paths IsNot Nothing Then paths.TryGetValue(tbl, current)
                Dim idx = gridNative.Rows.Add(tbl, current, "...")
                gridNative.Rows(idx).Tag = objName
                gridNative.Rows(idx).Cells("colNativePath").Tag = current
            Next
        End Sub

        Private Sub ShowUDOTables(objName As String)
            gridUDO.Rows.Clear()
            If String.IsNullOrWhiteSpace(objName) Then Return
            Dim paths As Dictionary(Of String, String) = Nothing
            _udoPaths.TryGetValue(objName, paths)
            For Each tbl In MappingUploadTables.GetUDOTables(objName)
                Dim current As String = Nothing
                If paths IsNot Nothing Then paths.TryGetValue(tbl, current)
                Dim idx = gridUDO.Rows.Add(tbl, current, "...")
                gridUDO.Rows(idx).Tag = objName
                gridUDO.Rows(idx).Cells("colUDOPath").Tag = current
            Next
        End Sub

        ' ========== Navegación/selección ==========
        Private Sub tv_AfterSelect(sender As Object, e As TreeViewEventArgs)
            Dim tv = DirectCast(sender, TreeView)
            Dim objName As String = Nothing
            If e.Node IsNot Nothing AndAlso e.Node.Nodes.Count = 0 Then
                objName = e.Node.Text
            End If

            If tv Is tvNative Then
                ShowNativeTables(objName)
            Else
                ShowUDOTables(objName)
            End If
        End Sub

        Private Sub gridNative_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
            If e.RowIndex < 0 OrElse e.ColumnIndex <> gridNative.Columns("colNativeBrowse").Index Then Return
            Dim _path = BrowseForSelected()
            If _path IsNot Nothing Then
                Dim row = gridNative.Rows(e.RowIndex)
                Dim objName = CStr(row.Tag)
                Dim tbl = CStr(row.Cells("colNativeObject").Value)
                Dim cell = row.Cells("colNativePath")
                cell.Tag = _path
                cell.Value = Path.GetFileName(CStr(_path))
                cell.ToolTipText = CStr(_path)

                Dim paths As Dictionary(Of String, String) = Nothing
                If Not _nativePaths.TryGetValue(objName, paths) Then
                    paths = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                    _nativePaths(objName) = paths
                End If
                paths(tbl) = _path
                UpdatePreviewTab(tbl, False)
            End If
        End Sub

        Private Sub gridUDO_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
            If e.RowIndex < 0 OrElse e.ColumnIndex <> gridUDO.Columns("colUDOBrowse").Index Then Return
            Dim _path = BrowseForSelected()
            If _path IsNot Nothing Then
                Dim row = gridUDO.Rows(e.RowIndex)
                Dim objName = CStr(row.Tag)
                Dim tbl = CStr(row.Cells("colUDOObject").Value)
                Dim cell = row.Cells("colUDOPath")
                cell.Tag = _path
                cell.Value = Path.GetFileName(CStr(_path))
                cell.ToolTipText = CStr(_path)

                Dim paths As Dictionary(Of String, String) = Nothing
                If Not _udoPaths.TryGetValue(objName, paths) Then
                    paths = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                    _udoPaths(objName) = paths
                End If
                paths(tbl) = _path
                UpdatePreviewTab(tbl, True)
            End If
        End Sub

        Private Sub UpdatePreviewTab(objName As String, fromUDOTab As Boolean)
            Dim tabs = If(fromUDOTab, tabsUDOPreview, tabsNativePreview)
            Dim target As TabPage = Nothing
            For Each tp As TabPage In tabs.TabPages
                If String.Equals(tp.Text, objName, StringComparison.OrdinalIgnoreCase) Then
                    target = tp
                    Exit For
                End If
            Next
            If target Is Nothing Then
                target = New TabPage(objName)
                Dim g As New DataGridView()
                g.Dock = DockStyle.Fill
                g.ReadOnly = True
                g.AllowUserToAddRows = False
                g.AllowUserToDeleteRows = False
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                target.Controls.Add(g)
                tabs.TabPages.Add(target)
            End If
            Dim dt = PreviewFirstRows(objName, fromUDOTab)
            CType(target.Controls(0), DataGridView).DataSource = dt
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

        Public Sub SetNativeObjects(categories As IEnumerable(Of NativeCategory))
            _nativeCategories.Clear()
            _nativeCategories.AddRange(categories)
            _nativePaths.Clear()
            BuildNative()
        End Sub

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
            _nativeCategories.Clear()
            Dim cat As New NativeCategory("General")
            cat.Objects.AddRange(objs)
            _nativeCategories.Add(cat)
            _nativePaths.Clear()
            BuildNative()
        End Sub

        Public Sub SetUDOObjects(categories As IEnumerable(Of UDOCategories))
            _udoCategories.Clear()
            _udoCategories.AddRange(categories)
            BuildUDO()
        End Sub

        Public Sub SetUDOObjects(objs As IEnumerable(Of String))
            _udoCategories.Clear()
            Dim cat As New UDOCategories("General")
            cat.Objects.AddRange(objs)
            _udoCategories.Add(cat)
            _udoPaths.Clear()
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
            For Each obj In _nativePaths.Values
                For Each kv In obj
                    If Not String.IsNullOrWhiteSpace(kv.Value) Then map(kv.Key) = kv.Value
                Next

            Next
            Return map
        End Function

        Public Function GetSelectedFilesUDOs() As Dictionary(Of String, String)
            Dim map As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            For Each obj In _udoPaths.Values
                For Each kv In obj
                    If Not String.IsNullOrWhiteSpace(kv.Value) Then map(kv.Key) = kv.Value
                Next

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
                    Dim cell = row.Cells(1)
                    path = TryCast(cell.Tag, String)
                    If String.IsNullOrWhiteSpace(path) Then path = CStr(cell.Value)
                    Exit For
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
                    Dim fields = parser.ReadFields()
                    If fields Is Nothing Then Exit While
                    If fields.Length > dt.Columns.Count Then
                        For i = dt.Columns.Count To fields.Length - 1
                            dt.Columns.Add($"Col{i + 1}")
                        Next
                    End If
                    dt.Rows.Add(fields)
                    c += 1
                End While
            End Using
            Return dt
        End Function

    End Class
End Namespace

