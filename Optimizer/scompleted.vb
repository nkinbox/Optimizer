Imports System.Threading
Public Class scompleted
    Public Property str As String
    Private Sub completed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        Me.label.Text = str
    End Sub

    Private Sub completed_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Dim i As Integer
        For i = 0 To 99
            Me.Opacity = i / 100
            Me.Refresh()
            Thread.Sleep(10)
        Next
        Thread.Sleep(1000)
        For i = 99 To 0 Step -1
            Me.Opacity = i / 100
            Me.Refresh()
            Thread.Sleep(10)
        Next
        Me.Close()
    End Sub
End Class