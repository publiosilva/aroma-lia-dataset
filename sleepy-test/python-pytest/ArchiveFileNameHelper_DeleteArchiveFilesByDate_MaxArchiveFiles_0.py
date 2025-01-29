def test_delete_archive_files_by_date_max_archive_files_0():
    temp_path = os.path.join(tempfile.gettempdir(), str(uuid.uuid4()))
    log_file = os.path.join(temp_path, "file.txt")
    try:
        archive_folder = os.path.join(temp_path, "archive")
        file_target = FileTarget(
            FileName=log_file,
            ArchiveFileName=os.path.join(archive_folder, "{#}.txt"),
            ArchiveAboveSize=50,
            LineEnding=LineEndingMode.LF,
            ArchiveNumbering=ArchiveNumberingMode.Date,
            ArchiveDateFormat="yyyyMMddHHmmssfff",
            Layout="${message}",
            MaxArchiveFiles=0
        )
        SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
        for i in range(19):
            logger.Debug("123456789")
            if i % 5 == 0:
                time.sleep(0.05)

        LogManager.Configuration = None
        file_count = len(list(os.listdir(archive_folder)))
        assert file_count == 3

        SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
        logger.Debug("1234567890")
        LogManager.Configuration = None
        file_count2 = len(list(os.listdir(archive_folder)))
        assert file_count2 == 4
    finally:
        if os.path.exists(log_file):
            os.remove(log_file)
        if os.path.exists(temp_path):
            shutil.rmtree(temp_path)
