import os
import unittest
from utils.config import load_config, get_llm_config, get_character_config, get_server_config


class TestConfigUtils(unittest.TestCase):
    def test_load_config_fallback(self):
        cfg = load_config("non_existent_config.yaml")
        self.assertIn("server", cfg)
        self.assertIn("llm", cfg)
        self.assertEqual(cfg["server"]["port"], 38438)

    def test_get_llm_config_defaults(self):
        cfg = {
            "llm": {
                "base_url": "http://localhost:1234/v1",
                "model": "test-model",
                "api_key": "test-key"
            }
        }
        llm = get_llm_config(cfg)
        self.assertEqual(llm["base_url"], "http://localhost:1234/v1")
        self.assertEqual(llm["model"], "test-model")
        self.assertEqual(llm["api_key"], "test-key")

    def test_get_character_config(self):
        cfg = {
            "characters": {
                "paimon": {
                    "enabled": True,
                    "tts_model_path": "custom/paimon.pth",
                    "prompt_file": "custom/prompt.txt"
                }
            }
        }
        char_cfg = get_character_config(cfg, "paimon")
        self.assertTrue(char_cfg["enabled"])
        self.assertEqual(char_cfg["tts_model_path"], "custom/paimon.pth")

        char_cfg_missing = get_character_config(cfg, "unknown")
        self.assertTrue(char_cfg_missing["enabled"])
        self.assertIsNone(char_cfg_missing["tts_model_path"])

    def test_get_server_config(self):
        cfg = {
            "server": {
                "host": "127.0.0.1",
                "port": 9000
            }
        }
        srv = get_server_config(cfg)
        self.assertEqual(srv["host"], "127.0.0.1")
        self.assertEqual(srv["port"], 9000)


if __name__ == "__main__":
    unittest.main()
