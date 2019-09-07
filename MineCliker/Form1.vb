Public Class Form1
    Declare Sub mouse_event Lib "user32" Alias "mouse_event" (ByVal dwFlags As Long, ByVal dx As Long, ByVal dy As Long, ByVal cButtons As Long, ByVal dwExtraInfo As Long)
    Dim keyPush As Keys = Keys.K
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
        If e.KeyCode = keyPush Then
            AFKFish()
        End If
    End Sub

    Private Sub clickFunction()
        MsgBox(Cursor.Position.ToString)
    End Sub

    Private Sub AFKFish()
        Windows.Forms.Cursor.Position = New System.Drawing.Point(Windows.Forms.Cursor.Position)
        mouse_event(&H8, 0, 0, 0, 1) 'cursor will go down (like a click)
        mouse_event(&H10, 0, 0, 0, 1) ' cursor goes up again
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(Cursor.Position.ToString)
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

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged, TextBox3.TextChanged, TextBox2.TextChanged

    End Sub
End Class
