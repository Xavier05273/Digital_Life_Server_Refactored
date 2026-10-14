import unittest
from unittest.mock import MagicMock, patch
from GPT.GPTService import GPTService


class TestGPTService(unittest.TestCase):
    @patch("GPT.GPTService.OpenAI")
    @patch("GPT.GPTService.load_config")
    def test_build_messages(self, mock_load_config, mock_openai):
        mock_load_config.return_value = {}
        args = MagicMock()
        args.character = "paimon"
        args.model = "test-model"
        args.proxy = None
        args.proxy = None

        service = GPTService(args)
        messages = service._build_messages("Hello world")

        self.assertEqual(len(messages), 2)
        self.assertEqual(messages[0]["role"], "system")
        self.assertEqual(messages[1]["role"], "user")
        self.assertEqual(messages[1]["content"], "Hello world")

    @patch("GPT.GPTService.OpenAI")
    @patch("GPT.GPTService.load_config")
    def test_ask_stream_delimiters(self, mock_load_config, mock_openai):
        mock_load_config.return_value = {}
        args = MagicMock()
        args.character = "paimon"
        args.model = "test-model"

        mock_client = MagicMock()
        mock_openai.return_value = mock_client

        # Mock chunk stream from OpenAI API
        chunk1 = MagicMock()
        chunk1.choices = [MagicMock(delta=MagicMock(content="Hello world! "))]
        chunk2 = MagicMock()
        chunk2.choices = [MagicMock(delta=MagicMock(content="This is a test."))]

        mock_client.chat.completions.create.return_value = [chunk1, chunk2]

        service = GPTService(args)
        results = list(service.ask_stream("Hi"))

        self.assertIn("Hello world!", results)
        self.assertIn("This is a test.", results)


if __name__ == "__main__":
    unittest.main()
