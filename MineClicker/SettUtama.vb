Imports IWshRuntimeLibrary
Imports System.IO
Imports System.Environment

Public Class SettUtama
  Dim KeysString As String
  Private Sub SettUtama_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    TopSetting()

    KeysString = My.Settings.KeyString

    If My.Settings.hide = True Then
      CheckBox1.Checked = True
    Else
      CheckBox1.Checked = False
    End If

    If My.Settings.FrontEnable = False Then
      CheckBox3.Checked = False
    Else
      CheckBox3.Checked = True
    End If

    ListKey()

    If Not IO.File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName) & ".lnk") Then
      CheckBox2.Checked = False
    Else
      CheckBox2.Checked = True
    End If
    makeSoutcut()
  End Sub

  Sub makeSoutcut()
    Dim WshShell As WshShell = New WshShell()
    Dim ShortcutPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Startup)

    Dim Shortcut As IWshShortcut = CType(WshShell.CreateShortcut(Path.Combine(ShortcutPath, Application.ProductName) & ".lnk"), IWshShortcut)
    Shortcut.TargetPath = Application.ExecutablePath
    Shortcut.Arguments = "-set"
    Shortcut.WorkingDirectory = Application.StartupPath
    Shortcut.WindowStyle = ProcessWindowStyle.Hidden
    Shortcut.Description = ""
    Shortcut.Save()
  End Sub

  Public Sub ListKey()
    ListBox1.Items.Add("1. Start/Stop Auto Click   (" + My.Settings.KeyString + ")")
    ListBox1.Items.Add("2. Enabled   (Alt + " + My.Settings.KeyString + ")")
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    FormSettings.ShowDialog()
  End Sub

  Private Sub SettUtama_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    TopFrm1()
  End Sub

  Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    If CheckBox1.Checked = True Then
      My.Settings.hide = True
    Else
      My.Settings.hide = False
    End If
  End Sub

  'Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
  '  If CheckBox2.Checked = True Then
  '    My.Settings.indicator = True
  '    CheckBox1.Enabled = True
  '  Else
  '    My.Settings.indicator = False
  '    CheckBox1.Enabled = False
  '    My.Settings.hide = False
  '  End If
  'End Sub

  Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
    If CheckBox3.Checked = True Then
      My.Settings.FrontEnable = True
    Else
      My.Settings.FrontEnable = False
    End If
  End Sub

  Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
    If CheckBox2.Checked = True Then
      If Not IO.File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName) & ".lnk") Then
        makeSoutcut()
      End If
    Else
      If IO.File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName) & ".lnk") Then
        IO.File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName) & ".lnk")
      End If
    End If
  End Sub
End Class