Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Drawing.Printing


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

    ' UNTUK PRINT OTOMATIS
    <DllImport("user32.dll")>
    Private Shared Function EnumChildWindows(hWndParent As IntPtr, lpEnumFunc As EnumChildProc, lParam As IntPtr) As Boolean
    End Function
    Private Delegate Function EnumChildProc(hWnd As IntPtr, lParam As IntPtr) As Boolean

    Private notifTimer As System.Windows.Forms.Timer
    Private lastNotifSig As String = ""



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

            StartSimpleNotifWatcher()

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

        If notifTimer IsNot Nothing Then Try : notifTimer.Stop() : notifTimer.Dispose() : Catch : End Try
    End Sub

    Private Sub VerifikasiQRCode_Click(sender As Object, e As EventArgs) Handles VerifikasiQRCode.Click
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

                VerifikasiQRCode.Text = "Check In (FINGER)"
                VerifikasiQRCode.BackColor = SystemColors.Control
            Catch ex As Exception
                MessageBox.Show("Error menutup Finger: " & ex.Message)
            End Try
            Return
        End If

        Dim exePath = "C:\Program Files (x86)\Aplikasi Sidik Jari BPJS Kesehatan\After.exe"

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
                MessageBox.Show("Gagal menemukan jendela Finger.")
                Return
            End If

            EmbedWindow(embeddedHandle)

            Thread.Sleep(1500)
            SetForegroundWindow(embeddedHandle)

            SendKeys.SendWait("https://fp.bpjs-kesehatan.go.id/finger-rest/")
            ClickLoginButton(embeddedHandle)
            Thread.Sleep(900)
            SendKeys.SendWait("admin2-0168r005")
            Thread.Sleep(300)
            SendKeys.SendWait("{TAB}")
            Thread.Sleep(300)
            SendKeys.SendWait("@BPJSAdella08")
            Thread.Sleep(500)
            SendKeys.SendWait("{ENTER}")

            StartSimpleNotifWatcher()

            VerifikasiQRCode.Text = "TUTUP FINGER"
            VerifikasiQRCode.BackColor = Color.IndianRed

        Catch ex As Exception
            MessageBox.Show("Gagal membuka Finger: " & ex.Message)
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
        monitorTimerQR.Interval = 1000
        AddHandler monitorTimerQR.Tick, AddressOf MonitorWindowQR
        monitorTimerQR.Start()
    End Sub

    Private Sub MonitorWindowQR(sender As Object, e As EventArgs)
        If launchedProcessQR IsNot Nothing Then
            If launchedProcessQR.HasExited Then
                monitorTimerQR.Stop()
                monitorTimerQR.Dispose()
                monitorTimerQR = Nothing
                launchedProcessQR = Nothing
                embeddedHandleQR = IntPtr.Zero
                VerifikasiQRCode.Text = "Verifikasi (QR CODE)"
                VerifikasiQRCode.BackColor = SystemColors.ControlLight
            Else
                MoveWindow(embeddedHandleQR, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height, True)
            End If
        End If
    End Sub

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

    Private Function FindWindowByTitle(partialTitle As String) As IntPtr
        Dim foundHandle As IntPtr = IntPtr.Zero

        EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                        If IsWindowVisible(hWnd) Then
                            Dim length As Integer = 256
                            Dim sb As New System.Text.StringBuilder(length)

                            If GetWindowText(hWnd, sb, length) > 0 Then
                                Dim title As String = sb.ToString()

                                If title.Contains(partialTitle) Then
                                    foundHandle = hWnd
                                    Return False
                                End If
                            End If
                        End If
                        Return True
                    End Function, IntPtr.Zero)

        Return foundHandle
    End Function
    Private Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnKembali.Click
        Try
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
                launchedProcess.Kill()
            End If

            Dim frmUtama As New FormUtama()
            frmUtama.Show()

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal kembali ke menu utama: " & ex.Message)
        End Try
    End Sub


    Private Sub StartSimpleNotifWatcher()
        If notifTimer IsNot Nothing Then
            Try : notifTimer.Stop() : notifTimer.Dispose() : Catch : End Try
        End If
        notifTimer = New System.Windows.Forms.Timer() With {.Interval = 350}
        AddHandler notifTimer.Tick, AddressOf NotifTimer_TickSimple
        notifTimer.Start()
    End Sub

    Private Sub NotifTimer_TickSimple(sender As Object, e As EventArgs)
        Try
            If launchedProcess Is Nothing OrElse launchedProcess.HasExited Then Exit Sub
            Dim pidTarget = launchedProcess.Id

            EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                            Try
                                Dim pid As Integer = 0
                                GetWindowThreadProcessId(hWnd, pid)
                                If pid <> pidTarget OrElse Not IsWindowVisible(hWnd) Then Return True

                                Dim title As String = GetWinText(hWnd)
                                Dim body As String = CollectChildTexts(hWnd)
                                Dim combined As String = (title & " " & body).Trim()
                                If String.IsNullOrWhiteSpace(combined) Then Return True

                                Dim low = combined.ToLowerInvariant()

                                Dim isSuccess As Boolean =
                                low.Contains("berhasil verifikasi") OrElse
                                low.Contains("peserta telah terdaftar hari ini") OrElse
                                (low.Contains("selamat datang") AndAlso low.Contains("berhasil"))

                                If isSuccess Then
                                    Dim sig = Date.Now.ToString("yyyyMMdd") & "|" & combined
                                    If sig <> lastNotifSig Then
                                        lastNotifSig = sig
                                        PrintNotifSlip(combined)
                                        CloseDialogIfPossible(hWnd)
                                    End If
                                End If
                            Catch
                            End Try
                            Return True
                        End Function, IntPtr.Zero)
        Catch
        End Try
    End Sub

    Private Function GetWinText(hWnd As IntPtr) As String
        Dim sb As New System.Text.StringBuilder(512)
        GetWindowText(hWnd, sb, sb.Capacity)
        Return sb.ToString()
    End Function

    Private Function CollectChildTexts(parent As IntPtr) As String
        Dim texts As New List(Of String)
        EnumChildWindows(parent, Function(h As IntPtr, l As IntPtr) As Boolean
                                     Try
                                         Dim t = GetWinText(h)
                                         If Not String.IsNullOrWhiteSpace(t) Then texts.Add(t)
                                     Catch
                                     End Try
                                     Return True
                                 End Function, IntPtr.Zero)
        Return String.Join(" ", texts.Distinct())
    End Function

    Private Sub CloseDialogIfPossible(dlg As IntPtr)
        Try
            Dim btnOk As IntPtr = FindWindowEx(dlg, IntPtr.Zero, "Button", Nothing)
            If btnOk <> IntPtr.Zero Then
                SendMessage(btnOk, BM_CLICK, IntPtr.Zero, IntPtr.Zero)
            Else
                SendKeys.SendWait("{ENTER}")
            End If
        Catch
        End Try
    End Sub


    Private Sub PrintNotifSlip(messageText As String)
        Dim nama As String = ExtractNamaWelcome(messageText)
        Dim doc As New PrintDocument()
        doc.DefaultPageSettings.PaperSize = New PaperSize("Slip80x90", 315, 360) ' 80mm
        doc.DefaultPageSettings.Margins = New Margins(10, 10, 10, 10)

        AddHandler doc.PrintPage,
        Sub(s, e)
            Dim g = e.Graphics
            Dim y As Single = 0, lh As Single = 18
            Dim fH As New Font("Segoe UI Semibold", 10, FontStyle.Bold)
            Dim f As New Font("Segoe UI", 8.5F)

            g.DrawString("RSU ADELLA SLAWl", fH, Brushes.Black, 0, y) : y += lh + 2
            g.DrawLine(Pens.Black, 0, y, e.MarginBounds.Width, y) : y += 6
            g.DrawString("TGL/JAM : " & Date.Now.ToString("dd-MM-yyyy HH:mm"), f, Brushes.Black, 0, y) : y += lh
            If nama <> "" Then g.DrawString("NAMA   : " & nama, f, Brushes.Black, 0, y) : y += lh
            y += 4
            g.DrawString(messageText, f, Brushes.Black, New RectangleF(0, y, e.MarginBounds.Width, 1000))
        End Sub

        Try
            doc.Print()
        Catch ex As Exception
            MessageBox.Show("Gagal mencetak: " & ex.Message)
        End Try
    End Sub

    Private Function ExtractNamaWelcome(text As String) As String
        Dim low = text.ToLowerInvariant()
        Dim i = low.IndexOf("selamat datang")
        If i < 0 Then Return ""
        Dim after = text.Substring(i + "selamat datang".Length).Trim()
        Dim j = after.ToLowerInvariant().IndexOf("anda")
        If j > 0 Then after = after.Substring(0, j).Trim()
        Return System.Text.RegularExpressions.Regex.Replace(after, "\s+", " ").Trim(" "c, "."c, ":"c)
    End Function


End Class