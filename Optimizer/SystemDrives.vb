Imports System.IO

Public Class SystemDrives
    Public Property dl As String = ""
    Dim allDrives() As DriveInfo
    Dim arr As ArrayList
    Private Sub Ok_Click(sender As Object, e As EventArgs) Handles Ok.Click
        Dim i As Integer = drives.SelectedIndex
        If i < 0 Then
            Return
        End If
        dl = arr.Item(i)
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub drives_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drives.SelectedIndexChanged
        Dim i As Integer = drives.SelectedIndex
        If i < 0 Then
            Return
        End If
        For j As Integer = 0 To (drives.Items.Count - 1)
            drives.SetItemCheckState(j, CheckState.Unchecked)
        Next
        drives.SetItemCheckState(i, CheckState.Checked)
    End Sub

    Private Sub SystemDrives_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        allDrives = DriveInfo.GetDrives()
        arr = New ArrayList
        Dim d As DriveInfo
        For Each d In allDrives
            If d.IsReady = True Then
                drives.Items.Add(d.Name, False)
                arr.Add(d.Name)
            End If
        Next
    End Sub
End Class