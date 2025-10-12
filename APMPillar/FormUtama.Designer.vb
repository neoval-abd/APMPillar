<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormUtama
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
        MainLayout = New TableLayoutPanel()
        Label1 = New Label()
        ButtonContainer = New TableLayoutPanel()
        BtnPasienBaru = New Button()
        BtnPasienKontrol = New Button()
        Label2 = New Label()
        PanelMain.SuspendLayout()
        MainLayout.SuspendLayout()
        ButtonContainer.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelMain
        ' 
        PanelMain.BackColor = Color.FromArgb(CByte(240), CByte(247), CByte(255))
        PanelMain.Controls.Add(MainLayout)
        PanelMain.Dock = DockStyle.Fill
        PanelMain.Location = New Point(0, 0)
        PanelMain.Name = "PanelMain"
        PanelMain.Padding = New Padding(30, 30, 30, 30)
        PanelMain.Size = New Size(1684, 791)
        PanelMain.TabIndex = 0
        ' 
        ' MainLayout
        ' 
        MainLayout.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        MainLayout.ColumnCount = 1
        MainLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        MainLayout.Controls.Add(Label1, 0, 0)
        MainLayout.Controls.Add(ButtonContainer, 0, 1)
        MainLayout.Controls.Add(Label2, 0, 2)
        MainLayout.Location = New Point(30, 30)
        MainLayout.Name = "MainLayout"
        MainLayout.RowCount = 3
        MainLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        MainLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 60.0F))
        MainLayout.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        MainLayout.Size = New Size(1624, 731)
        MainLayout.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.None
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 48.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        Label1.Location = New Point(260, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(1103, 86)
        Label1.TabIndex = 0
        Label1.Text = "ANJUNGAN MANDIRI PASIEN BPJS"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ButtonContainer
        ' 
        ButtonContainer.Anchor = AnchorStyles.None
        ButtonContainer.ColumnCount = 2
        ButtonContainer.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        ButtonContainer.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        ButtonContainer.Controls.Add(BtnPasienBaru, 0, 0)
        ButtonContainer.Controls.Add(BtnPasienKontrol, 1, 0)
        ButtonContainer.Location = New Point(162, 215)
        ButtonContainer.Name = "ButtonContainer"
        ButtonContainer.Padding = New Padding(20, 20, 20, 20)
        ButtonContainer.RowCount = 1
        ButtonContainer.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        ButtonContainer.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        ButtonContainer.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        ButtonContainer.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        ButtonContainer.RowStyles.Add(New RowStyle(SizeType.Absolute, 20.0F))
        ButtonContainer.Size = New Size(1300, 300)
        ButtonContainer.TabIndex = 1
        ' 
        ' BtnPasienBaru
        ' 
        BtnPasienBaru.Anchor = AnchorStyles.None
        BtnPasienBaru.BackColor = Color.White
        BtnPasienBaru.Cursor = Cursors.Hand
        BtnPasienBaru.FlatStyle = FlatStyle.Flat
        BtnPasienBaru.Font = New Font("Segoe UI", 26.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnPasienBaru.ForeColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        BtnPasienBaru.Location = New Point(110, 65)
        BtnPasienBaru.Name = "BtnPasienBaru"
        BtnPasienBaru.Size = New Size(450, 170)
        BtnPasienBaru.TabIndex = 0
        BtnPasienBaru.Text = "PASIEN BARU"
        BtnPasienBaru.UseVisualStyleBackColor = False
        ' 
        ' BtnPasienKontrol
        ' 
        BtnPasienKontrol.Anchor = AnchorStyles.None
        BtnPasienKontrol.BackColor = Color.FromArgb(CByte(0), CByte(120), CByte(212))
        BtnPasienKontrol.Cursor = Cursors.Hand
        BtnPasienKontrol.FlatStyle = FlatStyle.Flat
        BtnPasienKontrol.Font = New Font("Segoe UI", 26.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnPasienKontrol.ForeColor = Color.White
        BtnPasienKontrol.Location = New Point(740, 65)
        BtnPasienKontrol.Name = "BtnPasienKontrol"
        BtnPasienKontrol.Size = New Size(450, 170)
        BtnPasienKontrol.TabIndex = 1
        BtnPasienKontrol.Text = "PASIEN KONTROL"
        BtnPasienKontrol.UseVisualStyleBackColor = False
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = SystemColors.Highlight
        Label2.Location = New Point(3, 584)
        Label2.Name = "Label2"
        Label2.Size = New Size(1618, 147)
        Label2.TabIndex = 2
        Label2.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' FormUtama
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1684, 791)
        Controls.Add(PanelMain)
        MinimumSize = New Size(1024, 766)
        Name = "FormUtama"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ANJUNGAN PASIEN MANDIRI"
        WindowState = FormWindowState.Maximized
        PanelMain.ResumeLayout(False)
        MainLayout.ResumeLayout(False)
        MainLayout.PerformLayout()
        ButtonContainer.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelMain As Panel
    Friend WithEvents MainLayout As TableLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents ButtonContainer As TableLayoutPanel
    Friend WithEvents BtnPasienBaru As Button
    Friend WithEvents BtnPasienKontrol As Button
    Friend WithEvents Label2 As Label
End Class