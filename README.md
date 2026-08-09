# Digital Life Server Refactored

> ⚠️ **專案狀態：持續維護中（Active Development）**
> 本專案為原始 **Digital Life Server** 的社群維護版本，目標是修復過時 API、更新相依套件，並讓專案能夠在現代環境重新運作。

**原始專案：** https://github.com/zixiiu/Digital_Life_Server

---

## 專案介紹

Digital Life Server Refactored 是一個重新整理與維護的後端伺服器，用於驅動 **Digital Life** 語音 AI 角色。

目前支援透過 **LM Studio**（或其他 OpenAI Compatible API）連接本地大型語言模型，並可直接與原版 UE 用戶端 **T.exe** 相容。

---

# 功能特色

* ✅ 支援 OpenAI Compatible API（LM Studio、Ollama、vLLM...）
* ✅ 提供 Linux / WSL / Windows 一鍵啟動腳本
* ✅ 相容原版 UE Client（T.exe）

---

# 快速開始

## Linux / WSL

### 需求

* Python 3.10 以上（建議 3.11）
* Git
* C/C++ 編譯器（build-essential、python3-dev）
* LM Studio（或其他 OpenAI Compatible API）

### 安裝

```bash
cd ~

git clone https://github.com/Xavier05273/Digital_Life_Server_Refactored.git

cd Digital_Life_Server_Refactored

chmod +x setup.sh start.sh

./setup.sh
```

首次安裝將會：

* 建立 Python 虛擬環境（`.venv`）
* 安裝所有 Python 相依套件
* 初始化 TTS Submodule
* 編譯 `monotonic_align`

---

## Windows

### 需求

* Python 3.10 以上（建議 3.11）
* Git
* Visual Studio（Desktop development with C++）
* LM Studio（或其他 OpenAI Compatible API）

### 安裝

```bash
git clone https://github.com/Xavier05273/Digital_Life_Server_Refactored.git

cd Digital_Life_Server_Refactored

.\setup.bat
```

首次安裝將會：

* 建立 Python 虛擬環境（`.venv`）
* 安裝所有 Python 相依套件
* 初始化 TTS Submodule
* 編譯 `monotonic_align`

---

# 設定

請編輯：

```text
config.yaml
```

## LLM

設定你的 LLM API：

### LM Studio（本機）

```text
http://127.0.0.1:1234/v1
```
或是其他模型提供商

另外請修改：

* `llm.model`
* 如需要，可填寫API

依照自己的環境調整。

---

# 模型下載

模型請自行下載（Repository 不包含模型檔）。

可從以下來源取得：

* [Google Drive](https://drive.google.com/file/d/1_jp826uLmK8qT6BCJ_iyaU6WB9RSljv0/view?usp=sharing)（建議）
* [百度網盤](https://pan.baidu.com/s/1EnHDPADNdhDl71x_DHeElg?pwd=75gr)

下載完成後請放置於：

```text
Digital_Life_Server_Refactored/

├── ASR/resources/models/
├── SentimentEngine/models/
└── TTS/models/
```

---

# 啟動 LLM

以 LM Studio 為例：

1. 開啟 LM Studio
2. 進入 **Local Server**
3. 點擊 **Start Server**
4. 如有需要，開啟 **Allow connections from local network**
5. 載入模型（例如 `gemma-4-e4b-abliterated`）

---

# 啟動伺服器

## Linux / WSL

```bash
./start.sh
```

## Windows

```bat
.\start.bat
```

伺服器會依照 `config.yaml` 中的設定啟動。

預設：

```text
0.0.0.0:38438
```

等待 UE Client 連線。

---

# 連接 UE Client

下載：

**[T.exe](https://drive.google.com/drive/folders/1FWgK3M2Mh2gyF9gVj6v2vmvTcCn27UM9?usp=sharing)**

設定：

* Host：`127.0.0.1`
* Port：`38438`

若使用 Docker 或遠端部署，請改成對應 IP。

若使用 Android 版本連線，需要允許 Windows 防火牆接收 TCP 38438 連線。

---

# Docker（實驗性）

> ⚠️ 目前尚未完整測試。

若已安裝 Docker：

```bash
docker compose up --build
```

可透過修改：

* `docker-compose.yml`
* `OPENAI_BASE_URL`
* `LLM_MODEL`

切換模型或 API。

---

# 專案架構

* **ASR**：語音辨識（Paraformer）
* **LLM**：OpenAI Compatible API（LM Studio、Ollama、vLLM）
* **TTS**：VITS 語音合成
* **SentimentEngine**：ONNX 情緒分析
* **SocketServer.py**：負責與 UE Client 通訊

---

# 注意事項

* 本專案為 **Digital Life Server** 的重新維護版本。
* 已移除舊版 ChatGPT / revChatGPT 相依套件。
* 以 Python 3.10+ 為主要開發與測試環境。
* 歡迎提交 Issue、Pull Request 或提供改進建議。

---

# 致謝

本專案基於原作者 **Hupa** 及其團隊發布的 **Digital Life Server**（MIT License）進行維護與更新。

感謝原作者提供開源專案，讓社群得以在此基礎上持續改進與發展。
