public class TestCoordELFunctions extends TestCase {
    public void testHours1() throws Exception {
        init("coord-job-submit-freq");
        String expr = "${coord:hours(1)}";
        assertEquals("60", CoordELFunctions.evalAndWrap(eval, expr));
        assertEquals(TimeUnit.MINUTE, (TimeUnit) eval.getVariable("timeunit"));
    }

    public void testHours2() throws Exception {
        init("coord-job-submit-freq");
        String expr = "${coord:hours(coord:hours(1))}";
        assertEquals("3600", CoordELFunctions.evalAndWrap(eval, expr));
        assertEquals(TimeUnit.MINUTE, (TimeUnit) eval.getVariable("timeunit"));
    }
}
