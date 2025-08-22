Imports System.Windows.Forms

Public Class PrincipalForm
    Private vistas As List(Of UserControl)
    Private indiceActual As Integer

    Private Sub PrincipalForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        vistas = New List(Of UserControl) From {
            New Views.SelectTypeObject(),
            New Views.SelectTable(),
            New Views.ExecuteProcess()
        }
        indiceActual = 0
        MostrarVistaActual()
    End Sub

    Private Sub btnAtras_Click(sender As Object, e As EventArgs) Handles btnAtras.Click
        If indiceActual > 0 Then
            indiceActual -= 1
            MostrarVistaActual()
        End If
    End Sub

    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        If indiceActual < vistas.Count - 1 Then
            indiceActual += 1
            MostrarVistaActual()
        End If
    End Sub

    Private Sub MostrarVistaActual()
        panelPrincipal.Controls.Clear()
        Dim vista As UserControl = vistas(indiceActual)
        vista.Dock = DockStyle.Fill
        panelPrincipal.Controls.Add(vista)
        btnAtras.Enabled = indiceActual > 0
        btnSiguiente.Enabled = indiceActual < vistas.Count - 1
    End Sub
End Class
