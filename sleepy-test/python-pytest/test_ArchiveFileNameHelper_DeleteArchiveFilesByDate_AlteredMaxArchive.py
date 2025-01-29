import os
import shutil
import time
import uuid
from pathlib import Path
from pytest import fixture, mark

class TestArchiveFileNameHelper:
    def test_delete_archive_files_by_date_altered_max_archive(self):
        temp_path = Path(os.path.join(os.getenv("TEMP", "/tmp"), str(uuid.uuid4())))
        log_file = temp_path / "file.txt"
        archive_folder = temp_path / "archive"

        try:
            file_target = FileTarget(
                FileName=log_file,
                ArchiveFileName=archive_folder / "{#}.txt",
                ArchiveAboveSize=50,
                LineEnding=LineEndingMode.LF,
                ArchiveNumbering=ArchiveNumberingMode.Date,
                ArchiveDateFormat="yyyyMMddHHmmssfff",
                Layout="${message}",
                MaxArchiveFiles=5
            )
            SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)

            for i in range(29):
                logger.Debug("123456789")
                if i % 5 == 0:
                    time.sleep(0.05)

            LogManager.Configuration = None
            files = sorted(os.listdir(archive_folder))

            assert len(files) == file_target.MaxArchiveFiles

            file_target.MaxArchiveFiles = 2
            SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
            logger.Debug("1234567890")
            LogManager.Configuration = None

            files2 = sorted(os.listdir(archive_folder))

            assert len(files2) == file_target.MaxArchiveFiles
            assert files[0] not in files2
            assert files[1] not in files2
            assert files[2] not in files2
            assert files[3] not in files2
            assert files[4] == files2[0]
            assert files2[1] not in files
        finally:
            if log_file.exists():
                log_file.unlink()
            if temp_path.exists():
                shutil.rmtree(temp_path)
