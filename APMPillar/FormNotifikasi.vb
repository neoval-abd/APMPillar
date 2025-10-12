Imports System.Drawing.Printing

Public Class FormNotifikasi
    Private currentMessage As String = ""
    Private currentNama As String = ""
    Private currentTimestamp As DateTime

    Private HeaderLabel As Label
    Private SubHeaderLabel As Label
    Private MottoLabel As Label
    Private LblMessage As Label
    Private LblNama As Label
    Private LblTimestamp As Label
    Private WithEvents BtnPrint As Button
    Private WithEvents BtnClose As Button

    Public Property MessageText As String
        Get
            Return currentMessage
        End Get
        Set(value As String)
            currentMessage = value
            If LblMessage IsNot Nothing Then
                DisplayMessage()
            End If
        End Set
    End Property

    Public Property NamaPeserta As String
        Get
            Return currentNama
        End Get
        Set(value As String)
            currentNama = value
        End Set
    End Property

    Public Property Timestamp As DateTime
        Get
            Return currentTimestamp
        End Get
        Set(value As DateTime)
            currentTimestamp = value
        End Set
    End Property

    Public Property SourceApp As String

    Private Sub FormNotifikasi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.TopMost = True
            Me.FormBorderStyle = FormBorderStyle.FixedDialog
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.ClientSize = New Size(1000, 680)
            Me.Text = "Notifikasi Verifikasi"
            Me.BackColor = Color.White

            CreateControls()
            DisplayMessage()

        Catch ex As Exception
            MessageBox.Show("Error load form: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CreateControls()
        Try
            ' ===== HEADER RSU ADELLA =====
            HeaderLabel = New Label()
            HeaderLabel.Font = New Font("Segoe UI Semibold", 18, FontStyle.Bold)
            HeaderLabel.ForeColor = Color.RoyalBlue
            HeaderLabel.Text = "RSU ADELLA SLAWI"
            HeaderLabel.TextAlign = ContentAlignment.MiddleCenter
            HeaderLabel.Dock = DockStyle.Top
            HeaderLabel.Height = 50
            Me.Controls.Add(HeaderLabel)

            ' ===== PESAN =====
            LblMessage = New Label()
            LblMessage.Location = New Point(20, 50)
            LblMessage.Size = New Size(960, 400)
            LblMessage.Font = New Font("Segoe UI", 10)
            LblMessage.BorderStyle = BorderStyle.FixedSingle
            LblMessage.BackColor = Color.FromArgb(240, 248, 255)
            LblMessage.Padding = New Padding(8)
            LblMessage.Text = "Memuat pesan..."
            LblMessage.AutoEllipsis = True
            Me.Controls.Add(LblMessage)

            ' ===== NAMA =====
            LblNama = New Label()
            LblNama.Location = New Point(20, 470)
            LblNama.Size = New Size(460, 25)
            LblNama.Font = New Font("Segoe UI Semibold", 11, FontStyle.Bold)
            LblNama.ForeColor = Color.DarkBlue
            LblNama.Text = ""
            Me.Controls.Add(LblNama)

            ' ===== TIMESTAMP =====
            LblTimestamp = New Label()
            LblTimestamp.Location = New Point(20, 500)
            LblTimestamp.Size = New Size(460, 25)
            LblTimestamp.Font = New Font("Segoe UI", 9)
            LblTimestamp.ForeColor = Color.Gray
            LblTimestamp.Text = ""
            Me.Controls.Add(LblTimestamp)

            ' ===== BUTTON CETAK =====
            BtnPrint = New Button()
            BtnPrint.Size = New Size(400, 80)
            BtnPrint.Location = New Point((Me.ClientSize.Width - BtnPrint.Width) \ 2 - 210, 550)
            BtnPrint.Text = "🖨️ CETAK SEKARANG"
            BtnPrint.Font = New Font("Segoe UI", 14, FontStyle.Bold)
            BtnPrint.BackColor = Color.DodgerBlue
            BtnPrint.ForeColor = Color.White
            BtnPrint.FlatStyle = FlatStyle.Flat
            BtnPrint.Cursor = Cursors.Hand
            BtnPrint.FlatAppearance.BorderSize = 0
            AddHandler BtnPrint.Click, AddressOf BtnPrint_Click
            Me.Controls.Add(BtnPrint)

            ' ===== BUTTON TUTUP =====
            BtnClose = New Button()
            BtnClose.Size = New Size(400, 80)
            BtnClose.Location = New Point((Me.ClientSize.Width - BtnClose.Width) \ 2 + 210, 550)
            BtnClose.Text = "❌ Tutup"
            BtnClose.Font = New Font("Segoe UI", 14)
            BtnClose.BackColor = Color.Crimson
            BtnClose.ForeColor = Color.White
            BtnClose.FlatStyle = FlatStyle.Flat
            BtnClose.Cursor = Cursors.Hand
            BtnClose.FlatAppearance.BorderSize = 0
            AddHandler BtnClose.Click, AddressOf BtnClose_Click
            Me.Controls.Add(BtnClose)

        Catch ex As Exception
            MessageBox.Show("Error membuat controls: " & ex.Message, "Error")
        End Try
    End Sub

    Private Sub DisplayMessage()
        Try
            If LblMessage IsNot Nothing Then
                LblMessage.Text = currentMessage
            End If

            If LblNama IsNot Nothing Then
                If Not String.IsNullOrEmpty(currentNama) Then
                    LblNama.Text = "Nama Peserta: " & currentNama
                    LblNama.Visible = True
                Else
                    LblNama.Visible = False
                End If
            End If

            If LblTimestamp IsNot Nothing Then
                LblTimestamp.Text = "Waktu: " & currentTimestamp.ToString("dd-MM-yyyy HH:mm:ss")
            End If

        Catch ex As Exception
            MessageBox.Show("Error menampilkan pesan: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)
        Try
            BtnPrint.Enabled = False
            BtnPrint.Text = "⏳ Mencetak..."
            BtnPrint.BackColor = Color.Orange
            Application.DoEvents()

            PrintNotifSlip()

            'MessageBox.Show("Slip berhasil dicetak!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            BtnPrint.Text = "✅ Sudah Dicetak"
            BtnPrint.BackColor = Color.Green

            Dim closeTimer As New Timer With {.Interval = 1000}
            AddHandler closeTimer.Tick, Sub()
                                            closeTimer.Stop()
                                            closeTimer.Dispose()
                                            Me.Close()
                                        End Sub
            closeTimer.Start()

        Catch ex As Exception
            MessageBox.Show("Gagal mencetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            BtnPrint.Enabled = True
            BtnPrint.Text = "🖨️ CETAK SEKARANG"
            BtnPrint.BackColor = Color.DodgerBlue
        End Try
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub PrintNotifSlip()
        Dim doc As New PrintDocument()
        doc.DefaultPageSettings.PaperSize = New PaperSize("Slip80x90", 315, 315)
        doc.DefaultPageSettings.Margins = New Margins(10, 10, 10, 10)
        AddHandler doc.PrintPage, AddressOf Doc_PrintPage

        Try
            doc.Print()
        Catch ex As Exception
            Throw New Exception("Print error: " & ex.Message)
        End Try
    End Sub

    Private Sub Doc_PrintPage(sender As Object, e As PrintPageEventArgs)
        Try
            Dim g = e.Graphics
            Dim y As Single = 0
            Dim lineHeight As Single = 18
            Dim fontHeader As New Font("Segoe UI Semibold", 10, FontStyle.Bold)
            Dim fontNormal As New Font("Segoe UI", 8.5F)
            Dim fontItalic As New Font("Segoe UI", 8.0F, FontStyle.Italic)

            ' HEADER CETAK
            g.DrawString("                     RSU ADELLA SLAWI", fontHeader, Brushes.Black, 0, y)
            y += lineHeight
            g.DrawString("         Jl. Prof. Moh. Yamin No. 77 Kudaile Slawi", fontNormal, Brushes.Black, 0, y)
            y += lineHeight
            g.DrawString("                  ☎ (0283) 491154 / 491354", fontNormal, Brushes.Black, 0, y)
            y += lineHeight
            g.DrawLine(Pens.Black, 0, y, e.MarginBounds.Width, y)
            y += 6

            ' DATA
            If Not String.IsNullOrEmpty(currentNama) Then
                g.DrawString("Nama  : " & currentNama, fontNormal, Brushes.Black, 0, y)
                y += lineHeight
            End If
            g.DrawString("Waktu : " & currentTimestamp.ToString("dd-MM-yyyy HH:mm"), fontNormal, Brushes.Black, 0, y)
            y += lineHeight + 4

            ' PESAN
            g.DrawString(currentMessage, fontNormal, Brushes.Black, New RectangleF(0, y, e.MarginBounds.Width, 1000))

        Catch ex As Exception
            Throw New Exception("Print page error: " & ex.Message)
        End Try
    End Sub
End Class
