Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Drawing.Printing
Imports System.Text.RegularExpressions
Imports System.Windows.Automation
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D



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

    Private lastPopupHandle As IntPtr = IntPtr.Zero
    Private printCooldownUntil As DateTime = Date.MinValue

    ' UNTUK APLIKASI FINGER - DITAMBAH VARIABLE BARU
    Private fingerTimer As System.Windows.Forms.Timer
    Private fingerDataTimer As System.Windows.Forms.Timer
    Private lastFingerSig As String = ""
    Private lastFingerDataHash As String = ""
    Private fingerProcessId As Integer = 0

    ' TAMBAHAN UNTUK NOTIFIKASI
    Private notifQueue As New Queue(Of String)
    Private processedNotifs As New HashSet(Of String)
    Private lastNotifTime As DateTime = DateTime.MinValue
    Private Const NOTIF_COOLDOWN_MS As Integer = 5000

    ' TAMBAHAN UNTUK NOTIFIKASI VERSI FORM
    Private notifFormQueue As New Queue(Of NotifData)
    Private currentNotifForm As FormNotifikasi = Nothing
    Private processingNotif As Boolean = False

    'TAMBAHAN: KHUSUS UNTUK NOTIFIKASI YANG NEMPEL
    Private embeddedNotifTimer As System.Windows.Forms.Timer
    Private lastEmbeddedCheck As DateTime = DateTime.MinValue
    Private lastEmbeddedSig As String = ""
    Private lastEmbeddedFingerSig As String = ""
    Private lastGlobalTriggerHash As String = ""


    ' VALIDASI BIAR NGGA ASAL CETAK
    Private isFingerActive As Boolean = False  ' Track apakah Finger sedang aktif
    Private lastFingerPrintTime As DateTime = DateTime.MinValue  ' Cooldown cetak
    Private Const PRINT_COOLDOWN_SECONDS As Integer = 5  ' Cooldown 5 detik

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    ' DLL IMPORTS
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetClassName(hWnd As IntPtr, lpClassName As System.Text.StringBuilder, nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindow(hWnd As IntPtr, uCmd As UInteger) As IntPtr
    End Function
    Private Const GW_OWNER As UInteger = 4

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

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
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

    <DllImport("user32.dll")>
    Private Shared Function EnumChildWindows(hWndParent As IntPtr, lpEnumFunc As EnumChildProc, lParam As IntPtr) As Boolean
    End Function
    Private Delegate Function EnumChildProc(hWnd As IntPtr, lParam As IntPtr) As Boolean

    Private notifTimer As System.Windows.Forms.Timer
    Private lastNotifSig As String = ""

    Private Sub BtnCheckIn_Click(sender As Object, e As EventArgs) Handles BtnCheckIn.Click
        If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
            Try
                StopAllTimers()
                launchedProcess.Kill()
                launchedProcess = Nothing
                embeddedHandle = IntPtr.Zero
                isFingerActive = False  ' SET FALSE
                BtnCheckIn.Text = "Verifikasi (FRISTA)"
                BtnCheckIn.BackColor = SystemColors.Control
            Catch ex As Exception
                MessageBox.Show("Error menutup Frista: " & ex.Message)
            End Try
            Return
        End If

        Dim exePath = "C:\FRISTA\Frista.exe"
        Try
            isFingerActive = False  ' SET FALSE saat buka Frista

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
            SendKeys.SendWait("{TAB}")
            SendKeys.SendWait(" ")

            StartWindowMonitoring()
            StartSimpleNotifWatcher()

            BtnCheckIn.Text = "Tutup Frista"
            BtnCheckIn.BackColor = Color.IndianRed
        Catch ex As Exception
            MessageBox.Show("Gagal membuka Frista: " & ex.Message)
        End Try
    End Sub



    Private Sub VerifikasiQRCode_Click(sender As Object, e As EventArgs) Handles VerifikasiQRCode.Click
        If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
            Try
                StopAllTimers()
                launchedProcess.Kill()
                launchedProcess = Nothing
                embeddedHandle = IntPtr.Zero
                fingerProcessId = 0
                isFingerActive = False  ' SET FALSE
                VerifikasiQRCode.Text = "Verifikasi (FINGER)"
                VerifikasiQRCode.BackColor = SystemColors.Control
            Catch ex As Exception
                MessageBox.Show("Error menutup Finger: " & ex.Message)
            End Try
            Return
        End If

        Dim exePath = "C:\Program Files (x86)\Aplikasi Sidik Jari BPJS Kesehatan\After.exe"
        Try
            isFingerActive = True  ' SET TRUE saat buka Finger

            launchedProcess = Process.Start(exePath)
            fingerProcessId = launchedProcess.Id
            Console.WriteLine("Finger Process ID: " & fingerProcessId)

            Dim waited = 0
            Do While launchedProcess.MainWindowHandle = IntPtr.Zero AndAlso waited < 15000
                Thread.Sleep(500)
                waited += 500
                launchedProcess.Refresh()
            Loop

            embeddedHandle = launchedProcess.MainWindowHandle
            If embeddedHandle = IntPtr.Zero Then
                MessageBox.Show("Gagal menemukan jendela Finger.")
                Return
            End If

            Console.WriteLine("Main window handle: " & embeddedHandle.ToString())
            EmbedWindow(embeddedHandle)

            Thread.Sleep(1500)
            SetForegroundWindow(embeddedHandle)

            Thread.Sleep(600)
            SendKeys.SendWait("admin2-0168r005")
            Thread.Sleep(300)
            SendKeys.SendWait("{TAB}")
            Thread.Sleep(300)
            SendKeys.SendWait("@BPJSAdella08")
            Thread.Sleep(500)
            SendKeys.SendWait("{ENTER}")

            StartWindowMonitoring()
            StartFingerDataMonitoring()

            VerifikasiQRCode.Text = "TUTUP FINGER"
            VerifikasiQRCode.BackColor = Color.IndianRed

        Catch ex As Exception
            MessageBox.Show("Gagal membuka Finger: " & ex.Message)
        End Try
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
        Console.WriteLine("Window monitoring started")
    End Sub

    Private Sub MonitorTimer_Tick(sender As Object, e As EventArgs)
        If launchedProcess Is Nothing OrElse launchedProcess.HasExited Then
            If monitorTimer IsNot Nothing Then monitorTimer.Stop()
            Return
        End If

        Dim pidMain As Integer = launchedProcess.Id

        EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                        Try
                            If Not IsWindowVisible(hWnd) OrElse hWnd = embeddedHandle Then Return True

                            Dim pid As Integer = 0
                            GetWindowThreadProcessId(hWnd, pid)

                            ' Cek owner window juga
                            Dim owner = GetWindow(hWnd, GW_OWNER)
                            Dim ownerPid As Integer = -1
                            If owner <> IntPtr.Zero Then GetWindowThreadProcessId(owner, ownerPid)

                            Dim title = GetWinText(hWnd)
                            Dim cls = GetClassNameStr(hWnd)

                            ' Deteksi Frista
                            Dim isFrista = (cls.StartsWith("Chrome_") AndAlso
                                           title.IndexOf("bpjs-kesehatan", StringComparison.OrdinalIgnoreCase) >= 0)

                            ' Deteksi Finger - IMPROVED
                            Dim isFinger = (cls.StartsWith("Chrome_") AndAlso
                                          (title.IndexOf("Aplikasi Verifikasi dan Registrasi Sidik Jari", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                           title.IndexOf("Aplikasi Registrasi Sidik Jari", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
                                           title.IndexOf("fp.bpjs-kesehatan", StringComparison.OrdinalIgnoreCase) >= 0))

                            Dim shouldEmbed As Boolean = (pid = pidMain) OrElse (ownerPid = pidMain) OrElse isFrista OrElse isFinger

                            If shouldEmbed AndAlso hWnd <> embeddedHandle Then
                                Console.WriteLine("Embedding: " & title & " [" & cls & "]")
                                EmbedWindow(hWnd)
                            End If
                        Catch ex As Exception
                            Console.WriteLine("Monitor error: " & ex.Message)
                        End Try
                        Return True
                    End Function, IntPtr.Zero)
    End Sub

    Private Sub StartFingerDataMonitoring()
        If fingerDataTimer IsNot Nothing Then
            fingerDataTimer.Stop()
            fingerDataTimer.Dispose()
        End If

        fingerDataTimer = New System.Windows.Forms.Timer()
        fingerDataTimer.Interval = 1000
        AddHandler fingerDataTimer.Tick, AddressOf FingerDataTimer_Tick
        fingerDataTimer.Start()
        Console.WriteLine("Finger data monitoring started")
    End Sub

    Private Sub FingerDataTimer_Tick(sender As Object, e As EventArgs)
        Try
            If launchedProcess Is Nothing OrElse launchedProcess.HasExited Then
                If fingerDataTimer IsNot Nothing Then fingerDataTimer.Stop()
                Return
            End If

            Dim allWindows As New List(Of IntPtr)
            EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                            Try
                                Dim pid As Integer = 0
                                GetWindowThreadProcessId(hWnd, pid)
                                If pid = fingerProcessId AndAlso IsWindowVisible(hWnd) Then
                                    allWindows.Add(hWnd)
                                End If
                            Catch
                            End Try
                            Return True
                        End Function, IntPtr.Zero)

            For Each hwnd In allWindows
            Next

        Catch ex As Exception
            Console.WriteLine("Data monitoring error: " & ex.Message)
        End Try
    End Sub

    Private Function ExtractData(text As String, fieldName As String) As String
        Try
            Dim pattern1 = fieldName & "\s*:\s*([^\r\n\|:]+)"
            Dim match = Regex.Match(text, pattern1, RegexOptions.IgnoreCase)

            If match.Success Then
                Dim value = match.Groups(1).Value.Trim()
                value = Regex.Replace(value, "[^a-zA-Z0-9\s\-\.]", "").Trim()

                If fieldName.ToLower() = "nama" AndAlso value.ToUpper() = value AndAlso value.Length > 3 Then
                    Return value
                ElseIf fieldName.ToLower() <> "nama" Then
                    Return value
                End If
            End If

            ' Pattern 2: Cari ALL CAPS text untuk nama
            If fieldName.ToLower() = "nama" Then
                Dim lines = text.Split(New String() {"|", vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
                For Each line In lines
                    Dim trimmed = line.Trim()
                    If trimmed.ToUpper() = trimmed AndAlso trimmed.Length > 5 AndAlso trimmed.Length < 50 Then
                        ' Exclude keywords
                        If Not trimmed.Contains("KARTU") AndAlso Not trimmed.Contains("BPJS") AndAlso
                       Not trimmed.Contains("APLIKASI") AndAlso Not IsNumeric(trimmed) Then
                            Return trimmed
                        End If
                    End If
                Next
            End If

            Return ""
        Catch
            Return ""
        End Try
    End Function

    Private Function GetWinText(hWnd As IntPtr) As String
        Try
            Dim sb As New System.Text.StringBuilder(512)
            GetWindowText(hWnd, sb, sb.Capacity)
            Return sb.ToString()
        Catch
            Return ""
        End Try
    End Function

    Private Function GetClassNameStr(hWnd As IntPtr) As String
        Try
            Dim sb As New System.Text.StringBuilder(256)
            GetClassName(hWnd, sb, sb.Capacity)
            Return sb.ToString()
        Catch
            Return ""
        End Try
    End Function

    Private Sub EmbedWindow(hWnd As IntPtr)
        Try
            SetParent(hWnd, PanelFrista.Handle)

            Dim style As Integer = GetWindowLong(hWnd, GWL_STYLE)
            style = style And Not WS_CAPTION And Not WS_THICKFRAME
            SetWindowLong(hWnd, GWL_STYLE, style)

            Thread.Sleep(100)
            MoveWindow(hWnd, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height, True)

            embeddedHandle = hWnd
        Catch ex As Exception
            Console.WriteLine("Embed error: " & ex.Message)
        End Try
    End Sub

    Private Sub APM_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If embeddedHandle <> IntPtr.Zero Then
            MoveWindow(embeddedHandle, 0, 0, PanelFrista.ClientSize.Width, PanelFrista.ClientSize.Height, True)
        End If
    End Sub


    Private Sub ClickLoginButton(parentHandle As IntPtr)
        Try
            Dim loginButton As IntPtr = FindWindowEx(parentHandle, IntPtr.Zero, "Button", "Login")
            If loginButton <> IntPtr.Zero Then
                SendMessage(loginButton, BM_CLICK, IntPtr.Zero, IntPtr.Zero)
                Return
            End If
            SendKeys.SendWait("{TAB}")
            SendKeys.SendWait(" ")
        Catch
            Try : SendKeys.SendWait("{ENTER}") : Catch : End Try
        End Try
    End Sub

    Private Sub StopAllTimers()
        Try
            If monitorTimer IsNot Nothing Then monitorTimer.Stop() : monitorTimer.Dispose() : monitorTimer = Nothing
            If fingerTimer IsNot Nothing Then fingerTimer.Stop() : fingerTimer.Dispose() : fingerTimer = Nothing
            If fingerDataTimer IsNot Nothing Then fingerDataTimer.Stop() : fingerDataTimer.Dispose() : fingerDataTimer = Nothing
            If notifTimer IsNot Nothing Then notifTimer.Stop() : notifTimer.Dispose() : notifTimer = Nothing
            If embeddedNotifTimer IsNot Nothing Then embeddedNotifTimer.Stop() : embeddedNotifTimer.Dispose() : embeddedNotifTimer = Nothing
        Catch
        End Try
    End Sub

    Private Sub StartSimpleNotifWatcher()
        If notifTimer IsNot Nothing Then
            Try : notifTimer.Stop() : notifTimer.Dispose() : Catch : End Try
        End If

        notifTimer = New System.Windows.Forms.Timer() With {.Interval = 150}
        AddHandler notifTimer.Tick, AddressOf NotifTimer_TickImproved
        notifTimer.Start()

        If embeddedNotifTimer IsNot Nothing Then
            Try : embeddedNotifTimer.Stop() : embeddedNotifTimer.Dispose() : Catch : End Try
        End If

        embeddedNotifTimer = New System.Windows.Forms.Timer() With {.Interval = 500}
        AddHandler embeddedNotifTimer.Tick, AddressOf CheckEmbeddedNotifications
        embeddedNotifTimer.Start()
    End Sub

    Private Sub NotifTimer_TickImproved(sender As Object, e As EventArgs)
        Try
            If launchedProcess Is Nothing OrElse launchedProcess.HasExited Then Exit Sub
            Dim pidTarget = launchedProcess.Id

            Dim foundWindows As New List(Of IntPtr)
            EnumWindows(Function(hWnd As IntPtr, lParam As IntPtr) As Boolean
                            Try
                                Dim pid As Integer = 0
                                GetWindowThreadProcessId(hWnd, pid)
                                If pid = pidTarget AndAlso IsWindowVisible(hWnd) Then
                                    foundWindows.Add(hWnd)
                                End If
                            Catch
                            End Try
                            Return True
                        End Function, IntPtr.Zero)

            For Each hWnd In foundWindows
                ProcessNotificationWindowImproved(hWnd)
            Next
            ShowNextNotification()

        Catch ex As Exception
            Console.WriteLine("Notif Watcher Error: " & ex.Message)
        End Try
    End Sub

    Private Sub ProcessNotificationWindowImproved(hWnd As IntPtr)
        Try
            Dim title As String = GetWinText(hWnd)
            Dim body As String = CollectChildTexts(hWnd)
            Dim combined As String = (title & " " & body).Trim()

            If String.IsNullOrWhiteSpace(combined) Then Exit Sub

            Dim low = combined.ToLowerInvariant()

            Dim isSuccess As Boolean =
                low.Contains("berhasil verifikasi") OrElse
                low.Contains("peserta telah terdaftar hari ini") OrElse
                (low.Contains("selamat datang") AndAlso low.Contains("berhasil"))

            If isSuccess Then
                Dim contentHash = combined.GetHashCode().ToString()
                Dim timeWindow = Math.Floor(Date.Now.Ticks / TimeSpan.TicksPerSecond / 5)
                Dim signature = timeWindow.ToString() & "|" & contentHash

                If Not processedNotifs.Contains(signature) Then
                    processedNotifs.Add(signature)


                    Dim notifData As New NotifData With {
                        .Message = combined,
                        .Timestamp = DateTime.Now,
                        .Source = "FRISTA"
                    }

                    notifFormQueue.Enqueue(notifData)
                    CloseDialogIfPossible(hWnd)

                    If processedNotifs.Count > 100 Then processedNotifs.Clear()
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Process Window Error: " & ex.Message)
        End Try
    End Sub

    Private Function CollectChildTexts(parent As IntPtr) As String
        If parent = IntPtr.Zero Then Return ""
        Dim texts As New List(Of String)

        Try
            EnumChildWindows(parent,
                Function(h As IntPtr, l As IntPtr) As Boolean
                    Try
                        If IsWindowVisible(h) Then
                            Dim t = GetWinText(h)
                            If Not String.IsNullOrWhiteSpace(t) AndAlso t.Length > 2 Then
                                texts.Add(t.Trim())
                            End If
                        End If
                    Catch
                    End Try
                    Return True
                End Function, IntPtr.Zero)
        Catch
        End Try

        Return String.Join(" ", texts.Distinct())
    End Function

    Private Sub CloseDialogIfPossible(dlg As IntPtr)
        Try
            Dim btnOk As IntPtr = FindWindowEx(dlg, IntPtr.Zero, "Button", "OK")
            If btnOk = IntPtr.Zero Then
                btnOk = FindWindowEx(dlg, IntPtr.Zero, "Button", Nothing)
            End If

            If btnOk <> IntPtr.Zero Then
                SendMessage(btnOk, BM_CLICK, IntPtr.Zero, IntPtr.Zero)
                Thread.Sleep(100)
            End If

            SetForegroundWindow(dlg)
            Thread.Sleep(50)
            SendKeys.SendWait("{ENTER}")
            Thread.Sleep(50)
            SendKeys.SendWait("{ESC}")
        Catch ex As Exception
            Console.WriteLine("Close Dialog Error: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckEmbeddedNotifications(sender As Object, e As EventArgs)
        Try
            If embeddedHandle = IntPtr.Zero Then Exit Sub
            If launchedProcess Is Nothing OrElse launchedProcess.HasExited Then Exit Sub
            If (DateTime.Now - lastEmbeddedCheck).TotalMilliseconds < 400 Then Exit Sub

            lastEmbeddedCheck = DateTime.Now
            CheckEmbeddedChildWindows()
            CheckEmbeddedViaTextScan()
        Catch ex As Exception
            Console.WriteLine("Embedded check error: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckEmbeddedChildWindows()
        Try
            Dim foundTexts As New List(Of String)
            EnumChildWindows(embeddedHandle,
                Function(childHwnd As IntPtr, lParam As IntPtr) As Boolean
                    Try
                        If Not IsWindowVisible(childHwnd) Then Return True

                        Dim text = GetWinText(childHwnd)
                        If Not String.IsNullOrWhiteSpace(text) Then
                            foundTexts.Add(text.Trim())
                        End If

                        Dim subTexts = CollectChildTexts(childHwnd)
                        If Not String.IsNullOrWhiteSpace(subTexts) Then
                            foundTexts.Add(subTexts)
                        End If
                    Catch
                    End Try
                    Return True
                End Function, IntPtr.Zero)

            If foundTexts.Count > 0 Then
                Dim combined = String.Join(" ", foundTexts.Distinct())
                ProcessEmbeddedNotification(embeddedHandle, combined)
                ProcessEmbeddedFingerTextAndPrint(combined)
            End If
        Catch ex As Exception
            Console.WriteLine("Child windows check error: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckEmbeddedViaTextScan()
        Try
            If embeddedHandle = IntPtr.Zero Then Exit Sub

            Dim mainText = GetWinText(embeddedHandle)
            Dim childText = CollectChildTexts(embeddedHandle)
            Dim combined = (mainText & " " & childText).Trim()

            If Not String.IsNullOrWhiteSpace(combined) AndAlso combined.Length > 10 Then
                ProcessEmbeddedNotification(embeddedHandle, combined)
                ProcessEmbeddedFingerTextAndPrint(combined)
            End If
        Catch ex As Exception
            Console.WriteLine("Text scan error: " & ex.Message)
        End Try
    End Sub

    Private Sub ProcessEmbeddedNotification(hWnd As IntPtr, messageText As String)
        Try
            If String.IsNullOrWhiteSpace(messageText) Then Exit Sub

            Dim low = messageText.ToLowerInvariant()

            Dim isSuccess = (low.Contains("berhasil") AndAlso low.Contains("verifikasi")) OrElse
                           (low.Contains("selamat datang") AndAlso low.Contains("berhasil")) OrElse
                           (low.Contains("peserta telah terdaftar") AndAlso low.Contains("hari ini"))

            If Not isSuccess Then Exit Sub

            Dim contentHash = messageText.GetHashCode().ToString()
            Dim timeStamp = DateTime.Now.ToString("yyyyMMddHHmmss")
            Dim signature = timeStamp.Substring(0, 12) & "|" & contentHash

            If signature = lastEmbeddedSig Then Exit Sub
            If processedNotifs.Contains(signature) Then Exit Sub

            lastEmbeddedSig = signature
            processedNotifs.Add(signature)


            Dim notifData As New NotifData With {
                .Message = messageText,
                .Timestamp = DateTime.Now,
                .Source = "FRISTA-EMBEDDED"
            }

            notifFormQueue.Enqueue(notifData)

            Try : CloseDialogIfPossible(hWnd) : Catch : End Try

            ShowNextNotification()

            If processedNotifs.Count > 100 Then
                processedNotifs.Clear()
                lastEmbeddedSig = ""
            End If

            Dim resetTimer As New System.Windows.Forms.Timer() With {.Interval = 5000}
            AddHandler resetTimer.Tick, Sub()
                                            lastEmbeddedSig = ""
                                            resetTimer.Stop()
                                            resetTimer.Dispose()
                                        End Sub
            resetTimer.Start()

        Catch ex As Exception
            Console.WriteLine("Process embedded error: " & ex.Message)
        End Try
    End Sub

    Private Sub ShowNextNotification()
        Try
            If processingNotif OrElse notifFormQueue.Count = 0 Then Exit Sub
            If currentNotifForm IsNot Nothing AndAlso Not currentNotifForm.IsDisposed Then Exit Sub

            processingNotif = True

            Dim notifData = notifFormQueue.Dequeue()

            currentNotifForm = New FormNotifikasi()
            currentNotifForm.MessageText = notifData.Message
            currentNotifForm.NamaPeserta = notifData.Nama
            currentNotifForm.Timestamp = notifData.Timestamp
            currentNotifForm.SourceApp = notifData.Source

            AddHandler currentNotifForm.FormClosed, Sub()
                                                        currentNotifForm = Nothing
                                                        processingNotif = False
                                                    End Sub

            currentNotifForm.Show()
            LogNotification(notifData)

        Catch ex As Exception
            Console.WriteLine("Show Notification Error: " & ex.Message)
            processingNotif = False
        End Try
    End Sub

    Private Sub LogNotification(notifData As NotifData)
        Try
            Dim logPath = IO.Path.Combine(Application.StartupPath, "notification_log.txt")
            Dim logEntry = String.Format("[{0}] {1} - {2} - {3}" & vbCrLf,
                                        notifData.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                                        notifData.Source,
                                        notifData.Nama,
                                        notifData.Message)
            IO.File.AppendAllText(logPath, logEntry)
        Catch
        End Try
    End Sub

    Private Class NotifData
        Public Property Message As String
        Public Property Nama As String
        Public Property Timestamp As DateTime
        Public Property Source As String
    End Class

    Private Sub APM_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            StopAllTimers()

            If currentNotifForm IsNot Nothing AndAlso Not currentNotifForm.IsDisposed Then
                Try : currentNotifForm.Close() : Catch : End Try
            End If

            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
                Try : launchedProcess.Kill() : Catch : End Try
            End If

            Try
                If notifQueue IsNot Nothing Then notifQueue.Clear()
                If notifFormQueue IsNot Nothing Then notifFormQueue.Clear()
                If processedNotifs IsNot Nothing Then processedNotifs.Clear()
            Catch
            End Try

        Catch ex As Exception
            Console.WriteLine("Cleanup Error: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnTestExtract_Click.Click
        Try
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
                launchedProcess.Kill()
            End If

            Dim frmUtama As New FormUtama()
            FormUtama.Show()

            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal kembali ke menu utama: " & ex.Message)
        End Try
    End Sub


    Private Sub StartFingerNameWatcher()
        Console.WriteLine("StartFingerNameWatcher called (legacy)")
    End Sub

    Private Function ReadFingerNama(rootHandle As IntPtr) As String
        Return ""
    End Function

    Private Function GrabAfter(src As String, label As String) As String
        Dim idx = src.IndexOf(label, StringComparison.OrdinalIgnoreCase)
        If idx < 0 Then Return ""
        Dim seg = src.Substring(idx + label.Length).Trim()
        Dim cut = seg.IndexOfAny(New Char() {"|"c, vbCr(0), vbLf(0)})
        If cut > 0 Then seg = seg.Substring(0, cut).Trim()
        seg = Regex.Replace(seg, "\s+", " ").Trim(" "c, ":"c, "."c)
        Return seg
    End Function

    Private Sub ResetSignatureAfterDelay()
        Dim resetTimer As New System.Windows.Forms.Timer() With {.Interval = 3000}
        AddHandler resetTimer.Tick, Sub()
                                        lastNotifSig = ""
                                        lastFingerSig = ""
                                        resetTimer.Stop()
                                        resetTimer.Dispose()
                                    End Sub
        resetTimer.Start()
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


    Private Sub PrintFingerSlip(nama As String, noKartu As String, status As String)
        Try
            Dim doc As New PrintDocument()
            doc.DefaultPageSettings.PaperSize = New PaperSize("Slip80x90", 315, 315)
            doc.DefaultPageSettings.Margins = New Margins(10, 10, 10, 10)

            AddHandler doc.PrintPage,
            Sub(s, e)
                Dim g = e.Graphics
                Dim y As Single = 0, lh As Single = 18
                Dim fH As New Font("Segoe UI Semibold", 10, FontStyle.Bold)
                Dim f As New Font("Segoe UI", 8.5F)

                g.DrawString("           RSU ADELLA SLAWI", fH, Brushes.Black, 0, y) : y += lh + 2
                g.DrawLine(Pens.Black, 0, y, e.MarginBounds.Width, y) : y += 6

                g.DrawString("NAMA   : " & nama, f, Brushes.Black, 0, y) : y += lh
                g.DrawString("TGL/JAM: " & Date.Now.ToString("dd-MM-2025 HH:mm"), f, Brushes.Black, 0, y) : y += lh + 4

                ' Status/Message
                Dim messageText = "Hasil Pengenalan Wajah Selamat Datang " & nama & vbCrLf & "Anda berhasil verifikasi wajah"
                g.DrawString(messageText, f, Brushes.Black, New RectangleF(0, y, e.MarginBounds.Width, 100))
            End Sub

            doc.Print()
            Console.WriteLine("Finger slip printed: " & nama)
        Catch ex As Exception
            Console.WriteLine("Print finger slip error: " & ex.Message)
        End Try
    End Sub



    Private Sub ProcessEmbeddedFingerTextAndPrint(combined As String)
        If String.IsNullOrWhiteSpace(combined) Then Exit Sub
        Dim t As New System.Windows.Forms.Timer() With {.Interval = 5000}
        AddHandler t.Tick, Sub()
                               lastEmbeddedFingerSig = ""
                               t.Stop() : t.Dispose()
                           End Sub
        t.Start()
    End Sub



    Private Function CapturePanelFristaBitmap() As Bitmap
        Dim rect As Rectangle = PanelFrista.RectangleToScreen(PanelFrista.ClientRectangle)
        Dim bmp As New Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb)
        Using g As Graphics = Graphics.FromImage(bmp)
            g.CopyFromScreen(rect.Location, Point.Empty, rect.Size)
        End Using
        Return bmp
    End Function


    Private Sub PrintPanelFristaScreenshot(Optional header As String = "VERIFIKASI FINGER BPJS",
                                       Optional cropDetail As Boolean = False)
        Dim img As Bitmap = CapturePanelFristaBitmap()

        If cropDetail Then
            img = CropByRatios(img, left:=0.13F, top:=0.2F, width:=0.5F, height:=0.3F)
        End If

        Dim doc As New PrintDocument()
        doc.DefaultPageSettings.PaperSize = New PaperSize("Slip80x70", 315, 315)
        doc.DefaultPageSettings.Margins = New Margins(8, 8, 8, 8)

        AddHandler doc.PrintPage,
    Sub(s, e)
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = PixelOffsetMode.HighQuality

        Dim y As Integer = 0
        Dim fH As New Font("Segoe UI Semibold", 10, FontStyle.Bold)
        Dim f As New Font("Segoe UI", 7.0F)

        ' Header
        g.DrawString("                     RSU ADELLA SLAWI", fH, Brushes.Black, e.MarginBounds.Left, y) : y += 20
        g.DrawLine(Pens.Black, e.MarginBounds.Left, y, e.MarginBounds.Right, y) : y += 8
        g.DrawString(header, f, Brushes.Black, e.MarginBounds.Left, y) : y += 16
        g.DrawString("TGL/JAM: " & Date.Now.ToString("dd-MM-yyyy HH:mm"), f, Brushes.Black, e.MarginBounds.Left, y) : y += 12

        ' Gambar kotak data - fit ke lebar kertas
        Dim maxW = e.MarginBounds.Width
        Dim maxH = e.MarginBounds.Bottom - y

        Dim scale = maxW / CSng(img.Width)
        Dim drawW = CInt(img.Width * scale)
        Dim drawH = CInt(img.Height * scale)

        If drawH > maxH Then
            scale = maxH / CSng(img.Height)
            drawW = CInt(img.Width * scale)
            drawH = CInt(img.Height * scale)
        End If

        Dim x = e.MarginBounds.Left + (maxW - drawW) \ 2

        g.DrawImage(img, New Rectangle(x, y, drawW, drawH))
    End Sub

        AddHandler doc.EndPrint, Sub() img.Dispose()
        doc.Print()
    End Sub


    Private Function CropByRatios(src As Bitmap, left As Single, top As Single, width As Single, height As Single) As Bitmap
        Dim x = CInt(src.Width * left)
        Dim y = CInt(src.Height * top)
        Dim w = CInt(src.Width * width)
        Dim h = CInt(src.Height * height)
        Dim rect = Rectangle.Intersect(New Rectangle(x, y, w, h), New Rectangle(0, 0, src.Width, src.Height))
        Return src.Clone(rect, PixelFormat.Format32bppArgb)
    End Function

    Private Sub HeaderLayout_Paint(sender As Object, e As PaintEventArgs) Handles HeaderLayout.Paint

    End Sub



    Private Sub PrintViaScreenshot()
        Dim img As Bitmap = CapturePanelFristaBitmap()
        img = CropByRatios(img, left:=0.13F, top:=0.2F, width:=0.5F, height:=0.3F)
        img = ConvertToThermalBW(img)

    End Sub

    Private Function ConvertToThermalBW(src As Bitmap) As Bitmap
        Try
            Dim result As New Bitmap(src.Width, src.Height)

            For y As Integer = 0 To src.Height - 1
                For x As Integer = 0 To src.Width - 1
                    Dim pixel As Color = src.GetPixel(x, y)

                    Dim brightness As Integer = CInt(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11)

                    Dim newColor As Color = If(brightness < 150, Color.Black, Color.White)
                    result.SetPixel(x, y, newColor)
                Next
            Next

            Return result

        Catch ex As Exception
            Console.WriteLine("Convert error: " & ex.Message)
            Return src
        End Try
    End Function



    Private Sub BtnPreviewCetak_Click(sender As Object, e As EventArgs)
        Try
            If embeddedHandle = IntPtr.Zero Then
                MessageBox.Show("Aplikasi Finger belum dibuka!", "Info")
                Return
            End If

            Dim img As Bitmap = CapturePanelFristaBitmap()
            img = CropByRatios(img, left:=0.13F, top:=0.2F, width:=0.5F, height:=0.3F)
            img = ConvertToThermalBW(img)

            Dim previewPath = IO.Path.Combine(Application.StartupPath, "preview.png")
            img.Save(previewPath, ImageFormat.Png)
            img.Dispose()

            MessageBox.Show("Preview disimpan di: " & previewPath, "Preview", MessageBoxButtons.OK)
            Process.Start("explorer.exe", "/select," & previewPath)

        Catch ex As Exception
            MessageBox.Show("Error preview: " & ex.Message)
        End Try
    End Sub



    Private Function AdjustBrightnessContrast(src As Bitmap, brightness As Integer, contrast As Integer) As Bitmap
        Try
            Dim result As New Bitmap(src.Width, src.Height)
            Dim contrastFactor As Single = (259.0F * (contrast + 255.0F)) / (255.0F * (259.0F - contrast))

            For y As Integer = 0 To src.Height - 1
                For x As Integer = 0 To src.Width - 1
                    Dim pixel As Color = src.GetPixel(x, y)

                    Dim r As Integer = Math.Min(255, Math.Max(0, pixel.R + brightness))
                    Dim g As Integer = Math.Min(255, Math.Max(0, pixel.G + brightness))
                    Dim b As Integer = Math.Min(255, Math.Max(0, pixel.B + brightness))

                    r = CInt(Math.Min(255, Math.Max(0, contrastFactor * (r - 128) + 128)))
                    g = CInt(Math.Min(255, Math.Max(0, contrastFactor * (g - 128) + 128)))
                    b = CInt(Math.Min(255, Math.Max(0, contrastFactor * (b - 128) + 128)))

                    result.SetPixel(x, y, Color.FromArgb(r, g, b))
                Next
            Next

            Return result
        Catch ex As Exception
            Console.WriteLine("Adjust error: " & ex.Message)
            Return src
        End Try
    End Function




    ' ==================== VALIDASI SEBELUM CETAK ====================
    Private Function ValidateFingerScreenForPrint() As Boolean
        Try
            If Not isFingerActive Then
                Console.WriteLine("❌ Validasi gagal: Finger tidak aktif")
                Return False
            End If

            Dim timeSinceLastPrint = (DateTime.Now - lastFingerPrintTime).TotalSeconds
            If timeSinceLastPrint < PRINT_COOLDOWN_SECONDS Then
                Console.WriteLine("❌ Validasi gagal: Cooldown masih aktif (" & timeSinceLastPrint.ToString("0.0") & "s)")
                Return False
            End If

            If embeddedHandle = IntPtr.Zero Then
                Console.WriteLine("❌ Validasi gagal: Tidak ada window embedded")
                Return False
            End If

            Dim mainText = GetWinText(embeddedHandle)
            Dim childText = CollectChildTexts(embeddedHandle)
            Dim combined = (mainText & " " & childText).ToLowerInvariant()

            Dim hasValidKeyword = combined.Contains("sidik jari") OrElse
                                 combined.Contains("peserta sudah terdaftar") OrElse
                                 combined.Contains("verifikasi sidik jari") OrElse
                                 combined.Contains("nama") AndAlso combined.Contains("no. kartu")

            If Not hasValidKeyword Then
                Console.WriteLine("❌ Validasi gagal: Tidak ada keyword Finger yang valid")
                Console.WriteLine("   Text: " & combined.Substring(0, Math.Min(100, combined.Length)))
                Return False
            End If

            If combined.Contains("username") OrElse
               combined.Contains("password") OrElse
               combined.Contains("silahkan login") OrElse
               combined.Contains("error") OrElse
               combined.Contains("gagal") Then
                Console.WriteLine("❌ Validasi gagal: Masih di halaman login/error")
                Return False
            End If

            Console.WriteLine("✅ Validasi berhasil! Siap cetak")
            Return True

        Catch ex As Exception
            Console.WriteLine("❌ Error saat validasi: " & ex.Message)
            Return False
        End Try
    End Function




    Private Sub BtnCetakFinger_Click(sender As Object, e As EventArgs) Handles BtnCetakFinger.Click
        Try
            If Not ValidateFingerScreenForPrint() Then
                MessageBox.Show("Tidak dapat mencetak!" & vbCrLf & vbCrLf &
                              "Pastikan:" & vbCrLf &
                              "1. Aplikasi FINGER sedang aktif" & vbCrLf &
                              "2. Ada data verifikasi di layar" & vbCrLf &
                              "3. Bukan halaman login/error",
                              "Validasi Gagal",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning)
                Return
            End If

            Dim img As Bitmap = CapturePanelFristaBitmap()
            Dim croppedImg = CropByRatios(img, left:=0.135F, top:=0.24, width:=0.16F, height:=0.13)

            If IsImageBlankOrInvalid(croppedImg) Then
                img.Dispose()
                croppedImg.Dispose()
                MessageBox.Show("Area screenshot kosong atau tidak valid!" & vbCrLf &
                              "Pastikan data verifikasi terlihat di layar.",
                              "Screenshot Invalid",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning)
                Return
            End If

            img = ConvertToThermalBW(croppedImg)
            Dim scaledImg As New Bitmap(img, img.Width * 2, img.Height * 2)
            img.Dispose()
            img = scaledImg

            Dim doc As New PrintDocument()
            doc.DefaultPageSettings.PaperSize = New PaperSize("Slip80x90", 315, 315)
            doc.DefaultPageSettings.Margins = New Margins(15, 15, 15, 15)

            AddHandler doc.PrintPage,
            Sub(s, args)
                Dim g = args.Graphics
                g.InterpolationMode = InterpolationMode.NearestNeighbor
                g.SmoothingMode = SmoothingMode.None
                g.CompositingQuality = CompositingQuality.HighSpeed
                g.PixelOffsetMode = PixelOffsetMode.Half

                Dim y As Integer = 0
                Dim fHeader As New Font("Segoe UI Semibold", 9, FontStyle.Bold)
                Dim fNormal As New Font("Segoe UI", 7)

                g.DrawString("                  RSU ADELLA SLAWI", fHeader, Brushes.Black, 0, y)
                y += 18
                g.DrawLine(Pens.Black, 0, y, args.MarginBounds.Width, y) : y += 6
                g.DrawString("VERIFIKASI FINGER", fNormal, Brushes.Black, 0, y)
                y += 14
                g.DrawString(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), fNormal, Brushes.Black, 0, y)
                y += 16

                Dim maxW = args.MarginBounds.Width
                Dim maxH = args.MarginBounds.Bottom - y - 10
                Dim scale As Single = Math.Min(maxW / img.Width, maxH / img.Height)
                Dim drawW As Integer = CInt(img.Width * scale)
                Dim drawH As Integer = CInt(img.Height * scale)
                Dim startX As Integer = (maxW - drawW) \ 2

                g.DrawImage(img, New Rectangle(startX, y, drawW, drawH))
            End Sub

            AddHandler doc.EndPrint, Sub() img.Dispose()
            doc.Print()

            lastFingerPrintTime = DateTime.Now

            Console.WriteLine("✅ Print berhasil!")
            MessageBox.Show("Berhasil cetak!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            Console.WriteLine("❌ Print error: " & ex.Message)
            MessageBox.Show("Gagal cetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub




    ' ==================== FUNGSI VALIDASI GAMBAR ====================
    Private Function IsImageBlankOrInvalid(img As Bitmap) As Boolean
        Try
            Dim sampleCount As Integer = 0
            Dim whiteCount As Integer = 0

            For y As Integer = 0 To img.Height - 1 Step Math.Max(1, img.Height \ 10)
                For x As Integer = 0 To img.Width - 1 Step Math.Max(1, img.Width \ 10)
                    Dim pixel = img.GetPixel(x, y)
                    Dim brightness = CInt(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11)

                    sampleCount += 1
                    If brightness > 240 Then whiteCount += 1
                Next
            Next

            Dim whitePercentage = (whiteCount / sampleCount) * 100
            Return whitePercentage > 90

        Catch
            Return True
        End Try
    End Function
End Class
