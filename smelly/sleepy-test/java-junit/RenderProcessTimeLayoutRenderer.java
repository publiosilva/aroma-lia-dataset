import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

public class ProcessTimeLayoutRendererTests {

    @Test
    public void renderProcessTimeLayoutRenderer() throws InterruptedException {
        {
            String layout = "${processtime}";
            var timestamp = LogEventInfo.ZeroDate;
            Thread.sleep(16);
            var logEvent = new LogEventInfo(LogLevel.Debug, "logger1", "message1");
            var time = logEvent.getTimeStamp().toInstant().atZone(ZoneId.of("UTC")).toLocalDateTime().toInstant(ZoneOffset.UTC).toEpochMilli() - timestamp.toInstant().atZone(ZoneId.of("UTC")).toLocalDateTime().toInstant(ZoneOffset.UTC).toEpochMilli();
            var expected = String.format("%02d:%02d:%02d.%03d",
                    TimeUnit.MILLISECONDS.toHours(time),
                    TimeUnit.MILLISECONDS.toMinutes(time) % 60,
                    TimeUnit.MILLISECONDS.toSeconds(time) % 60,
                    time % 1000);
            assertLayoutRendererOutput(layout, logEvent, expected);
        }
    }
}
