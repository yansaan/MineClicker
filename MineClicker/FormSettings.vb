Public Class FormSettings
    Dim KeysData As Integer
    Private Sub FormSettings_Load(sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler Me.KeyDown, AddressOf SavePress
    End Sub

    Private Sub SavePress(ByVal o As Object, ByVal e As KeyEventArgs)
        e.SuppressKeyPress = True
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
            MsgBox("this number")
        End If
        TextBox1.Text = e.KeyData.ToString + " / " + KeysData.ToString

    End Sub

    Private Sub FormSettings_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.RunningKey.Enabled = True
    End Sub
End Class