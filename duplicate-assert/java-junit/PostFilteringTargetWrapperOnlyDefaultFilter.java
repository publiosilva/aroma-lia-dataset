import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertEquals;

public class PostFilteringTargetWrapperTests {

    @Test
    public void PostFilteringTargetWrapperOnlyDefaultFilter() {
        var target = new MyTarget();
        var wrapper = new PostFilteringTargetWrapper();
        wrapper.setWrappedTarget(target);
        wrapper.setDefaultFilter("level >= LogLevel.Info"); // by default log info and above
        wrapper.initialize(null);
        target.initialize(null);
        var exceptions = new ArrayList<Exception>();
        wrapper.writeAsyncLogEvent(new LogEventInfo(LogLevel.Info, "Logger1", "Hello").withContinuation(exceptions::add));
        assertEquals(1, target.getEvents().size());
        wrapper.writeAsyncLogEvent(new LogEventInfo(LogLevel.Debug, "Logger1", "Hello").withContinuation(exceptions::add));
        assertEquals(1, target.getEvents().size());
    }
}
