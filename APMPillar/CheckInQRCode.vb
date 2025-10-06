Public Class CheckInQRCode
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New APM()
        frm.Show()
        Me.Hide()
    End Sub
End Class