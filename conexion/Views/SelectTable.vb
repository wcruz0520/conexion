Imports System.Windows.Forms
Imports System.Drawing

Namespace Views
    Public Class SelectTable
        Inherits UserControl

        Private panelHeader As Panel
        Private lblTitulo As Label
        Public Sublbl As Label

        ' Tabs
        Private tabs As TabControl
        Private tabNative As TabPage
        Private tabUdo As TabPage

        ' Native tab controls
        Private grpNative As GroupBox
        Private tvNative As TreeView
        Private lblNativeHint As Label

        ' UDO tab controls
        Private grpUDO As GroupBox
        Private tvUDO As TreeView
        Private lblUDOHint As Label

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub InitializeComponent()
            SuspendLayout()
            BackColor = Color.White
            Dock = DockStyle.Fill

            ' ===== Header =====
            panelHeader = New Panel() With {
        .Dock = DockStyle.Top, .Height = 70,
        .Padding = New Padding(20, 20, 20, 10), .BackColor = Color.White
      }

            lblTitulo = New Label() With {
        .Text = "Paso 3: Seleccionar Tabla",
        .Font = New Font("Calibri", 15, FontStyle.Bold),
        .AutoSize = True, .Location = New Point(20, 8)
      }
            panelHeader.Controls.Add(lblTitulo)

            Sublbl = New Label() With {
        .Font = New Font("Calibri", 11, FontStyle.Regular),
        .AutoSize = True, .Location = New Point(20, 36)
      }
            panelHeader.Controls.Add(Sublbl)

            ' ===== Tabs =====
            tabs = New TabControl() With {.Dock = DockStyle.Fill, .Padding = New Point(12, 6)}
            tabNative = New TabPage("Nativas")
            tabUdo = New TabPage("UDO")

            ' --- Native tab content
            grpNative = New GroupBox() With {.Text = "Tablas nativas", .Dock = DockStyle.Fill, .Padding = New Padding(10)}
            tvNative = New TreeView() With {.Dock = DockStyle.Fill, .HideSelection = False}
            lblNativeHint = New Label() With {
        .Dock = DockStyle.Top, .Height = 20, .Text = "Selecciona una tabla nativa...",
        .ForeColor = Color.DimGray
      }
            grpNative.Controls.Add(tvNative)
            grpNative.Controls.Add(lblNativeHint)
            tabNative.Controls.Add(grpNative)

            ' --- UDO tab content
            grpUDO = New GroupBox() With {.Text = "Tablas de usuario (UDO)", .Dock = DockStyle.Fill, .Padding = New Padding(10)}
            tvUDO = New TreeView() With {.Dock = DockStyle.Fill, .HideSelection = False}
            lblUDOHint = New Label() With {
        .Dock = DockStyle.Top, .Height = 20, .Text = "Selecciona una tabla UDO...",
        .ForeColor = Color.DimGray
      }
            grpUDO.Controls.Add(tvUDO)
            grpUDO.Controls.Add(lblUDOHint)
            tabUdo.Controls.Add(grpUDO)

            tabs.TabPages.Add(tabNative)
            tabs.TabPages.Add(tabUdo)

            Controls.Add(tabs)
            Controls.Add(panelHeader)

            AddHandler tvNative.AfterSelect, AddressOf TvNative_AfterSelect
            AddHandler tvUDO.AfterSelect, AddressOf TvUDO_AfterSelect

            UpdateHeader()
            PopulateTrees()
            ApplyEnableState()
            ResumeLayout(False)
        End Sub

        ' ===== Helpers =====
        Private Sub UpdateHeader()
            Dim n As String = If(SubMain.SelectedOptionNative, "")
            Dim u As String = If(SubMain.SelectedOptionUDO, "")
            Dim act As String = If(SubMain.SelectedTypeObject2, "")
            Sublbl.Text = "Paso 3: Seleccionar tabla (" & n & "/" & u & ") que se va " & act
        End Sub

        Private Sub PopulateTrees()
            ' Nativas
            MappingNative.Populate(tvNative, If(SubMain.SelectedOptionNative, ""))

            ' UDOs
            MappingUDOs.Populate(tvUDO, If(SubMain.SelectedOptionUDO, ""))
        End Sub

        Private Sub ApplyEnableState()
            Dim hasNative As Boolean = Not String.IsNullOrWhiteSpace(If(SubMain.SelectedOptionNative, ""))
            Dim hasUDO As Boolean = Not String.IsNullOrWhiteSpace(If(SubMain.SelectedOptionUDO, ""))

            tvNative.Enabled = hasNative
            lblNativeHint.Visible = hasNative
            tabNative.Text = If(hasNative, "Nativas", "Nativas (no elegido en Paso 1)")

            tvUDO.Enabled = hasUDO
            lblUDOHint.Visible = hasUDO
            tabUdo.Text = If(hasUDO, "UDO", "UDO (no elegido en Paso 1)")

            ' Selección inicial de pestaña
            If hasNative AndAlso Not hasUDO Then
                tabs.SelectedTab = tabNative
            ElseIf hasUDO AndAlso Not hasNative Then
                tabs.SelectedTab = tabUdo
            ElseIf hasUDO AndAlso hasNative Then
                If tabs.SelectedTab Is Nothing Then tabs.SelectedTab = tabNative
            End If
        End Sub

        ' ===== Handlers =====
        Private Sub TvNative_AfterSelect(sender As Object, e As TreeViewEventArgs)
            If e.Node IsNot Nothing AndAlso e.Node.Parent IsNot Nothing Then
                Dim code = MappingNative.GetSelectedTableCode(e.Node)
                ' Ej.: guarda ambos, visible y código real
                SubMain.SelectedNativeTable = e.Node.Text
                SubMain.SelectedNativeTableCode = code
            End If
        End Sub

        Private Sub TvUDO_AfterSelect(sender As Object, e As TreeViewEventArgs)
            If e.Node IsNot Nothing AndAlso e.Node.Parent IsNot Nothing Then
                Dim code = MappingUDOs.GetSelectedTableCode(e.Node)
                SubMain.SelectedUDOTable = e.Node.Text
                SubMain.SelectedUDOTableCode = code
            End If
        End Sub

        Protected Overrides Sub OnVisibleChanged(e As EventArgs)
            MyBase.OnVisibleChanged(e)
            If Visible Then
                UpdateHeader()
                PopulateTrees()
                ApplyEnableState()
            End If
        End Sub

        ' Código de tabla seleccionada (nativa)
        Public ReadOnly Property SelectedNativeTableCode As String
            Get
                Dim node = If(tvNative?.SelectedNode, Nothing)
                If node Is Nothing OrElse node.Parent Is Nothing Then Return ""
                Dim map = TryCast(node.Tag, MappingNative.TableMap)
                Return If(map?.Table, "")
            End Get
        End Property

        ' Código de tabla seleccionada (UDO)
        Public ReadOnly Property SelectedUDOTableCode As String
            Get
                Dim node = If(tvUDO?.SelectedNode, Nothing)
                If node Is Nothing OrElse node.Parent Is Nothing Then Return ""
                Dim map = TryCast(node.Tag, MappingUDOs.TableMap)
                Return If(map?.Table, "")
            End Get
        End Property

        ' Valida que haya selección según la pestaña visible
        Public Function HasSelection(ByRef errorMsg As String) As Boolean
            Dim onNative As Boolean = (tabs?.SelectedTab Is tabNative)
            Dim onUDO As Boolean = (tabs?.SelectedTab Is tabUdo)

            If onNative Then
                If String.IsNullOrWhiteSpace(SelectedNativeTableCode) Then
                    errorMsg = "Selecciona una tabla nativa antes de continuar."
                    Return False
                End If
            ElseIf onUDO Then
                If String.IsNullOrWhiteSpace(SelectedUDOTableCode) Then
                    errorMsg = "Selecciona una tabla UDO antes de continuar."
                    Return False
                End If
            Else
                ' Fallback si por alguna razón no hay pestaña activa
                If String.IsNullOrWhiteSpace(SelectedNativeTableCode) AndAlso
                   String.IsNullOrWhiteSpace(SelectedUDOTableCode) Then
                    errorMsg = "Selecciona una tabla antes de continuar."
                    Return False
                End If
            End If

            Return True
        End Function

    End Class
End Namespace
