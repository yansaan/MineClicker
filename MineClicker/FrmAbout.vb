Public Class FrmAbout
    Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.OpenAnotherForm = True
    End Sub

    Private Sub FrmAbout_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Form1.OpenAnotherForm = False
    End Sub
End Class