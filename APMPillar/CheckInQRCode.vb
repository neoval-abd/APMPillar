Public Class CheckInQRCode
    Private launchedProcess As Object

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New APM()
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
            Dim baruDaftar As New BaruDaftar()
            baruDaftar.Show()

            ' Tutup form APM saat ini
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal kembali ke menu utama: " & ex.Message)
        End Try
    End Sub
End Class