Public Class FormUtama

    Private Sub FormUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.WindowState = FormWindowState.Maximized ' full screen biar lebih keren di APM
    End Sub

    Private Sub BtnPasienBaru_Click(sender As Object, e As EventArgs) Handles BtnPasienBaru.Click
        Dim frm As New ValidasiCheckIn()
        frm.Show()
        Me.Hide()
    End Sub

    Private Sub BtnPasienKontrol_Click(sender As Object, e As EventArgs) Handles BtnPasienKontrol.Click
        Dim frm As New APM()
        frm.Show()
        Me.Hide()
    End Sub

End Class
