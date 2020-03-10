Public Class FormSettings
  Dim KeysData As Integer
  Dim KeyInput As String
  Dim keyResult As String

  Dim reset As Boolean

  Private Sub FormSettings_Load(sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    TopSettKey()

    KeysData = My.Settings.KeyPush
    KeyInput = My.Settings.KeyString

    If Not My.Settings.KeyPush = 117 Then
      Button2.Enabled = True
    End If

    Label2.Text = KeysData
    LabelPress.Text = KeyInput
    Label1.Text = ""
    AddHandler Me.KeyUp, AddressOf SavePress
  End Sub

  Private Sub SavePress(ByVal o As Object, ByVal e As KeyEventArgs)
    e.SuppressKeyPress = True

    If e.KeyValue.ToString = "9" Or e.KeyValue.ToString = "13" Then

    ElseIf e.KeyValue.ToString = "20" Or
    e.KeyValue.ToString = "144" Or
    e.KeyValue.ToString = "91" Or
    e.KeyValue.ToString = "93" Or
    e.KeyValue.ToString = "44" Or
    e.KeyValue.ToString = "27" Or
    e.KeyValue.ToString = "16" Or
    e.KeyValue.ToString = "17" Or
    e.KeyValue.ToString = "18" Or
    (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Then
      Label1.Text = "This input not support"
    Else
      KeysData = e.KeyValue

      If e.KeyData.ToString = "D1" Or
          e.KeyData.ToString = "D2" Or
          e.KeyData.ToString = "D3" Or
          e.KeyData.ToString = "D4" Or
          e.KeyData.ToString = "D5" Or
          e.KeyData.ToString = "D6" Or
          e.KeyData.ToString = "D7" Or
          e.KeyData.ToString = "D8" Or
          e.KeyData.ToString = "D9" Or
          e.KeyData.ToString = "D0" Then
        KeyInput = e.KeyCode.ToString.Replace("D", "")
      ElseIf e.KeyValue.ToString = "192" Then
        KeyInput = "~"
      ElseIf e.KeyValue.ToString = "188" Then
        KeyInput = ","
      ElseIf e.KeyValue.ToString = "190" Then
        KeyInput = "."
      ElseIf e.KeyValue.ToString = "191" Then
        KeyInput = "/"
      ElseIf e.KeyValue.ToString = "186" Then
        KeyInput = ";"
      ElseIf e.KeyValue.ToString = "222" Then
        KeyInput = "'"
      ElseIf e.KeyValue.ToString = "220" Then
        KeyInput = "\"
      ElseIf e.KeyValue.ToString = "219" Then
        KeyInput = "["
      ElseIf e.KeyValue.ToString = "221" Then
        KeyInput = "]"
      ElseIf e.KeyValue.ToString = "189" Then
        KeyInput = "-"
      ElseIf e.KeyValue.ToString = "187" Then
        KeyInput = "="
      Else
        KeyInput = e.KeyCode.ToString
      End If
      Label1.Text = ""

      Label2.Text = KeysData
      LabelPress.Text = KeyInput
    End If
  End Sub

  Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    Dim ResetSetting As MsgBoxResult = MsgBox("Are you sure you want to change it to the default key (F6)?", vbYesNo + vbQuestion, "Reseting")
    If ResetSetting = vbYes Then

      Me.Close()
    End If
  End Sub

  Private Sub FormSettings_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    SettUtama.ListBox1.Items.Clear()
    SettUtama.ListKey()
  End Sub

  Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    My.Settings.KeyPush = KeysData
    My.Settings.KeyString = KeyInput
    Me.Close()
  End Sub

  Private Sub FormSettings_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    If Not My.Settings.KeyPush = KeysData Then
      Dim result As Integer = MsgBox("Are you saving key before exit?", MsgBoxStyle.Information + MsgBoxStyle.YesNoCancel, "Settings")
      If result = DialogResult.Yes Then
        Button1_Click(sender, e)
      ElseIf result = DialogResult.Cancel Then
        e.Cancel = True
      End If
    End If
  End Sub
End Class