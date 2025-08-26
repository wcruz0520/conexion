Imports SAPbobsCOM
Imports Helpers.ConnectSAP
Module SubMain
    Public oCompany As SAPbobsCOM.Company
    'Public cnSAP As ConnectSAP
    Public guardaLog As Log
    Public NombreClase As String
    Public BaseForm As PrincipalForm
    Public SelectedOptionNative As String
    Public SelectedOptionUDO As String
    Public SelectedTypeObject2 As String

    <STAThread()>
    Sub Main()

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)

        'cnSAP = New ConnectSAP(oCompany)
        guardaLog = New Log
        NombreClase = "SubMain"
        BaseForm = New PrincipalForm
        BaseForm.Text = "SAP Business One Data Transfer WorkBench for UDO"

        'If cnSAP.conectSAP() Then

        '    If oCompany.Connected Then
        '        guardaLog.RegistrarLOG(NombreClase, 1, String.Format("Conexion SAP exitosa, CompanyName={0} ,DataBase={1}  ", oCompany.CompanyName, oCompany.CompanyDB))
        '        'BaseForm.Text = String.Format("Conectado a: {1}", oCompany.CompanyName, oCompany.CompanyDB)
        '        'BaseForm.Show()
        '    End If
        'Else
        '    guardaLog.RegistrarLOG(NombreClase, 3, "Error en la Conexion SAP , Validar el Archivo de Configuracion e Iniciar nuevamente el Servicio ")
        'End If

        Application.Run(BaseForm)

    End Sub

End Module
