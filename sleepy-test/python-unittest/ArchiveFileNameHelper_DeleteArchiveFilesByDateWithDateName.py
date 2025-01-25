import unittest
import os
import time
import shutil
from pathlib import Path

class TestArchiveFileNameHelper(unittest.TestCase):
    def test_delete_archive_files_by_date_with_date_name(self):
        max_archive_files = 3
        LogManager.throw_exceptions = True
        temp_path = os.path.join(Path(os.getenv("TEMP") or "/tmp"), str(uuid.uuid4()))

        try:
            log_file = os.path.join(temp_path, "${date:format=yyyyMMddHHmmssfff}.txt")
            file_target = FileTarget(
                file_name=log_file,
                archive_file_name=os.path.join(temp_path, "{#}.txt"),
                archive_every=FileArchivePeriod.YEAR,
                line_ending=LineEndingMode.LF,
                archive_numbering=ArchiveNumberingMode.DATE,
                archive_date_format="yyyyMMddHHmmssfff",
                layout="${message}",
                max_archive_files=max_archive_files,
            )

            SimpleConfigurator.configure_for_target_logging(file_target, LogLevel.DEBUG)

            for _ in range(4):
                logger.debug("123456789")
                time.sleep(0.05)

            LogManager.configuration = None
            files = sorted(Path(temp_path).iterdir())
            self.assertEqual(max_archive_files + 1, len(files))

            SimpleConfigurator.configure_for_target_logging(file_target, LogLevel.DEBUG)
            time.sleep(0.05)
            logger.debug("123456789")
            LogManager.configuration = None
            files2 = sorted(Path(temp_path).iterdir())
            self.assertEqual(max_archive_files + 1, len(files2))
            self.assertNotIn(files[0], files2)
            self.assertEqual(files[1], files2[0])
            self.assertEqual(files[2], files2[1])
            self.assertEqual(files[3], files2[2])
            self.assertNotIn(files2[3], files)
        finally:
            if os.path.exists(temp_path):
                shutil.rmtree(temp_path)
