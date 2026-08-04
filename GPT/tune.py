import logging
import os


def get_tune(character, model=None):
    # 決定使用哪一版 prompt：3.5 / 4 / default
    m = (model or "").lower()

    if "3.5" in m:
        ver = "35"
    elif "4" in m:
        ver = "4"
    else:
        # 預設使用 4 版 prompt（或你自訂）
        ver = "4"

    filename = f"{character}{ver}.txt"

    # 優先讀 GPT/prompts/，若不存在則 fallback prompts_default
    path_custom = os.path.join("GPT", "prompts", filename)
    path_default = os.path.join("GPT", "prompts_default", filename)

    if os.path.exists(path_custom):
        logging.info(f'LLM prompt: {path_custom}')
        with open(path_custom, 'r', encoding='utf-8') as f:
            return f.read()
    elif os.path.exists(path_default):
        logging.info(f'LLM prompt (default): {path_default}')
        with open(path_default, 'r', encoding='utf-8') as f:
            return f.read()
    else:
        # 如果都沒有，回傳一個基本 system prompt
        logging.warning(
            f'Prompt file not found for character={character}, ver={ver}. '
            f'Using fallback system prompt.'
        )
        return (
            "You are a helpful and friendly AI assistant. "
            "Respond in short, natural sentences suitable for speech."
        )


exceed_reply = """
你問的太多了，我們的毛都被你撸禿了，你自己去準備一個API，或者一小時後再來吧。
"""

error_reply = """
你等一下，我連接不上大腦了。你是不是網有問題，或者是帳號填錯了？
"""
