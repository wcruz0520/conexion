Imports System.Configuration

Public Class Login
    Public cnSAP As ConnectSAP
    Public FontSt As Font
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FontSt = New Font("Calibri", 10, FontStyle.Regular)
        Me.Text = "Iniciar Sesión"

        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 70
        pnlHeader.Padding = New Padding(20, 20, 20, 10)
        pnlHeader.BackColor = Color.White 'Color.FromArgb(46, 254, 132)

        Dim lbl As New Label()
        lbl.Text = "Login"
        lbl.Font = New Font("Calibri", 15, FontStyle.Bold)
        lbl.AutoSize = True
        lbl.Location = New Point(20, 10)
        pnlHeader.Controls.Add(lbl)

        Dim Sublbl As New Label()
        Sublbl.Text = "Iniciar sesión con usuario SAP con licencia profesional"
        Sublbl.Font = New Font("Calibri", 11, FontStyle.Regular)
        Sublbl.AutoSize = True
        Sublbl.Location = New Point(20, 35)
        pnlHeader.Controls.Add(Sublbl)

        cmbCompania.Font = FontSt
        txtSAPUser.Font = FontSt
        txtSAPPw.Font = FontSt

        CargarCompanias()
    End Sub

    Private Sub btnConectar_Click(sender As Object, e As EventArgs) Handles btnConectar.Click
        ToggleInputs(False)
        btnConectar.Enabled = False
        btnConectar.Text = "Conectando..."
        Me.UseWaitCursor = True

        cnSAP = New ConnectSAP(SubMain.oCompany)
        If cnSAP.conectSAP(Me.cmbCompania.Text, Me.txtSAPUser.Text, Me.txtSAPPw.Text) Then
            If SubMain.oCompany.Connected Then
                guardaLog.RegistrarLOG(NombreClase, 1, String.Format("Conexion SAP exitosa, CompanyName={0} ,DataBase={1}  ", oCompany.CompanyName, oCompany.CompanyDB))
                SubMain.BaseForm.UpdateConnectionStatus()
                Me.Close()
            End If
        Else
            MessageBox.Show("La conexión no fue posible, revisar las credenciales", "Conexión SAP", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.UseWaitCursor = False
            btnConectar.Text = "Conectar"
            btnConectar.Enabled = True
            ToggleInputs(True)
        End If
    End Sub

    Private Sub ToggleInputs(enabled As Boolean)
        txtSAPUser.Enabled = enabled
        txtSAPPw.Enabled = enabled
        cmbCompania.Enabled = enabled
    End Sub

    Private Sub CargarCompanias()
        Try
            Dim tmpCompany As New SAPbobsCOM.Company()
            tmpCompany.Server = ConfigurationManager.AppSettings("DevServer")
            tmpCompany.LicenseServer = ConfigurationManager.AppSettings("LicenseServer")
            tmpCompany.DbServerType = CType(CInt(ConfigurationManager.AppSettings("DevServerType")), SAPbobsCOM.BoDataServerTypes)
            tmpCompany.UseTrusted = Boolean.Parse(ConfigurationManager.AppSettings("UseTrusted"))
            If tmpCompany.UseTrusted = False Then
                tmpCompany.DbUserName = ConfigurationManager.AppSettings("DevDBUser")
                tmpCompany.DbPassword = ConfigurationManager.AppSettings("DevDBPassword")
            End If

            Dim rs As SAPbobsCOM.Recordset = tmpCompany.GetCompanyList()
            cmbCompania.Items.Clear()
            While Not rs.EoF
                cmbCompania.Items.Add(rs.Fields.Item(0).Value.ToString())
                rs.MoveNext()
            End While
            If cmbCompania.Items.Count > 0 Then
                cmbCompania.SelectedIndex = 0
            End If
        Catch ex As Exception
            'ignore load errors
        End Try
    End Sub
End Class