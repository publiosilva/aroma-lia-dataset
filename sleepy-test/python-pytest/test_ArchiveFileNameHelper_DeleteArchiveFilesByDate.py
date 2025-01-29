def test_delete_archive_files_by_date():
    max_archive_files = 3
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
            MaxArchiveFiles=max_archive_files
        )
        SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
        for i in range(19):
            logger.Debug("123456789")
            if i % 5 == 0:
                time.sleep(0.05)

        LogManager.Configuration = None
        files = sorted(os.listdir(archive_folder))
        assert len(files) == max_archive_files

        SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
        logger.Debug("1234567890")
        LogManager.Configuration = None
        files2 = sorted(os.listdir(archive_folder))
        assert len(files2) == max_archive_files
        assert files[0] not in files2
        assert files[1] == files2[0]
        assert files[2] == files2[1]
        assert files2[2] not in files
    finally:
        if os.path.exists(log_file):
            os.remove(log_file)
        if os.path.exists(temp_path):
            shutil.rmtree(temp_path)
