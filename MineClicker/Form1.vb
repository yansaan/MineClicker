Imports System.Runtime.InteropServices
Public Class Form1
  <DllImport("user32.dll")>
  Public Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
  End Function

  Const KeyDownBit As Integer = &H8000

  Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

  Private keyPush As Integer
  Dim KeysString As String

  Private comb As Boolean
  Private CombPush As Integer

  Dim RunClick As Boolean = False
  Public OpenAnotherForm As Boolean = False
  Dim Repeat As Integer
  Dim InvDelay As Integer
  Dim repeatEnabled As Boolean

  Dim TimeRepeat As Integer

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ComboLongClick.Items.Add("3 Seconds")
    ComboLongClick.Items.Add("5 Seconds")
    ComboLongClick.Items.Add("Unlimited")
    ComboLongClick.Items.Add("Custom...")
    ComboLongClick.Text = "Custom..."

    InvDelay = My.Settings.TimeDelay

    TxtDelay.Text = InvDelay

    TxtRepeat.Text = My.Settings.TimeRepeat

    If My.Settings.mode = 1 Then
      FishingFunction.Checked = True
      BreakFunction.Checked = False

      ComboLongClick.Enabled = False
      TxtLong.Enabled = False
    ElseIf My.Settings.mode = 2 Then
      FishingFunction.Checked = False
      BreakFunction.Checked = True

      ComboLongClick.Enabled = False
      TxtLong.Enabled = True
    End If
  End Sub

  'Private Sub ClickFunction()
  '    MsgBox(Cursor.Position.ToString)
  'End Sub

  Private Sub AFKFish()
    Windows.Forms.Cursor.Position = New System.Drawing.Point(Windows.Forms.Cursor.Position)
    mouse_event(&H8, 0, 0, 0, 1) 'cursor will go down (like a click)
    mouse_event(&H10, 0, 0, 0, 1) ' cursor goes up again
  End Sub

  Private Sub FishingFunction_CheckedChanged(sender As Object, e As EventArgs) Handles FishingFunction.CheckedChanged
    If BreakFunction.Checked = True Then
      BreakFunction.Checked = False
      FishingFunction.Checked = True
      My.Settings.mode = 1
    End If

    ComboLongClick.Enabled = False
    TxtLong.Enabled = False
  End Sub

  Private Sub BreakFunction_CheckedChanged(sender As Object, e As EventArgs) Handles BreakFunction.CheckedChanged
    If FishingFunction.Checked = True Then
      FishingFunction.Checked = False
      BreakFunction.Checked = True
      My.Settings.mode = 2
    End If

    'ComboLongClick.Enabled = True
    TxtLong.Enabled = True
  End Sub

  Private Sub TimerFishing_Tick(sender As Object, e As EventArgs) Handles TimerFishing.Tick
    AFKFish()
    If repeatEnabled = True Then
      If TimeRepeat = 0 Then
        TimerFishing.Enabled = False
        NotifyIcon1.Icon = My.Resources.FishGrey
      Else
        TimeRepeat -= 1
      End If
    End If
  End Sub

  Private Sub RunningKey_Tick(sender As Object, e As EventArgs) Handles RunningKey.Tick
    If My.Settings.indicator = True Then
      NotifyIcon1.Visible = True
    Else
      NotifyIcon1.Visible = False
    End If

    comb = My.Settings.CombEnable
    keyPush = My.Settings.KeyPush
    CombPush = My.Settings.Combination

    If comb = True Then
      KeysString = My.Settings.combString + " + " + My.Settings.KeyString
    Else
      KeysString = My.Settings.KeyString
    End If

    If TxtRepeat.Text = "" Then
      Repeat = 0
    Else
      Repeat = TxtRepeat.Text
    End If

    If TxtDelay.Text = "" Then
      InvDelay = 0
    Else
      InvDelay = TxtDelay.Text
    End If

    My.Settings.TimeDelay = InvDelay
    My.Settings.TimeRepeat = Repeat

    If InvDelay = 0 Or TxtLong.Text = "" Or TxtRepeat.Text = "" Then
      Label7.Text = "AutoClick not running but input null"
    Else
      If InvDelay < 200 Then
        Label7.Text = "AutoClick not running when setting to minimal"
      ElseIf InvDelay > 99999 Then
        Label7.Text = "AutoClick not running when setting to maximal"
      Else
        Label7.Text = "Press (" + KeysString + ") for play and Stop when in Minecraft World"

        TimerFishing.Interval = InvDelay

        If RunClick = True Then
            Runing()
          End If
        End If
    End If
  End Sub

  Private Sub Runing()
    If comb = False Then
      If ((GetAsyncKeyState(keyPush) And KeyDownBit) = KeyDownBit) Then
        While GetAsyncKeyState(keyPush)
        End While

        KeyEnabled()
      End If
    Else
      If ((GetAsyncKeyState(keyPush) And KeyDownBit) = KeyDownBit) And ((GetAsyncKeyState(CombPush) And KeyDownBit) = KeyDownBit) Then
        While GetAsyncKeyState(keyPush)
        End While
        While GetAsyncKeyState(CombPush)
        End While

        KeyEnabled()
      End If
    End If
  End Sub
  Private Sub KeyEnabled()
    If Repeat > 0 Then
      repeatEnabled = True
    Else
      repeatEnabled = False
    End If

    If TimerFishing.Enabled = False Then
      NotifyIcon1.Icon = My.Resources.Fish

      If My.Settings.StartFast = True Then
        AFKFish()
        If repeatEnabled = True Then
          If Repeat = 1 Then
            NotifyIcon1.Icon = My.Resources.FishGrey
          ElseIf Repeat > 1 Then
            TimerFishing.Enabled = True
            TimeRepeat = Repeat - 1
          End If
        Else
          TimerFishing.Enabled = True
        End If
      Else
        If repeatEnabled = True Then
          TimeRepeat = Repeat
        End If
        TimerFishing.Enabled = True
      End If
    ElseIf TimerFishing.Enabled = True Then
      TimerFishing.Enabled = False
      NotifyIcon1.Icon = My.Resources.FishGrey
    End If
  End Sub

  Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
    SettUtama.ShowDialog()
  End Sub
  Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
    FrmAbout.ShowDialog()
  End Sub

  Private Sub TxtDelay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDelay.KeyPress, TxtLong.KeyPress, TxtRepeat.KeyPress
    'e.Handled = Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar) Or e.KeyChar = ",")
    e.Handled = Not (Char.IsDigit(e.KeyChar) Or Char.IsControl(e.KeyChar))
  End Sub

  Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
    RunClick = False
  End Sub

  Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
    If OpenAnotherForm = False Then
      RunClick = True
    End If
    If Me.WindowState = FormWindowState.Minimized Then
      If My.Settings.hide = True Then
        Me.Hide()
      End If
    End If
  End Sub

  Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

  End Sub

  Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    My.Settings.Save()
  End Sub

  Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
    Me.Show()
    Me.WindowState = FormWindowState.Normal
  End Sub

  Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
    Me.Show()
    Me.WindowState = FormWindowState.Normal
  End Sub

  Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
    Me.Close()
  End Sub

  Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
    MessageBox.Show("1 Second = 1.000 Intervals" & ControlChars.CrLf &
                      "Interval duration for allowed is 200 - 99.999 Intervals", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information)
  End Sub

  Private Sub TutorialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialToolStripMenuItem.Click

  End Sub
End Class
