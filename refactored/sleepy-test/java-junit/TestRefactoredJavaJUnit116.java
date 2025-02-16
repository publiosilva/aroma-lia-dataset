public class TestShell extends TestCase {
  public void testShellCommandTimerLeak() throws Exception {
    String quickCommand[] = new String[] {"/bin/sleep", "100"};
    
    int timersBefore = countTimerThreads();
    System.err.println("before: " + timersBefore);
    
    for (int i = 0; i < 10; i++) {
      Shell.ShellCommandExecutor shexec = new Shell.ShellCommandExecutor(
            quickCommand, null, null, 1);
      try {
        shexec.execute();
        fail("Bad command should throw exception");
      } catch (Exception e) {
        // expected
      }
    }
    // Thread.sleep(1000);
    int timersAfter = countTimerThreads();
    System.err.println("after: " + timersAfter);
    assertEquals(timersBefore, timersAfter);
  }
}
