' Diseñador: dos pestañas (Nativas/UDOs) con layout tipo SAP
Namespace Views
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class UploadFiles
        Inherits System.Windows.Forms.UserControl

        Private components As System.ComponentModel.IContainer
        Friend WithEvents panelHeader As Panel
        Friend WithEvents lblTitle As Label
        Friend WithEvents lblHelp As Label
        Friend WithEvents panelTop As Panel
        Friend WithEvents lblSelect As Label
        Friend WithEvents cboFileType As ComboBox
        Friend WithEvents tabs As TabControl
        Friend WithEvents tabNative As TabPage
        Friend WithEvents tabUDOs As TabPage

        ' Nativas
        Friend WithEvents splitNative As SplitContainer
        Friend WithEvents tvNative As TreeView
        Friend WithEvents gridNative As DataGridView
        Friend WithEvents colNativeObject As DataGridViewTextBoxColumn
        Friend WithEvents colNativePath As DataGridViewTextBoxColumn
        Friend WithEvents colNativeBrowse As DataGridViewButtonColumn

        ' UDOs
        Friend WithEvents splitUDO As SplitContainer
        Friend WithEvents tvUDO As TreeView
        Friend WithEvents gridUDO As DataGridView
        Friend WithEvents colUDOObject As DataGridViewTextBoxColumn
        Friend WithEvents colUDOPath As DataGridViewTextBoxColumn
        Friend WithEvents colUDOBrowse As DataGridViewButtonColumn

        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then components.Dispose()
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()

            Me.panelHeader = New Panel()
            Me.lblTitle = New Label()
            Me.lblHelp = New Label()
            Me.panelTop = New Panel()
            Me.lblSelect = New Label()
            Me.cboFileType = New ComboBox()
            Me.tabs = New TabControl()
            Me.tabNative = New TabPage()
            Me.tabUDOs = New TabPage()

            Me.splitNative = New SplitContainer()
            Me.tvNative = New TreeView()
            Me.gridNative = New DataGridView()
            Me.colNativeObject = New DataGridViewTextBoxColumn()
            Me.colNativePath = New DataGridViewTextBoxColumn()
            Me.colNativeBrowse = New DataGridViewButtonColumn()

            Me.splitUDO = New SplitContainer()
            Me.tvUDO = New TreeView()
            Me.gridUDO = New DataGridView()
            Me.colUDOObject = New DataGridViewTextBoxColumn()
            Me.colUDOPath = New DataGridViewTextBoxColumn()
            Me.colUDOBrowse = New DataGridViewButtonColumn()

            ' ===== Header =====
            Me.panelHeader.Dock = DockStyle.Top
            Me.panelHeader.Height = 92
            Me.panelHeader.Padding = New Padding(16, 12, 16, 8)

            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New Font("Segoe UI", 13.0!, FontStyle.Bold)
            Me.lblTitle.Text = "Step 4: Select Data Source"

            Me.lblHelp.AutoSize = True
            Me.lblHelp.Top = 34
            Me.lblHelp.MaximumSize = New Size(1200, 0)
            Me.lblHelp.Text =
              "To define data sources for business objects, do the following:" & Environment.NewLine &
              "1. From the ""Select File Type"" dropdown list, select a file delimiter character type." & Environment.NewLine &
              "2. Select parent or child business object(s) to be imported." & Environment.NewLine &
              "3. Choose the adjacent browse button to define the navigation path." & Environment.NewLine &
              "Do not change the position of or delete any column from the standard templates."

            Me.panelHeader.Controls.Add(Me.lblTitle)
            Me.panelHeader.Controls.Add(Me.lblHelp)

            ' ===== Top (Select File Type) =====
            Me.panelTop.Dock = DockStyle.Top
            Me.panelTop.Height = 42
            Me.panelTop.Padding = New Padding(16, 6, 16, 6)

            Me.lblSelect.Text = "Select File Type"
            Me.lblSelect.AutoSize = True
            Me.lblSelect.Left = 16
            Me.lblSelect.Top = 12

            Me.cboFileType.DropDownStyle = ComboBoxStyle.DropDownList
            Me.cboFileType.Left = 150
            Me.cboFileType.Top = 8
            Me.cboFileType.Width = 250

            Me.panelTop.Controls.Add(Me.lblSelect)
            Me.panelTop.Controls.Add(Me.cboFileType)

            ' ===== TabControl =====
            Me.tabs.Dock = DockStyle.Fill
            Me.tabs.Alignment = TabAlignment.Top
            Me.tabs.Appearance = TabAppearance.Normal
            Me.tabs.Controls.Add(Me.tabNative)
            Me.tabs.Controls.Add(Me.tabUDOs)

            ' ===== Tab Nativas =====
            Me.tabNative.Text = "Nativas"
            Me.tabNative.Padding = New Padding(8)
            Me.tabNative.UseVisualStyleBackColor = True

            Me.splitNative.Dock = DockStyle.Fill
            Me.splitNative.SplitterDistance = 300
            Me.splitNative.Panel1.Padding = New Padding(0, 4, 8, 0)
            Me.splitNative.Panel2.Padding = New Padding(8, 4, 0, 0)

            Me.tvNative.Dock = DockStyle.Fill

            Me.gridNative.Dock = DockStyle.Fill
            Me.gridNative.AllowUserToAddRows = False
            Me.gridNative.AllowUserToDeleteRows = False
            Me.gridNative.RowHeadersVisible = False
            Me.gridNative.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Me.colNativeObject.HeaderText = "Business Object"
            Me.colNativeObject.ReadOnly = True
            Me.colNativePath.HeaderText = "File Path"
            Me.colNativeBrowse.HeaderText = ""
            Me.colNativeBrowse.Width = 60
            Me.colNativeBrowse.Text = "..."
            Me.colNativeBrowse.UseColumnTextForButtonValue = True
            Me.gridNative.Columns.AddRange(New DataGridViewColumn() {Me.colNativeObject, Me.colNativePath, Me.colNativeBrowse})

            Me.splitNative.Panel1.Controls.Add(Me.tvNative)
            Me.splitNative.Panel2.Controls.Add(Me.gridNative)
            Me.tabNative.Controls.Add(Me.splitNative)

            ' ===== Tab UDOs =====
            Me.tabUDOs.Text = "UDOs"
            Me.tabUDOs.Padding = New Padding(8)
            Me.tabUDOs.UseVisualStyleBackColor = True

            Me.splitUDO.Dock = DockStyle.Fill
            Me.splitUDO.SplitterDistance = 300
            Me.splitUDO.Panel1.Padding = New Padding(0, 4, 8, 0)
            Me.splitUDO.Panel2.Padding = New Padding(8, 4, 0, 0)

            Me.tvUDO.Dock = DockStyle.Fill

            Me.gridUDO.Dock = DockStyle.Fill
            Me.gridUDO.AllowUserToAddRows = False
            Me.gridUDO.AllowUserToDeleteRows = False
            Me.gridUDO.RowHeadersVisible = False
            Me.gridUDO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Me.colUDOObject.HeaderText = "Business Object"
            Me.colUDOObject.ReadOnly = True
            Me.colUDOPath.HeaderText = "File Path"
            Me.colUDOBrowse.HeaderText = ""
            Me.colUDOBrowse.Width = 60
            Me.colUDOBrowse.Text = "..."
            Me.colUDOBrowse.UseColumnTextForButtonValue = True
            Me.gridUDO.Columns.AddRange(New DataGridViewColumn() {Me.colUDOObject, Me.colUDOPath, Me.colUDOBrowse})

            Me.splitUDO.Panel1.Controls.Add(Me.tvUDO)
            Me.splitUDO.Panel2.Controls.Add(Me.gridUDO)
            Me.tabUDOs.Controls.Add(Me.splitUDO)

            ' ===== Control =====
            Me.Controls.Add(Me.tabs)
            Me.Controls.Add(Me.panelTop)
            Me.Controls.Add(Me.panelHeader)
            Me.Name = "UploadFiles"
            Me.BackColor = Color.White
            Me.Size = New Size(980, 560)
        End Sub
    End Class
End Namespace
