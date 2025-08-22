Imports System.Configuration

Public Class ConnectSAP
    Public _oCompany As SAPbobsCOM.Company
    Public guardaLog As Log
    Public NombreClase As String

    Public Sub New(ByRef oCompany As SAPbobsCOM.Company)
        Me._oCompany = oCompany
        guardaLog = New Log
        NombreClase = "ConnectSAP"
    End Sub

    Public Function conectSAP() As Boolean
        Try
            Dim ErrCode As Long
            Dim ErrMsg As String = ""
            'GuardaLog(String.Format("Conexion SAP"))
            guardaLog.RegistrarLOG(NombreClase, 2, "Conexion SAP")

            oCompany = New SAPbobsCOM.Company()

            'oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English

            oCompany.DbServerType = ConfigurationManager.AppSettings("DevServerType")
            oCompany.UseTrusted = ConfigurationManager.AppSettings("UseTrusted")
            oCompany.CompanyDB = ConfigurationManager.AppSettings("DevDatabase")
            oCompany.UserName = ConfigurationManager.AppSettings("DevSBOUser")
            oCompany.Password = ConfigurationManager.AppSettings("DevSBOPassword")

            Try
                If CInt(ConfigurationManager.AppSettings("SAP_VERSION")) < 10 Then
                    oCompany.Server = ConfigurationManager.AppSettings("DevServer")
                    oCompany.LicenseServer = ConfigurationManager.AppSettings("LicenseServer")
                    oCompany.DbUserName = ConfigurationManager.AppSettings("DevDBUser")
                    oCompany.DbPassword = ConfigurationManager.AppSettings("DevDBPassword")
                Else
                    oCompany.Server = ConfigurationManager.AppSettings("DevServer")
                    'oCompany.SLDServer = ConfigurationManager.AppSettings("LicenseServer")
                End If
            Catch ex As Exception
                'GuardaLog(ex.Message)
                guardaLog.RegistrarLOG(NombreClase, 3, ex.Message)
            End Try

            If oCompany.Connected Then
                'GuardaLog("Company en estado Conectado a SAP BO " + oCompany.CompanyDB.ToString())
                guardaLog.RegistrarLOG(NombreClase, 1, "Company en estado Conectado a SAP BO " + oCompany.CompanyDB.ToString())
                Return True
            End If

            ErrCode = oCompany.Connect()

            If ErrCode <> 0 Then
                oCompany.GetLastError(ErrCode, ErrMsg)
                'GuardaLog("Error al conectarse a SAP ,funcion : oCompany.Connect :" + ErrCode.ToString() + " - " + ErrMsg.ToString)
                guardaLog.RegistrarLOG(NombreClase, 3, "Error al conectarse a SAP ,funcion : oCompany.Connect :" + ErrCode.ToString() + " - " + ErrMsg.ToString)
                Return False
            Else
                ' GuardaLog("Conectado a SAP BO" + oCompany.CompanyDB.ToString())
                guardaLog.RegistrarLOG(NombreClase, 1, "Conectado a SAP BO" + oCompany.CompanyDB.ToString())
                Return True
            End If

        Catch ex As Exception
            'GuardaLog("Error al conectarse a SAP , funcion: conectSAP , EX :" + ex.Message.ToString())
            guardaLog.RegistrarLOG(NombreClase, 3, "Error al conectarse a SAP , funcion: conectSAP , EX :" + ex.Message.ToString())
            Return False
        End Try

    End Function

End Class
