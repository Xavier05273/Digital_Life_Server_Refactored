# 繁體中文安裝教學（Ubuntu / WSL）

本教學將從零開始介紹如何部署 **Digital Life Server Refactored**。

---

# 一、準備環境

## 1. 安裝 WSL（已使用 Linux 可略過）

以系統管理員身分開啟 **PowerShell**：

```bash
wsl --install
```

重新開機後，再執行：

```bash
wsl --install -d Ubuntu-24.04
```

依照畫面設定 Ubuntu 使用者名稱與密碼即可。

---

## 2. 安裝必要套件

### 安裝 Python

```bash
sudo apt update
sudo apt install -y python3 python3-pip python3-venv
```

確認安裝成功：

```bash
python3 --version
```

---

### 安裝 C/C++ 編譯工具

```bash
sudo apt install -y build-essential python3-dev
```

---

### 安裝 Git

```bash
sudo apt install -y git
```

確認安裝：

```bash
git --version
```

---

# 二、下載專案

執行：

```bash
cd ~

git clone https://github.com/Xavier05273/Digital_Life_Server_Refactored.git

cd Digital_Life_Server_Refactored
```

給予執行權限：

```bash
chmod +x setup.sh start.sh
```

開始安裝：

```bash
./setup.sh
```

首次安裝可能需要幾分鐘。

---

# 三、設定模型

打開根目錄中的：

```text
config.yaml
```

建議使用 **Visual Studio Code** 或其他文字編輯器。

修改以下內容：

## LLM Base URL

LM Studio：

```text
http://127.0.0.1:1234/v1
```

Ollama：

```text
http://127.0.0.1:11434/v1
```

---

## LLM Model

請填入你實際使用的模型名稱，例如：

```text
gemma-4-e4b-abliterated
```

---

# 四、下載模型

請下載專案所需模型：

* [Google Drive](https://drive.google.com/file/d/1_jp826uLmK8qT6BCJ_iyaU6WB9RSljv0/view?usp=sharing)（建議）
* [百度網盤](https://pan.baidu.com/s/1EnHDPADNdhDl71x_DHeElg?pwd=75gr)

下載完成後，放入以下目錄：

```text
Digital_Life_Server_Refactored/
├── ASR/resources/models/
├── SentimentEngine/models/
└── TTS/models/
```

---

# 五、啟動伺服器

執行：

```bash
cd Digital_Life_Server_Refactored

./start.sh paimon
```

如果畫面出現：

```text
Server is listening on ('0.0.0.0', 38438)
```

表示伺服器已成功啟動。

---

# 六、連接圖形介面

下載：

**T.exe**

啟動後設定：

* Host：通常為 `127.0.0.1`
* Port：通常為 `38438`

若你的設定不同，請依照 `config.yaml` 修改即可。

---

# 常見問題

## setup.sh 執行失敗

請確認：

* Python 3 已安裝
* Git 已安裝
* 網路可正常連線

---

## 無法連接 LLM

請確認：

* LM Studio 或 Ollama 已啟動
* `base_url` 設定正確
* `model` 名稱與實際模型一致

---

## 無法找到模型

請再次確認模型已放入：

```text
ASR/resources/models/
SentimentEngine/models/
TTS/models/
```

並確認檔案完整下載。

