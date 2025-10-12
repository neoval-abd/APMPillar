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
            ' Tutup 
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
                launchedProcess.Kill()
            End If

            ' Buka form utama
            Dim frmUtama As New FormUtama()
            frmUtama.Show()

            ' Tutup form 
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal kembali : " & ex.Message)
        End Try
    End Sub

    Private Sub BtnBelumDaftar_Click(sender As Object, e As EventArgs) Handles BtnBelumDaftar.Click
        Try
            If launchedProcess IsNot Nothing AndAlso Not launchedProcess.HasExited Then
                launchedProcess.Kill()
            End If

            ' Buka form 
            Dim baruDaftar As New BaruDaftar()
            baruDaftar.Show()

            ' Tutup form 
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal kembali ke menu utama: " & ex.Message)
        End Try
    End Sub
End Class