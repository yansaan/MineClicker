Imports System.Runtime.InteropServices
Public Class Form1
  <DllImport("user32.dll")>
  Public Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
  End Function

  ' Return True if another instance
  ' of this program is already running.
  Private Function AlreadyRunning() As Boolean
    ' Get our process name.
    Dim my_proc As Process = Process.GetCurrentProcess
    Dim my_name As String = my_proc.ProcessName

    ' Get information about processes with this name.
    Dim procs() As Process =
        Process.GetProcessesByName(my_name)

    ' If there is only one, it's us.
    If procs.Length = 1 Then Return False

    ' If there is more than one process,
    ' see if one has a StartTime before ours.
    Dim i As Integer
    For i = 0 To procs.Length - 1
      If procs(i).StartTime < my_proc.StartTime Then _
            Return True
    Next i

    ' If we get here, we were first.
    Return False
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
  Dim autoClick As Boolean = False

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    If AlreadyRunning() Then
      RunningKey.Enabled = False
      MessageBox.Show(
        "Another instance is already running.",
        "Already Running",
        MessageBoxButtons.OK,
        MessageBoxIcon.Exclamation)
      Me.Close()
    End If

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

      ComboLongClick.Enabled = True
      TxtLong.Enabled = True
    End If
  End Sub

  Private Sub AFKFish()
    Windows.Forms.Cursor.Position = New System.Drawing.Point(Windows.Forms.Cursor.Position)
    mouse_event(&H8, 0, 0, 0, 1)
    mouse_event(&H10, 0, 0, 0, 1)
  End Sub

  Private Sub FishingFunction_CheckedChanged(sender As Object, e As EventArgs) Handles FishingFunction.CheckedChanged
    If BreakFunction.Checked = True Then
      BreakFunction.Checked = False
      FishingFunction.Checked = True
      My.Settings.mode = 1
    End If

    ComboLongClick.Enabled = False
    TxtLong.Enabled = False
    TxtDelay.Enabled = True
  End Sub

  Private Sub BreakFunction_CheckedChanged(sender As Object, e As EventArgs) Handles BreakFunction.CheckedChanged
    If FishingFunction.Checked = True Then
      FishingFunction.Checked = False
      BreakFunction.Checked = True
      My.Settings.mode = 2
    End If

    ComboLongClick.Enabled = True

    If ComboLongClick.Text = "Custom..." Then
      TxtLong.Enabled = True
    Else
      TxtLong.Enabled = False
    End If
  End Sub

  Private Sub TimerFishing_Tick(sender As Object, e As EventArgs) Handles TimerFishing.Tick
    If repeatEnabled = True Then
      If TimeRepeat = 0 Then
        TimerFishing.Enabled = False
        NotifyIcon1.Icon = My.Resources.FishGrey
        autoClick = False
      Else
        AFKFish()
        TimeRepeat -= 1
      End If
    Else
      AFKFish()
    End If
  End Sub

  Private Sub RunningKey_Tick(sender As Object, e As EventArgs) Handles RunningKey.Tick
    If Me.WindowState = FormWindowState.Normal And OpenAnotherForm = False Then
      If My.Settings.FrontEnable = True Then
        Me.TopMost = True
      Else
        Me.TopMost = False
      End If
    End If

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

    If My.Settings.mode = 2 Then
      If TxtLong.Text = 0 Then
        TxtDelay.Enabled = False
      Else
        TxtDelay.Enabled = True
      End If
    End If

    My.Settings.TimeDelay = InvDelay
    My.Settings.TimeRepeat = Repeat

    If My.Settings.mode = 1 Then
      If InvDelay = 0 Or TxtLong.Text = "" Or TxtRepeat.Text = "" Then
        Label7.Text = "AutoClick not running but input null"
      Else
        If InvDelay < 200 Then
          Label7.Text = "AutoClick not running when setting to minimal"
        ElseIf InvDelay > 99999 Then
          Label7.Text = "AutoClick not running when setting to maximal"
        Else
          Label7.Text = "Press (" + KeysString + ") for play and Stop when in Minecraft World"

          If RunClick = True Then
            Runing()
          End If
        End If
      End If
    ElseIf My.Settings.mode = 2 Then
      Label7.Text = "This Feature not avaliable for now"
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
    If autoClick = False Then
      autoClick = True
      NotifyIcon1.Icon = My.Resources.Fish
      If Repeat > 0 Then
        Call RepeatClicker()
      Else
        Call Clicker()
      End If
    Else
      NotifyIcon1.Icon = My.Resources.FishGrey
      autoClick = False

      If My.Settings.mode = 1 Then
        TimerFishing.Enabled = False
      End If
    End If
  End Sub

  Private Sub Clicker()
    If autoClick = True Then
      If My.Settings.mode = 1 Then
        AFKFish()
        TimerFishing.Interval = InvDelay
        TimerFishing.Enabled = True
      ElseIf My.Settings.mode = 2 Then

      End If
    End If
  End Sub

  Private Sub RepeatClicker()
    If autoClick = True Then
      If My.Settings.mode = 1 Then
        AFKFish()
        If Repeat > 1 Then
          repeatEnabled = True
          TimerFishing.Interval = InvDelay
          TimeRepeat = Repeat - 1
          TimerFishing.Enabled = True
        Else
          NotifyIcon1.Icon = My.Resources.FishGrey
          autoClick = False
        End If
      ElseIf My.Settings.mode = 2 Then

      End If
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
    Process.Start("https://github.com/yansaan/MineClicker#how-to-use")
  End Sub

  Private Sub ComboLongClick_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboLongClick.SelectedIndexChanged
    If ComboLongClick.Text = "Custom..." Then
      TxtLong.Enabled = True
    Else
      TxtLong.Enabled = False
    End If

    If ComboLongClick.Text = "3 Seconds" Then
      TxtLong.Text = 3
    ElseIf ComboLongClick.Text = "5 Seconds" Then
      TxtLong.Text = 5
    ElseIf ComboLongClick.Text = "Unlimited" Then
      TxtLong.Text = 0
    End If

    If My.Settings.mode = 2 Then
      If TxtLong.Text = 0 Then
        TxtDelay.Enabled = False
      Else
        TxtDelay.Enabled = True
      End If
    End If
  End Sub
End Class
