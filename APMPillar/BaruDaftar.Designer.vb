<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BaruDaftar
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BaruDaftar))
        LayoutRoot = New TableLayoutPanel()
        Label1 = New Label()
        Panel1 = New Panel()
        FlowButtons = New FlowLayoutPanel()
        BtnKembali = New Button()
        BtnLanjut = New Button()
        LayoutRoot.SuspendLayout()
        FlowButtons.SuspendLayout()
        SuspendLayout()
        ' 
        ' LayoutRoot
        ' 
        LayoutRoot.BackColor = Color.White
        LayoutRoot.ColumnCount = 3
        LayoutRoot.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 13.984375F))
        LayoutRoot.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 73.046875F))
        LayoutRoot.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 12.96875F))
        LayoutRoot.Controls.Add(Label1, 1, 0)
        LayoutRoot.Controls.Add(Panel1, 1, 1)
        LayoutRoot.Controls.Add(FlowButtons, 1, 2)
        LayoutRoot.Dock = DockStyle.Fill
        LayoutRoot.Location = New Point(0, 0)
        LayoutRoot.Name = "LayoutRoot"
        LayoutRoot.RowCount = 3
        LayoutRoot.RowStyles.Add(New RowStyle(SizeType.Percent, 7.375F))
        LayoutRoot.RowStyles.Add(New RowStyle(SizeType.Percent, 75.75F))
        LayoutRoot.RowStyles.Add(New RowStyle(SizeType.Percent, 16.875F))
        LayoutRoot.Size = New Size(1280, 800)
        LayoutRoot.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.None
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.Highlight
        Label1.Location = New Point(299, 4)
        Label1.Name = "Label1"
        Label1.Size = New Size(695, 51)
        Label1.TabIndex = 0
        Label1.Text = "SILAHKAN IKUTI PANDUAN BERIKUT:"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel1
        ' 
        Panel1.Anchor = AnchorStyles.None
        Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), Image)
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Location = New Point(179, 62)
        Panel1.Margin = New Padding(0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(935, 599)
        Panel1.TabIndex = 1
        ' 
        ' FlowButtons
        ' 
        FlowButtons.Anchor = AnchorStyles.None
        FlowButtons.AutoSize = True
        FlowButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink
        FlowButtons.Controls.Add(BtnKembali)
        FlowButtons.Controls.Add(BtnLanjut)
        FlowButtons.Location = New Point(388, 702)
        FlowButtons.Margin = New Padding(0)
        FlowButtons.Name = "FlowButtons"
        FlowButtons.Size = New Size(517, 61)
        FlowButtons.TabIndex = 2
        FlowButtons.WrapContents = False
        ' 
        ' BtnKembali
        ' 
        BtnKembali.BackColor = SystemColors.ButtonHighlight
        BtnKembali.FlatAppearance.BorderColor = SystemColors.HotTrack
        BtnKembali.FlatAppearance.BorderSize = 2
        BtnKembali.FlatStyle = FlatStyle.Flat
        BtnKembali.Font = New Font("Segoe UI Semibold", 16.0F, FontStyle.Bold)
        BtnKembali.ForeColor = SystemColors.HotTrack
        BtnKembali.Location = New Point(0, 0)
        BtnKembali.Margin = New Padding(0, 0, 20, 0)
        BtnKembali.Name = "BtnKembali"
        BtnKembali.Size = New Size(200, 61)
        BtnKembali.TabIndex = 3
        BtnKembali.Text = "<< KEMBALI"
        BtnKembali.UseVisualStyleBackColor = False
        ' 
        ' BtnLanjut
        ' 
        BtnLanjut.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        BtnLanjut.BackColor = SystemColors.HotTrack
        BtnLanjut.FlatAppearance.BorderSize = 0
        BtnLanjut.FlatStyle = FlatStyle.Flat
        BtnLanjut.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnLanjut.ForeColor = SystemColors.ButtonHighlight
        BtnLanjut.Location = New Point(220, 0)
        BtnLanjut.Margin = New Padding(0)
        BtnLanjut.Name = "BtnLanjut"
        BtnLanjut.Size = New Size(297, 61)
        BtnLanjut.TabIndex = 4
        BtnLanjut.Text = "LANJUT VERIFIKASI"
        BtnLanjut.UseVisualStyleBackColor = False
        ' 
        ' BaruDaftar
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.White
        ClientSize = New Size(1280, 800)
        Controls.Add(LayoutRoot)
        MinimumSize = New Size(1024, 640)
        Name = "BaruDaftar"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ANJUNGAN PASIEN MANDIRI"
        WindowState = FormWindowState.Maximized
        LayoutRoot.ResumeLayout(False)
        LayoutRoot.PerformLayout()
        FlowButtons.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents LayoutRoot As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FlowButtons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents BtnKembali As System.Windows.Forms.Button
    Friend WithEvents BtnLanjut As System.Windows.Forms.Button
End Class
