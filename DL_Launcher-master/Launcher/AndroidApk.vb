Imports System.IO

Public Class SelectClient
    Dim ip As String, port As String

    Private Sub AndroidApk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Height = 410
        Width = 270
        ' Read from main form via Login.Instance (avoids Protected MainForm issues)
        If Login.Instance IsNot Nothing Then
            ip = Login.Instance.IPBox.Text
            port = Login.Instance.PortBox.Text
        End If
        IPAddr.Text = ip & ":" & port
    End Sub

    Private Sub Label2_Click_1(sender As Object, e As EventArgs) Handles Label2.Click
        ' 打開根目錄下的 apks 資料夾（相對於 Digital_Life_Server_Refactored）
        Dim serverRoot As String = IO.Path.GetFullPath(Application.StartupPath & "\..\..\..\..")
        Dim apksDir As String = IO.Path.Combine(serverRoot, "apks")
        If Directory.Exists(apksDir) Then
            Shell("cmd /c explorer.exe """ & apksDir & """", vbHide)
        Else
            MsgBox("找不到 apks 資料夾：" & vbCrLf & apksDir, MsgBoxStyle.Exclamation, "提示")
        End If
    End Sub

    Private Sub ClientDetector_Tick(sender As Object, e As EventArgs) Handles ClientDetector.Tick
        ' 當已有客戶端連接（TextStatus="已连接"）時，自動關閉手機端選擇視窗。
        If Login.Instance IsNot Nothing AndAlso Login.Instance.TextStatus.Text.Contains("已连接") Then
            ClientDetector.Enabled = False
            Close()
        End If
    End Sub
End Class
