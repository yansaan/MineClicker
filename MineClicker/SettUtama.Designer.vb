<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettUtama
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
    Me.Button1 = New System.Windows.Forms.Button()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TextBox1 = New System.Windows.Forms.TextBox()
    Me.CheckBox1 = New System.Windows.Forms.CheckBox()
    Me.CheckBox2 = New System.Windows.Forms.CheckBox()
    Me.SuspendLayout()
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(220, 12)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(75, 23)
    Me.Button1.TabIndex = 0
    Me.Button1.Text = "Set"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(6, 17)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(90, 13)
    Me.Label1.TabIndex = 1
    Me.Label1.Text = "Key for Play/Stop"
    '
    'TextBox1
    '
    Me.TextBox1.BackColor = System.Drawing.SystemColors.Control
    Me.TextBox1.Location = New System.Drawing.Point(102, 14)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.ReadOnly = True
    Me.TextBox1.Size = New System.Drawing.Size(112, 20)
    Me.TextBox1.TabIndex = 2
    '
    'CheckBox1
    '
    Me.CheckBox1.AutoSize = True
    Me.CheckBox1.Location = New System.Drawing.Point(171, 41)
    Me.CheckBox1.Name = "CheckBox1"
    Me.CheckBox1.Size = New System.Drawing.Size(103, 17)
    Me.CheckBox1.TabIndex = 3
    Me.CheckBox1.Text = "Minimize to Hide"
    Me.CheckBox1.UseVisualStyleBackColor = True
    '
    'CheckBox2
    '
    Me.CheckBox2.AutoSize = True
    Me.CheckBox2.Location = New System.Drawing.Point(9, 41)
    Me.CheckBox2.Name = "CheckBox2"
    Me.CheckBox2.Size = New System.Drawing.Size(129, 17)
    Me.CheckBox2.TabIndex = 4
    Me.CheckBox2.Text = "Set Notify as indicator"
    Me.CheckBox2.UseVisualStyleBackColor = True
    '
    'SettUtama
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(300, 102)
    Me.Controls.Add(Me.CheckBox2)
    Me.Controls.Add(Me.CheckBox1)
    Me.Controls.Add(Me.TextBox1)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.Button1)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "SettUtama"
    Me.ShowInTaskbar = False
    Me.Text = "Settings"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents Button1 As Button
  Friend WithEvents Label1 As Label
  Friend WithEvents TextBox1 As TextBox
  Friend WithEvents CheckBox1 As CheckBox
  Friend WithEvents CheckBox2 As CheckBox
End Class
