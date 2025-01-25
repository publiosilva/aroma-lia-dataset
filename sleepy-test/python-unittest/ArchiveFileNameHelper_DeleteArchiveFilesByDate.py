import unittest
import os
import time
import shutil
from pathlib import Path

class TestArchiveFileNameHelper(unittest.TestCase):
    def test_delete_archive_files_by_date(self):
        max_archive_files = 3
        temp_path = os.path.join(Path(os.getenv("TEMP") or "/tmp"), str(uuid.uuid4()))
        log_file = os.path.join(temp_path, "file.txt")

        try:
            archive_folder = os.path.join(temp_path, "archive")
            file_target = FileTarget(
                file_name=log_file,
                archive_file_name=os.path.join(archive_folder, "{#}.txt"),
                archive_above_size=50,
                line_ending=LineEndingMode.LF,
                archive_numbering=ArchiveNumberingMode.DATE,
                archive_date_format="yyyyMMddHHmmssfff",
                layout="${message}",
                max_archive_files=max_archive_files,
            )

            SimpleConfigurator.configure_for_target_logging(file_target, LogLevel.DEBUG)

            for i in range(19):
                logger.debug("123456789")
                if i % 5 == 0:
                    time.sleep(0.05)

            LogManager.configuration = None
            files = sorted(Path(archive_folder).iterdir())
            self.assertEqual(max_archive_files, len(files))

            SimpleConfigurator.configure_for_target_logging(file_target, LogLevel.DEBUG)
            logger.debug("1234567890")
            LogManager.configuration = None
            files2 = sorted(Path(archive_folder).iterdir())
            self.assertEqual(max_archive_files, len(files2))
            self.assertNotIn(files[0], files2)
            self.assertEqual(files[1], files2[0])
            self.assertEqual(files[2], files2[1])
            self.assertNotIn(files2[2], files)
        finally:
            if os.path.exists(log_file):
                os.remove(log_file)
            if os.path.exists(temp_path):
                shutil.rmtree(temp_path)
