<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckInQRCode
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
        Label2 = New Label()
        Panel1 = New Panel()
        Button1 = New Button()
        BtnKembali = New Button()
        SuspendLayout()
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 36.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label2.ForeColor = SystemColors.MenuHighlight
        Label2.Location = New Point(473, 36)
        Label2.Name = "Label2"
        Label2.Size = New Size(904, 81)
        Label2.TabIndex = 1
        Label2.Text = "SCAN KESAN PESAN (KESSAN)"
        ' 
        ' Panel1
        ' 
        Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), Image)
        Panel1.BackgroundImageLayout = ImageLayout.Stretch
        Panel1.Location = New Point(384, 150)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1097, 665)
        Panel1.TabIndex = 2
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 18.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button1.ForeColor = SystemColors.MenuHighlight
        Button1.Location = New Point(780, 871)
        Button1.Name = "Button1"
        Button1.Size = New Size(421, 77)
        Button1.TabIndex = 3
        Button1.Text = "LANJUT VERIFIKASI DATA"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' BtnKembali
        ' 
        BtnKembali.BackColor = SystemColors.ButtonHighlight
        BtnKembali.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnKembali.Location = New Point(69, 881)
        BtnKembali.Name = "BtnKembali"
        BtnKembali.Size = New Size(246, 67)
        BtnKembali.TabIndex = 4
        BtnKembali.Text = "<< KEMBALI"
        BtnKembali.UseVisualStyleBackColor = False
        ' 
        ' CheckInQRCode
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 1055)
        Controls.Add(BtnKembali)
        Controls.Add(Button1)
        Controls.Add(Panel1)
        Controls.Add(Label2)
        ForeColor = SystemColors.Highlight
        Name = "CheckInQRCode"
        Text = "ANJUNGAN PASIEN MANDIRI"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents BtnKembali As Button
End Class
