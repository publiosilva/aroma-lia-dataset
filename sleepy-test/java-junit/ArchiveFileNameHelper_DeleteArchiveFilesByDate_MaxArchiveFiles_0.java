import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;

import java.io.File;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.UUID;

public class ArchiveFileNameHelper {

    @Test
    public void deleteArchiveFilesByDate_MaxArchiveFiles_0() throws Exception {
        Path tempPath = Paths.get(System.getProperty("java.io.tmpdir"), UUID.randomUUID().toString());
        Path logFile = tempPath.resolve("file.txt");

        try {
            Path archiveFolder = tempPath.resolve("archive");
            FileTarget fileTarget = new FileTarget();
            fileTarget.setFileName(logFile.toString());
            fileTarget.setArchiveFileName(archiveFolder.resolve("{#}.txt").toString());
            fileTarget.setArchiveAboveSize(50);
            fileTarget.setLineEnding(LineEndingMode.LF);
            fileTarget.setArchiveNumbering(ArchiveNumberingMode.DATE);
            fileTarget.setArchiveDateFormat("yyyyMMddHHmmssSSS");
            fileTarget.setLayout("${message}");
            fileTarget.setMaxArchiveFiles(0);

            SimpleConfigurator.configureForTargetLogging(fileTarget, LogLevel.DEBUG);

            for (int i = 0; i < 19; ++i) {
                logger.debug("123456789");
                if (i % 5 == 0) {
                    Thread.sleep(50);
                }
            }

            LogManager.setConfiguration(null);
            long fileCount = Files.list(archiveFolder).count();
            assertEquals(3, fileCount);

            SimpleConfigurator.configureForTargetLogging(fileTarget, LogLevel.DEBUG);
            logger.debug("1234567890");
            LogManager.setConfiguration(null);

            long fileCount2 = Files.list(archiveFolder).count();
            assertEquals(4, fileCount2);
        } finally {
            Files.deleteIfExists(logFile);
            if (Files.exists(tempPath)) {
                Files.walk(tempPath)
                    .sorted((a, b) -> b.compareTo(a))
                    .map(Path::toFile)
                    .forEach(File::delete);
            }
        }
    }
}
