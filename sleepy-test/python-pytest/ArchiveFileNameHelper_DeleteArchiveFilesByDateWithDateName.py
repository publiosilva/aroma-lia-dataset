def test_delete_archive_files_by_date_with_date_name():
    max_archive_files = 3
    LogManager.ThrowExceptions = True
    temp_path = os.path.join(tempfile.gettempdir(), str(uuid.uuid4()))
    try:
        log_file = os.path.join(temp_path, "${date:format=yyyyMMddHHmmssfff}.txt")
        file_target = FileTarget(
            FileName=log_file,
            ArchiveFileName=os.path.join(temp_path, "{#}.txt"),
            ArchiveEvery=FileArchivePeriod.Year,
            LineEnding=LineEndingMode.LF,
            ArchiveNumbering=ArchiveNumberingMode.Date,
            ArchiveDateFormat="yyyyMMddHHmmssfff",
            Layout="${message}",
            MaxArchiveFiles=max_archive_files
        )
        SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
        for i in range(4):
            logger.Debug("123456789")
            time.sleep(0.05)

        LogManager.Configuration = None
        files = sorted(os.listdir(temp_path))
        assert len(files) == max_archive_files + 1

        SimpleConfigurator.ConfigureForTargetLogging(file_target, LogLevel.Debug)
        time.sleep(0.05)
        logger.Debug("123456789")
        LogManager.Configuration = None
        files2 = sorted(os.listdir(temp_path))
        assert len(files2) == max_archive_files + 1
        assert files[0] not in files2
        assert files[1] == files2[0]
        assert files[2] == files2[1]
        assert files[3] == files2[2]
        assert files2[3] not in files
    finally:
        if os.path.exists(temp_path):
            shutil.rmtree(temp_path)
