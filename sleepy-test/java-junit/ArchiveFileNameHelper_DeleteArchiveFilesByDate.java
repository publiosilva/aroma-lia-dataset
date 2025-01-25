import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertFalse;

import java.io.File;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;
import java.util.stream.Collectors;

public class ArchiveFileNameHelper {

    @Test
    public void deleteArchiveFilesByDate() throws Exception {
        final int maxArchiveFiles = 3;
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
            fileTarget.setMaxArchiveFiles(maxArchiveFiles);

            SimpleConfigurator.configureForTargetLogging(fileTarget, LogLevel.DEBUG);

            for (int i = 0; i < 19; ++i) {
                logger.debug("123456789");
                if (i % 5 == 0) {
                    Thread.sleep(50);
                }
            }

            LogManager.setConfiguration(null);
            List<Path> files = Files.list(archiveFolder).sorted().collect(Collectors.toList());
            assertEquals(maxArchiveFiles, files.size());

            SimpleConfigurator.configureForTargetLogging(fileTarget, LogLevel.DEBUG);
            logger.debug("1234567890");
            LogManager.setConfiguration(null);

            List<Path> files2 = Files.list(archiveFolder).sorted().collect(Collectors.toList());
            assertEquals(maxArchiveFiles, files2.size());
            assertFalse(files2.contains(files.get(0)));
            assertEquals(files.get(1), files2.get(0));
            assertEquals(files.get(2), files2.get(1));
            assertFalse(files.contains(files2.get(2)));
        } finally {
            Files.deleteIfExists(logFile);
            if (Files.exists(tempPath)) {
                Files.walk(tempPath)
                    .sorted(Comparator.reverseOrder())
                    .map(Path::toFile)
                    .forEach(File::delete);
            }
        }
    }
}
