import os
import yaml


def load_config(path: str = "config.yaml") -> dict:
    """
    Load config.yaml; if missing, return a safe default.
    Environment variables and CLI args should override this in each service.
    """
    if not os.path.exists(path):
        # Fallback defaults (for backward compatibility)
        return {
            "server": {"host": "0.0.0.0", "port": 38438},
            "llm": {
                "base_url": "http://127.0.0.1:1234/v1",
                "model": "gpt-4o-mini",
                "api_key": "",
            },
            "characters": {},
        }

    with open(path, "r", encoding="utf-8") as f:
        cfg = yaml.safe_load(f) or {}

    # Ensure basic structure exists
    cfg.setdefault("server", {})
    cfg["server"].setdefault("host", "0.0.0.0")
    cfg["server"].setdefault("port", 38438)

    llm = cfg.setdefault("llm", {})
    llm.setdefault("base_url", "http://127.0.0.1:1234/v1")
    llm.setdefault("model", "gpt-4o-mini")
    llm.setdefault("api_key", "")

    cfg.setdefault("characters", {})

    return cfg


def get_llm_config(cfg: dict, args=None):
    """
    Resolve LLM settings with priority:
      CLI arg > env var > config.yaml > hard-coded default.
    """
    llm = cfg.get("llm", {})

    base_url = (
        getattr(args, "base_url", None) or
        os.getenv("OPENAI_BASE_URL") or
        llm.get("base_url") or
        "http://127.0.0.1:1234/v1"
    )

    model = (
        getattr(args, "model", None) or
        os.getenv("LLM_MODEL") or
        llm.get("model") or
        "gpt-4o-mini"
    )

    api_key = (
        getattr(args, "APIKey", None) or
        os.getenv("OPENAI_API_KEY") or
        llm.get("api_key") or
        ""
    )

    return {
        "base_url": base_url,
        "model": model,
        "api_key": api_key or "lm-studio",  # LM Studio default
    }


def get_character_config(cfg: dict, character_name: str):
    """
    Get config for a specific character (e.g. 'paimon').
    Returns dict with keys: enabled, tts_model_path, prompt_file.
    """
    chars = cfg.get("characters", {})
    char_cfg = chars.get(character_name.lower(), {})

    return {
        "enabled": char_cfg.get("enabled", True),
        "tts_model_path": char_cfg.get("tts_model_path"),
        "prompt_file": char_cfg.get("prompt_file"),
    }


def get_server_config(cfg: dict, args=None):
    """
    Resolve server bind config with priority:
      CLI arg > env var > config.yaml > default.
    """
    srv = cfg.get("server", {})

    host = (
        getattr(args, "host", None) or
        os.getenv("SERVER_HOST") or
        srv.get("host") or
        "0.0.0.0"
    )

    port = int(
        getattr(args, "port", None) or
        os.getenv("SERVER_PORT") or
        srv.get("port", 38438)
    )

    return {"host": host, "port": port}
