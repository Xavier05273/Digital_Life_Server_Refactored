import logging
import os
import time


class FlushingFileHandler(logging.FileHandler):
    def __init__(self, filename, mode="a", encoding=None, delay=False, formatter=None):
        # Force UTF-8 to avoid UnicodeEncodeError on Windows (cp1252) with Chinese text
        super().__init__(filename, mode, encoding or "utf-8", delay)
        self.formatter = formatter

    def emit(self, record):
        try:
            super().emit(record)
        except UnicodeEncodeError:
            # Fallback: replace unencodable chars instead of crashing
            pass
        try:
            self.nice_try(record)
        except IOError:
            time.sleep(0.2)
            self.nice_try(record)

    def nice_try(self, record):
        with open('log_async.log', 'a', encoding='utf-8') as f:
            f.write(self.formatter.format(record) + '\n')
