Imports System.Runtime.InteropServices
Public Class Form1
    <DllImport("user32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
    End Function

    Const KeyDownBit As Integer = &H8000

    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)

    ReadOnly keyPush As Integer = My.Settings.KeyPush
    ReadOnly CombPush As Integer = My.Settings.Combination
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboLongClick.Items.Add("Stone Pickaxe")
        ComboLongClick.Items.Add("Iron Pickaxe")
        ComboLongClick.Items.Add("Diamond Pickaxe")
        ComboLongClick.Items.Add("Custom Time")
        ComboLongClick.Text = "Iron Pickaxe"
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
        End If
    End Sub

    Private Sub BreakFunction_CheckedChanged(sender As Object, e As EventArgs) Handles BreakFunction.CheckedChanged
        If FishingFunction.Checked = True Then
            FishingFunction.Checked = False
            BreakFunction.Checked = True
        End If
    End Sub

    Private Sub TimerFishing_Tick(sender As Object, e As EventArgs) Handles TimerFishing.Tick
        AFKFish()
    End Sub

    Private Sub RunningKey_Tick(sender As Object, e As EventArgs) Handles RunningKey.Tick
        If (GetAsyncKeyState(keyPush) And KeyDownBit) = KeyDownBit Then
            While GetAsyncKeyState(keyPush)
            End While
            If TimerFishing.Enabled = False Then
                TimerFishing.Enabled = True
            ElseIf TimerFishing.Enabled = True Then
                TimerFishing.Enabled = False
            End If
        End If
    End Sub

    Private Sub TxtDelay_TextChanged(sender As Object, e As EventArgs) Handles TxtDelay.TextChanged
        TimerFishing.Interval = TxtDelay.Text
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click
        FormSettings.ShowDialog()
        RunningKey.Enabled = False
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        FrmAbout.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
End Class
