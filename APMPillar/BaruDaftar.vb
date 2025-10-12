Imports System.Diagnostics

Partial Class BaruDaftar
    Private launchedProcess As Process = Nothing

    Private Sub BtnKembali_Click(sender As Object, e As EventArgs) Handles BtnKembali.Click
        Try
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then launchedProcess.Kill()
            Dim f As New ValidasiCheckIn()
            f.Show()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal kembali : " & ex.Message)
        End Try
    End Sub

    Private Sub BtnLanjut_Click(sender As Object, e As EventArgs) Handles BtnLanjut.Click
        Try
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then launchedProcess.Kill()
            Dim f As New CheckInQRCode()
            f.Show()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal lanjut : " & ex.Message)
        End Try
    End Sub
End Class
