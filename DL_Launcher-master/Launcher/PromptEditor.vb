Imports System.IO

Public Class PromptEditor
    ' Fixed prompt file for Paimon (option A).
    Private ReadOnly ServerRoot As String = IO.Path.GetFullPath(Application.StartupPath & "\..\..\..\..")
    Private ReadOnly PromptFile As String = IO.Path.Combine(ServerRoot, "GPT", "prompts", "paimon4.txt")
    Private ReadOnly DefaultPromptFile As String = IO.Path.Combine(ServerRoot, "GPT", "prompts_default", "paimon4.txt")

    Private Sub PromptEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Height = 840
        ActionBar.Text = " 提示词编辑器 - Paimon"

        ' Load prompt content.
        If File.Exists(PromptFile) Then
            PromptTextBox.Text = File.ReadAllText(PromptFile)
        Else
            PromptTextBox.Text = "[未找到提示词]" & vbCrLf & _
                                 "路徑：" & PromptFile & vbCrLf & _
                                 "可點擊「恢复」從預設檔載入。"
        End If
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        Try
            ' Ensure directory exists.
            Dim dir As String = Path.GetDirectoryName(PromptFile)
            If Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            File.WriteAllText(PromptFile, PromptTextBox.Text)
            Msgbox2(Me, "成功", "已保存！点击确定关闭窗口", , False)
            Close()
        Catch ex As Exception
            Msgbox2(Me, "错误", "保存失败：" & vbCrLf & ex.Message, , False)
        End Try
    End Sub

    Private Sub Restore_Click(sender As Object, e As EventArgs) Handles Restore.Click
        Dim confirm As String = Msgbox2(Me, "提示", _
                                         "要恢复原始提示词吗？" & vbCrLf & _
                                         "当前提示词将被覆盖！", , True)
        If confirm <> "OK" Then Exit Sub

        Try
            ' Check default file exists.
            If Not File.Exists(DefaultPromptFile) Then
                Msgbox2(Me, "错误", "找不到原始提示词文件：" & vbCrLf & DefaultPromptFile, , False)
                Exit Sub
            End If

            ' Ensure prompts directory exists.
            Dim dir As String = Path.GetDirectoryName(PromptFile)
            If Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            ' Copy default over current prompt file.
            File.Copy(DefaultPromptFile, PromptFile, overwrite:=True)

            ' Refresh textbox with restored content.
            PromptTextBox.Text = File.ReadAllText(PromptFile)

            Msgbox2(Me, "成功", "已恢复原始提示词！", , False)
        Catch ex As Exception
            Msgbox2(Me, "错误", "恢复失败：" & vbCrLf & ex.Message, , False)
        End Try
    End Sub
End Class
