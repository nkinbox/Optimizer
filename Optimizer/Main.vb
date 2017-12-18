Imports System.Threading
Imports System.Diagnostics
Imports NetFwTypeLib
Imports System.Drawing.Drawing2D
Imports System.Security.Principal
Imports System.IO

Public Class Main
    Private ll As Boolean = False
    Private pp As Boolean = False
    Private qq As Boolean = False
    Private per As Integer = 0
    Private pn As Point
    Private usageThread As Thread
    Private cont As Boolean = True
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        drawGUI()
    End Sub
    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If Not checkvalidity() Then
            MessageBox.Show("This Software's trial period has expired.", "Trial Version", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Dim key As String
            key = InputBox("Enter Registration Key", "Register U Optimizer", "")
            If key = "" Then
                Me.Close()
                Exit Sub
            Else
                Try
                    key = "http://kismetsoftwaresolutions.com/reguoptimizer.php?key=" & key
                    key = New System.Net.WebClient().DownloadString(key)
                    If key.Equals("approved") Then
                        Dim wrapper As New Simple3Des
                        regcom(remtime)
                        regcom(register)
                        key = wrapper.EncryptData("34742")
                        My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\SHAD1", "Threshold", key)
                        MessageBox.Show("Registration Key Successfully applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Registration Key Failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Close()
                        Exit Sub
                    End If
                Catch ex As Exception
                    MessageBox.Show("Registration Key Failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                    Exit Sub
                End Try
            End If
        End If
        Dim myPrincipal As WindowsPrincipal = New WindowsPrincipal(WindowsIdentity.GetCurrent())
        If (myPrincipal.IsInRole(WindowsBuiltInRole.Administrator) = False) Then
            MessageBox.Show("You need to run the application using the ""run as administrator"" option", "administrator right required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If
        usageThread = New Thread(AddressOf usage)
        usageThread.Start()
        Dim t As New Thread(AddressOf firewallcheck)
        t.Start(False)
    End Sub
    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        cont = False
    End Sub
    Private Sub drawGUI()
        namelb.Text = My.Computer.Name
        Dim osn As String = My.Computer.Info.OSFullName
        If osn.Contains("10") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("_10")
        ElseIf osn.Contains("8.1") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("_8_1")
        ElseIf osn.Contains("8") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("_8")
        ElseIf osn.Contains("7") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("_7")
        ElseIf osn.Contains("xp") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("xp")
        ElseIf osn.Contains("XP") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("xp")
        ElseIf osn.Contains("vista") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("vista")
        ElseIf osn.Contains("VISTA") Then
            OSname.Image = My.Resources.ResourceManager.GetObject("vista")
        End If

        minimizebtn.BackColor = Color.FromArgb(52, 119, 206)
        closebtn.BackColor = Color.FromArgb(52, 119, 206)
        PaneTop_1.BackColor = Color.FromArgb(14, 29, 53)
        PaneTop_2.BackColor = Color.FromArgb(14, 29, 53)
        Cleaner.BackColor = Color.FromArgb(66, 133, 244)
        Scanner.BackColor = Color.FromArgb(49, 98, 180)
        DiskChecker.BackColor = Color.FromArgb(32, 64, 116)
        Recovery.BackColor = Color.FromArgb(59, 119, 218)

        Cleaner_2.BackColor = Color.FromArgb(206, 145, 31)
        Scanner_2.BackColor = Color.FromArgb(244, 172, 37)
        DiskChecker_2.BackColor = Color.FromArgb(231, 163, 35)
        Recovery_2.BackColor = Color.FromArgb(167, 118, 25)

        Cleaner_2.Visible = False
        Scanner_2.Visible = False
        DiskChecker_2.Visible = False
        Recovery_2.Visible = False

        Cleaner_3.Visible = False
        Scanner_3.Visible = False
        DiskChecker_3.Visible = False
        Recovery_3.Visible = False

        Info.BackColor = Color.FromArgb(0, 77, 147)
        Softwares.BackColor = Color.FromArgb(0, 117, 224)
        Processes.BackColor = Color.FromArgb(0, 110, 211)
        Services.BackColor = Color.FromArgb(0, 44, 83)

        Info_2.BackColor = Color.FromArgb(224, 140, 22)
        Softwares_2.BackColor = Color.FromArgb(161, 100, 16)
        Processes_2.BackColor = Color.FromArgb(199, 124, 20)
        Services_2.BackColor = Color.FromArgb(237, 148, 24)

        Info_2.Visible = False
        Softwares_2.Visible = False
        Processes_2.Visible = False
        Services_2.Visible = False

        statusLeft.Visible = False
        statusRight.Visible = False

    End Sub

#Region "Hover Effects"

    Private Sub Cleaner_MouseEnter(sender As Object, e As EventArgs) Handles CleanerLogo.Click
        toggler(1)
    End Sub

    Private Sub Cleaner_MouseLeave(sender As Object, e As EventArgs) Handles Cleaner_2.Click
        toggler(0)
    End Sub

    Private Sub Scanner_MouseEnter(sender As Object, e As EventArgs) Handles ScannerLogo.Click
        toggler(2)
    End Sub

    Private Sub Scanner_MouseLeave(sender As Object, e As EventArgs) Handles Scanner_2.Click
        toggler(0)
    End Sub

    Private Sub Diskchecker_MouseEnter(sender As Object, e As EventArgs) Handles DiskCheckLogo.Click
        toggler(3)
    End Sub

    Private Sub DiskChecker_MouseLeave(sender As Object, e As EventArgs) Handles DiskChecker_2.Click
        toggler(0)
    End Sub

    Private Sub Recovery_MouseEnter(sender As Object, e As EventArgs) Handles RecoveryLogo.Click
        toggler(4)
    End Sub

    Private Sub Recovery_MouseLeave(sender As Object, e As EventArgs) Handles Recovery_2.Click
        toggler(0)
    End Sub

    Private Sub SoftwareLogo_Click(sender As Object, e As EventArgs) Handles SoftwareLogo.Click
        toggler(5)
    End Sub

    Private Sub ServicesLogo_Click(sender As Object, e As EventArgs) Handles ServicesLogo.Click
        toggler(6)
    End Sub

    Private Sub ProcessesLogo_Click(sender As Object, e As EventArgs) Handles ProcessesLogo.Click
        toggler(7)
    End Sub

    Private Sub InfoLogo_Click(sender As Object, e As EventArgs) Handles InfoLogo.Click
        toggler(8)
    End Sub

    Private Sub Softwares_2_Paint(sender As Object, e As EventArgs) Handles Softwares_2.Click
        toggler(0)
    End Sub

    Private Sub Services_2_Paint(sender As Object, e As EventArgs) Handles Services_2.Click
        toggler(0)
    End Sub

    Private Sub Processes_2_Paint(sender As Object, e As EventArgs) Handles Processes_2.Click
        toggler(0)
    End Sub

    Private Sub Info_2_Paint(sender As Object, e As EventArgs) Handles Info_2.Click
        toggler(0)
    End Sub
    Private Sub toggler(i As Integer)
        ' MsgBox(qq)
        If qq Then
            Return
        End If
        Select Case i
            Case 1
                pp = False
                Cleaner.Visible = False
                'Cleaner.Invalidate()
                Cleaner_2.Visible = True
                Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 2
                pp = False
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = False
                'Scanner.Invalidate()
                Scanner_2.Visible = True
                Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 3
                pp = False
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = False
                'DiskChecker.Invalidate()
                DiskChecker_2.Visible = True
                DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 4
                pp = False
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = False
                'Recovery.Invalidate()
                Recovery_2.Visible = True
                Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 5
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = False
                'Softwares.Invalidate()
                Softwares_2.Visible = True
                Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 6
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = False
                'Services.Invalidate()
                Services_2.Visible = True
                Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 7
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = False
                'Processes.Invalidate()
                Processes_2.Visible = True
                Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 8
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = False
                Info.Invalidate()
                Info_2.Visible = True
                Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
            Case 9
                Cleaner.Visible = False
                'Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = True
                Cleaner_3.Invalidate()
                Scanner_3.Visible = False
                'Scanner_3.Invalidate()
                DiskChecker_3.Visible = False
                'DiskChecker_3.Invalidate()
                Recovery_3.Visible = False
                'Recovery_3.Invalidate()
            Case 10
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = False
                'Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                'Cleaner_3.Invalidate()
                Scanner_3.Visible = True
                Scanner_3.Invalidate()
                DiskChecker_3.Visible = False
                'DiskChecker_3.Invalidate()
                Recovery_3.Visible = False
                'Recovery_3.Invalidate()
            Case 11
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = False
                'DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                'Cleaner_3.Invalidate()
                Scanner_3.Visible = False
                'Scanner_3.Invalidate()
                DiskChecker_3.Visible = True
                DiskChecker_3.Invalidate()
                Recovery_3.Visible = False
                'Recovery_3.Invalidate()
            Case 12
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = False
                'Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                'Cleaner_3.Invalidate()
                Scanner_3.Visible = False
                'Scanner_3.Invalidate()
                DiskChecker_3.Visible = False
                'DiskChecker_3.Invalidate()
                Recovery_3.Visible = True
                Recovery_3.Invalidate()
            Case Else
                Cleaner.Visible = True
                Cleaner.Invalidate()
                Cleaner_2.Visible = False
                'Cleaner_2.Invalidate()
                Scanner.Visible = True
                Scanner.Invalidate()
                Scanner_2.Visible = False
                'Scanner_2.Invalidate()
                DiskChecker.Visible = True
                DiskChecker.Invalidate()
                DiskChecker_2.Visible = False
                'DiskChecker_2.Invalidate()
                Recovery.Visible = True
                Recovery.Invalidate()
                Recovery_2.Visible = False
                'Recovery_2.Invalidate()
                Info.Visible = True
                Info.Invalidate()
                Info_2.Visible = False
                'Info_2.Invalidate()
                Softwares.Visible = True
                Softwares.Invalidate()
                Softwares_2.Visible = False
                'Softwares_2.Invalidate()
                Processes.Visible = True
                Processes.Invalidate()
                Processes_2.Visible = False
                'Processes_2.Invalidate()
                Services.Visible = True
                Services.Invalidate()
                Services_2.Visible = False
                'Services_2.Invalidate()
                Cleaner_3.Visible = False
                Scanner_3.Visible = False
                DiskChecker_3.Visible = False
                Recovery_3.Visible = False
        End Select
    End Sub
#End Region
#Region "Form's State"
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Private Sub PaneTop_1_MouseDown(sender As Object, e As MouseEventArgs) Handles PaneTop_1.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PaneTop_1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PaneTop_1.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub PaneTop_1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PaneTop_1.MouseUp
        drag = False
    End Sub
    Private Sub PaneTop_2_MouseDown(sender As Object, e As MouseEventArgs) Handles PaneTop_2.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub PaneTop_2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PaneTop_2.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub
    Private Sub PaneTop_2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PaneTop_2.MouseUp
        drag = False
    End Sub
    Private Sub minimize_Click(sender As Object, e As EventArgs) Handles minimizebtn.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub minimize_MouseEnter(sender As Object, e As EventArgs) Handles minimizebtn.MouseEnter
        minimizebtn.BackColor = Color.FromArgb(36, 82, 142)
    End Sub

    Private Sub minimize_MouseLeave(sender As Object, e As EventArgs) Handles minimizebtn.MouseLeave
        minimizebtn.BackColor = Color.FromArgb(52, 119, 206)
    End Sub

    Private Sub close_Click(sender As Object, e As EventArgs) Handles closebtn.Click
        Me.Close()
    End Sub

    Private Sub close_MouseEnter(sender As Object, e As EventArgs) Handles closebtn.MouseEnter
        closebtn.BackColor = Color.FromArgb(36, 82, 142)
    End Sub

    Private Sub close_MouseLeave(sender As Object, e As EventArgs) Handles closebtn.MouseLeave
        closebtn.BackColor = Color.FromArgb(52, 119, 206)
    End Sub
#End Region
#Region "Loading"
    Private Sub showLoading(lpanel As LoadingPanel)
        Try
            If (ll) Then
                Return
            End If
            pn = lpanel.PointToScreen(New Point(0, 0))
            Dim temp As Bitmap
            Dim tempG As Graphics
            temp = New Bitmap(lpanel.ClientRectangle.Width, lpanel.ClientRectangle.Height, lpanel.CreateGraphics())
            tempG = Graphics.FromImage(temp)
            tempG.CopyFromScreen(pn.X, pn.Y, 0, 0, lpanel.Size)
            lpanel.BackgroundImage = temp
            lpanel.Visible = True
            Dim t As New Thread(AddressOf loadingThread)
            t.Start(lpanel)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub loadingThread(lpanel As LoadingPanel)
        Try
            If (ll) Then
                Return
            End If
            ll = True
            Dim li As Integer = lpanel.ClientRectangle.Width
            Dim bit, temp As Bitmap
            Dim g, tempG As Graphics
            temp = New Bitmap(lpanel.ClientRectangle.Width, lpanel.ClientRectangle.Height, lpanel.CreateGraphics())
            tempG = Graphics.FromImage(temp)
            tempG.CopyFromScreen(pn.X, pn.Y, 0, 0, lpanel.Size)
            While li > 0
                bit = New Bitmap(lpanel.ClientRectangle.Width, lpanel.ClientRectangle.Height)
                g = Graphics.FromImage(bit)
                g.DrawImage(temp, 0, 0)
                g.FillRectangle(New SolidBrush(Color.FromArgb(255, 255, 255)), lpanel.ClientRectangle.Width - 2 * li, 0, 1, lpanel.ClientRectangle.Height)
                g.FillRectangle(New SolidBrush(Color.FromArgb(255, 255, 255)), 0, lpanel.ClientRectangle.Width - 2 * li, lpanel.ClientRectangle.Width, 1)
                li = li - 2
                loading(lpanel, bit, False)
                Thread.Sleep(10)
            End While
            bit = New Bitmap(lpanel.ClientRectangle.Width, lpanel.ClientRectangle.Height)
            loading(lpanel, bit, True)
            ll = False
        Catch ex As Exception
        End Try
    End Sub
    Private Delegate Sub LoadingDel(ByVal lpanel As LoadingPanel, ByVal val As Bitmap, ByVal bool As Boolean)
    Private Sub loading(ByVal lpanel As LoadingPanel, ByVal val As Bitmap, ByVal bool As Boolean)
        Try
            If lpanel.InvokeRequired Then
                lpanel.Invoke(New LoadingDel(AddressOf loading), New Object() {lpanel, val, bool})
            Else
                If (bool) Then
                    lpanel.Visible = False
                Else
                    lpanel.BackgroundImage = val
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub status(lpanel As LoadingPanel, perc As Integer)
        per = perc
        Dim t As New Thread(AddressOf statusThread)
        t.Start(lpanel)
    End Sub
    Private Sub statusThread(lpanel As LoadingPanel)
        Try
            Dim li As Integer = lpanel.ClientRectangle.Width
            li = CInt(per * li / 100)
            Dim bit As Bitmap
            Dim g As Graphics
            bit = New Bitmap(lpanel.ClientRectangle.Width, lpanel.ClientRectangle.Height)
            g = Graphics.FromImage(bit)
            g.FillRectangle(New SolidBrush(Color.FromArgb(167, 118, 25)), 0, 0, lpanel.ClientRectangle.Width, lpanel.ClientRectangle.Height)
            g.FillRectangle(New SolidBrush(Color.FromArgb(66, 133, 244)), 0, 0, li, lpanel.ClientRectangle.Height)
            loading2(lpanel, bit)
        Catch ex As Exception
        End Try
    End Sub
    Private Delegate Sub Loading2Del(ByVal lpanel As LoadingPanel, ByVal val As Bitmap)
    Private Sub loading2(ByVal lpanel As LoadingPanel, ByVal val As Bitmap)
        Try
            If lpanel.InvokeRequired Then
                lpanel.Invoke(New Loading2Del(AddressOf loading2), New Object() {lpanel, val})
            Else
                lpanel.BackgroundImage = val
            End If
        Catch ex As Exception
        End Try
    End Sub
#End Region
#Region "Right Panel Commands"
#Region "Updates"
    Private Delegate Sub cpudata(ByVal val As String)
    Private Sub cpuUpdate(ByVal val As String)
        Try
            If cpuUsage.InvokeRequired Then
                cpuUsage.Invoke(New cpudata(AddressOf cpuUpdate), New Object() {val})
            Else
                cpuUsage.Text = val
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Delegate Sub ramdata(ByVal val As String)
    Private Sub ramUpdate(ByVal val As String)
        Try
            If ramUsage.InvokeRequired Then
                ramUsage.Invoke(New ramdata(AddressOf ramUpdate), New Object() {val})
            Else
                ramUsage.Text = val
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Delegate Sub firewallDel(ByVal val As String)
    Private Sub firewallupdate(ByVal val As String)
        If firewall.InvokeRequired Then
            firewall.Invoke(New firewallDel(AddressOf firewallupdate), New Object() {val})
        Else
            If (val.Equals("on")) Then
                firewall.Text = "firewall On"
                firewall.BackColor = Color.FromArgb(47, 148, 0)
            Else
                firewall.Text = "firewall Off"
                firewall.BackColor = Color.FromArgb(255, 44, 0)
            End If
        End If
    End Sub
    Private Sub usage()
        Try
            Dim cpu As PerformanceCounter
            Dim ram As PerformanceCounter
            Dim total As ULong = My.Computer.Info.TotalPhysicalMemory
            cpu = New PerformanceCounter()
            ram = New PerformanceCounter("Memory", "Available Bytes")
            cpu.CounterName = "% Processor Time"
            cpu.CategoryName = "Processor"
            cpu.InstanceName = "_Total"
            While cont
                Try
                    cpuUpdate(CInt(cpu.NextValue()) & "%")
                    Thread.Sleep(800)
                    ramUpdate(100 - CInt(CULng(ram.NextValue()) * 100 / total) & "%")
                Catch ex As Exception
                End Try
            End While
        Catch ex As Exception
            Shell("cmd.exe /c taskmgr", AppWinStyle.NormalNoFocus)
            MessageBox.Show("Optimizer needs to be Restarted.", "Restart Optimizer", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub
    Private Sub firewallcheck(ByVal str As Boolean)
        'My.Settings.Reload()
        'If Not My.Settings.firewall Then
        '    'allowtfirewall()
        'End If
        'My.Settings.Save()
        Dim q As Boolean
        Dim NetFwMgrType As Type = Type.GetTypeFromProgID("HNetCfg.FwMgr", False)
        Dim mgr As INetFwMgr = DirectCast(Activator.CreateInstance(NetFwMgrType), INetFwMgr)
        Dim Firewallenabled As Boolean = mgr.LocalPolicy.CurrentProfile.FirewallEnabled
        If mgr.LocalPolicy.CurrentProfile.FirewallEnabled Then
            firewallupdate("on")
            q = False
        Else
            firewallupdate("off")
            q = True
        End If
        If str Then
            Dim Proc As Process = New Process
            Dim top As String = "netsh.exe"
            If q Then
                Proc.StartInfo.Arguments = ("advfirewall set allprofiles state on")
            Else
                Proc.StartInfo.Arguments = ("advfirewall set allprofiles state off")
            End If
            Proc.StartInfo.FileName = top
            Proc.StartInfo.UseShellExecute = False
            Proc.StartInfo.RedirectStandardOutput = True
            Proc.StartInfo.CreateNoWindow = True
            Proc.Start()
            Proc.WaitForExit()
            If mgr.LocalPolicy.CurrentProfile.FirewallEnabled Then
                firewallupdate("on")
            Else
                firewallupdate("off")
            End If
        End If
    End Sub
    Private Sub allowtfirewall()
        Try
            MsgBox(My.Settings.firewall)
            Dim Proc As Process = New Process
            Dim top As String = "netsh.exe"
            Proc.StartInfo.Arguments = ("advfirewall firewall add rule name=""U Optimizer"" dir=in action=allow program=""" & My.Computer.FileSystem.CurrentDirectory & "\Optimizer.exe"" enable=yes")
            Proc.StartInfo.FileName = top
            Proc.StartInfo.UseShellExecute = False
            Proc.StartInfo.RedirectStandardOutput = True
            Proc.StartInfo.CreateNoWindow = True
            Proc.Start()
            Proc.WaitForExit()
            My.Settings.firewall = True
            MsgBox(My.Settings.firewall)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub firewall_Click(sender As Object, e As EventArgs) Handles firewall.Click
        Dim t As New Thread(AddressOf firewallcheck)
        t.Start(True)
    End Sub
#End Region
    Private Sub softwaresbtn_Click(sender As Object, e As EventArgs) Handles softwaresbtn.Click
        showLoading(statusRight)
        Dim t As New Thread(AddressOf softwaresP)
        t.Start()
    End Sub

    Private Sub servicesbtn_Click(sender As Object, e As EventArgs) Handles servicesbtn.Click
        showLoading(statusRight)
        Dim t As New Thread(AddressOf servicesP)
        t.Start()
    End Sub

    Private Sub processesbtn_Click(sender As Object, e As EventArgs) Handles processesbtn.Click
        showLoading(statusRight)
        Dim t As Thread = New Thread(AddressOf processesP)
        t.Start()
    End Sub

    Private Sub infobtn_Click(sender As Object, e As EventArgs) Handles infobtn.Click
        showLoading(statusRight)
        Dim t As Thread = New Thread(AddressOf sysinfo)
        t.SetApartmentState(ApartmentState.STA)
        t.Start()
    End Sub
    Private Sub sysinfo()
        Thread.Sleep(1500)
        Dim path As String = My.Computer.FileSystem.CurrentDirectory
        Dim str As String = ""
        Shell("cmd.exe /c systeminfo | clip", AppWinStyle.Hide, True)
        Dim p As Process = Process.Start("notepad.exe")
        p.WaitForInputIdle()
        SendKeys.SendWait("^V")
        Clipboard.Clear()
    End Sub
    Private Sub softwaresP()
        Thread.Sleep(3000)
        Shell("cmd.exe /c Control appwiz.cpl", AppWinStyle.Hide)
    End Sub
    Private Sub servicesP()
        Thread.Sleep(2300)
        Shell("cmd.exe /c services.msc", AppWinStyle.Hide)
    End Sub
    Private Sub processesP()
        Thread.Sleep(2300)
        Shell("cmd.exe /c taskmgr", AppWinStyle.Hide)
    End Sub
#End Region
#Region "Left Panel Commands"
#Region "initialize"
    Private Sub Cleanerbtn_Click(sender As Object, e As EventArgs) Handles Cleanerbtn.Click
        If pp Then
            Return
        End If
        pp = True
        showLoading(statusLeft)
        toggler(9)
    End Sub

    Private Sub Scannerbtn_Click(sender As Object, e As EventArgs) Handles Scannerbtn.Click
        If pp Then
            Return
        End If
        pp = True
        showLoading(statusLeft)
        toggler(10)
    End Sub

    Private Sub DiskRepairbtn_Click(sender As Object, e As EventArgs) Handles DiskRepairbtn.Click
        If pp Then
            Return
        End If
        pp = True
        showLoading(statusLeft)
        toggler(11)
    End Sub

    Private Sub Recoverybtn_Click(sender As Object, e As EventArgs) Handles Recoverybtn.Click
        If pp Then
            Return
        End If
        pp = True
        showLoading(statusLeft)
        toggler(12)
    End Sub
#End Region
#Region "Start"
    Private Sub CleanerStart_Click(sender As Object, e As EventArgs) Handles CleanerStart.Click
        If qq Then
            Return
        End If
        qq = True
        Dim t As New Thread(AddressOf cleanerThread)
        t.Start()
    End Sub

    Private Sub ScannerStart_Click(sender As Object, e As EventArgs) Handles ScannerStart.Click
        If qq Then
            Return
        End If
        qq = True
        Dim t As New Thread(AddressOf scannerThread)
        t.Start()
    End Sub

    Private Sub DiskCheckerStart_Click(sender As Object, e As EventArgs) Handles DiskCheckerStart.Click
        If qq Then
            Return
        End If
        qq = True
        Dim f As SystemDrives
        f = New SystemDrives
        f.ShowDialog()
        If f.dl.Equals("") Then
        Else
            Dim t As New Thread(AddressOf diskcheckThread)
            t.Start(f.dl.Split(":")(0))
        End If
    End Sub
    Private Sub RecoveryStart_Click(sender As Object, e As EventArgs) Handles RecoveryStart.Click
        If qq Then
            Return
        End If
        qq = True
        Dim f As SystemDrives
        f = New SystemDrives
        f.ShowDialog()
        If f.dl.Equals("") Then
        Else
            Dim t As New Thread(AddressOf recoveryThread)
            t.Start(f.dl.Split(":")(0))
        End If
    End Sub
    Private Sub close1_Click(sender As Object, e As EventArgs) Handles close1.Click
        qq = False
        pp = False
        toggler(0)
    End Sub
    Private Sub close2_Click(sender As Object, e As EventArgs) Handles close2.Click
        pp = False
        qq = False
        toggler(0)
    End Sub
    Private Sub close3_Click(sender As Object, e As EventArgs) Handles close3.Click
        qq = False
        pp = False
        toggler(0)
    End Sub
    Private Sub close4_Click(sender As Object, e As EventArgs) Handles close4.Click
        qq = False
        pp = False
        toggler(0)
    End Sub
#End Region
#Region "Empty recyclebin"
    Private Declare Function SHEmptyRecycleBin Lib "shell32.dll" Alias "SHEmptyRecycleBinA" (ByVal hWnd As Int32, ByVal pszRootPath As String, ByVal dwFlags As Int32) As Int32
    Private Declare Function SHUpdateRecycleBinIcon Lib "shell32.dll" () As Int32

    Private Const SHERB_NOCONFIRMATION = &H1
    Private Const SHERB_NOPROGRESSUI = &H2
    Private Const SHERB_NOSOUND = &H4
    Private Sub EmptyRecycleBin()
        SHEmptyRecycleBin(Me.Handle.ToInt32, vbNullString, SHERB_NOCONFIRMATION + SHERB_NOSOUND)
        SHUpdateRecycleBinIcon()
    End Sub
#End Region
    Private Function ProcessRunning(ByVal ProcessName As String) As Integer
        Try
            Return Process.GetProcessesByName(ProcessName).GetUpperBound(0) + 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Private Delegate Sub completeD(ByVal lpanel As Panel, bool As Boolean)
    Private Sub complete(ByVal lpanel As Panel, bool As Boolean)
        Try
            If lpanel.InvokeRequired Then
                lpanel.Invoke(New completeD(AddressOf complete), New Object() {lpanel, bool})
            Else
                lpanel.Visible = bool
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub completetask(panel As Integer)
        Select Case panel
            Case 1
                complete(Cleaner_3, False)
                complete(Cleaner, True)
            Case 2
                complete(Scanner_3, False)
                complete(Scanner, True)
            Case 3
                complete(DiskChecker_3, False)
                complete(DiskChecker, True)
            Case 4
                complete(Recovery_3, False)
                complete(Recovery, True)
        End Select
        pp = False
        qq = False
    End Sub
    Private Sub cleanerThread()
        Try
            status(cleanerS, 0)
            Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255")
            Thread.Sleep(500)
            Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8")
            Thread.Sleep(500)
            Shell("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 4351")
            Thread.Sleep(500)
            status(cleanerS, 10)
            If Functiod.Cache.GetChromeCache <> "" Then
                Functiod.removal.ChromeCache()
                Functiod.removal.Chromeothers()
            End If
            status(cleanerS, 20)
            If ProcessRunning("firefox") = 1 Then
            Else
                If Functiod.Cache.GetFirefoxCache <> "" Then
                    Functiod.removal.FirefoxCache()
                End If
            End If
            status(cleanerS, 30)
            If Functiod.Cache.GetIECookies <> "" Then
                Functiod.removal.IECookies()
            End If
            status(cleanerS, 40)
            If Functiod.Cache.getInernetFiles <> "" Then
                Functiod.removal.InternetFiles()
            End If
            If Functiod.Cache.GetOperaCache <> "" Then
                Functiod.removal.OperaCache()
            End If
            status(cleanerS, 50)
            If Functiod.Cache.GetSafariCache <> "" Then
                Functiod.removal.SafariCache()
                Functiod.removal.safarihistory()
                Functiod.removal.SafariWebPre()
            End If
            status(cleanerS, 60)
            If CheckBox1.Checked = True Then
                EmptyRecycleBin()
            End If
            status(cleanerS, 80)
            Functiod.removal.TempFiles()
            Functiod.removal.TempFileWinDir()
            status(cleanerS, 90)
            Try
                Dim pathas As String = Path.GetPathRoot(Environment.SystemDirectory)
                Dim files As String
                For Each files In Directory.GetDirectories(pathas & "Windows\Prefetch")
                    Try
                        Directory.Delete(files, True)
                    Catch ex As Exception
                    End Try
                Next files
                For Each files In Directory.GetFiles(pathas & "Windows\Prefetch")
                    Try
                        File.Delete(files)
                    Catch ex As Exception
                    End Try
                Next files
            Catch ex As Exception
            End Try
            status(cleanerS, 100)
            Thread.Sleep(1000)
            status(cleanerS, 0)
            completetask(1)
        Catch ex As Exception
        End Try
        Dim f As scompleted
        f = New scompleted
        f.str = "Cleaned"
        f.ShowDialog()
    End Sub
    Private Sub diskcheckThread(d As String)
        Try
            Dim proc As ProcessStartInfo = New ProcessStartInfo("chkdsk.exe")
            Dim pr As Process
            proc.CreateNoWindow = True
            proc.UseShellExecute = False
            proc.Arguments = d & ": /f"
            proc.RedirectStandardInput = True
            proc.RedirectStandardOutput = True
            proc.RedirectStandardError = True
            pr = Process.Start(proc)
            Dim restart As Boolean = False
            Dim buffer As Char() = "0"
            Dim out As String = ""
            Dim i As Integer = 0
            While Not pr.HasExited
                Try
                    pr.StandardOutput.ReadBlock(buffer, 0, 1)
                    out = out & buffer
                    If Asc(buffer) = 13 Or Asc(buffer) = 10 Or Asc(buffer) = 41 Then
                        i = i + 1
                        'MsgBox(buffer & " => " & Asc(buffer) & vbNewLine & out)
                        If i < 90 Then
                            status(diskcheckS, i)
                        End If
                        Thread.Sleep(20)
                        If (out.Contains("Y/N")) Then
                            pr.StandardInput.WriteLine("y")
                            If out.Contains("restart") Then
                                'MsgBox("restart")
                                restart = True
                            End If
                        End If
                        out = ""
                    End If
                Catch ex As Exception
                End Try
            End While
            status(diskcheckS, 100)
            Thread.Sleep(2000)
            status(diskcheckS, 0)
            completetask(3)
            If (restart) Then
                MessageBox.Show("System needs to be Restarted.", "Restart System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Shell("Shutdown.exe -r -t 5")
            End If
        Catch ex As Exception
        End Try
        Dim f As scompleted
        f = New scompleted
        f.str = "Disk Checked"
        f.ShowDialog()
    End Sub
    Private Sub recoveryThread(d As String)
        Try
            Dim proc As ProcessStartInfo = New ProcessStartInfo("attrib.exe")
            Dim pr As Process
            proc.CreateNoWindow = True
            proc.UseShellExecute = False
            proc.Arguments = "-h -r -s /s /d " & d & ":\*.*"
            proc.RedirectStandardInput = True
            proc.RedirectStandardOutput = True
            proc.RedirectStandardError = True
            pr = Process.Start(proc)
            Dim i As Double = 0.0
            While Not pr.HasExited
                Thread.Sleep(200)
                i = i + 0.1
                If i < 90.0 Then
                    status(recoveryS, CInt(i))
                End If
            End While
            status(recoveryS, 100)
            Thread.Sleep(1000)
            status(recoveryS, 0)
            completetask(4)
        Catch ex As Exception

        End Try
        Dim f As scompleted
        f = New scompleted
        f.str = "Recovered"
        f.ShowDialog()
    End Sub
    Private Sub scannerThread()

        Try
            Dim result As Integer = MessageBox.Show("Interrupting this process May lead to unstable system." & vbNewLine & "Do not Turn Off PC until this process finishes." & vbNewLine & "Click OK to continue...", "Optimizer Scanner", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)
            If result = DialogResult.OK Then
                Shell("sfc.exe /scannow", AppWinStyle.NormalFocus)
                status(diskcheckS, 100)
                Thread.Sleep(2000)
                status(diskcheckS, 0)
                completetask(2)
            End If
        Catch ex As Exception
        End Try
        Dim f As scompleted
        f = New scompleted
        f.str = "Scanned"
        f.ShowDialog()
    End Sub
#End Region
    Private Function checkvalidity() As Boolean
        Dim remaining As String
        Dim rtimes As Integer = 0
        Dim wrapper As New Simple3Des
        remaining = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\SHAD1", "Threshold", Nothing)
        If remaining = Nothing Then
            remaining = wrapper.EncryptData("7")
            rtimes = 7
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\SHAD1", "Threshold", remaining)
        Else
            Try
                rtimes = CInt(wrapper.DecryptData(remaining))
            Catch ex As Exception
                rtimes = 0
            End Try
        End If
        If rtimes = 34742 Then
            regcom(remtime)
            regcom(register)
        ElseIf rtimes = 0 Then
            remtime.Text = "System can be optimized " & rtimes & " more times"
            Return False
        Else
            rtimes = rtimes - 1
            remaining = wrapper.EncryptData(CStr(rtimes))
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\SHAD1", "Threshold", remaining)
            remtime.Text = "System can be optimized " & rtimes & " more times"
        End If
        Return True
    End Function
    Private Delegate Sub regcomD(lb As Label)
    Private Sub regcom(lb As Label)
        If lb.InvokeRequired Then
            lb.Invoke(New regcomD(AddressOf regcom), New Object() {lb})
        Else
            lb.Visible = False
        End If
    End Sub
    Private Sub register_Click(sender As Object, e As EventArgs) Handles register.Click
        Dim key As String
        key = InputBox("Enter Registration Key", "Register U Optimizer", "")
        If key = "" Then
            Return
        End If
        Dim t As New Thread(AddressOf register_product)
        t.Start(key)
    End Sub
    Private Sub register_product(key As String)
        Try
            key = "http://kismetsoftwaresolutions.com/reguoptimizer.php?key=" & key
            key = New System.Net.WebClient().DownloadString(key)
            If key.Equals("approved") Then
                Dim wrapper As New Simple3Des
                regcom(remtime)
                regcom(register)
                key = wrapper.EncryptData("34742")
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\SHAD1", "Threshold", key)
                MessageBox.Show("Registration Key Successfully applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Registration Key Failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Registration Key Failure!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
