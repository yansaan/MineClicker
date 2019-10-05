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
    Dim repeatEnabled As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboLongClick.Items.Add("Stone Pickaxe")
        ComboLongClick.Items.Add("Iron Pickaxe")
        ComboLongClick.Items.Add("Diamond Pickaxe")
        ComboLongClick.Items.Add("Custom Time")
        ComboLongClick.Text = "Custom Time"

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
        TxtDelay.Text = My.Settings.TimeDelay
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
            If Repeat = 0 Then
                TimerFishing.Enabled = False
            Else
                Repeat -= 1
            End If
        End If
    End Sub

    Private Sub RunningKey_Tick(sender As Object, e As EventArgs) Handles RunningKey.Tick
        comb = My.Settings.CombEnable
        keyPush = My.Settings.KeyPush
        CombPush = My.Settings.Combination

        If comb = True Then
            KeysString = My.Settings.combString + " + " + My.Settings.KeyString
        Else
            KeysString = My.Settings.KeyString
        End If

        My.Settings.TimeRepeat = TxtRepeat.Text

        If TxtDelay.Text = "" Or TxtLong.Text = "" Or TxtRepeat.Text = "" Then
            Label7.Text = "AutoClick not running but input null"
        Else
            If (TxtDelay.Text >= 200) Then
                Label7.Text = " Open Minecraft, and press (" + KeysString + ") for play and Stop"
                TimerFishing.Interval = TxtDelay.Text
                My.Settings.TimeDelay = TxtDelay.Text
                If RunClick = True Then
                    Runing()
                End If
            Else
                Label7.Text = "AutoClick not running when setting to minimal"
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
        If TimerFishing.Enabled = False Then
            TimerFishing.Enabled = True
            NotifyIcon1.Icon = My.Resources.Fish
        ElseIf TimerFishing.Enabled = True Then
            TimerFishing.Enabled = False
            NotifyIcon1.Icon = My.Resources.FishGrey
        End If
        Repeat = TxtRepeat.Text

        If Repeat > 0 Then
            repeatEnabled = True
        Else
            repeatEnabled = False
        End If
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        FormSettings.ShowDialog()
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmAbout.ShowDialog()
    End Sub

    Private Sub TxtDelay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDelay.KeyPress, TxtRepeat.KeyPress, TxtLong.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        RunClick = False
    End Sub

    Private Sub Form1_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
        If OpenAnotherForm = False Then
            RunClick = True
        End If
        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            Me.Hide()
        End If
    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick
        Me.Show()
        NotifyIcon1.Visible = False
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.Save()
    End Sub
End Class
