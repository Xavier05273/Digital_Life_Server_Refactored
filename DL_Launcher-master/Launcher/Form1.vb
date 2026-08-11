Imports System.Threading
Imports System.IO
Imports System.Text

Public Class Login
    ' Shared instance for cross-form access (avoids Protected MainForm issues)
    Private Shared _instance As Login = Nothing

    Public Shared ReadOnly Property Instance() As Login
        Get
            Return _instance
        End Get
    End Property

    Dim model(), characterName() As String
    Dim apiKey, chatVer, proxy, stream, chara, brainWash As String
    Dim ip, port As String
    Dim apiKeyConfig, chatVerConfig, proxyConfig, actionBarText, showCommand, charaConfig, showLog As String
    Dim lastLine As String
    Dim configFile = Application.StartupPath & "/Config/config.ini"
    Dim modelFile = Application.StartupPath & "/Config/model.ini"
    ' Path to root config.yaml (same depth as start.bat)
    Dim configYamlPath As String = IO.Path.GetFullPath(Application.StartupPath & "\..\..\..\..\config.yaml")

    Private Sub Login_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ' 防止最小化/最大化恢復後視窗高度或寬度跑掉
        If Me.WindowState = FormWindowState.Normal Then
            Dim expectedHeight As Integer = 680
            If showCommand = "False" Then
                expectedHeight = 640
            End If

            ' 只修正偏大的情況，避免干擾正常操作
            If Me.Height > expectedHeight + 10 Then
                Me.Height = expectedHeight
            End If

            Dim expectedWidth As Integer = 470
            If CheckLog.Checked OrElse (showLog = "True") Then
                expectedWidth = 1060
            End If

            If Me.Width > expectedWidth + 10 Then
                Me.Width = expectedWidth
            End If
        End If
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _instance = Me

        If File.Exists("loglauncher.log") Then File.Delete("loglauncher.log")
        actionBarText = " 《数字生命》" & ReadConfig("uiconfig", "version", configFile)
        Thread.Sleep(500)
        ActionBar.Text = actionBarText
        logData.BackColor = Color.FromArgb(231, 231, 231)
        CheckForIllegalCrossThreadCalls = False
        Height = 680
        Width = 470
        If File.Exists("log.log") Then File.Delete("log.log")
        File.WriteAllText("log.log", "")
        File.WriteAllText("logdata.txt", "")

        ' Legacy ini reads (kept for backward compat; UI fields now sourced from config.yaml below)
        apiKeyConfig = ReadConfig("uiconfig", "apikey", configFile)
        chatVerConfig = ReadConfig("uiconfig", "chatver", configFile)
        proxyConfig = ReadConfig("uiconfig", "proxy", configFile)
        charaConfig = ReadConfig("uiconfig", "character", configFile)
        Dim customInputConfig As String = ReadConfig("uiconfig", "custominput", configFile)
        Dim serverAddrConfig As String = ReadConfig("uiconfig", "serveraddr", configFile)
        Dim serverPortConfig As String = ReadConfig("uiconfig", "serverport", configFile)
        showLog = ReadConfig("uiconfig", "showlog", configFile)
        showCommand = ReadConfig("uiconfig", "showcommand", configFile)

        ' Load config.yaml into a simple dictionary (llm.base_url, llm.api_key, llm.model, server.host, server.port)
        Dim yaml As New Dictionary(Of String, String)
        Log("configYamlPath = " & configYamlPath)
        If IO.File.Exists(configYamlPath) Then
            yaml = ReadConfigYaml(configYamlPath)
            Log("ReadConfigYaml keys: " & String.Join(", ", yaml.Keys))
        Else
            Log("config.yaml not found at: " & configYamlPath)
        End If

        ' Use config.yaml values as primary source for the 5 input fields.
        Dim llmBaseUrl As String = Trim(DictGet(yaml, "llm.base_url", ""))
        Dim llmApiKey As String = Trim(DictGet(yaml, "llm.api_key", ""))
        Dim llmModel As String = Trim(DictGet(yaml, "llm.model", ""))
        Dim serverHost As String = Trim(DictGet(yaml, "server.host", "0.0.0.0"))
        Dim serverPortStr As String = Trim(DictGet(yaml, "server.port", "38438"))

        ' If config.yaml is empty for a field, fall back to legacy ini / defaults.
        If llmBaseUrl = "" Then llmBaseUrl = apiKeyConfig
        If llmApiKey = "" Then llmApiKey = proxyConfig
        If llmModel = "" Then llmModel = customInputConfig
        If serverHost = "0.0.0.0" AndAlso serverAddrConfig <> "" Then serverHost = serverAddrConfig
        If serverPortStr = "38438" AndAlso serverPortConfig <> "" Then serverPortStr = serverPortConfig

        Log("llm.base_url=" & llmBaseUrl)
        Log("llm.api_key=" & llmApiKey)
        Log("llm.model=" & llmModel)
        Log("server.host=" & serverHost)
        Log("server.port=" & serverPortStr)

        ' ComboIPSelect removed; IP now fixed to localhost.
        ip = " --ip 127.0.0.1"

        If showCommand = "True" Then
            Height = 680
        Else
            Height = 640
        End If

        Log("Starting launcher.")

        If Not Directory.Exists("GPT\prompts") Then
            Shell("cmd /c echo d | xcopy GPT\prompts_default GPT\prompts")
            Log("Prompts not exist, copied.")
        Else
            Log("Prompts folder exist, skip copy.")
        End If

        If showLog = "True" Then
            Width = 1060
        Else
            Width = 470
        End If

        ' API Key (maps to llm.api_key in config.yaml)
        apiKey = ""
        TextProxServer.Text = llmApiKey

        If llmApiKey <> "" Then
            apiKey = " --APIKey " & llmApiKey
        End If

        ' Base URL field: populate from config.yaml (llm.base_url)
        TextAPIKey.Text = llmBaseUrl

        ' Proxy: no longer used; kept for backward compat only.
        proxy = ""

        ' Always API mode (chatVer=3)
        chatVer = " --chatVer 3"
        chatVerConfig = "3"

        ' Stream: fixed True (no UI toggle)
        stream = " --stream True"

        ' Brainwash: fixed False (no UI toggle)
        brainWash = " --brainwash False"

        ' Character: fixed to paimon only.
        ComboCharacterSelect.Items.Clear()
        ComboCharacterSelect.Items.Add("paimon")
        ComboCharacterSelect.Text = "paimon"

        chara = " --character paimon"

        ' Custom input (maps to llm.model in config.yaml)
        If llmModel <> "" Then
            TextCustomInput.Text = llmModel
        End If

        ' Server settings from config.yaml
        TextServerAddr.Text = serverHost
        TextServerPort.Text = serverPortStr

        UpdateCommand()

    End Sub

    Private Sub CheckLog_CheckedChanged(sender As Object, e As EventArgs) Handles CheckLog.CheckedChanged
        If CheckLog.Checked Then
            Width = 1070
        Else
            Width = 470
        End If
    End Sub

    Private Sub CheckLog_NewCheckedChanged(sender As Object, e As EventArgs) Handles CheckLog.Click
        If CheckLog.Checked Then
            Width = 1060
        Else
            Width = 470
        End If
    End Sub

    ' ComboIPSelect_SelectedIndexChanged removed; IP is fixed to 127.0.0.1.

    Private Sub LaunchPH_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub ActionBar_Click(sender As Object, e As EventArgs) Handles ActionBar.Click
        If showCommand = "True" Then
            showCommand = "False"
            WriteConfig("uiconfig", "showcommand", "False", configFile)
            Height = 640
        Else
            showCommand = "True"
            WriteConfig("uiconfig", "showcommand", "True", configFile)
            Height = 680
        End If
    End Sub

    Private Sub TextStatus_Click_1(sender As Object, e As EventArgs) Handles TextStatus.Click
        If TextStatus.Text = "服务器已启动！点击此处选择客户端" Then
            SelectClient.Show()
        End If
    End Sub

    Private Sub Editor_Click(sender As Object, e As EventArgs) Handles Editor.Click
        PromptEditor.Show()
    End Sub

    ' BtnNewAction1: 開啟電腦端 (啟動 T.exe)
    Private Sub BtnNewAction1_Click(sender As Object, e As EventArgs) Handles BtnNewAction1.Click
        Dim ip As String = Me.IPBox.Text
        Dim port As String = Me.PortBox.Text
        If ip <> "" AndAlso port <> "" Then
            ' 使用根目錄 Client\T.exe（與 start.bat 同層）
            Dim serverRoot As String = IO.Path.GetFullPath(Application.StartupPath & "\..\..\..\..")
            Dim tExe As String = IO.Path.Combine(serverRoot, "Client", "T.exe")
            Log("Starting desktop client T.exe from: " & tExe)
            Shell("cmd /c """ & tExe & """ -ip=" & ip & " -port=" & port, vbHide)
        Else
            Msgbox2(Me, "提示", "伺服器尚未啟動，無法開啟電腦端。", , False)
        End If
    End Sub

    ' BtnNewAction2: 打開手機端 (顯示 APK/QR 表單)
    Private Sub BtnNewAction2_Click(sender As Object, e As EventArgs) Handles BtnNewAction2.Click
        Dim ip As String = Me.IPBox.Text
        Dim port As String = Me.PortBox.Text
        If ip <> "" AndAlso port <> "" Then
            SelectClient.Show()
        Else
            Msgbox2(Me, "提示", "伺服器尚未啟動，無法開啟手機端。", , False)
        End If
    End Sub

    Private Sub MaterialButton1_Click(sender As Object, e As EventArgs) Handles Launch.Click
        SaveConfig()
        ' 清空根目錄（與 start.bat 同層）的 log_async.log，避免舊日誌累積（被佔用則跳過）
        Dim serverRoot As String = IO.Path.GetFullPath(Application.StartupPath & "\..\..\..\..")
        Dim logFile As String = IO.Path.Combine(serverRoot, "log_async.log")
        Try
            File.WriteAllText(logFile, "")
        Catch ex As Exception
            ' 日誌被佔用時不阻擋啟動，僅記錄
            Log("Warning: could not clear log_async.log (in use): " & ex.Message)
        End Try
        Dim launcher As New Thread(AddressOf StartServer)
        launcher.Start()
    End Sub

    Sub StartServer()
        Log("Starting socketserver.")
        Launch.Text = "启动中"
        TextStatus.Text = "正在启动..."
        TextStatus.BackColor = Color.Red

        ' Kill existing server / client processes
        Shell("cmd /c taskkill /f /t /im SocketServer.exe", vbHide)
        Shell("cmd /c taskkill /f /t /im T.exe", vbHide)
        SelectClient.Close()
        Thread.Sleep(1000)

        ' Determine Digital_Life_Server_Refactored root:
        ' bin/Debug -> Launcher -> DL_Launcher-master -> Digital_Life_Server_Refactored (4 levels up)
        Dim serverRoot As String = IO.Path.GetFullPath(Application.StartupPath & "\..\..\..\..")
        Dim startBat As String = IO.Path.Combine(serverRoot, "start.bat")

        If Not IO.File.Exists(startBat) Then
            Log("Error: start.bat not found at " & startBat)
            TextStatus.Text = "找不到 start.bat"
            TextStatus.BackColor = Color.Red
            Launch.Text = "启动服务器"
            Exit Sub
        End If

        ' Launch via start.bat (hidden; logs go to log_async.log and UI)
        Dim cmd As String = "cmd /c cd /d """ & serverRoot & """ && start.bat"
        Shell(cmd, vbHide)
        Log("Starting server via start.bat from: " & serverRoot)

        ' Use the same log file as the Python server (in server root)
        Dim logFile As String = IO.Path.Combine(serverRoot, "log_async.log")

        Dim logContent, newPart, logSpliter(), lastContent, totalContent() As String
        Dim totalLine As Integer
        Dim lastLength As Integer = 0

        Do

            Thread.Sleep(1000)
            Try
                Using fs As New FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite Or FileShare.Delete)
                    Using sr As New StreamReader(fs, Encoding.UTF8)
                        logContent = sr.ReadToEnd()
                    End Using
                End Using
            Catch ex As Exception
                ' If file is missing or locked, skip this cycle
                Log("Error reading log_async.log: " & ex.Message)
                Continue Do
            End Try

            ' Only inspect newly appended lines so each event fires once (no flicker).
            newPart = ""
            If logContent.Length > lastLength Then
                newPart = logContent.Substring(lastLength)
                lastLength = logContent.Length
            End If

            logSpliter = Split(logContent, " INFO ")
            totalContent = Split(logContent, vbCrLf)
            totalLine = UBound(totalContent)
            logData.Text = logContent + vbCrLf
            logData.SelectionStart = logData.Text.Length
            logData.ScrollToCaret()

            Try
                lastContent = logSpliter(UBound(logSpliter))

                ' Initializing / offline: only act on new "Initializing Server" lines.
                If newPart.Contains("Initializing Server") Then
                    Dim procOutput As String = SC("tasklist | findstr /i SocketServer.exe").Trim()
                    Dim tasklistRaw As String = SC("tasklist /fi ""IMAGENAME eq SocketServer.exe""").Trim()

                    ' Diagnostic logs: help identify why "服务器进程已退出" appears on some machines.
                    Log("[DIAG] newPart contains Initializing Server")
                    Log("[DIAG] tasklist|findstr output length=" & procOutput.Length)
                    If Not String.IsNullOrEmpty(procOutput) Then
                        Log("[DIAG] tasklist|findstr raw: " & procOutput.Replace(vbCrLf, "; "))
                    End If
                    Log("[DIAG] tasklistRaw length=" & tasklistRaw.Length)
                    If Not String.IsNullOrEmpty(tasklistRaw) Then
                        Log("[DIAG] tasklistRaw: " & tasklistRaw.Replace(vbCrLf, "; "))
                    End If

                    ' Primary check: if findstr found anything non-empty, consider it online.
                    Dim isOnline As Boolean = (Not String.IsNullOrEmpty(procOutput)) OrElse (Not String.IsNullOrEmpty(tasklistRaw))

                    If Not isOnline Then
                        ' 真的沒有程序在跑 → 才判定為崩潰
                        Log("Socketserver status: offline !!!")
                        TextStatus.Text = "未启动"
                        TextStatus.BackColor = Color.Red
                        logData.AppendText(vbCrLf + "服务器进程已退出！")
                        Shell("cmd /c taskkill /f /t /im T.exe", vbHide)

                        Exit Do
                    Else
                        Log("Socketserver status: online.")
                    End If
                End If

                ' Server is listening on: fire only when this line first appears.
                If newPart.Contains("Server is listening on") Then
                    Log("Waiting for connection...")

                    ' Extract address from "Server is listening on ('0.0.0.0', 38438)..."
                    Dim listenIdx As Integer = InStr(logContent, "Server is listening on")
                    Dim afterText As String = Mid(logContent, listenIdx + Len("Server is listening on"))

                    ' Example: ("0.0.0.0", 38438)...
                    Dim endParen As Integer = InStr(afterText, ")")
                    If endParen > 0 Then
                        Dim addrPart As String = afterText.Substring(0, endParen - 1).Trim()

                        ' Remove parentheses and quotes
                        addrPart = addrPart.Replace("(", "").Replace(")", "").Replace("""", "")

                        Dim parts() As String = Split(addrPart, ",")
                        If parts.Length >= 2 Then
                            port = Trim(parts(1))
                        End If
                    End If

                    ' 顯示本機 LAN IP（手機端用），T.exe 仍走 127.0.0.1
                    Dim localIp As String = GetLocalIP()
                    ip = " --ip 127.0.0.1"
                    Launch.Text = "重启服务器"
                    TextStatus.Text = "服务已启动。点击此处选择客户端"
                    IPBox.Text = localIp
                    PortBox.Text = Replace(port, vbCrLf, "")
                    TextStatus.BackColor = Color.FromArgb(0, 192, 0)
                    Thread.Sleep(2000)

                End If

                ' Connected by: fire only when this line first appears.
                If newPart.Contains("Connected by") Then
                    Log("Client connected.")

                    Launch.Text = "重启服务器"
                    TextStatus.Text = "已连接"
                    SelectClient.Close()

                End If

                ' WinError (disconnect): fire only on newly appeared WinError lines.
                If newPart.Contains("WinError") Then
                    Log("Client disconnected.")

                    Launch.Text = "重启服务器"
                    TextStatus.Text = "服务已启动。点击此处选择客户端"
                    TextStatus.BackColor = Color.FromArgb(0, 192, 0)

                End If

                logData.Focus()
                logData.ScrollToCaret()

            Catch ex As Exception
                Log("Error in log loop: " & ex.Message)
            End Try
        Loop

        logData.Height = 584
        Dim ipRawData() As String

        ipRawData = Split(File.ReadAllText("logdata.txt"), "Server is listening on ")

    End Sub

    Private Sub ComboCharacterSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboCharacterSelect.SelectedIndexChanged
        chara = " --character " & ComboCharacterSelect.Text
        UpdateCommand()
    End Sub

    Private Sub TextAPIKey_TextChanged(sender As Object, e As EventArgs) Handles TextAPIKey.TextChanged
        If TextAPIKey.Text = "" Then
            apiKey = ""
        Else
            apiKey = " --APIKey " & TextAPIKey.Text
        End If
        UpdateCommand()
    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click
        SaveConfig()
        Msgbox2(Me, "成功", "已保存配置！", , False, )
    End Sub

    Private Sub TextProxServer_TextChanged(sender As Object, e As EventArgs) Handles TextProxServer.TextChanged
        If TextProxServer.Text = "" Then
            proxy = ""
        Else
            proxy = " --proxy " & TextProxServer.Text
        End If
        UpdateCommand()
    End Sub

    Private Sub TextCustomInput_TextChanged(sender As Object, e As EventArgs) Handles TextCustomInput.TextChanged
        ' 目前僅保留值，用途待定義
    End Sub

    Sub UpdateCommand()
        ' Build command for API mode only (chatVer=3)
        para.Text = "SocketServer.exe " & chatVer & apiKey & ip & proxy & stream & brainWash & chara
        Log("Command update：" & para.Text)

    End Sub

    Private Sub Login_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Shell("cmd /c taskkill /f /t /im SocketServer.exe", vbHide)
        Shell("cmd /c taskkill /f /t /im Launcher.exe", vbHide)
    End Sub

    Sub SaveConfig()
        ' Always API mode (chatVer=3), stream=True, brainwash=False (no UI toggles)
        WriteConfig("uiconfig", "chatver", "3", configFile)
        WriteConfig("uiconfig", "apikey", TextAPIKey.Text, configFile)
        WriteConfig("uiconfig", "proxy", TextProxServer.Text, configFile)

        ' Stream fixed True
        WriteConfig("uiconfig", "stream", "True", configFile)

        ' Brainwash fixed False
        WriteConfig("uiconfig", "brainwash", "False", configFile)

        WriteConfig("uiconfig", "character", ComboCharacterSelect.Text, configFile)

        ' Custom input (用途待定義，先保留)
        WriteConfig("uiconfig", "custominput", TextCustomInput.Text, configFile)

        ' Server settings
        WriteConfig("uiconfig", "serveraddr", TextServerAddr.Text, configFile)
        WriteConfig("uiconfig", "serverport", TextServerPort.Text, configFile)

        If CheckLog.Checked Then
            WriteConfig("uiconfig", "showlog", "True", configFile)
        Else
            WriteConfig("uiconfig", "showlog", "False", configFile)
        End If

        ' Also write the 5 fields into config.yaml (primary source of truth)
        Dim values As New Dictionary(Of String, String)
        values("llm.base_url") = Trim(TextAPIKey.Text)
        values("llm.api_key") = Trim(TextProxServer.Text)
        values("llm.model") = Trim(TextCustomInput.Text)
        values("server.host") = Trim(TextServerAddr.Text)
        values("server.port") = Trim(TextServerPort.Text)

        If IO.File.Exists(configYamlPath) Then
            WriteConfigYaml(configYamlPath, values)
        End If
    End Sub

    ' Simple YAML reader for our config.yaml structure (llm.*, server.*)
    Private Function ReadConfigYaml(path As String) As Dictionary(Of String, String)
        Dim result As New Dictionary(Of String, String)
        Try
            Dim lines = IO.File.ReadAllLines(path, Encoding.UTF8)
            Dim currentSection As String = ""

            For Each line In lines
                Dim trimmed = line.Trim()
                If trimmed = "" OrElse trimmed.StartsWith("#") Then Continue For

                ' Detect top-level section: no leading space/tab and ends with ":"
                Dim isIndented As Boolean = (line.Length > 0 AndAlso (Char.IsWhiteSpace(line(0)) OrElse line(0) = vbTab))
                If Not isIndented AndAlso trimmed.EndsWith(":") Then
                    currentSection = trimmed.Substring(0, trimmed.Length - 1).Trim().ToLower()
                    Continue For
                End If

                ' Key-value under a section: must be indented and contain ":"
                If isIndented AndAlso trimmed.Contains(":") AndAlso Not String.IsNullOrEmpty(currentSection) Then
                    Dim colonIdx As Integer = trimmed.IndexOf(":"c)
                    Dim keyPart = trimmed.Substring(0, colonIdx).Trim()
                    Dim valuePart = trimmed.Substring(colonIdx + 1).Trim()

                    ' Strip quotes and braces if present
                    valuePart = valuePart.TrimStart("{").TrimEnd("}").Replace("""", "").Replace("'", "").Trim()

                    Dim fullKey As String = currentSection & "." & keyPart
                    result(fullKey) = valuePart
                End If
            Next

        Catch ex As Exception
            ' On parse error, return what we have; server will still use defaults.
        End Try

        Return result
    End Function

    ' Simple YAML writer that updates only the specified keys in-place.
    Private Sub WriteConfigYaml(path As String, values As Dictionary(Of String, String))
        Try
            Dim lines = IO.File.ReadAllLines(path, Encoding.UTF8)
            Dim sb As New StringBuilder()
            Dim currentSection As String = ""

            For Each line In lines
                Dim trimmed = line.Trim()

                ' Skip empty/comment lines as-is
                If trimmed = "" OrElse trimmed.StartsWith("#") Then
                    sb.AppendLine(line)
                    Continue For
                End If

                ' Detect top-level section (no leading whitespace, ends with ":")
                Dim isIndented As Boolean = (line.Length > 0 AndAlso (Char.IsWhiteSpace(line(0)) OrElse line(0) = vbTab))
                If Not isIndented AndAlso trimmed.EndsWith(":") Then
                    currentSection = trimmed.Substring(0, trimmed.Length - 1).Trim().ToLower()
                    sb.AppendLine(line)
                    Continue For
                End If

                ' Indented key-value under a section
                If isIndented AndAlso trimmed.Contains(":") AndAlso Not String.IsNullOrEmpty(currentSection) Then
                    Dim colonIdx As Integer = trimmed.IndexOf(":"c)
                    Dim keyPart = trimmed.Substring(0, colonIdx).Trim()
                    Dim fullKey As String = currentSection & "." & keyPart

                    ' Check if we have a new value for this key
                    If values.ContainsKey(fullKey) Then
                        Dim indent As String = line.Substring(0, line.Length - trimmed.Length)
                        Dim val As String = values(fullKey)

                        ' Quote as string unless it's purely numeric (for port etc.)
                        Dim isNumeric As Boolean = False
                        Try
                            If Not String.IsNullOrEmpty(val) Then
                                Dim testVal As Double
                                isNumeric = Double.TryParse(val, testVal)
                            End If
                        Catch
                            isNumeric = False
                        End Try

                        Dim q As String = Chr(34) ' double quote character
                        If isNumeric Then
                            sb.AppendLine(indent & keyPart & ": " & val)
                        Else
                            sb.AppendLine(indent & keyPart & ": " & q & val & q)
                        End If
                    Else
                        sb.AppendLine(line)
                    End If
                Else
                    ' Not a target line; keep as-is
                    sb.AppendLine(line)
                End If
            Next

            IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8)
        Catch ex As Exception
            ' Silently ignore; user still gets ini save and a toast.
        End Try
    End Sub

    ' Helper: safe dictionary get with default (for older VB.NET without GetValueOrDefault)
    Private Function DictGet(Of K, V)(d As Dictionary(Of K, V), key As K, [default] As V) As V
        Dim value As V = Nothing
        If d.TryGetValue(key, value) Then
            Return value
        Else
            Return [default]
        End If
    End Function

    ' 取得本機 LAN IPv4（排除 127.0.0.1），失敗時回傳 127.0.0.1
    Private Function GetLocalIP() As String
        Try
            Dim host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())
            For Each ipEntry In host.AddressList
                If ipEntry.AddressFamily = Net.Sockets.AddressFamily.InterNetwork AndAlso _
                   Not System.Net.IPAddress.IsLoopback(ipEntry) Then
                    Return ipEntry.ToString()
                End If
            Next
        Catch ex As Exception
            ' 抓不到時 fallback
        End Try
        Return "127.0.0.1"
    End Function

End Class
