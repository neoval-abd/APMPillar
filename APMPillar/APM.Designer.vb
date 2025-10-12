<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class APM
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        BtnCheckIn = New Button()
        LabelTitle = New Label()
        PanelFrista = New Panel()
        BtnCetakFinger = New Button()
        BtnTestExtract_Click = New Button()
        HeaderLayout = New TableLayoutPanel()
        ButtonRow = New TableLayoutPanel()
        VerifikasiQRCode = New Button()
        BottomButtonRow = New TableLayoutPanel()
        HeaderLayout.SuspendLayout()
        ButtonRow.SuspendLayout()
        BottomButtonRow.SuspendLayout()
        SuspendLayout()
        ' 
        ' BtnCheckIn
        ' 
        BtnCheckIn.Anchor = AnchorStyles.None
        BtnCheckIn.BackColor = SystemColors.ControlLight
        BtnCheckIn.Font = New Font("Segoe UI Black", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCheckIn.ForeColor = SystemColors.ActiveCaptionText
        BtnCheckIn.Location = New Point(24, 4)
        BtnCheckIn.Margin = New Padding(8, 4, 8, 4)
        BtnCheckIn.Name = "BtnCheckIn"
        BtnCheckIn.Size = New Size(354, 80)
        BtnCheckIn.TabIndex = 1
        BtnCheckIn.Text = "Verifikasi (FRISTA)"
        BtnCheckIn.UseVisualStyleBackColor = False
        ' 
        ' LabelTitle
        ' 
        LabelTitle.Anchor = AnchorStyles.None
        LabelTitle.AutoSize = True
        LabelTitle.Font = New Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LabelTitle.ForeColor = SystemColors.Highlight
        LabelTitle.Location = New Point(188, 8)
        LabelTitle.Name = "LabelTitle"
        LabelTitle.Size = New Size(631, 51)
        LabelTitle.TabIndex = 0
        LabelTitle.Text = "VERIFIKASI WAJAH / FINGER BPJS"
        LabelTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' PanelFrista
        ' 
        PanelFrista.Dock = DockStyle.Fill
        PanelFrista.Location = New Point(0, 227)
        PanelFrista.Margin = New Padding(3, 4, 3, 4)
        PanelFrista.Name = "PanelFrista"
        PanelFrista.Size = New Size(1008, 617)
        PanelFrista.TabIndex = 0
        ' 
        ' BtnCetakFinger
        ' 
        BtnCetakFinger.Anchor = AnchorStyles.None
        BtnCetakFinger.BackColor = SystemColors.Highlight
        BtnCetakFinger.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCetakFinger.ForeColor = SystemColors.ButtonHighlight
        BtnCetakFinger.Location = New Point(464, 8)
        BtnCetakFinger.Margin = New Padding(8, 8, 8, 8)
        BtnCetakFinger.Name = "BtnCetakFinger"
        BtnCetakFinger.Size = New Size(272, 56)
        BtnCetakFinger.TabIndex = 0
        BtnCetakFinger.Text = "CETAK VERIFIKASI"
        BtnCetakFinger.UseVisualStyleBackColor = False
        ' 
        ' BtnTestExtract_Click
        ' 
        BtnTestExtract_Click.AccessibleRole = AccessibleRole.Border
        BtnTestExtract_Click.Anchor = AnchorStyles.None
        BtnTestExtract_Click.BackColor = SystemColors.ButtonHighlight
        BtnTestExtract_Click.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnTestExtract_Click.ForeColor = SystemColors.Highlight
        BtnTestExtract_Click.Location = New Point(64, 8)
        BtnTestExtract_Click.Margin = New Padding(8, 8, 8, 8)
        BtnTestExtract_Click.Name = "BtnTestExtract_Click"
        BtnTestExtract_Click.Size = New Size(272, 56)
        BtnTestExtract_Click.TabIndex = 3
        BtnTestExtract_Click.Text = "<< KEMBALI"
        BtnTestExtract_Click.UseVisualStyleBackColor = False
        ' 
        ' HeaderLayout
        ' 
        HeaderLayout.ColumnCount = 1
        HeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        HeaderLayout.Controls.Add(LabelTitle, 0, 0)
        HeaderLayout.Controls.Add(ButtonRow, 0, 1)
        HeaderLayout.Controls.Add(BottomButtonRow, 0, 2)
        HeaderLayout.Dock = DockStyle.Top
        HeaderLayout.Location = New Point(0, 0)
        HeaderLayout.Margin = New Padding(3, 4, 3, 4)
        HeaderLayout.Name = "HeaderLayout"
        HeaderLayout.Padding = New Padding(0, 8, 0, 8)
        HeaderLayout.RowCount = 3
        HeaderLayout.RowStyles.Add(New RowStyle())
        HeaderLayout.RowStyles.Add(New RowStyle())
        HeaderLayout.RowStyles.Add(New RowStyle())
        HeaderLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, 16.0F))
        HeaderLayout.Size = New Size(1008, 227)
        HeaderLayout.TabIndex = 1
        ' 
        ' ButtonRow
        ' 
        ButtonRow.Anchor = AnchorStyles.None
        ButtonRow.ColumnCount = 2
        ButtonRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        ButtonRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        ButtonRow.Controls.Add(BtnCheckIn, 0, 0)
        ButtonRow.Controls.Add(VerifikasiQRCode, 1, 0)
        ButtonRow.Location = New Point(101, 61)
        ButtonRow.Margin = New Padding(2, 2, 2, 2)
        ButtonRow.Name = "ButtonRow"
        ButtonRow.RowCount = 1
        ButtonRow.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        ButtonRow.Size = New Size(805, 88)
        ButtonRow.TabIndex = 2
        ' 
        ' VerifikasiQRCode
        ' 
        VerifikasiQRCode.Anchor = AnchorStyles.None
        VerifikasiQRCode.BackColor = SystemColors.ControlLight
        VerifikasiQRCode.Font = New Font("Segoe UI Black", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        VerifikasiQRCode.ForeColor = SystemColors.ActiveCaptionText
        VerifikasiQRCode.Location = New Point(426, 4)
        VerifikasiQRCode.Margin = New Padding(8, 4, 8, 4)
        VerifikasiQRCode.Name = "VerifikasiQRCode"
        VerifikasiQRCode.Size = New Size(354, 80)
        VerifikasiQRCode.TabIndex = 2
        VerifikasiQRCode.Text = "Verifikasi (FINGER)"
        VerifikasiQRCode.UseVisualStyleBackColor = False
        ' 
        ' BottomButtonRow
        ' 
        BottomButtonRow.Anchor = AnchorStyles.None
        BottomButtonRow.ColumnCount = 2
        BottomButtonRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        BottomButtonRow.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        BottomButtonRow.Controls.Add(BtnTestExtract_Click, 0, 0)
        BottomButtonRow.Controls.Add(BtnCetakFinger, 1, 0)
        BottomButtonRow.Location = New Point(104, 153)
        BottomButtonRow.Margin = New Padding(2, 2, 2, 2)
        BottomButtonRow.Name = "BottomButtonRow"
        BottomButtonRow.RowCount = 1
        BottomButtonRow.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        BottomButtonRow.RowStyles.Add(New RowStyle(SizeType.Absolute, 16.0F))
        BottomButtonRow.Size = New Size(800, 72)
        BottomButtonRow.TabIndex = 3
        ' 
        ' APM
        ' 
        AutoScaleDimensions = New SizeF(96.0F, 96.0F)
        AutoScaleMode = AutoScaleMode.Dpi
        BackColor = Color.White
        ClientSize = New Size(1008, 844)
        Controls.Add(PanelFrista)
        Controls.Add(HeaderLayout)
        Margin = New Padding(3, 4, 3, 4)
        MinimumSize = New Size(1024, 822)
        Name = "APM"
        StartPosition = FormStartPosition.CenterScreen
        Text = "VERIFIKASI WAJAH / FINGER BPJS"
        WindowState = FormWindowState.Maximized
        HeaderLayout.ResumeLayout(False)
        HeaderLayout.PerformLayout()
        ButtonRow.ResumeLayout(False)
        BottomButtonRow.ResumeLayout(False)
        ResumeLayout(False)

    End Sub

    Friend WithEvents BtnCheckIn As Button
    Friend WithEvents LabelTitle As Label
    Friend WithEvents PanelFrista As Panel
    Friend WithEvents HeaderLayout As TableLayoutPanel
    Friend WithEvents VerifikasiQRCode As Button
    Friend WithEvents BtnTestExtract_Click As Button
    Friend WithEvents BtnCetakFinger As Button
    Friend WithEvents ButtonRow As TableLayoutPanel
    Friend WithEvents BottomButtonRow As TableLayoutPanel
End Class