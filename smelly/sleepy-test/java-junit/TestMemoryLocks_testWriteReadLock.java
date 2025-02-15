public class TestMemoryLocks extends TestCase {
    public void testReadLock() throws Exception {
        StringBuffer sb = new StringBuffer("");
        Locker l1 = new ReadLocker("a", 1, -1, sb);
        Locker l2 = new ReadLocker("a", 2, -1, sb);

        new Thread(l1).start();
        Thread.sleep(500);
        new Thread(l2).start();
        Thread.sleep(500);
        l1.finish();
        Thread.sleep(500);
        l2.finish();
        Thread.sleep(500);
        assertEquals("a:1-L a:2-L a:1-U a:2-U", sb.toString().trim());
    }
}
