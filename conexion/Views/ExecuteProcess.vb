Imports System.Windows.Forms
Imports System.Drawing
Imports conexion.ImportModels

Namespace Views
    Public Class ExecuteProcess
        Inherits UserControl

        ' ====== en Views.ExecuteProcess ======
        Private _cancel As Boolean

        Public Sub RunSimulation()
            RunPipeline(simulate:=True)
        End Sub

        Public Sub RunReal()
            RunPipeline(simulate:=False)
        End Sub

        Private Sub RunPipeline(simulate As Boolean)
            Try
                ' 1) Construir lista de importers (UDO primero, luego nativas)
                Dim importers As New List(Of IImporter)()

                ' UDO: pares Cabecera/Detalle a partir del diccionario
                ' Convención: para cada cabecera @TABLA existe su detalle emparejado en Upload_FilesUDO
                ' Si tu UploadFiles devuelve pares ya correctos, mapea aquí uno a uno (ejemplo simple):
                Dim udoPairs As New List(Of Tuple(Of String, String, String, String))
                ' Construye pares por nombre: asume que si hay @SS_REEMCAB debe existir @SS_REEMDET
                ' (Si ya tienes la estructura de pares, úsala directamente)
                For Each kv In SubMain.Upload_FilesUDO
                    Dim tbl = kv.Key
                    If tbl.StartsWith("@") AndAlso tbl.EndsWith("CAB", StringComparison.OrdinalIgnoreCase) Then
                        Dim cab = tbl
                        Dim det = tbl.Substring(0, tbl.Length - 3) & "DET"
                        Dim cabPath = kv.Value
                        Dim detPath As String = Nothing
                        If SubMain.Upload_FilesUDO.TryGetValue(det, detPath) Then
                            importers.Add(New UdoImporter(SubMain.oCompany, cab, det, cabPath, detPath, SubMain.Upload_SelectedDelimiter))
                        End If
                    End If
                Next

                ' Nativas: cada tabla es independiente
                For Each kv In SubMain.Upload_FilesNative
                    importers.Add(New NativeTableImporter(SubMain.oCompany, kv.Key, kv.Value, SubMain.Upload_SelectedDelimiter))
                Next

                ' Si no hay nada que procesar:
                If importers.Count = 0 Then
                    MessageBox.Show("No hay archivos seleccionados para procesar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                ' 2) Total de filas
                Dim total As Integer = importers.Sum(Function(im) im.CountRows())

                ' 3) Mostrar progreso
                Dim pf As New ProgressForm()
                pf.Initialize(total, isSimulation:=simulate)
                AddHandler pf.CancelRequested, Sub() _cancel = True
                pf.Show()

                ' 4) Procesar fila-a-fila, acumulando resultados
                Dim results As New List(Of RowResult)()
                Dim processed As Integer = 0, ok As Integer = 0, fail As Integer = 0

                _cancel = False
                For Each im In importers
                    If _cancel Then Exit For
                    Dim tableName As String = Nothing
                    If TypeOf im Is UdoImporter Then
                        ' Header table como nombre de tabla
                        Dim u = DirectCast(im, UdoImporter)
                        tableName = "UDO"
                    Else
                        Dim n = DirectCast(im, NativeTableImporter)
                        tableName = "Nativa"
                    End If

                    Dim idxOffset = processed
                    im.Process(simulate,
                        Sub(localIdx As Integer, key As String, act As RowAction, okRow As Boolean, msg As String)
                            processed += 1
                            Dim rr As New RowResult With {
                                .Index = processed,
                                .Key = key,
                                .Action = act,
                                .State = If(okRow, RowState.Success, RowState.Error),
                                .Message = msg,
                                .TableName = tableName
                            }
                            results.Add(rr)
                            If okRow Then ok += 1 Else fail += 1
                            pf.UpdateStatus(processed, total, ok, fail, key)
                            Application.DoEvents()
                            If _cancel Then Throw New OperationCanceledException()
                        End Sub)
                Next

                pf.Close()

                ' 5) Mostrar resultados
                Dim rf As New ResultsForm(results)
                rf.ShowDialog()

            Catch ex As OperationCanceledException
                ' Cancelado por el usuario: mostramos lo procesado hasta el momento
                MessageBox.Show("Proceso cancelado por el usuario.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error inesperado: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' log técnico
                SubMain.guardaLog.RegistrarLOG("ExecuteProcess", 3, ex.ToString())
            End Try
        End Sub

    End Class
End Namespace
