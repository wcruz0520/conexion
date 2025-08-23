Public Class Login
    Public cnSAP As ConnectSAP
    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

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
    End Sub

    Private Sub btnConectar_Click(sender As Object, e As EventArgs) Handles btnConectar.Click
        cnSAP = New ConnectSAP(SubMain.oCompany)
        If cnSAP.conectSAP() Then
            If SubMain.oCompany.Connected Then
                guardaLog.RegistrarLOG(NombreClase, 1, String.Format("Conexion SAP exitosa, CompanyName={0} ,DataBase={1}  ", oCompany.CompanyName, oCompany.CompanyDB))
            End If
        End If
    End Sub
End Class