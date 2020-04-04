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

  ' Keyboard Function
  Private keyPush As Integer
  Dim KeysString As String

  ' Auto Click Function
  Dim Clickers As Boolean = False
  Dim InvDelay As Integer

  ' Repeat Function
  Dim Repeat As Integer
  Dim repeatEnabled As Boolean
  Private getRepeat As Integer
  Dim TimeRepeat As Integer

  ' Left Running Function
  Dim secBreak As Integer
  Private LongMine As String
  Private getSec As Integer

  Private Unlimited As Boolean = False
  Dim longBySelect As Integer
  Dim longSelect As Boolean = True

  Private SecVal As Integer
  Private MilVal As Integer
  Private milLong As Boolean = False

  Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    If AlreadyRunning() Then
      RunningKey.Enabled = False
      MessageBox.Show(
        "Another instance is already running.",
        "Already Running",
    MessageBoxButtons.OK,
    MessageBoxIcon.Exclamation)
      Me.Close()
    Else
      NotifyIcon1.Visible = True

      ComboLongClick.Items.Add("3 Seconds")
      ComboLongClick.Items.Add("5 Seconds")
      ComboLongClick.Items.Add("Unlimited")
      ComboLongClick.Items.Add("Custom...")

      Select Case My.Settings.LongSelect
        Case 1
          ComboLongClick.Text = "3 Seconds"
        Case 2
          ComboLongClick.Text = "5 Seconds"
        Case 3
          ComboLongClick.Text = "Unlimited"
        Case 0
          ComboLongClick.Text = "Custom..."
      End Select

      ComboBoxClick.Items.Add("AFK Fish (Right)")
      ComboBoxClick.Items.Add("Auto Coublestone (Left)")

      Select Case My.Settings.mode
        Case 1
          ComboBoxClick.Text = "AFK Fish (Right)"
        Case 2
          ComboBoxClick.Text = "Auto Coublestone (Left)"
      End Select

      Try
        LongMine = My.Settings.TimeLong
        TxtLong.Value = LongMine
      Catch ex As Exception
        My.Settings.TimeLong = TxtLong.Minimum
        LongMine = TxtLong.Minimum
        TxtLong.Value = TxtLong.Minimum
      End Try

      InvDelay = My.Settings.TimeDelay
      TxtDelay.Value = InvDelay

      Repeat = My.Settings.TimeRepeat
      TxtRepeat.Value = Repeat

      Modes()
    End If
  End Sub

  Private Sub ValLong()
    If Unlimited = True Then
      TxtDelay.Enabled = False
      TxtRepeat.Enabled = False
      CheckBox1.Enabled = False
    Else
      TxtDelay.Enabled = True
      CheckBox1.Enabled = True
      If CheckBox1.Checked = False Then
        TxtRepeat.Enabled = False
      Else
        TxtRepeat.Enabled = True
      End If
      Repeater()
    End If
  End Sub

  Private Sub Repeater()
    Select Case CheckBox1.Checked
      Case True
        My.Settings.EnabledRepeat = True
        TxtRepeat.Enabled = True
      Case Else
        My.Settings.EnabledRepeat = False
        TxtRepeat.Enabled = False
    End Select
  End Sub

  Private Sub Modes()
    Select Case ComboBoxClick.Text
      Case "AFK Fish (Right)"
        ComboLongClick.Enabled = False
        TxtLong.Enabled = False
        My.Settings.mode = 1
        NotifyIcon1.Icon = My.Resources.fishoff

        CheckBox1.Enabled = True
        TxtDelay.Enabled = True

        Repeater()
      Case "Auto Coublestone (Left)"
        ComboLongClick.Enabled = True
        My.Settings.mode = 2
        NotifyIcon1.Icon = My.Resources.mineoff

        ClickModes()
    End Select
  End Sub

  Private Sub ClickModes()
    Select Case ComboLongClick.Text
      Case "3 Seconds"
        longSelect = True
        longBySelect = 3
        Unlimited = False
        TxtLong.Enabled = False
        My.Settings.LongSelect = 1
      Case "5 Seconds"
        longSelect = True
        Unlimited = False
        longBySelect = 5
        TxtLong.Enabled = False
        My.Settings.LongSelect = 2
      Case "Unlimited"
        longSelect = True
        Unlimited = True
        TxtLong.Enabled = False
        My.Settings.LongSelect = 3
      Case "Custom..."
        longSelect = False
        Unlimited = False
        TxtLong.Enabled = True
        My.Settings.LongSelect = 0
    End Select
    ValLong()
  End Sub

  Private Sub RunningKey_Tick(sender As Object, e As EventArgs) Handles RunningKey.Tick
    keyPush = My.Settings.KeyPush
    KeysString = My.Settings.KeyString

    My.Settings.TimeDelay = InvDelay
    My.Settings.TimeRepeat = Repeat
    My.Settings.TimeLong = LongMine

    Label7.Text = "Press (" + KeysString + ") for play and Stop when in Minecraft World"

    If Me.WindowState = FormWindowState.Minimized Then
      Runing()
    End If
  End Sub

  Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
    SettUtama.ShowDialog()
  End Sub
  Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
    FrmAbout.ShowDialog()
  End Sub

  Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
    If Me.WindowState = FormWindowState.Minimized Then
      TxtDelay_ValueChanged(sender, e)
      TxtLong_ValueChanged(sender, e)
      TxtRepeat_ValueChanged(sender, e)

      My.Settings.Save()
      If My.Settings.hide = True Then
        Me.Hide()
      End If
    End If
  End Sub

  Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    My.Settings.Save()
  End Sub

  Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
    Me.Show()
    Me.ShowInTaskbar = True
    Me.WindowState = FormWindowState.Normal
  End Sub

  Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
    Me.Show()
    Me.ShowInTaskbar = True
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
    ClickModes()
  End Sub

  Private Sub AFKFish()
    Windows.Forms.Cursor.Position = New System.Drawing.Point(Windows.Forms.Cursor.Position)
    mouse_event(&H8, 0, 0, 0, 1)
    mouse_event(&H10, 0, 0, 0, 1)
  End Sub

  Private Sub Runing()
    If ((GetAsyncKeyState(keyPush) And KeyDownBit) = KeyDownBit) Then
      While GetAsyncKeyState(keyPush)
      End While

      KeyEnabled()
    End If
    'Else
    'If ((GetAsyncKeyState(keyPush) And KeyDownBit) = KeyDownBit) And ((GetAsyncKeyState(CombPush) And KeyDownBit) = KeyDownBit) Then
    '  While GetAsyncKeyState(keyPush)
    '  End While
    '  While GetAsyncKeyState(CombPush)
    '  End While

    '  KeyEnabled()
    'End If
    'End If
  End Sub

  Private Sub ComboBoxClick_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxClick.SelectedIndexChanged
    Modes()
  End Sub

  Private Sub KeyEnabled()
    'Check click
    If My.Settings.mode = 1 Then
      'Check ON OFF
      If Clickers = False Then
        'Change Icon and setting
        DelayRunning.Interval = InvDelay
        Clickers = True
        NotifyIcon1.Icon = My.Resources.fishon
        'Check repeat
        FishClicker()
      Else
        DelayRunning.Enabled = False
        Clickers = False
        NotifyIcon1.Icon = My.Resources.fishoff
      End If
    ElseIf My.Settings.mode = 2 Then

      Select Case Clickers
        Case True
          Clickers = False
          LeftFuncion.Enabled = False
          DelayRunning.Enabled = False
          NotifyIcon1.Icon = My.Resources.mineoff
        Case False
          Clickers = True
          NotifyIcon1.Icon = My.Resources.mineon
      End Select

      If Unlimited = True Then
        UnlimitedBreak()
      Else
        Select Case longSelect
          Case True
            SecVal = longBySelect
            milLong = False
            MilVal = 0
          Case False
            MilSecCheck()
        End Select

        BreakRepeat()
      End If
    End If
  End Sub

  Private Sub MilSecCheck()
    If LongMine.ToString.Contains(",") Then
      milLong = True

      Dim SplitLong() As String = LongMine.Split(",")
      SecVal = SplitLong(0)
      Integer.TryParse(SplitLong(1), MilVal)

      MilVal *= 100
      MsgBox(MilVal)
    Else
      milLong = False
      MilVal = 0
      SecVal = LongMine
    End If
  End Sub

  Private Sub UnlimitedBreak()
    If Clickers = False Then
      mouse_event(&H4, 0, 0, 0, 1) ' cursor goes up
    Else
      mouse_event(&H2, 0, 0, 0, 1) 'cursor will go down (like a click)
    End If
  End Sub

  Private Sub BreakRepeat()
    If CheckBox1.Checked = True Then
      repeatEnabled = True
      getRepeat = Repeat - 1
    Else
      repeatEnabled = False
    End If

    Breaking()
  End Sub

  Sub Breaking()
    If Clickers = True Then
      DelayRunning.Interval = InvDelay
      LeftFuncion.Interval = 1000
      getSec = SecVal - 1
      LeftFuncion.Enabled = True
      mouse_event(&H2, 0, 0, 0, 1) 'cursor will go down (like a click)
    End If
  End Sub

  Private Sub LeftFuncion_Tick(sender As Object, e As EventArgs) Handles LeftFuncion.Tick
    Select Case getSec
      Case 0
        Select Case milLong
          Case True
            LeftFuncion.Interval = MilVal
            milLong = False
          Case False
            LeftFuncion.Enabled = False
            LeftFuncion.Interval = 1000
            mouse_event(&H4, 0, 0, 0, 1) ' cursor goes up

            Select Case repeatEnabled
              Case True
                If getRepeat < 1 Then
                  Clickers = False
                  NotifyIcon1.Icon = My.Resources.mineoff
                Else
                  getRepeat -= 1
                  DelayRunning.Enabled = True
                End If
              Case False
                DelayRunning.Enabled = True
            End Select
        End Select
      Case Else
        getSec -= 1
    End Select
  End Sub

  Private Sub TimerFishing_Tick(sender As Object, e As EventArgs) Handles DelayRunning.Tick
    If My.Settings.mode = 1 Then
      'Untuk Afk Fish

      If repeatEnabled = True Then
        Select Case getRepeat
          Case 0
            DelayRunning.Enabled = False
            Clickers = False
            NotifyIcon1.Icon = My.Resources.fishoff
          Case Else
            AFKFish()
            getRepeat -= 1
        End Select
      Else
        AFKFish()
      End If
    Else
      ' Untuk Breaker
      DelayRunning.Enabled = False
      getSec = SecVal
      If MilVal > 0 Then
        milLong = True
      End If

      LeftFuncion.Enabled = True
      mouse_event(&H2, 0, 0, 0, 1) 'cursor will go down (like a click)
    End If
  End Sub

  Private Sub FishClicker()
    AFKFish()
    If CheckBox1.Checked = False Then
      repeatEnabled = False
    Else
      repeatEnabled = True
      getRepeat = Repeat - 1
    End If
    DelayRunning.Enabled = True
  End Sub

  Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    Repeater()
  End Sub

  Private Sub TxtRepeat_ValueChanged(sender As Object, e As EventArgs) Handles TxtRepeat.ValueChanged
    Repeat = TxtRepeat.Value
  End Sub

  Private Sub TxtDelay_ValueChanged(sender As Object, e As EventArgs) Handles TxtDelay.ValueChanged
    InvDelay = TxtDelay.Value
  End Sub

  Private Sub TxtLong_ValueChanged(sender As Object, e As EventArgs) Handles TxtLong.ValueChanged
    LongMine = TxtLong.Value
  End Sub
End Class
