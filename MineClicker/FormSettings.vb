Public Class FormSettings
    Dim KeysData As Integer = My.Settings.KeyPush
    Dim KeyInput As String = My.Settings.KeyString
    Dim keyResult As String

    Dim comb As Boolean = My.Settings.CombEnable

    Dim SaveResult As String = ""

    Dim Savedkey As Integer = My.Settings.KeyPush
    Dim keyComb As Integer = My.Settings.Combination
    Dim keyCombSaved As Integer = My.Settings.Combination
    Dim combInput As String = My.Settings.combString
    Dim combLong As Boolean = False
    Private Sub FormSettings_Load(sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Form1.OpenAnotherForm = True

        If comb = False Then
            keyResult = KeyInput.ToString()
            Label2.Text = My.Settings.KeyPush.ToString
        Else
            keyResult = "(" + combInput + ") + (" + KeyInput.ToString() + ")"
            Label2.Text = keyComb.ToString + ", " + My.Settings.KeyPush.ToString
        End If
        SaveResult = keyResult


        LabelPress.Text = keyResult
        Label1.Text = ""
        AddHandler Me.KeyUp, AddressOf SavePress
    End Sub

    Private Sub DisableKey(sender As Object, e As KeyEventArgs)
        e.SuppressKeyPress = True

        If (e.KeyValue.ToString = "16" Or
            e.KeyValue.ToString = "17" Or
            e.KeyValue.ToString = "18") And KeysData = 0 Then
            keyComb = 0
            KeysData = Savedkey
            combLong = False

            LabelPress.Text = keyResult
        End If
    End Sub

    Private Sub SavePress(ByVal o As Object, ByVal e As KeyEventArgs)
        e.SuppressKeyPress = True
        If e.KeyValue.ToString = "9" Or e.KeyValue.ToString = "13" Then

        ElseIf e.KeyValue.ToString = "20" Or
        e.KeyValue.ToString = "144" Or
        e.KeyValue.ToString = "91" Or
        e.KeyValue.ToString = "93" Or
        e.KeyValue.ToString = "44" Or
        (e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9) Then
            Label1.Text = "This input not support"

        ElseIf e.KeyValue.ToString = "27" Then
            If combLong = True Then
                keyComb = keyCombSaved
                KeysData = Savedkey
                combLong = False

                Label1.Text = ""
            Else
                Label1.Text = "This input not support"
            End If

        ElseIf e.KeyValue.ToString = "16" Or
            e.KeyValue.ToString = "17" Or
            e.KeyValue.ToString = "18" Then
            KeysData = 0
            keyComb = e.KeyValue

            Label1.Text = "Press Esc for cencel combination"

            comb = True
            combLong = True

            If e.KeyValue.ToString = "16" Then
                combInput = "Shift"
            ElseIf e.KeyValue.ToString = "17" Then
                combInput = "Ctrl"
            ElseIf e.KeyValue.ToString = "18" Then
                combInput = "Alt"
            End If

            keyResult = "(" + combInput + ") + ()"
        Else
            KeysData = e.KeyValue
            Savedkey = e.KeyValue

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

            If combLong = False Then
                keyComb = 0
                keyCombSaved = 0
                comb = False
                keyResult = KeyInput + " / " + KeysData.ToString
            Else
                keyCombSaved = keyComb
                keyResult = "(" + combInput + ") + (" + KeyInput + ")"
            End If

            combLong = False

            SaveResult = keyResult
            Label1.Text = ""
        End If

        If e.KeyValue.ToString = "27" Then
            LabelPress.Text = SaveResult
        Else
            LabelPress.Text = keyResult
        End If

        If comb = True Then
            Label2.Text = keyComb.ToString + ", " + KeysData.ToString
        Else
            Label2.Text = KeysData.ToString
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ResetSetting As MsgBoxResult = MsgBox("", vbYesNo + vbQuestion, "Reseting")
        If ResetSetting = vbYes Then
            My.Settings.Reset()
            LabelPress.Text = "F6"
            Label2.Text = My.Settings.KeyPush.ToString

        End If
    End Sub

    Private Sub FormSettings_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.OpenAnotherForm = False
    End Sub
End Class