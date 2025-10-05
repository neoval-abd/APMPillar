<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class APM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.BtnCheckIn = New System.Windows.Forms.Button()
        Me.LabelTitle = New System.Windows.Forms.Label()
        Me.PanelFrista = New System.Windows.Forms.Panel()
        Me.HeaderLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.SuspendLayout()
        '
        'HeaderLayout 
        '
        Me.HeaderLayout.ColumnCount = 3
        Me.HeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        Me.HeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
        Me.HeaderLayout.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0!))
        Me.HeaderLayout.RowCount = 2
        Me.HeaderLayout.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Me.HeaderLayout.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        Me.HeaderLayout.Dock = DockStyle.Top
        Me.HeaderLayout.Padding = New Padding(0, 15, 0, 15)
        Me.HeaderLayout.GrowStyle = TableLayoutPanelGrowStyle.FixedSize
        '
        'LabelTitle 
        '
        Me.LabelTitle.AutoSize = True
        Me.LabelTitle.Text = "SILAHKAN KLIK UNTUK VERIFIKASI WAJAH"
        Me.LabelTitle.TextAlign = ContentAlignment.MiddleCenter
        Me.LabelTitle.Anchor = AnchorStyles.None
        Me.HeaderLayout.Controls.Add(Me.LabelTitle, 1, 0)
        '
        'BtnCheckIn 
        '
        Me.BtnCheckIn.Size = New Size(457, 59)
        Me.BtnCheckIn.Text = "Check In (FRISTA)"
        Me.BtnCheckIn.UseVisualStyleBackColor = True
        Me.BtnCheckIn.Anchor = AnchorStyles.None
        Me.HeaderLayout.Controls.Add(Me.BtnCheckIn, 1, 1)
        '
        'PanelFrista 
        '
        Me.PanelFrista.Dock = DockStyle.Fill
        '
        'APM (form)
        '
        Me.AutoScaleDimensions = New SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(1680, 791)
        Me.Controls.Add(Me.PanelFrista)
        Me.Controls.Add(Me.HeaderLayout)
        Me.Name = "APM"
        Me.Text = "APM"
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents BtnCheckIn As Button
    Friend WithEvents LabelTitle As Label
    Friend WithEvents PanelFrista As Panel
    Friend WithEvents HeaderLayout As TableLayoutPanel
End Class
