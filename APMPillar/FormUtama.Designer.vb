<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormUtama
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Label1 = New Label()
        Label2 = New Label()
        BtnPasienBaru = New Button()
        BtnPasienKontrol = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 36.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.Location = New Point(431, 77)
        Label1.Name = "Label1"
        Label1.Size = New Size(1030, 81)
        Label1.TabIndex = 0
        Label1.Text = "ANJUNGAN MANDIRI PASIEN BPJS"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 25.8000011F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point, CByte(0))
        Label2.Location = New Point(733, 158)
        Label2.Name = "Label2"
        Label2.Size = New Size(0, 60)
        Label2.TabIndex = 1
        ' 
        ' BtnPasienBaru
        ' 
        BtnPasienBaru.Font = New Font("Segoe UI", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnPasienBaru.Location = New Point(480, 425)
        BtnPasienBaru.Name = "BtnPasienBaru"
        BtnPasienBaru.Size = New Size(399, 111)
        BtnPasienBaru.TabIndex = 2
        BtnPasienBaru.Text = "PASIEN BARU"
        BtnPasienBaru.UseVisualStyleBackColor = True
        ' 
        ' BtnPasienKontrol
        ' 
        BtnPasienKontrol.Font = New Font("Segoe UI", 22.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnPasienKontrol.Location = New Point(1005, 425)
        BtnPasienKontrol.Name = "BtnPasienKontrol"
        BtnPasienKontrol.Size = New Size(399, 111)
        BtnPasienKontrol.TabIndex = 3
        BtnPasienKontrol.Text = "PASIEN KONTROL"
        BtnPasienKontrol.UseVisualStyleBackColor = True
        ' 
        ' FormUtama
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1920, 1055)
        Controls.Add(BtnPasienKontrol)
        Controls.Add(BtnPasienBaru)
        Controls.Add(Label2)
        Controls.Add(Label1)
        ForeColor = SystemColors.Highlight
        Name = "FormUtama"
        Text = "Anjungan Pasien Mandiri"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BtnPasienBaru As Button
    Friend WithEvents BtnPasienKontrol As Button
End Class
