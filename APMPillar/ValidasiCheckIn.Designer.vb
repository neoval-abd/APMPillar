<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ValidasiCheckIn
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
        Label1 = New Label()
        BtnBelumDaftar = New Button()
        BtnCheckIn = New Button()
        BtnKembali = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = SystemColors.Highlight
        Label1.Location = New Point(477, 95)
        Label1.Name = "Label1"
        Label1.Size = New Size(1020, 60)
        Label1.TabIndex = 0
        Label1.Text = "APAKAH SUDAH MENDAFTAR MELALUI MJKN ?"
        ' 
        ' BtnBelumDaftar
        ' 
        BtnBelumDaftar.Font = New Font("Segoe UI Semibold", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnBelumDaftar.ForeColor = SystemColors.HotTrack
        BtnBelumDaftar.Location = New Point(543, 308)
        BtnBelumDaftar.Margin = New Padding(3, 2, 3, 2)
        BtnBelumDaftar.Name = "BtnBelumDaftar"
        BtnBelumDaftar.Size = New Size(376, 119)
        BtnBelumDaftar.TabIndex = 1
        BtnBelumDaftar.Text = "BELUM DAFTAR"
        BtnBelumDaftar.UseVisualStyleBackColor = True
        ' 
        ' BtnCheckIn
        ' 
        BtnCheckIn.Font = New Font("Segoe UI Semibold", 24.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCheckIn.ForeColor = SystemColors.HotTrack
        BtnCheckIn.Location = New Point(1024, 308)
        BtnCheckIn.Margin = New Padding(3, 2, 3, 2)
        BtnCheckIn.Name = "BtnCheckIn"
        BtnCheckIn.Size = New Size(376, 119)
        BtnCheckIn.TabIndex = 2
        BtnCheckIn.Text = "CHECK IN (MJKN)"
        BtnCheckIn.UseVisualStyleBackColor = True
        ' 
        ' BtnKembali
        ' 
        BtnKembali.BackColor = SystemColors.ButtonHighlight
        BtnKembali.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnKembali.Location = New Point(66, 865)
        BtnKembali.Name = "BtnKembali"
        BtnKembali.Size = New Size(246, 67)
        BtnKembali.TabIndex = 4
        BtnKembali.Text = "<< KEMBALI"
        BtnKembali.UseVisualStyleBackColor = False
        ' 
        ' ValidasiCheckIn
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1920, 1055)
        Controls.Add(BtnKembali)
        Controls.Add(BtnCheckIn)
        Controls.Add(BtnBelumDaftar)
        Controls.Add(Label1)
        ForeColor = SystemColors.Highlight
        Name = "ValidasiCheckIn"
        Text = "Anjungan Pasien Mandiri"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents BtnBelumDaftar As Button
    Friend WithEvents BtnCheckIn As Button
    Friend WithEvents BtnKembali As Button
End Class
