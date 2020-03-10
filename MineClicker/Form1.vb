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

  Dim Clickers As Boolean = False
  Dim Repeat As Integer
  Dim InvDelay As Integer
  Dim repeatEnabled As Boolean
  Private getRepeat As Integer
  Dim TimeRepeat As Integer
  Dim secBreak As Integer
  Private LongMine As Integer
  Private getSec As Integer

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

    NotifyIcon1.Visible = True

    ComboLongClick.Items.Add("3 Seconds")
    ComboLongClick.Items.Add("5 Seconds")
    ComboLongClick.Items.Add("Unlimited")
    ComboLongClick.Items.Add("Custom...")

    If My.Settings.LongSelect = 0 Then
      ComboLongClick.Text = "Custom..."
    ElseIf My.Settings.LongSelect = 1 Then
      ComboLongClick.Text = "3 Seconds"
    ElseIf My.Settings.LongSelect = 2 Then
      ComboLongClick.Text = "5 Seconds"
    ElseIf My.Settings.LongSelect = 3 Then
      ComboLongClick.Text = "Unlimited"
    End If

    ComboBoxClick.Items.Add("AFK Fish (Right)")
    ComboBoxClick.Items.Add("Auto Coublestone (Left)")

    LongMine = My.Settings.TimeLong
    TxtLong.Text = LongMine

    InvDelay = My.Settings.TimeDelay
    TxtDelay.Text = InvDelay

    Repeat = My.Settings.TimeRepeat
    TxtRepeat.Text = Repeat

    Select Case CheckBox1.Checked
      Case True
        TxtRepeat.Enabled = True
      Case Else
        TxtRepeat.Enabled = False
    End Select

    If My.Settings.mode = 1 Then
      ComboBoxClick.Text = "AFK Fish (Right)"
      ComboLongClick.Enabled = False

      TxtLong.Enabled = False
    ElseIf My.Settings.mode = 2 Then
      ComboBoxClick.Text = "Auto Coublestone (Left)"
      NotifyIcon1.Icon = My.Resources.fishoff
      ComboLongClick.Enabled = True
      Select Case My.Settings.LongSelect
        Case 0
          TxtLong.Enabled = True
        Case Else
          TxtLong.Enabled = False
      End Select
      NotifyIcon1.Icon = My.Resources.mineoff
    End If

    If (My.Application.CommandLineArgs.Count > 0) Then
      If My.Application.CommandLineArgs(0) = "-set" Then
        Me.WindowState = FormWindowState.Minimized
        Me.ShowInTaskbar = False
      End If
    End If

    If Me.WindowState = FormWindowState.Normal Then
      TopFrm1()
    End If

    If My.Settings.EnabledRepeat Then
      CheckBox1.Checked = True
    End If
  End Sub

  Private Sub RunningKey_Tick(sender As Object, e As EventArgs) Handles RunningKey.Tick
    keyPush = My.Settings.KeyPush
    KeysString = My.Settings.KeyString

    My.Settings.TimeDelay = InvDelay
    My.Settings.TimeRepeat = Repeat
    My.Settings.TimeLong = LongMine

    If InvDelay = 0 Then
      Label7.Text = "AutoClick not running but input null"
    Else
      If InvDelay < 200 Then
        Label7.Text = "AutoClick not running when setting to minimal"
      ElseIf InvDelay > 99999 Then
        Label7.Text = "AutoClick not running when setting to maximal"
      Else
        Label7.Text = "Press (" + KeysString + ") for play and Stop when in Minecraft World"
        If Me.WindowState = FormWindowState.Minimized Then
          Runing()
        End If
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

  Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
    If Me.WindowState = FormWindowState.Minimized Then
      If TxtDelay.Text = "" Then
        TxtDelay.Text = 0
      End If
      If TxtLong.Text = "" Then
        TxtLong.Text = 0
      End If
      If TxtRepeat.Text = "" Then
        TxtRepeat.Text = 0
      End If

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
    If ComboLongClick.Text = "Custom..." Then
      My.Settings.LongSelect = 0
    ElseIf ComboLongClick.Text = "3 Seconds" Then
      My.Settings.LongSelect = 1
    ElseIf ComboLongClick.Text = "5 Seconds" Then
      My.Settings.LongSelect = 2
    ElseIf ComboLongClick.Text = "Unlimited" Then
      My.Settings.LongSelect = 3
    End If

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
      If LongMine = 0 Then
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
      End If
    End If
  End Sub

  Private Sub TxtLong_TextChanged(sender As Object, e As EventArgs) Handles TxtLong.TextChanged
    If My.Settings.mode <> 2 Then
      Return
    End If

    Select Case TxtLong.Text
      Case ""
        LongMine = 0
      Case Else
        LongMine = TxtLong.Text
    End Select

    If LongMine = 0 Then

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
    End If
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
    'Change Click mode
    If ComboBoxClick.Text = "AFK Fish (Right)" Then
      My.Settings.mode = 1
      ComboLongClick.Enabled = False
      TxtLong.Enabled = False

      CheckBox1.Enabled = True
      TxtRepeat.Enabled = True
      Select Case CheckBox1.Checked
        Case True
          TxtRepeat.Enabled = True
        Case Else
          TxtRepeat.Enabled = False
      End Select

      NotifyIcon1.Icon = My.Resources.fishoff
    ElseIf ComboBoxClick.Text = "Auto Coublestone (Left)" Then
      My.Settings.mode = 2
      ComboLongClick.Enabled = True
      Select Case My.Settings.LongSelect
        Case 0
          TxtLong.Enabled = True
        Case Else
          TxtLong.Enabled = False
      End Select
      NotifyIcon1.Icon = My.Resources.mineoff

      If TxtLong.Text = 0 Then
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
      End If
    End If
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
      Select Case TxtLong.Text
        Case 0
          If Clickers = True Then
            mouse_event(&H4, 0, 0, 0, 1)
            Clickers = False
            NotifyIcon1.Icon = My.Resources.mineoff
          Else
            Clickers = True
            NotifyIcon1.Icon = My.Resources.mineon
            mouse_event(&H2, 0, 0, 0, 1)
          End If
        Case Else
          If CheckBox1.Checked = False Or Repeat = 0 Then
            repeatEnabled = False
            Breaking()
          Else
            repeatEnabled = True
            BreakRepeat()
          End If
      End Select
    End If
  End Sub

  Private Sub BreakRepeat()
    If Clickers = False Then
      Clickers = True
      NotifyIcon1.Icon = My.Resources.mineon
      getRepeat = Repeat - 1

      getSec = LongMine - 1
      LeftFuncion.Enabled = True

      mouse_event(&H2, 0, 0, 0, 1)
    Else
      DelayRunning.Enabled = False
      LeftFuncion.Enabled = False
      mouse_event(&H4, 0, 0, 0, 1)

      Clickers = False
      NotifyIcon1.Icon = My.Resources.mineoff
    End If
  End Sub

  Private Sub FishClicker()
    AFKFish()
    If CheckBox1.Checked = False Or Repeat = 0 Then
      repeatEnabled = False
    Else
      repeatEnabled = True
      getRepeat = Repeat - 1
    End If
    DelayRunning.Enabled = True
  End Sub

  Private Sub TimerFishing_Tick(sender As Object, e As EventArgs) Handles DelayRunning.Tick
    'Untuk Afk Fish
    If My.Settings.mode = 1 Then

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
      If repeatEnabled = False Then
        DelayRunning.Enabled = False
        LeftFuncion.Enabled = True
        getSec = LongMine - 1
        mouse_event(&H2, 0, 0, 0, 1)
      Else
        Select Case getRepeat
          Case 0
            DelayRunning.Enabled = False
            Clickers = False
            NotifyIcon1.Icon = My.Resources.mineoff
          Case Else
            getRepeat -= 1

            DelayRunning.Enabled = False
            LeftFuncion.Enabled = True
            getSec = LongMine - 1
            mouse_event(&H2, 0, 0, 0, 1)
        End Select
      End If
    End If
  End Sub

  Sub Breaking()
    If Clickers = False Then
      Clickers = True
      NotifyIcon1.Icon = My.Resources.mineon
      getSec = LongMine - 1
      LeftFuncion.Enabled = True

      mouse_event(&H2, 0, 0, 0, 1)
    Else
      DelayRunning.Enabled = False
      LeftFuncion.Enabled = False
      mouse_event(&H4, 0, 0, 0, 1)

      Clickers = False
      NotifyIcon1.Icon = My.Resources.mineoff
    End If
  End Sub

  Private Sub LeftFuncion_Tick(sender As Object, e As EventArgs) Handles LeftFuncion.Tick
    Select Case getSec
      Case 0
        mouse_event(&H4, 0, 0, 0, 1)
        DelayRunning.Enabled = True
        LeftFuncion.Enabled = False
      Case Else
        getSec -= 1
    End Select
  End Sub

  Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
    Select Case CheckBox1.Checked
      Case True
        My.Settings.EnabledRepeat = True
        TxtRepeat.Enabled = True
      Case Else
        TxtRepeat.Enabled = False
        My.Settings.EnabledRepeat = False
    End Select
  End Sub

  Private Sub TxtDelay_TextChanged(sender As Object, e As EventArgs) Handles TxtDelay.TextChanged
    Select Case TxtDelay.Text
      Case ""
        InvDelay = 0
      Case Else
        InvDelay = TxtDelay.Text
    End Select
  End Sub

  Private Sub TxtRepeat_TextChanged(sender As Object, e As EventArgs) Handles TxtRepeat.TextChanged
    Select Case TxtRepeat.Text
      Case ""
        Repeat = 0
      Case Else
        Repeat = TxtRepeat.Text
    End Select
  End Sub
End Class
