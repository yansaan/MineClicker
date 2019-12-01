Public Class SettUtama
  Dim KeysString As String
  Private Sub SettUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Form1.OpenAnotherForm = True

    If My.Settings.CombEnable = True Then
      KeysString = My.Settings.combString + " + " + My.Settings.KeyString
    Else
      KeysString = My.Settings.KeyString
    End If

    TextBox1.Text = KeysString

    If My.Settings.hide = True Then
      CheckBox1.Checked = True
    Else
      CheckBox1.Checked = False
    End If

    If My.Settings.indicator = True Then
      CheckBox2.Checked = True
      CheckBox1.Enabled = True
    Else
      CheckBox2.Checked = False
      CheckBox1.Enabled = False
    End If
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    FormSettings.ShowDialog()
  End Sub

  Private Sub SettUtama_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    Form1.OpenAnotherForm = False
  End Sub

  Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    If CheckBox1.Checked = True Then
      My.Settings.hide = True
    Else
      My.Settings.hide = False
    End If
  End Sub

  Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
    If CheckBox2.Checked = True Then
      My.Settings.indicator = True
      CheckBox1.Enabled = True
    Else
      My.Settings.indicator = False
      CheckBox1.Enabled = False
      My.Settings.hide = False
    End If
  End Sub
End Class