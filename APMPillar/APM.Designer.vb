<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class APM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        BtnCheckIn = New Button()
        LabelTitle = New Label()
        PanelFrista = New Panel()
        HeaderLayout = New TableLayoutPanel()
        VerifikasiQRCode = New Button()
        BtnKembali = New Button()
        HeaderLayout.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnCheckIn
        ' 
        BtnCheckIn.Anchor = AnchorStyles.None
        BtnCheckIn.BackColor = SystemColors.ControlLight
        BtnCheckIn.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCheckIn.ForeColor = SystemColors.ActiveCaptionText
        BtnCheckIn.Location = New Point(675, 107)
        BtnCheckIn.Margin = New Padding(3, 4, 3, 4)
        BtnCheckIn.Name = "BtnCheckIn"
        BtnCheckIn.Size = New Size(510, 96)
        BtnCheckIn.TabIndex = 1
        BtnCheckIn.Text = "Verifikasi (FRISTA)"
        BtnCheckIn.UseVisualStyleBackColor = False
        ' 
        ' LabelTitle
        ' 
        LabelTitle.Anchor = AnchorStyles.None
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelTitle.ForeColor = SystemColors.Highlight
        LabelTitle.Location = New Point(606, 20)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(648, 60)
        LabelTitle.TabIndex = 0
        LabelTitle.Text = "ANJUNGAN PASIEN MANDIRI"
        LabelTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' PanelFrista
        ' 
        PanelFrista.Dock = DockStyle.Fill
        PanelFrista.Location = New Point(0, 250)
        PanelFrista.Margin = New Padding(3, 4, 3, 4)
        PanelFrista.Name = "PanelFrista"
        PanelFrista.Size = New Size(1920, 805)
        PanelFrista.TabIndex = 0
        ' 
        ' HeaderLayout
        ' 
        HeaderLayout.ColumnCount = 3
        HeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 47.6303329F))
        HeaderLayout.ColumnStyles.Add(New ColumnStyle())
        HeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 52.3696671F))
        HeaderLayout.Controls.Add(LabelTitle, 1, 0)
        HeaderLayout.Controls.Add(VerifikasiQRCode, 2, 1)
        HeaderLayout.Controls.Add(BtnCheckIn, 1, 1)
        HeaderLayout.Controls.Add(BtnKembali, 0, 1)
        HeaderLayout.Dock = DockStyle.Top
        HeaderLayout.GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        HeaderLayout.Location = New Point(0, 0)
        HeaderLayout.Margin = New Padding(3, 4, 3, 4)
        HeaderLayout.Name = "HeaderLayout"
        HeaderLayout.Padding = New Padding(0, 20, 0, 20)
        HeaderLayout.RowCount = 2
        HeaderLayout.RowStyles.Add(New RowStyle())
        HeaderLayout.RowStyles.Add(New RowStyle())
        HeaderLayout.Size = New Size(1920, 250)
        HeaderLayout.TabIndex = 1
        ' 
        ' VerifikasiQRCode
        ' 
        VerifikasiQRCode.Anchor = AnchorStyles.None
        VerifikasiQRCode.BackColor = SystemColors.ControlLight
        VerifikasiQRCode.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        VerifikasiQRCode.ForeColor = SystemColors.ActiveCaptionText
        VerifikasiQRCode.Location = New Point(1333, 109)
        VerifikasiQRCode.Margin = New Padding(3, 4, 3, 4)
        VerifikasiQRCode.Name = "VerifikasiQRCode"
        VerifikasiQRCode.Size = New Size(510, 92)
        VerifikasiQRCode.TabIndex = 2
        VerifikasiQRCode.Text = "Verifikasi (FINGER)"
        VerifikasiQRCode.UseVisualStyleBackColor = False
        ' 
        ' BtnKembali
        ' 
        BtnKembali.BackColor = SystemColors.ButtonHighlight
        BtnKembali.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnKembali.Location = New Point(3, 83)
        BtnKembali.Name = "BtnKembali"
        BtnKembali.Size = New Size(246, 67)
        BtnKembali.TabIndex = 3
        BtnKembali.Text = "<< KEMBALI"
        BtnKembali.UseVisualStyleBackColor = False
        ' 
        ' APM
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1920, 1055)
        Controls.Add(PanelFrista)
        Controls.Add(HeaderLayout)
        Margin = New Padding(3, 4, 3, 4)
        Name = "APM"
        Text = "ANJUNGAN PASIEN MANDIRI"
        HeaderLayout.ResumeLayout(False)
        HeaderLayout.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents BtnCheckIn As Button
    Friend WithEvents LabelTitle As Label
    Friend WithEvents PanelFrista As Panel
    Friend WithEvents HeaderLayout As TableLayoutPanel
    Friend WithEvents VerifikasiQRCode As Button
    Friend WithEvents BtnKembali As Button
End Class
