Imports System.Runtime.InteropServices
Imports System.Diagnostics
Imports System.Threading

Public Class APM
    Private launchedProcess As Process
    Private embeddedHandle As IntPtr = IntPtr.Zero
    Private monitorTimer As System.Windows.Forms.Timer

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
        Dim exePath As String = "C:\FRISTA\Frista.exe"

        Try
            launchedProcess = Process.Start(exePath)

            Dim waited As Integer = 0
            Do While (launchedProcess.MainWindowHandle = IntPtr.Zero) AndAlso (waited < 10000)
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

    Private Sub APM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class