<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ValidasiCheckIn
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        PanelMain = New Panel()
        TableLayoutMain = New TableLayoutPanel()
        Label1 = New Label()
        TableLayoutButtons = New TableLayoutPanel()
        BtnBelumDaftar = New Button()
        BtnCheckIn = New Button()
        BtnKembali = New Button()
        PanelMain.SuspendLayout()
        TableLayoutMain.SuspendLayout()
        TableLayoutButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelMain
        ' 
        PanelMain.BackColor = Color.FromArgb(CByte(240), CByte(247), CByte(255))
        PanelMain.Controls.Add(TableLayoutMain)
        PanelMain.Controls.Add(BtnKembali)
        PanelMain.Dock = DockStyle.Fill
        PanelMain.Location = New Point(0, 0)
        PanelMain.Margin = New Padding(3, 4, 3, 4)
        PanelMain.Name = "PanelMain"
        PanelMain.Padding = New Padding(34, 40, 34, 40)
        PanelMain.Size = New Size(1924, 1055)
        PanelMain.TabIndex = 0
        ' 
        ' TableLayoutMain
        ' 
        TableLayoutMain.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TableLayoutMain.ColumnCount = 1
        TableLayoutMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        TableLayoutMain.Controls.Add(Label1, 0, 0)
        TableLayoutMain.Controls.Add(TableLayoutButtons, 0, 1)
        TableLayoutMain.Location = New Point(34, 40)
        TableLayoutMain.Margin = New Padding(3, 4, 3, 4)
        TableLayoutMain.Name = "TableLayoutMain"
        TableLayoutMain.RowCount = 2
        TableLayoutMain.RowStyles.Add(New RowStyle(SizeType.Percent, 25.0F))
        TableLayoutMain.RowStyles.Add(New RowStyle(SizeType.Percent, 75.0F))
        TableLayoutMain.Size = New Size(1856, 882)
        TableLayoutMain.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.None
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 32.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        Label1.Location = New Point(219, 74)
        Label1.Name = "Label1"
        Label1.Size = New Size(1417, 72)
        Label1.TabIndex = 0
        Label1.Text = "APAKAH SUDAH MENDAFTAR MELALUI MOBILE JKN ?"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TableLayoutButtons
        ' 
        TableLayoutButtons.Anchor = AnchorStyles.None
        TableLayoutButtons.ColumnCount = 2
        TableLayoutButtons.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        TableLayoutButtons.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        TableLayoutButtons.Controls.Add(BtnBelumDaftar, 0, 0)
        TableLayoutButtons.Controls.Add(BtnCheckIn, 1, 0)
        TableLayoutButtons.Location = New Point(185, 351)
        TableLayoutButtons.Margin = New Padding(3, 4, 3, 4)
        TableLayoutButtons.Name = "TableLayoutButtons"
        TableLayoutButtons.Padding = New Padding(23, 27, 23, 27)
        TableLayoutButtons.RowCount = 1
        TableLayoutButtons.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        TableLayoutButtons.Size = New Size(1486, 400)
        TableLayoutButtons.TabIndex = 1
        ' 
        ' BtnBelumDaftar
        ' 
        BtnBelumDaftar.Anchor = AnchorStyles.None
        BtnBelumDaftar.BackColor = Color.White
        BtnBelumDaftar.Cursor = Cursors.Hand
        BtnBelumDaftar.FlatStyle = FlatStyle.Flat
        BtnBelumDaftar.Font = New Font("Segoe UI", 26.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnBelumDaftar.ForeColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        BtnBelumDaftar.Location = New Point(126, 86)
        BtnBelumDaftar.Margin = New Padding(3, 4, 3, 4)
        BtnBelumDaftar.Name = "BtnBelumDaftar"
        BtnBelumDaftar.Size = New Size(514, 227)
        BtnBelumDaftar.TabIndex = 0
        BtnBelumDaftar.Text = "BELUM DAFTAR"
        BtnBelumDaftar.UseVisualStyleBackColor = False
        ' 
        ' BtnCheckIn
        ' 
        BtnCheckIn.Anchor = AnchorStyles.None
        BtnCheckIn.BackColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        BtnCheckIn.Cursor = Cursors.Hand
        BtnCheckIn.FlatStyle = FlatStyle.Flat
        BtnCheckIn.Font = New Font("Segoe UI", 26.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCheckIn.ForeColor = Color.White
        BtnCheckIn.Location = New Point(846, 86)
        BtnCheckIn.Margin = New Padding(3, 4, 3, 4)
        BtnCheckIn.Name = "BtnCheckIn"
        BtnCheckIn.Size = New Size(514, 227)
        BtnCheckIn.TabIndex = 1
        BtnCheckIn.Text = "CHECK IN" & vbCrLf & "(MJKN)"
        BtnCheckIn.UseVisualStyleBackColor = False
        ' 
        ' BtnKembali
        ' 
        BtnKembali.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        BtnKembali.BackColor = Color.White
        BtnKembali.Cursor = Cursors.Hand
        BtnKembali.FlatStyle = FlatStyle.Flat
        BtnKembali.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnKembali.ForeColor = Color.FromArgb(CByte(60), CByte(60), CByte(60))
        BtnKembali.Location = New Point(37, 931)
        BtnKembali.Margin = New Padding(3, 4, 3, 4)
        BtnKembali.Name = "BtnKembali"
        BtnKembali.Size = New Size(286, 80)
        BtnKembali.TabIndex = 1
        BtnKembali.Text = "<<  KEMBALI"
        BtnKembali.TextAlign = ContentAlignment.MiddleLeft
        BtnKembali.UseVisualStyleBackColor = False
        ' 
        ' ValidasiCheckIn
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 1055)
        Controls.Add(PanelMain)
        Margin = New Padding(3, 4, 3, 4)
        MinimumSize = New Size(1168, 838)
        Name = "ValidasiCheckIn"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ANJUNGAN PASIEN MANDIRI"
        WindowState = FormWindowState.Maximized
        PanelMain.ResumeLayout(False)
        TableLayoutMain.ResumeLayout(False)
        TableLayoutMain.PerformLayout()
        TableLayoutButtons.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelMain As Panel
    Friend WithEvents TableLayoutMain As TableLayoutPanel
    Friend WithEvents TableLayoutButtons As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents BtnBelumDaftar As Button
    Friend WithEvents BtnCheckIn As Button
    Friend WithEvents BtnKembali As Button
End Class