Public Class ValidasiCheckIn
    Private launchedProcess As Process

    Private Sub BtnCheckIn_Click(sender As Object, e As EventArgs) Handles BtnCheckIn.Click
        ' Pasien Kontrol — sementara arahkan juga ke APM
        Dim frm As New CheckInQRCode()
        frm.Show()
        Me.Hide()
    End Sub

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

    Private Sub ValidasiCheckIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class