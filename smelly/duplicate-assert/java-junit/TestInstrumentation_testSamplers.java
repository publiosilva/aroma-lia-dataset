public class TestInstrumentation extends TestCase {
    public void testCron() throws Exception {
        Instrumentation.Cron cron = new Instrumentation.Cron();
        long start = System.currentTimeMillis();
        assertEquals("", 0, cron.getStart(), TOLERANCE);
        assertEquals("", 0, cron.getEnd(), TOLERANCE);
        assertEquals(cron.getStart(), cron.getEnd());
        assertEquals(0, cron.getOwn());
        assertEquals(0, cron.getTotal());

        cron.start();
        long s = System.currentTimeMillis();
        Thread.sleep(INTERVAL);
        cron.stop();
        long realOwnDelay = System.currentTimeMillis() - s;
        long now = System.currentTimeMillis();
        assertEquals("", start, cron.getStart(), TOLERANCE);
        assertEquals("", now, cron.getEnd(), TOLERANCE);
        assertEquals("", INTERVAL, cron.getTotal(), TOLERANCE);
        assertEquals("", INTERVAL, cron.getOwn(), TOLERANCE);
        assertEquals("", cron.getTotal(), cron.getOwn(), TOLERANCE);

        long realTotalDelay = System.currentTimeMillis() - s;
        s = System.currentTimeMillis();
        Thread.sleep(INTERVAL);

        cron.start();

        realTotalDelay += System.currentTimeMillis() - s;

        s = System.currentTimeMillis();
        Thread.sleep(INTERVAL);
        cron.stop();
        now = System.currentTimeMillis();

        realTotalDelay += System.currentTimeMillis() - s;
        realOwnDelay += System.currentTimeMillis() - s;

        assertEquals("", start, cron.getStart(), TOLERANCE);
        assertEquals("", now, cron.getEnd(), TOLERANCE);
        assertEquals("", realTotalDelay, cron.getTotal(), TOLERANCE);
        assertEquals("", realOwnDelay, cron.getOwn(), TOLERANCE);
    }
}
