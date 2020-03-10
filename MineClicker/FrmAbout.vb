Public Class FrmAbout
  Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    TopAbout()
    NameProduct.Text = My.Application.Info.ProductName
    LblVer.Text = String.Format("Ver. {0}.{1}a", My.Application.Info.Version.Major.ToString, My.Application.Info.Version.Minor.ToString)
    Copyright.Text = My.Application.Info.Copyright
  End Sub

  Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
    Process.Start("https://fb.me/yansaanxyz")
  End Sub

  Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
    Process.Start("https://twitter.com/yansaan_")
  End Sub

  Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
    Process.Start("https://instagram.com/yansaan_")
  End Sub

  Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
    Process.Start("https://yansaan.carrd.co/#work")
  End Sub

  Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
    Process.Start("https://yansaan.carrd.co")
  End Sub

  Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
    Process.Start("https://github.com/yansaan/MineClicker")
  End Sub

  Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    Process.Start("https://yansaan.carrd.co/#mineclicker")
  End Sub

  Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
    Process.Start("https://patreon.com/yansaan_")
  End Sub

  Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
    Process.Start("https://karyakarsa.com/yansaan")
  End Sub
  Private Sub PictureBox11_Click(sender As Object, e As EventArgs) Handles PictureBox11.Click
    Process.Start("https://trakteer.id/yansaan")
  End Sub
End Class