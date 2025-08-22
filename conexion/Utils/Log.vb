Imports System.IO
Imports System.Text

Public Class Log

    Public tiposLog As Dictionary(Of Integer, String)
    Public tipo As Integer = 0
    Public msg As String = ""
    Public Ruta As String
    Public Ruta_NombArch As String

    Public Sub New()
        tiposLog = New Dictionary(Of Integer, String)
        tiposLog.Add(1, "Exito")
        tiposLog.Add(2, "Informativo")
        tiposLog.Add(3, "Error")
    End Sub

    Public Sub RegistrarLOG(NombreArchLog As String, _tipoLog As Integer, _msg As String)
        Ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LOG")
        Ruta_NombArch = Path.Combine(Ruta, $"{NombreArchLog}.txt")

        Dim sTexto As New StringBuilder

        sTexto.AppendLine("FECHA: " & Now)
        sTexto.AppendLine("----------------------------------------------------------")
        sTexto.AppendLine($"{tiposLog(_tipoLog).ToString()}: {_msg.ToString()}")

        Try
            Dim oTextWriter As TextWriter = New StreamWriter(Ruta_NombArch, True)
            oTextWriter.WriteLine(sTexto.ToString)
            oTextWriter.Flush()
            oTextWriter.Close()
            oTextWriter = Nothing


        Catch ex As Exception
            ' EventLog.WriteEntry("MyWindowsService", "Error: " & ex.Message.ToString)
            Console.WriteLine($"Error: {ex.Message.ToString}")
        Finally

        End Try
    End Sub



End Class
