<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits MaterialSkin.Controls.MaterialForm

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupGPTVersion = New System.Windows.Forms.GroupBox()
        Me.ComboCharacterSelect = New MaterialSkin.Controls.MaterialComboBox()
        Me.TextProxServer = New MaterialSkin.Controls.MaterialMaskedTextBox()
        Me.TextAPIKey = New MaterialSkin.Controls.MaterialMaskedTextBox()
        Me.TextCustomInput = New MaterialSkin.Controls.MaterialMaskedTextBox()
        Me.GroupServerSettings = New System.Windows.Forms.GroupBox()
        Me.TextServerAddr = New MaterialSkin.Controls.MaterialMaskedTextBox()
        Me.TextServerPort = New MaterialSkin.Controls.MaterialMaskedTextBox()
        Me.Launch = New MaterialSkin.Controls.MaterialButton()
        Me.ActionBar = New System.Windows.Forms.Label()
        Me.para = New System.Windows.Forms.TextBox()
        Me.logData = New System.Windows.Forms.TextBox()
        Me.Save = New MaterialSkin.Controls.MaterialButton()
        Me.TextStatus = New System.Windows.Forms.Label()
        Me.CheckLog = New MaterialSkin.Controls.MaterialCheckbox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Editor = New MaterialSkin.Controls.MaterialButton()
        Me.BtnNewAction1 = New MaterialSkin.Controls.MaterialButton()
        Me.BtnNewAction2 = New MaterialSkin.Controls.MaterialButton()
        Me.IPBox = New System.Windows.Forms.TextBox()
        Me.PortBox = New System.Windows.Forms.TextBox()
        Me.GroupGPTVersion.SuspendLayout()
        Me.GroupServerSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupGPTVersion
        '
        Me.GroupGPTVersion.BackColor = System.Drawing.SystemColors.Control
        Me.GroupGPTVersion.Controls.Add(Me.ComboCharacterSelect)
        Me.GroupGPTVersion.Controls.Add(Me.TextProxServer)
        Me.GroupGPTVersion.Controls.Add(Me.TextAPIKey)
        Me.GroupGPTVersion.Controls.Add(Me.TextCustomInput)
        Me.GroupGPTVersion.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.GroupGPTVersion.Location = New System.Drawing.Point(24, 88)
        Me.GroupGPTVersion.Name = "GroupGPTVersion"
        Me.GroupGPTVersion.Size = New System.Drawing.Size(424, 270)
        Me.GroupGPTVersion.TabIndex = 3
        Me.GroupGPTVersion.TabStop = False
        Me.GroupGPTVersion.Text = "URL 與 API 設定"
        '
        'GroupServerSettings
        '
        Me.GroupServerSettings.BackColor = System.Drawing.SystemColors.Control
        Me.GroupServerSettings.Controls.Add(Me.TextServerPort)
        Me.GroupServerSettings.Controls.Add(Me.TextServerAddr)
        Me.GroupServerSettings.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.GroupServerSettings.Location = New System.Drawing.Point(24, 368)
        Me.GroupServerSettings.Name = "GroupServerSettings"
        Me.GroupServerSettings.Size = New System.Drawing.Size(424, 150)
        Me.GroupServerSettings.TabIndex = 45
        Me.GroupServerSettings.TabStop = False
        Me.GroupServerSettings.Text = "服務器設定"
        '
        'TextServerAddr
        '
        Me.TextServerAddr.AllowPromptAsInput = True
        Me.TextServerAddr.AnimateReadOnly = False
        Me.TextServerAddr.AsciiOnly = False
        Me.TextServerAddr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TextServerAddr.BeepOnError = False
        Me.TextServerAddr.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextServerAddr.Depth = 0
        Me.TextServerAddr.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.TextServerAddr.HidePromptOnLeave = False
        Me.TextServerAddr.HideSelection = True
        Me.TextServerAddr.Hint = "填入服務器地址(建議保留預設0.0.0.0)"
        Me.TextServerAddr.InsertKeyMode = System.Windows.Forms.InsertKeyMode.[Default]
        Me.TextServerAddr.LeadingIcon = Nothing
        Me.TextServerAddr.Location = New System.Drawing.Point(24, 42)
        Me.TextServerAddr.Mask = ""
        Me.TextServerAddr.MaxLength = 32767
        Me.TextServerAddr.MouseState = MaterialSkin.MouseState.OUT
        Me.TextServerAddr.Name = "TextServerAddr"
        Me.TextServerAddr.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextServerAddr.PrefixSuffixText = Nothing
        Me.TextServerAddr.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.TextServerAddr.ReadOnly = False
        Me.TextServerAddr.RejectInputOnFirstFailure = False
        Me.TextServerAddr.ResetOnPrompt = True
        Me.TextServerAddr.ResetOnSpace = True
        Me.TextServerAddr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextServerAddr.SelectedText = ""
        Me.TextServerAddr.SelectionLength = 0
        Me.TextServerAddr.SelectionStart = 0
        Me.TextServerAddr.ShortcutsEnabled = True
        Me.TextServerAddr.Size = New System.Drawing.Size(376, 28)
        Me.TextServerAddr.SkipLiterals = True
        Me.TextServerAddr.TabIndex = 46
        Me.TextServerAddr.TabStop = False
        Me.TextServerAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextServerAddr.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextServerAddr.TrailingIcon = Nothing
        Me.TextServerAddr.UseSystemPasswordChar = False
        Me.TextServerAddr.UseTallSize = False
        Me.TextServerAddr.ValidatingType = Nothing
        '
        'TextServerPort
        '
        Me.TextServerPort.AllowPromptAsInput = True
        Me.TextServerPort.AnimateReadOnly = False
        Me.TextServerPort.AsciiOnly = False
        Me.TextServerPort.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TextServerPort.BeepOnError = False
        Me.TextServerPort.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextServerPort.Depth = 0
        Me.TextServerPort.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.TextServerPort.HidePromptOnLeave = False
        Me.TextServerPort.HideSelection = True
        Me.TextServerPort.Hint = "填入伺服器端口(建議保留預設38438)"
        Me.TextServerPort.InsertKeyMode = System.Windows.Forms.InsertKeyMode.[Default]
        Me.TextServerPort.LeadingIcon = Nothing
        Me.TextServerPort.Location = New System.Drawing.Point(24, 90)
        Me.TextServerPort.Mask = ""
        Me.TextServerPort.MaxLength = 32767
        Me.TextServerPort.MouseState = MaterialSkin.MouseState.OUT
        Me.TextServerPort.Name = "TextServerPort"
        Me.TextServerPort.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextServerPort.PrefixSuffixText = Nothing
        Me.TextServerPort.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.TextServerPort.ReadOnly = False
        Me.TextServerPort.RejectInputOnFirstFailure = False
        Me.TextServerPort.ResetOnPrompt = True
        Me.TextServerPort.ResetOnSpace = True
        Me.TextServerPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextServerPort.SelectedText = ""
        Me.TextServerPort.SelectionLength = 0
        Me.TextServerPort.SelectionStart = 0
        Me.TextServerPort.ShortcutsEnabled = True
        Me.TextServerPort.Size = New System.Drawing.Size(376, 28)
        Me.TextServerPort.SkipLiterals = True
        Me.TextServerPort.TabIndex = 47
        Me.TextServerPort.TabStop = False
        Me.TextServerPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextServerPort.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextServerPort.TrailingIcon = Nothing
        Me.TextServerPort.UseSystemPasswordChar = False
        Me.TextServerPort.UseTallSize = False
        Me.TextServerPort.ValidatingType = Nothing
        '
        'ComboCharacterSelect
        '
        Me.ComboCharacterSelect.AutoResize = False
        Me.ComboCharacterSelect.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ComboCharacterSelect.Depth = 0
        Me.ComboCharacterSelect.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.ComboCharacterSelect.DropDownHeight = 174
        Me.ComboCharacterSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboCharacterSelect.DropDownWidth = 121
        Me.ComboCharacterSelect.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.ComboCharacterSelect.ForeColor = System.Drawing.Color.FromArgb(CType(CType(222, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ComboCharacterSelect.FormattingEnabled = True
        Me.ComboCharacterSelect.Hint = "角色选择"
        Me.ComboCharacterSelect.IntegralHeight = False
        Me.ComboCharacterSelect.ItemHeight = 43
        Me.ComboCharacterSelect.Location = New System.Drawing.Point(24, 182)
        Me.ComboCharacterSelect.MaxDropDownItems = 4
        Me.ComboCharacterSelect.MouseState = MaterialSkin.MouseState.OUT
        Me.ComboCharacterSelect.Name = "ComboCharacterSelect"
        Me.ComboCharacterSelect.Size = New System.Drawing.Size(376, 49)
        Me.ComboCharacterSelect.StartIndex = 0
        Me.ComboCharacterSelect.TabIndex = 36
        '
        'TextCustomInput
        '
        Me.TextCustomInput.AllowPromptAsInput = True
        Me.TextCustomInput.AnimateReadOnly = False
        Me.TextCustomInput.AsciiOnly = False
        Me.TextCustomInput.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TextCustomInput.BeepOnError = False
        Me.TextCustomInput.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextCustomInput.Depth = 0
        Me.TextCustomInput.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.TextCustomInput.HidePromptOnLeave = False
        Me.TextCustomInput.HideSelection = True
        Me.TextCustomInput.Hint = "填入模型名"
        Me.TextCustomInput.InsertKeyMode = System.Windows.Forms.InsertKeyMode.[Default]
        Me.TextCustomInput.LeadingIcon = Nothing
        Me.TextCustomInput.Location = New System.Drawing.Point(24, 126)
        Me.TextCustomInput.Mask = ""
        Me.TextCustomInput.MaxLength = 32767
        Me.TextCustomInput.MouseState = MaterialSkin.MouseState.OUT
        Me.TextCustomInput.Name = "TextCustomInput"
        Me.TextCustomInput.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextCustomInput.PrefixSuffixText = Nothing
        Me.TextCustomInput.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.TextCustomInput.ReadOnly = False
        Me.TextCustomInput.RejectInputOnFirstFailure = False
        Me.TextCustomInput.ResetOnPrompt = True
        Me.TextCustomInput.ResetOnSpace = True
        Me.TextCustomInput.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextCustomInput.SelectedText = ""
        Me.TextCustomInput.SelectionLength = 0
        Me.TextCustomInput.SelectionStart = 0
        Me.TextCustomInput.ShortcutsEnabled = True
        Me.TextCustomInput.Size = New System.Drawing.Size(376, 28)
        Me.TextCustomInput.SkipLiterals = True
        Me.TextCustomInput.TabIndex = 44
        Me.TextCustomInput.TabStop = False
        Me.TextCustomInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextCustomInput.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextCustomInput.TrailingIcon = Nothing
        Me.TextCustomInput.UseSystemPasswordChar = False
        Me.TextCustomInput.UseTallSize = False
        Me.TextCustomInput.ValidatingType = Nothing
        '
        'TextProxServer
        '
        Me.TextProxServer.AllowPromptAsInput = True
        Me.TextProxServer.AnimateReadOnly = False
        Me.TextProxServer.AsciiOnly = False
        Me.TextProxServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TextProxServer.BeepOnError = False
        Me.TextProxServer.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextProxServer.Depth = 0
        Me.TextProxServer.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.TextProxServer.HidePromptOnLeave = False
        Me.TextProxServer.HideSelection = True
        Me.TextProxServer.Hint = "填入API (若無則留空)"
        Me.TextProxServer.InsertKeyMode = System.Windows.Forms.InsertKeyMode.[Default]
        Me.TextProxServer.LeadingIcon = Nothing
        Me.TextProxServer.Location = New System.Drawing.Point(24, 84)
        Me.TextProxServer.Mask = ""
        Me.TextProxServer.MaxLength = 32767
        Me.TextProxServer.MouseState = MaterialSkin.MouseState.OUT
        Me.TextProxServer.Name = "TextProxServer"
        Me.TextProxServer.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextProxServer.PrefixSuffixText = Nothing
        Me.TextProxServer.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.TextProxServer.ReadOnly = False
        Me.TextProxServer.RejectInputOnFirstFailure = False
        Me.TextProxServer.ResetOnPrompt = True
        Me.TextProxServer.ResetOnSpace = True
        Me.TextProxServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextProxServer.SelectedText = ""
        Me.TextProxServer.SelectionLength = 0
        Me.TextProxServer.SelectionStart = 0
        Me.TextProxServer.ShortcutsEnabled = True
        Me.TextProxServer.Size = New System.Drawing.Size(376, 28)
        Me.TextProxServer.SkipLiterals = True
        Me.TextProxServer.TabIndex = 22
        Me.TextProxServer.TabStop = False
        Me.TextProxServer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextProxServer.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.ToolTip1.SetToolTip(Me.TextProxServer, "请在开始之前确保你的网络可以访问chatGPT")
        Me.TextProxServer.TrailingIcon = Nothing
        Me.TextProxServer.UseSystemPasswordChar = False
        Me.TextProxServer.UseTallSize = False
        Me.TextProxServer.ValidatingType = Nothing
        '
        'TextAPIKey
        '
        Me.TextAPIKey.AllowPromptAsInput = True
        Me.TextAPIKey.AnimateReadOnly = False
        Me.TextAPIKey.AsciiOnly = False
        Me.TextAPIKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.TextAPIKey.BeepOnError = False
        Me.TextAPIKey.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextAPIKey.Depth = 0
        Me.TextAPIKey.Font = New System.Drawing.Font("微软雅黑", 10.0!)
        Me.TextAPIKey.HidePromptOnLeave = False
        Me.TextAPIKey.HideSelection = True
        Me.TextAPIKey.Hint = "填入模型供應商URL"
        Me.TextAPIKey.InsertKeyMode = System.Windows.Forms.InsertKeyMode.[Default]
        Me.TextAPIKey.LeadingIcon = Nothing
        Me.TextAPIKey.Location = New System.Drawing.Point(24, 42)
        Me.TextAPIKey.Mask = ""
        Me.TextAPIKey.MaxLength = 32767
        Me.TextAPIKey.MouseState = MaterialSkin.MouseState.OUT
        Me.TextAPIKey.Name = "TextAPIKey"
        Me.TextAPIKey.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextAPIKey.PrefixSuffixText = Nothing
        Me.TextAPIKey.PromptChar = Global.Microsoft.VisualBasic.ChrW(95)
        Me.TextAPIKey.ReadOnly = False
        Me.TextAPIKey.RejectInputOnFirstFailure = False
        Me.TextAPIKey.ResetOnPrompt = True
        Me.TextAPIKey.ResetOnSpace = True
        Me.TextAPIKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextAPIKey.SelectedText = ""
        Me.TextAPIKey.SelectionLength = 0
        Me.TextAPIKey.SelectionStart = 0
        Me.TextAPIKey.ShortcutsEnabled = True
        Me.TextAPIKey.Size = New System.Drawing.Size(376, 28)
        Me.TextAPIKey.SkipLiterals = True
        Me.TextAPIKey.TabIndex = 18
        Me.TextAPIKey.TabStop = False
        Me.TextAPIKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextAPIKey.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals
        Me.TextAPIKey.TrailingIcon = Nothing
        Me.TextAPIKey.UseSystemPasswordChar = False
        Me.TextAPIKey.UseTallSize = False
        Me.TextAPIKey.ValidatingType = Nothing
        '
        'Launch
        '
        Me.Launch.AutoSize = False
        Me.Launch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Launch.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.Launch.Depth = 0
        Me.Launch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Launch.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Launch.HighEmphasis = True
        Me.Launch.Icon = Nothing
        Me.Launch.Location = New System.Drawing.Point(24, 582)
        Me.Launch.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Launch.MouseState = MaterialSkin.MouseState.HOVER
        Me.Launch.Name = "Launch"
        Me.Launch.NoAccentTextColor = System.Drawing.Color.Empty
        Me.Launch.Size = New System.Drawing.Size(104, 40)
        Me.Launch.TabIndex = 15
        Me.Launch.Text = "启动服务器"
        Me.ToolTip1.SetToolTip(Me.Launch, "启动服务")
        Me.Launch.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.Launch.UseAccentColor = False
        Me.Launch.UseVisualStyleBackColor = True
        '
        'ActionBar
        '
        Me.ActionBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(63, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(181, Byte), Integer))
        Me.ActionBar.Font = New System.Drawing.Font("微软雅黑", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ActionBar.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ActionBar.Location = New System.Drawing.Point(0, 24)
        Me.ActionBar.Name = "ActionBar"
        Me.ActionBar.Size = New System.Drawing.Size(1128, 48)
        Me.ActionBar.TabIndex = 16
        Me.ActionBar.Text = " 《数字生命》"
        Me.ActionBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'para
        '
        Me.para.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.para.Location = New System.Drawing.Point(24, 638)
        Me.para.Multiline = True
        Me.para.Name = "para"
        Me.para.Size = New System.Drawing.Size(1024, 24)
        Me.para.TabIndex = 26
        '
        'logData
        '
        Me.logData.BackColor = System.Drawing.SystemColors.Menu
        Me.logData.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.logData.Location = New System.Drawing.Point(480, 96)
        Me.logData.Multiline = True
        Me.logData.Name = "logData"
        Me.logData.ReadOnly = True
        Me.logData.Size = New System.Drawing.Size(568, 526)
        Me.logData.TabIndex = 30
        '
        'Save (Button 1: leftmost)
        '
        Me.Save.AutoSize = False
        Me.Save.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Save.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.Save.Depth = 0
        Me.Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Save.HighEmphasis = True
        Me.Save.Icon = Nothing
        Me.Save.Location = New System.Drawing.Point(24, 530)
        Me.Save.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Save.MouseState = MaterialSkin.MouseState.HOVER
        Me.Save.Name = "Save"
        Me.Save.NoAccentTextColor = System.Drawing.Color.Empty
        Me.Save.Size = New System.Drawing.Size(99, 40)
        Me.Save.TabIndex = 31
        Me.Save.Text = "保存配置"
        Me.ToolTip1.SetToolTip(Me.Save, "保存所有配置项")
        Me.Save.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.Save.UseAccentColor = False
        Me.Save.UseVisualStyleBackColor = True
        '
        'TextStatus
        '
        Me.TextStatus.BackColor = System.Drawing.Color.Red
        Me.TextStatus.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.TextStatus.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.TextStatus.Location = New System.Drawing.Point(133, 582)
        Me.TextStatus.Name = "TextStatus"
        Me.TextStatus.Size = New System.Drawing.Size(218, 40)
        Me.TextStatus.TabIndex = 33
        Me.TextStatus.Text = "未启动"
        Me.TextStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CheckLog
        '
        Me.CheckLog.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CheckLog.Depth = 0
        Me.CheckLog.Location = New System.Drawing.Point(370, 584)
        Me.CheckLog.Margin = New System.Windows.Forms.Padding(0)
        Me.CheckLog.MouseLocation = New System.Drawing.Point(-1, -1)
        Me.CheckLog.MouseState = MaterialSkin.MouseState.HOVER
        Me.CheckLog.Name = "CheckLog"
        Me.CheckLog.ReadOnly = False
        Me.CheckLog.Ripple = True
        Me.CheckLog.Size = New System.Drawing.Size(80, 40)
        Me.CheckLog.TabIndex = 40
        Me.CheckLog.Text = "日志"
        Me.ToolTip1.SetToolTip(Me.CheckLog, "显示实时日志")
        Me.CheckLog.UseVisualStyleBackColor = True
        '
        'Editor (Button 2)
        '
        Me.Editor.AutoSize = False
        Me.Editor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Editor.CharacterCasing = MaterialSkin.Controls.MaterialButton.CharacterCasingEnum.Lower
        Me.Editor.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.Editor.Depth = 0
        Me.Editor.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Editor.HighEmphasis = True
        Me.Editor.Icon = Nothing
        Me.Editor.Location = New System.Drawing.Point(133, 530)
        Me.Editor.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.Editor.MouseState = MaterialSkin.MouseState.HOVER
        Me.Editor.Name = "Editor"
        Me.Editor.NoAccentTextColor = System.Drawing.Color.Empty
        Me.Editor.Size = New System.Drawing.Size(99, 40)
        Me.Editor.TabIndex = 39
        Me.Editor.Text = "编辑提示词"
        Me.Editor.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.Editor.UseAccentColor = False
        Me.Editor.UseVisualStyleBackColor = True
        '
        'BtnNewAction1 (Button 3)
        '
        Me.BtnNewAction1.AutoSize = False
        Me.BtnNewAction1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BtnNewAction1.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.BtnNewAction1.Depth = 0
        Me.BtnNewAction1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnNewAction1.HighEmphasis = True
        Me.BtnNewAction1.Icon = Nothing
        Me.BtnNewAction1.Location = New System.Drawing.Point(242, 530)
        Me.BtnNewAction1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.BtnNewAction1.MouseState = MaterialSkin.MouseState.HOVER
        Me.BtnNewAction1.Name = "BtnNewAction1"
        Me.BtnNewAction1.NoAccentTextColor = System.Drawing.Color.Empty
        Me.BtnNewAction1.Size = New System.Drawing.Size(99, 40)
        Me.BtnNewAction1.TabIndex = 48
        Me.BtnNewAction1.Text = "開啟電腦端"
        Me.BtnNewAction1.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.BtnNewAction1.UseAccentColor = False
        Me.BtnNewAction1.UseVisualStyleBackColor = True
        '
        'BtnNewAction2 (Button 4)
        '
        Me.BtnNewAction2.AutoSize = False
        Me.BtnNewAction2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BtnNewAction2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.[Default]
        Me.BtnNewAction2.Depth = 0
        Me.BtnNewAction2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnNewAction2.HighEmphasis = True
        Me.BtnNewAction2.Icon = Nothing
        Me.BtnNewAction2.Location = New System.Drawing.Point(351, 530)
        Me.BtnNewAction2.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.BtnNewAction2.MouseState = MaterialSkin.MouseState.HOVER
        Me.BtnNewAction2.Name = "BtnNewAction2"
        Me.BtnNewAction2.NoAccentTextColor = System.Drawing.Color.Empty
        Me.BtnNewAction2.Size = New System.Drawing.Size(99, 40)
        Me.BtnNewAction2.TabIndex = 49
        Me.BtnNewAction2.Text = "打開手機端"
        Me.BtnNewAction2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained
        Me.BtnNewAction2.UseAccentColor = False
        Me.BtnNewAction2.UseVisualStyleBackColor = True
        '
        'IPBox
        '
        Me.IPBox.Location = New System.Drawing.Point(664, 840)
        Me.IPBox.Name = "IPBox"
        Me.IPBox.Size = New System.Drawing.Size(128, 21)
        Me.IPBox.TabIndex = 41
        '
        'PortBox
        '
        Me.PortBox.Location = New System.Drawing.Point(664, 864)
        Me.PortBox.Name = "PortBox"
        Me.PortBox.Size = New System.Drawing.Size(128, 21)
        Me.PortBox.TabIndex = 42
        '
        'Login
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1117, 1074)
        Me.Controls.Add(Me.BtnNewAction2)
        Me.Controls.Add(Me.BtnNewAction1)
        Me.Controls.Add(Me.PortBox)
        Me.Controls.Add(Me.IPBox)
        Me.Controls.Add(Me.Editor)
        Me.Controls.Add(Me.CheckLog)
        Me.Controls.Add(Me.TextStatus)
        Me.Controls.Add(Me.Save)
        Me.Controls.Add(Me.logData)
        Me.Controls.Add(Me.para)
        Me.Controls.Add(Me.ActionBar)
        Me.Controls.Add(Me.Launch)
        Me.Controls.Add(Me.GroupGPTVersion)
        Me.Controls.Add(Me.GroupServerSettings)
        Me.DrawerWidth = 400
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None
        Me.MaximizeBox = False
        Me.Name = "Login"
        Me.Padding = New System.Windows.Forms.Padding(3, 26, 3, 3)
        Me.Sizable = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "数字生命"
        Me.GroupGPTVersion.ResumeLayout(False)
        Me.GroupGPTVersion.PerformLayout()
        Me.GroupServerSettings.ResumeLayout(False)
        Me.GroupServerSettings.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupGPTVersion As GroupBox
    Friend WithEvents TextAPIKey As MaterialSkin.Controls.MaterialMaskedTextBox
    Friend WithEvents Launch As MaterialSkin.Controls.MaterialButton
    Friend WithEvents ActionBar As Label
    Friend WithEvents TextProxServer As MaterialSkin.Controls.MaterialMaskedTextBox
    Friend WithEvents para As TextBox
    Friend WithEvents logData As TextBox
    Friend WithEvents Save As MaterialSkin.Controls.MaterialButton
    Friend WithEvents TextStatus As Label
    Friend WithEvents ComboCharacterSelect As MaterialSkin.Controls.MaterialComboBox
    Friend WithEvents CheckLog As MaterialSkin.Controls.MaterialCheckbox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Editor As MaterialSkin.Controls.MaterialButton
    Friend WithEvents BtnNewAction1 As MaterialSkin.Controls.MaterialButton
    Friend WithEvents BtnNewAction2 As MaterialSkin.Controls.MaterialButton
    Friend WithEvents IPBox As TextBox
    Friend WithEvents PortBox As TextBox
    Friend WithEvents TextCustomInput As MaterialSkin.Controls.MaterialMaskedTextBox
    Friend WithEvents GroupServerSettings As GroupBox
    Friend WithEvents TextServerAddr As MaterialSkin.Controls.MaterialMaskedTextBox
    Friend WithEvents TextServerPort As MaterialSkin.Controls.MaterialMaskedTextBox
End Class
