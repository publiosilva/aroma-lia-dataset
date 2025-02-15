using Xunit;

namespace DefaultNamespace
{
    public class ProcessTimeLayoutRendererTests
    {
        [Fact]
        public void RenderProcessTimeLayoutRenderer()
        {
            {
                var layout = "${processtime}";
                var timestamp = LogEventInfo.ZeroDate;
                System.Threading.Thread.Sleep(16);
                var logEvent = new LogEventInfo(LogLevel.Debug, "logger1", "message1");
                var time = logEvent.TimeStamp.ToUniversalTime() - timestamp;
                var expected = time.ToString("hh\\:mm\\:ss\\.fff");
                AssertLayoutRendererOutput(layout, logEvent, expected);
            }
        }
    }
}
