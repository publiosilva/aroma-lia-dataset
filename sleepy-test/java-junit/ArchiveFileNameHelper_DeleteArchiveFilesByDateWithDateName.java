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
    public void deleteArchiveFilesByDateWithDateName() throws Exception {
        final int maxArchiveFiles = 3;
        LogManager.setThrowExceptions(true);
        Path tempPath = Paths.get(System.getProperty("java.io.tmpdir"), UUID.randomUUID().toString());

        try {
            Path logFile = tempPath.resolve("${date:format=yyyyMMddHHmmssSSS}.txt");
            FileTarget fileTarget = new FileTarget();
            fileTarget.setFileName(logFile.toString());
            fileTarget.setArchiveFileName(tempPath.resolve("{#}.txt").toString());
            fileTarget.setArchiveEvery(FileArchivePeriod.YEAR);
            fileTarget.setLineEnding(LineEndingMode.LF);
            fileTarget.setArchiveNumbering(ArchiveNumberingMode.DATE);
            fileTarget.setArchiveDateFormat("yyyyMMddHHmmssSSS");
            fileTarget.setLayout("${message}");
            fileTarget.setMaxArchiveFiles(maxArchiveFiles);

            SimpleConfigurator.configureForTargetLogging(fileTarget, LogLevel.DEBUG);

            for (int i = 0; i < 4; ++i) {
                logger.debug("123456789");
                Thread.sleep(50);
            }

            LogManager.setConfiguration(null);
            List<Path> files = Files.list(tempPath).sorted().collect(Collectors.toList());
            assertEquals(maxArchiveFiles + 1, files.size());

            SimpleConfigurator.configureForTargetLogging(fileTarget, LogLevel.DEBUG);
            Thread.sleep(50);
            logger.debug("123456789");
            LogManager.setConfiguration(null);

            List<Path> files2 = Files.list(tempPath).sorted().collect(Collectors.toList());
            assertEquals(maxArchiveFiles + 1, files2.size());
            assertFalse(files2.contains(files.get(0)));
            assertEquals(files.get(1), files2.get(0));
            assertEquals(files.get(2), files2.get(1));
            assertEquals(files.get(3), files2.get(2));
            assertFalse(files.contains(files2.get(3)));
        } finally {
            if (Files.exists(tempPath)) {
                Files.walk(tempPath)
                    .sorted(Comparator.reverseOrder())
                    .map(Path::toFile)
                    .forEach(File::delete);
            }
        }
    }
}
