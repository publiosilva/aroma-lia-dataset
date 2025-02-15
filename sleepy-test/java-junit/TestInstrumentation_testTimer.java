public class TestInstrumentation extends TestCase {
    public void testTimer() throws Exception {
        Instrumentation.Timer timer = new Instrumentation.Timer();

        assertEquals(0, timer.getTicks());
        assertEquals(0, timer.getTotal());
        assertEquals(0, timer.getOwn());
        assertEquals(0, timer.getOwnAvg());
        assertEquals(0, timer.getTotalAvg());
        assertEquals(0, timer.getOwnSquareSum());
        assertEquals(0, timer.getTotalSquareSum());
        assertEquals(0, timer.getOwnMin());
        assertEquals(0, timer.getOwnMax());
        assertEquals(0, timer.getTotalMin());
        assertEquals(0, timer.getTotalMax());

        assertEquals(0, timer.getValue().getTicks());
        assertEquals(0, timer.getValue().getTotal());
        assertEquals(0, timer.getValue().getOwn());
        assertEquals(0, timer.getValue().getOwnAvg());
        assertEquals(0, timer.getValue().getTotalAvg());
        assertEquals(0, timer.getValue().getOwnSquareSum());
        assertEquals(0, timer.getValue().getTotalSquareSum());
        assertEquals(0, timer.getValue().getOwnMin());
        assertEquals(0, timer.getValue().getOwnMax());
        assertEquals(0, timer.getValue().getTotalMin());
        assertEquals(0, timer.getValue().getTotalMax());

        Instrumentation.Cron cron1 = new Instrumentation.Cron();
        cron1.start();
        Thread.sleep(INTERVAL);
        cron1.stop();
        timer.addCron(cron1);

        assertEquals(1, timer.getTicks());
        assertEquals(cron1.getTotal(), timer.getTotal());
        assertEquals(cron1.getOwn(), timer.getOwn());
        assertEquals(cron1.getOwn(), timer.getOwnAvg());
        assertEquals(cron1.getTotal(), timer.getTotalAvg());
        assertEquals(cron1.getOwn() * cron1.getOwn(), timer.getOwnSquareSum());
        assertEquals(cron1.getTotal() * cron1.getTotal(), timer.getTotalSquareSum());
        assertEquals(cron1.getOwn(), timer.getOwnMin());
        assertEquals(cron1.getOwn(), timer.getOwnMax());
        assertEquals(cron1.getTotal(), timer.getTotalMin());
        assertEquals(cron1.getTotal(), timer.getTotalMax());

        assertEquals(1, timer.getValue().getTicks());
        assertEquals(cron1.getTotal(), timer.getValue().getTotal());
        assertEquals(cron1.getOwn(), timer.getValue().getOwn());
        assertEquals(cron1.getOwn(), timer.getValue().getOwnAvg());
        assertEquals(cron1.getTotal(), timer.getValue().getTotalAvg());
        assertEquals(cron1.getOwn() * cron1.getOwn(), timer.getValue().getOwnSquareSum());
        assertEquals(cron1.getTotal() * cron1.getTotal(), timer.getValue().getTotalSquareSum());
        assertEquals(cron1.getOwn(), timer.getValue().getOwnMin());
        assertEquals(cron1.getOwn(), timer.getValue().getOwnMax());
        assertEquals(cron1.getTotal(), timer.getValue().getTotalMin());
        assertEquals(cron1.getTotal(), timer.getValue().getTotalMax());

        Instrumentation.Cron cron2 = new Instrumentation.Cron();
        cron2.start();
        Thread.sleep(INTERVAL * 2);
        cron2.stop();
        timer.addCron(cron2);

        assertEquals(2, timer.getTicks());
        assertEquals(cron1.getTotal() + cron2.getTotal(), timer.getTotal());
        assertEquals(cron1.getOwn() + cron2.getOwn(), timer.getOwn());
        assertEquals((cron1.getOwn() + cron2.getOwn()) / 2, timer.getOwnAvg());
        assertEquals((cron1.getTotal() + cron2.getTotal()) / 2, timer.getTotalAvg());
        assertEquals(cron1.getOwn() * cron1.getOwn() + cron2.getOwn() * cron2.getOwn(),
                     timer.getOwnSquareSum());
        assertEquals(cron1.getTotal() * cron1.getTotal() + cron2.getTotal() * cron2.getTotal(),
                     timer.getTotalSquareSum());
        assertEquals(cron1.getOwn(), timer.getOwnMin());
        assertEquals(cron2.getOwn(), timer.getOwnMax());
        assertEquals(cron1.getTotal(), timer.getTotalMin());
        assertEquals(cron2.getTotal(), timer.getTotalMax());
    }
}
