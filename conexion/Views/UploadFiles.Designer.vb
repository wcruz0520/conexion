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

        ' UDOs
        Friend WithEvents splitUDO As SplitContainer
        Friend WithEvents tvUDO As TreeView
        Friend WithEvents gridUDO As DataGridView

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
            Me.panelHeader = New System.Windows.Forms.Panel()
            Me.lblTitle = New System.Windows.Forms.Label()
            Me.lblHelp = New System.Windows.Forms.Label()
            Me.panelTop = New System.Windows.Forms.Panel()
            Me.lblSelect = New System.Windows.Forms.Label()
            Me.cboFileType = New System.Windows.Forms.ComboBox()
            Me.tabs = New System.Windows.Forms.TabControl()
            Me.tabNative = New System.Windows.Forms.TabPage()
            Me.splitNative = New System.Windows.Forms.SplitContainer()
            Me.gridNative = New System.Windows.Forms.DataGridView()
            Me.colNativeObject = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colNativePath = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colNativeBrowse = New System.Windows.Forms.DataGridViewButtonColumn()
            Me.tvNative = New System.Windows.Forms.TreeView()
            Me.tabUDOs = New System.Windows.Forms.TabPage()
            Me.splitUDO = New System.Windows.Forms.SplitContainer()
            Me.gridUDO = New System.Windows.Forms.DataGridView()
            Me.colUDOObject = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colUDOPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.colUDOBrowse = New System.Windows.Forms.DataGridViewButtonColumn()
            Me.tvUDO = New System.Windows.Forms.TreeView()
            Me.panelHeader.SuspendLayout()
            Me.panelTop.SuspendLayout()
            Me.tabs.SuspendLayout()
            Me.tabNative.SuspendLayout()
            CType(Me.splitNative, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitNative.Panel1.SuspendLayout()
            Me.splitNative.Panel2.SuspendLayout()
            Me.splitNative.SuspendLayout()
            CType(Me.gridNative, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tabUDOs.SuspendLayout()
            CType(Me.splitUDO, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.splitUDO.Panel1.SuspendLayout()
            Me.splitUDO.Panel2.SuspendLayout()
            Me.splitUDO.SuspendLayout()
            CType(Me.gridUDO, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panelHeader
            '
            Me.panelHeader.Controls.Add(Me.lblTitle)
            Me.panelHeader.Controls.Add(Me.lblHelp)
            Me.panelHeader.Dock = System.Windows.Forms.DockStyle.Top
            Me.panelHeader.Location = New System.Drawing.Point(0, 0)
            Me.panelHeader.Name = "panelHeader"
            Me.panelHeader.Padding = New System.Windows.Forms.Padding(16, 12, 16, 8)
            Me.panelHeader.Size = New System.Drawing.Size(980, 40)
            Me.panelHeader.TabIndex = 2
            '
            'lblTitle
            '
            Me.lblTitle.AutoSize = True
            Me.lblTitle.Font = New System.Drawing.Font("Calibri", 11.0!, System.Drawing.FontStyle.Bold)
            Me.lblTitle.Location = New System.Drawing.Point(0, 0)
            Me.lblTitle.Name = "lblTitle"
            Me.lblTitle.Size = New System.Drawing.Size(223, 23)
            Me.lblTitle.TabIndex = 0
            Me.lblTitle.Text = "Paso 1: Cargar los archivos."
            '
            'lblHelp
            '
            Me.lblHelp.AutoSize = True
            Me.lblHelp.Location = New System.Drawing.Point(3, 19)
            Me.lblHelp.MaximumSize = New System.Drawing.Size(1200, 0)
            Me.lblHelp.Name = "lblHelp"
            Me.lblHelp.Size = New System.Drawing.Size(528, 17)
            Me.lblHelp.TabIndex = 1
            Me.lblHelp.Text = "No olvidar el tipo de archivo a cargar depende de la opción que escoja más abajo." &
    ""
            '
            'panelTop
            '
            Me.panelTop.Controls.Add(Me.lblSelect)
            Me.panelTop.Controls.Add(Me.cboFileType)
            Me.panelTop.Dock = System.Windows.Forms.DockStyle.Top
            Me.panelTop.Location = New System.Drawing.Point(0, 40)
            Me.panelTop.Name = "panelTop"
            Me.panelTop.Padding = New System.Windows.Forms.Padding(16, 6, 16, 6)
            Me.panelTop.Size = New System.Drawing.Size(980, 36)
            Me.panelTop.TabIndex = 1
            '
            'lblSelect
            '
            Me.lblSelect.AutoSize = True
            Me.lblSelect.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblSelect.Location = New System.Drawing.Point(424, 13)
            Me.lblSelect.Name = "lblSelect"
            Me.lblSelect.Size = New System.Drawing.Size(159, 18)
            Me.lblSelect.TabIndex = 0
            Me.lblSelect.Text = "Seleccionar Tipo Archivo"
            '
            'cboFileType
            '
            Me.cboFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboFileType.Location = New System.Drawing.Point(560, 9)
            Me.cboFileType.Name = "cboFileType"
            Me.cboFileType.Size = New System.Drawing.Size(171, 24)
            Me.cboFileType.TabIndex = 1
            '
            'tabs
            '
            Me.tabs.Controls.Add(Me.tabNative)
            Me.tabs.Controls.Add(Me.tabUDOs)
            Me.tabs.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tabs.Location = New System.Drawing.Point(0, 76)
            Me.tabs.Name = "tabs"
            Me.tabs.SelectedIndex = 0
            Me.tabs.Size = New System.Drawing.Size(980, 484)
            Me.tabs.TabIndex = 0
            '
            'tabNative
            '
            Me.tabNative.Controls.Add(Me.splitNative)
            Me.tabNative.Location = New System.Drawing.Point(4, 25)
            Me.tabNative.Name = "tabNative"
            Me.tabNative.Padding = New System.Windows.Forms.Padding(8)
            Me.tabNative.Size = New System.Drawing.Size(972, 455)
            Me.tabNative.TabIndex = 0
            Me.tabNative.Text = "Nativas"
            Me.tabNative.UseVisualStyleBackColor = True
            '
            'splitNative
            '
            Me.splitNative.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitNative.Location = New System.Drawing.Point(8, 8)
            Me.splitNative.Name = "splitNative"
            '
            'splitNative.Panel1
            '
            Me.splitNative.Panel1.BackColor = System.Drawing.Color.White
            Me.splitNative.Panel1.Controls.Add(Me.gridNative)
            Me.splitNative.Panel1.Padding = New System.Windows.Forms.Padding(0, 4, 8, 0)
            '
            'splitNative.Panel2
            '
            Me.splitNative.Panel2.Controls.Add(Me.tvNative)
            Me.splitNative.Panel2.Padding = New System.Windows.Forms.Padding(8, 4, 0, 0)
            Me.splitNative.Size = New System.Drawing.Size(956, 439)
            Me.splitNative.SplitterDistance = 410
            Me.splitNative.TabIndex = 0
            '
            'gridNative
            '
            Me.gridNative.AllowUserToAddRows = False
            Me.gridNative.AllowUserToDeleteRows = False
            Me.gridNative.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.gridNative.BackgroundColor = System.Drawing.SystemColors.Window
            Me.gridNative.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.gridNative.ColumnHeadersHeight = 25
            Me.gridNative.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colNativeObject, Me.colNativePath, Me.colNativeBrowse})
            Me.gridNative.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridNative.Location = New System.Drawing.Point(0, 4)
            Me.gridNative.Name = "gridNative"
            Me.gridNative.RowHeadersVisible = False
            Me.gridNative.RowHeadersWidth = 51
            Me.gridNative.Size = New System.Drawing.Size(402, 435)
            Me.gridNative.TabIndex = 0
            '
            'colNativeObject
            '
            Me.colNativeObject.FillWeight = 133.9572!
            Me.colNativeObject.HeaderText = "Business Object"
            Me.colNativeObject.MinimumWidth = 6
            Me.colNativeObject.Name = "colNativeObject"
            Me.colNativeObject.ReadOnly = True
            '
            'colNativePath
            '
            Me.colNativePath.FillWeight = 133.9572!
            Me.colNativePath.HeaderText = "File Path"
            Me.colNativePath.MinimumWidth = 6
            Me.colNativePath.Name = "colNativePath"
            '
            'colNativeBrowse
            '
            Me.colNativeBrowse.FillWeight = 32.08556!
            Me.colNativeBrowse.HeaderText = ""
            Me.colNativeBrowse.MinimumWidth = 6
            Me.colNativeBrowse.Name = "colNativeBrowse"
            Me.colNativeBrowse.Text = "..."
            Me.colNativeBrowse.UseColumnTextForButtonValue = True
            '
            'tvNative
            '
            Me.tvNative.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvNative.Location = New System.Drawing.Point(8, 4)
            Me.tvNative.Name = "tvNative"
            Me.tvNative.Size = New System.Drawing.Size(534, 435)
            Me.tvNative.TabIndex = 0
            '
            'tabUDOs
            '
            Me.tabUDOs.Controls.Add(Me.splitUDO)
            Me.tabUDOs.Location = New System.Drawing.Point(4, 25)
            Me.tabUDOs.Name = "tabUDOs"
            Me.tabUDOs.Padding = New System.Windows.Forms.Padding(8)
            Me.tabUDOs.Size = New System.Drawing.Size(972, 455)
            Me.tabUDOs.TabIndex = 1
            Me.tabUDOs.Text = "UDOs"
            Me.tabUDOs.UseVisualStyleBackColor = True
            '
            'splitUDO
            '
            Me.splitUDO.Dock = System.Windows.Forms.DockStyle.Fill
            Me.splitUDO.Location = New System.Drawing.Point(8, 8)
            Me.splitUDO.Name = "splitUDO"
            '
            'splitUDO.Panel1
            '
            Me.splitUDO.Panel1.Controls.Add(Me.gridUDO)
            Me.splitUDO.Panel1.Padding = New System.Windows.Forms.Padding(0, 4, 8, 0)
            '
            'splitUDO.Panel2
            '
            Me.splitUDO.Panel2.Controls.Add(Me.tvUDO)
            Me.splitUDO.Panel2.Padding = New System.Windows.Forms.Padding(8, 4, 0, 0)
            Me.splitUDO.Size = New System.Drawing.Size(956, 439)
            Me.splitUDO.SplitterDistance = 478
            Me.splitUDO.TabIndex = 0
            '
            'gridUDO
            '
            Me.gridUDO.AllowUserToAddRows = False
            Me.gridUDO.AllowUserToDeleteRows = False
            Me.gridUDO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
            Me.gridUDO.BackgroundColor = System.Drawing.SystemColors.Window
            Me.gridUDO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.gridUDO.ColumnHeadersHeight = 29
            Me.gridUDO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colUDOObject, Me.colUDOPath, Me.colUDOBrowse})
            Me.gridUDO.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridUDO.Location = New System.Drawing.Point(0, 4)
            Me.gridUDO.Name = "gridUDO"
            Me.gridUDO.ReadOnly = True
            Me.gridUDO.RowHeadersVisible = False
            Me.gridUDO.RowHeadersWidth = 51
            Me.gridUDO.Size = New System.Drawing.Size(470, 435)
            Me.gridUDO.TabIndex = 0
            '
            'colUDOObject
            '
            Me.colUDOObject.FillWeight = 133.9572!
            Me.colUDOObject.HeaderText = "Business Object"
            Me.colUDOObject.MinimumWidth = 6
            Me.colUDOObject.Name = "colUDOObject"
            Me.colUDOObject.ReadOnly = True
            '
            'colUDOPath
            '
            Me.colUDOPath.FillWeight = 133.9572!
            Me.colUDOPath.HeaderText = "File Path"
            Me.colUDOPath.MinimumWidth = 6
            Me.colUDOPath.Name = "colUDOPath"
            Me.colUDOPath.ReadOnly = True
            '
            'colUDOBrowse
            '
            Me.colUDOBrowse.FillWeight = 32.08556!
            Me.colUDOBrowse.HeaderText = ""
            Me.colUDOBrowse.MinimumWidth = 6
            Me.colUDOBrowse.Name = "colUDOBrowse"
            Me.colUDOBrowse.ReadOnly = True
            Me.colUDOBrowse.Text = "..."
            Me.colUDOBrowse.UseColumnTextForButtonValue = True
            '
            'tvUDO
            '
            Me.tvUDO.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tvUDO.Location = New System.Drawing.Point(8, 4)
            Me.tvUDO.Name = "tvUDO"
            Me.tvUDO.Size = New System.Drawing.Size(466, 435)
            Me.tvUDO.TabIndex = 0
            '
            'UploadFiles
            '
            Me.BackColor = System.Drawing.Color.White
            Me.Controls.Add(Me.tabs)
            Me.Controls.Add(Me.panelTop)
            Me.Controls.Add(Me.panelHeader)
            Me.Name = "UploadFiles"
            Me.Size = New System.Drawing.Size(980, 560)
            Me.panelHeader.ResumeLayout(False)
            Me.panelHeader.PerformLayout()
            Me.panelTop.ResumeLayout(False)
            Me.panelTop.PerformLayout()
            Me.tabs.ResumeLayout(False)
            Me.tabNative.ResumeLayout(False)
            Me.splitNative.Panel1.ResumeLayout(False)
            Me.splitNative.Panel2.ResumeLayout(False)
            CType(Me.splitNative, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitNative.ResumeLayout(False)
            CType(Me.gridNative, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tabUDOs.ResumeLayout(False)
            Me.splitUDO.Panel1.ResumeLayout(False)
            Me.splitUDO.Panel2.ResumeLayout(False)
            CType(Me.splitUDO, System.ComponentModel.ISupportInitialize).EndInit()
            Me.splitUDO.ResumeLayout(False)
            CType(Me.gridUDO, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents colUDOObject As DataGridViewTextBoxColumn
        Friend WithEvents colUDOPath As DataGridViewTextBoxColumn
        Friend WithEvents colUDOBrowse As DataGridViewButtonColumn
        Friend WithEvents colNativeObject As DataGridViewTextBoxColumn
        Friend WithEvents colNativePath As DataGridViewTextBoxColumn
        Friend WithEvents colNativeBrowse As DataGridViewButtonColumn
    End Class
End Namespace
