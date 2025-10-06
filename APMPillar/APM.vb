Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Threading

Public Class APM
    Private launchedProcess As Process
    Private embeddedHandle As IntPtr = IntPtr.Zero
    Private monitorTimer As System.Windows.Forms.Timer
    Private launchedProcessQR As Process = Nothing
    Private monitorTimerQR As System.Windows.Forms.Timer = Nothing
    Private embeddedHandleQR As IntPtr = IntPtr.Zero

    Private Const GWL_STYLE As Integer = -16
    Private Const WS_CAPTION As Integer = &HC00000
    Private Const WS_THICKFRAME As Integer = &H40000
    Private Const BM_CLICK As Integer = &HF5

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetWindowText(hWnd As IntPtr, lpString As System.Text.StringBuilder, nMaxCount As Integer) As Integer
    End Function

    Private Const SWP_NOZORDER As Integer = &H4
    Private Const SWP_NOACTIVATE As Integer = &H10

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As Integer) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindowEx(hWndParent As IntPtr, hWndChildAfter As IntPtr, lpszClass As String, lpszWindow As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetParent(hWndChild As IntPtr, hWndNewParent As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowLong(hWnd As IntPtr, nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SetWindowLong(hWnd As IntPtr, nIndex As Integer, dwNewLong As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function MoveWindow(hWnd As IntPtr, X As Integer, Y As Integer, nWidth As Integer, nHeight As Integer, bRepaint As Boolean) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowThreadProcessId(hWnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function EnumWindows(lpEnumFunc As EnumWindowsProc, lParam As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function IsWindowVisible(hWnd As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    Private Delegate Function EnumWindowsProc(hWnd As IntPtr, lParam As IntPtr) As Boolean

    Private Sub BtnCheckIn_Click(sender As Object, e As EventArgs) Handles BtnCheckIn.Click
        ' Frista jalan, tutup 
        If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
            Try
                If monitorTimer IsNot Nothing Then
                    monitorTimer.Stop()
                    monitorTimer.Dispose()
                    monitorTimer = Nothing
                End If

                launchedProcess.Kill()
                launchedProcess = Nothing
                embeddedHandle = IntPtr.Zero

                BtnCheckIn.Text = "Check In (FRISTA)"
                BtnCheckIn.BackColor = SystemColors.Control
            Catch ex As Exception
                MessageBox.Show("Error menutup Frista: " & ex.Message)
            End Try
            Return
        End If

        ' Jika belum jalan, buka Frista
        Dim exePath = "C:\FRISTA\Frista.exe"

        Try
            launchedProcess = Process.Start(exePath)

            Dim waited = 0
            Do While launchedProcess.MainWindowHandle = IntPtr.Zero AndAlso waited < 10000
                Thread.Sleep(500)
                waited += 500
                launchedProcess.Refresh()
            Loop

            embeddedHandle = launchedProcess.MainWindowHandle

            If embeddedHandle = IntPtr.Zero Then
                MessageBox.Show("Gagal menemukan jendela Frista.")
                Return
            End If

            EmbedWindow(embeddedHandle)

            Thread.Sleep(1500)
            SetForegroundWindow(embeddedHandle)

            SendKeys.SendWait("admin2-0168r005")
            Thread.Sleep(300)
            SendKeys.SendWait("{TAB}")
            Thread.Sleep(300)
            SendKeys.SendWait("@BPJSAdella08")
            Thread.Sleep(500)

            ClickLoginButton(embeddedHandle)
            StartWindowMonitoring()

            ' tampilan button
            BtnCheckIn.Text = "Tutup Frista"
            BtnCheckIn.BackColor = Color.IndianRed

        Catch ex As Exception
            MessageBox.Show("Gagal membuka Frista: " & ex.Message)
        End Try
    End Sub

    Private Sub ClickLoginButton(parentHandle As IntPtr)
        Try
            Dim loginButton As IntPtr = FindWindowEx(parentHandle, IntPtr.Zero, "Button", "Login")

            If loginButton <> IntPtr.Zero Then
                SendMessage(loginButton, BM_CLICK, IntPtr.Zero, IntPtr.Zero)
                Return
            End If

            Thread.Sleep(200)
            SendKeys.SendWait("{TAB}")
            Thread.Sleep(200)
            SendKeys.SendWait(" ")

        Catch ex As Exception
            Try
                SendKeys.SendWait("{ENTER}")
            Catch
            End Try
        End Try
    End Sub

    Private Sub EmbedWindow(hWnd As IntPtr)
        SetParent(hWnd, PanelFrista.Handle)

        Dim style As Integer = GetWindowLong(hWnd, GWL_STYLE)
        style = style And Not WS_CAPTION And Not WS_THICKFRAME
        SetWindowLong(hWnd, GWL_STYLE, style)

        Thread.Sleep(100)

        ' ukuran panel penuh
        MoveWindow(hWnd, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height, True)

        embeddedHandle = hWnd
    End Sub

    Private Sub StartWindowMonitoring()
        If monitorTimer IsNot Nothing Then
            monitorTimer.Stop()
            monitorTimer.Dispose()
        End If

        monitorTimer = New System.Windows.Forms.Timer()
        monitorTimer.Interval = 500
        AddHandler monitorTimer.Tick, AddressOf MonitorTimer_Tick
        monitorTimer.Start()
    End Sub

    Private Sub MonitorTimer_Tick(sender As Object, e As EventArgs)
        If launchedProcess Is Nothing OrElse launchedProcess.HasExited Then
            If monitorTimer IsNot Nothing Then
                monitorTimer.Stop()
            End If
            Return
        End If

        Dim processId As Integer = launchedProcess.Id
        Dim foundWindows As New List(Of IntPtr)

        Try
            EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                            Try
                                Dim pid As Integer = 0
                                GetWindowThreadProcessId(hWnd, pid)

                                If pid = processId AndAlso IsWindowVisible(hWnd) AndAlso hWnd <> embeddedHandle Then
                                    foundWindows.Add(hWnd)
                                End If
                            Catch
                            End Try

                            Return True
                        End Function, IntPtr.Zero)

            If foundWindows.Count > 0 Then
                For Each newWindow As IntPtr In foundWindows
                    EmbedWindow(newWindow)
                    Exit For
                Next
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub APM_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If embeddedHandle <> IntPtr.Zero Then
            MoveWindow(embeddedHandle, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height, True)
        End If
    End Sub

    Private Sub APM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If monitorTimer IsNot Nothing Then
            Try
                monitorTimer.Stop()
                monitorTimer.Dispose()
            Catch
            End Try
        End If

        If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
            Try
                launchedProcess.Kill()
            Catch
            End Try
        End If
    End Sub

    Private Sub VerifikasiQRCode_Click(sender As Object, e As EventArgs) Handles VerifikasiQRCode.Click
        ' kalau sudah jalan → tutup
        If launchedProcessQR IsNot Nothing AndAlso Not launchedProcessQR.HasExited Then
            Try
                If monitorTimerQR IsNot Nothing Then
                    monitorTimerQR.Stop()
                    monitorTimerQR.Dispose()
                    monitorTimerQR = Nothing
                End If
                launchedProcessQR.Kill()
                launchedProcessQR = Nothing
                embeddedHandleQR = IntPtr.Zero
                VerifikasiQRCode.Text = "Verifikasi (QR CODE)"
                VerifikasiQRCode.BackColor = SystemColors.ControlLight
            Catch ex As Exception
                MessageBox.Show("Error menutup PILAR: " & ex.Message)
            End Try
            Return
        End If


        Try
            Dim psi As New ProcessStartInfo
            psi.FileName = "C:\Program Files (x86)\Aplikasi Sidik Jari BPJS Kesehatan\After.exe"
            psi.WorkingDirectory = "C:\Program Files (x86)\Aplikasi Sidik Jari BPJS Kesehatan"
            psi.UseShellExecute = True
            launchedProcessQR = Process.Start(psi)

            ' Tunggu sampai window utama benar-benar muncul
            Dim waited = 0
            Dim foundHandle = IntPtr.Zero
            Do While foundHandle = IntPtr.Zero AndAlso waited < 15000
                Thread.Sleep(1000)
                foundHandle = FindWindowByTitle("PILAR - Automatic Registration")
                waited += 1000
            Loop

            If foundHandle = IntPtr.Zero Then
                MessageBox.Show("Tidak menemukan window utama FINGER")
                Return
            End If

            ' Pastikan splash screen ditutup sebelum embed
            Thread.Sleep(2000)

            ' Embed window utama ke panel APM
            EmbedWindowQR(foundHandle)
            embeddedHandleQR = foundHandle
            SetForegroundWindow(foundHandle)
            StartWindowMonitoringQR()

            SendKeys.SendWait("urlbpjs")
            Thread.Sleep(300)
            SendKeys.SendWait("admin2-0168r005")
            Thread.Sleep(300)
            SendKeys.SendWait("{TAB}")
            Thread.Sleep(300)
            SendKeys.SendWait("@BPJSAdella08")
            Thread.Sleep(500)

            VerifikasiQRCode.Text = "Tutup PILAR"
            VerifikasiQRCode.BackColor = Color.IndianRed

        Catch ex As Exception
            MessageBox.Show("Gagal embed PILAR: " & ex.Message)
        End Try

    End Sub


    Private Sub EmbedWindowQR(handle As IntPtr)
        Const WS_CHILD As Integer = &H40000000
        Const WS_POPUP As Integer = &H80000000
        Const WS_EX_APPWINDOW As Integer = &H40000
        Const GWL_EXSTYLE As Integer = -20
        Const SWP_NOZORDER As Integer = &H4
        Const SWP_NOACTIVATE As Integer = &H10
        Const SWP_FRAMECHANGED As Integer = &H20

        ' Hilangkan border dan jadikan child window
        Dim style As Integer = GetWindowLong(handle, GWL_STYLE)
        style = (style Or WS_CHILD) And Not WS_POPUP And Not WS_CAPTION And Not WS_THICKFRAME
        SetWindowLong(handle, GWL_STYLE, style)

        Dim ex As Integer = GetWindowLong(handle, GWL_EXSTYLE)
        ex = ex And Not WS_EX_APPWINDOW
        SetWindowLong(handle, GWL_EXSTYLE, ex)

        SetParent(handle, PanelFrista.Handle)
        SetWindowPos(handle, IntPtr.Zero, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height,
                 SWP_NOZORDER Or SWP_NOACTIVATE Or SWP_FRAMECHANGED)
    End Sub


    Private Sub StartWindowMonitoringQR()
        monitorTimerQR = New System.Windows.Forms.Timer()
        monitorTimerQR.Interval = 1000 ' Check setiap 1 detik
        AddHandler monitorTimerQR.Tick, AddressOf MonitorWindowQR
        monitorTimerQR.Start()
    End Sub

    Private Sub MonitorWindowQR(sender As Object, e As EventArgs)
        If launchedProcessQR IsNot Nothing Then
            If launchedProcessQR.HasExited Then
                ' Aplikasi ditutup dari luar
                monitorTimerQR.Stop()
                monitorTimerQR.Dispose()
                monitorTimerQR = Nothing
                launchedProcessQR = Nothing
                embeddedHandleQR = IntPtr.Zero
                VerifikasiQRCode.Text = "Verifikasi (QR CODE)"
                VerifikasiQRCode.BackColor = SystemColors.ControlLight
            Else
                ' Pastikan window tetap di posisi yang benar
                MoveWindow(embeddedHandleQR, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height, True)
            End If
        End If
    End Sub

    ' Tambahkan method baru ini setelah MonitorWindowQR
    Private Function GetProcessWindows(processId As Integer) As List(Of IntPtr)
        Dim windows As New List(Of IntPtr)

        EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                        Try
                            Dim pid As Integer = 0
                            GetWindowThreadProcessId(hWnd, pid)

                            If pid = processId AndAlso IsWindowVisible(hWnd) Then
                                windows.Add(hWnd)
                            End If
                        Catch
                        End Try
                        Return True
                    End Function, IntPtr.Zero)

        Return windows
    End Function

    ' Tambahkan method ini setelah GetProcessWindows
    Private Function FindWindowByTitle(partialTitle As String) As IntPtr
        Dim foundHandle As IntPtr = IntPtr.Zero

        EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                        If IsWindowVisible(hWnd) Then
                            Dim length As Integer = 256
                            Dim sb As New System.Text.StringBuilder(length)

                            ' Get window title
                            If GetWindowText(hWnd, sb, length) > 0 Then
                                Dim title As String = sb.ToString()

                                ' Cek apakah title mengandung text yang dicari
                                If title.Contains(partialTitle) Then
                                    foundHandle = hWnd
                                    Return False ' Stop enumeration
                                End If
                            End If
                        End If
                        Return True ' Continue enumeration
                    End Function, IntPtr.Zero)

        Return foundHandle
    End Function
    Private Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnKembali.Click
        Try
            ' Tutup semua proses FRISTA jika masih terbuka
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
                launchedProcess.Kill()
            End If

            ' Buka form utama
            Dim frmUtama As New FormUtama()
            frmUtama.Show()

            ' Tutup form APM saat ini
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal kembali ke menu utama: " & ex.Message)
        End Try
    End Sub
End Class