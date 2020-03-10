Module TopForm
  Public Sub TopFrm1()
    If My.Settings.FrontEnable = True Then
      Form1.TopMost = True
    Else
      Form1.TopMost = False
    End If
  End Sub

  Public Sub TopSetting()
    If My.Settings.FrontEnable = True Then
      SettUtama.TopMost = True
    Else
      SettUtama.TopMost = False
    End If
  End Sub
  Public Sub TopSettKey()
    If My.Settings.FrontEnable = True Then
      FormSettings.TopMost = True
    Else
      FormSettings.TopMost = False
    End If
  End Sub
  Public Sub TopAbout()
    If My.Settings.FrontEnable = True Then
      FrmAbout.TopMost = True
    Else
      FrmAbout.TopMost = False
    End If
  End Sub
End Module
