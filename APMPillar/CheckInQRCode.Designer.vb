<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CheckInQRCode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CheckInQRCode))
        MainLayout = New TableLayoutPanel()
        Label2 = New Label()
        Panel1 = New Panel()
        ButtonLayout = New TableLayoutPanel()
        BtnKembali = New Button()
        Button1 = New Button()
        MainLayout.SuspendLayout()
        ButtonLayout.SuspendLayout()
        SuspendLayout()
        ' 
        ' MainLayout
        ' 
        MainLayout.BackColor = Color.FromArgb(CByte(240), CByte(247), CByte(255))
        MainLayout.ColumnCount = 1
        MainLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100F))
        MainLayout.Controls.Add(Label2, 0, 0)
        MainLayout.Controls.Add(Panel1, 0, 1)
        MainLayout.Controls.Add(ButtonLayout, 0, 2)
        MainLayout.Dock = DockStyle.Fill
        MainLayout.Location = New Point(0, 0)
        MainLayout.Margin = New Padding(2)
        MainLayout.Name = "MainLayout"
        MainLayout.Padding = New Padding(24, 16, 24, 16)
        MainLayout.RowCount = 3
        MainLayout.RowStyles.Add(New RowStyle())
        MainLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        MainLayout.RowStyles.Add(New RowStyle())
        MainLayout.Size = New Size(1536, 844)
        MainLayout.TabIndex = 0
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.None
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 36.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = SystemColors.HotTrack
        Label2.Location = New Point(405, 24)
        Label2.Margin = New Padding(2, 8, 2, 16)
        Label2.Name = "Label2"
        Label2.Size = New Size(725, 65)
        Label2.TabIndex = 1
        Label2.Text = "SCAN KESAN PESAN (KESSAN)"
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.None
        Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), Image)
        Panel1.BackgroundImageLayout = ImageLayout.Zoom
        Panel1.Location = New Point(329, 121)
        Panel1.Margin = New Padding(16)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(878, 595)
        Panel1.TabIndex = 2
        ' 
        ' ButtonLayout
        ' 
        ButtonLayout.Anchor = AnchorStyles.None
        ButtonLayout.ColumnCount = 2
        ButtonLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        ButtonLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        ButtonLayout.Controls.Add(BtnKembali, 0, 0)
        ButtonLayout.Controls.Add(Button1, 1, 0)
        ButtonLayout.Location = New Point(428, 735)
        ButtonLayout.Margin = New Padding(2)
        ButtonLayout.Name = "ButtonLayout"
        ButtonLayout.RowCount = 1
        ButtonLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        ButtonLayout.Size = New Size(679, 91)
        ButtonLayout.TabIndex = 3
        ' 
        ' BtnKembali
        ' 
        BtnKembali.Anchor = AnchorStyles.None
        BtnKembali.BackColor = SystemColors.ButtonHighlight
        BtnKembali.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnKembali.ForeColor = SystemColors.Highlight
        BtnKembali.Location = New Point(71, 16)
        BtnKembali.Margin = New Padding(16)
        BtnKembali.Name = "BtnKembali"
        BtnKembali.Size = New Size(197, 59)
        BtnKembali.TabIndex = 4
        BtnKembali.Text = "<< KEMBALI"
        BtnKembali.UseVisualStyleBackColor = False
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.None
        Button1.BackColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        Button1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = SystemColors.ButtonHighlight
        Button1.Location = New Point(355, 16)
        Button1.Margin = New Padding(16)
        Button1.Name = "Button1"
        Button1.Size = New Size(308, 59)
        Button1.TabIndex = 3
        Button1.Text = "LANJUT VERIFIKASI"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' CheckInQRCode
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        ClientSize = New Size(1536, 844)
        Controls.Add(MainLayout)
        ForeColor = SystemColors.Highlight
        Margin = New Padding(2)
        MinimumSize = New Size(822, 622)
        Name = "CheckInQRCode"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ANJUNGAN PASIEN MANDIRI"
        WindowState = FormWindowState.Maximized
        MainLayout.ResumeLayout(False)
        MainLayout.PerformLayout()
        ButtonLayout.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents MainLayout As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonLayout As TableLayoutPanel
    Friend WithEvents Button1 As Button
    Friend WithEvents BtnKembali As Button
End Class