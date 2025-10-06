Public Class ValidasiCheckIn
    Private Sub BtnCheckIn_Click(sender As Object, e As EventArgs) Handles BtnCheckIn.Click
        ' Pasien Kontrol — sementara arahkan juga ke APM
        Dim frm As New CheckInQRCode()
        frm.Show()
        Me.Hide()
    End Sub
End Class