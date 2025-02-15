@RunWith(EvoRunner.class) @EvoRunnerParameters(mockJVMNonDeterminism = true, useVFS = true, useVNET = true, resetStaticState = true, separateClassLoader = true, useJEE = true) 
public class PollsChoiceModelImpl_ESTest extends PollsChoiceModelImpl_ESTest_scaffolding {
  @Test(timeout = 4000)
  public void test00()  throws Throwable  {
      PollsChoiceImpl pollsChoiceImpl0 = new PollsChoiceImpl();
      PollsChoiceWrapper pollsChoiceWrapper0 = new PollsChoiceWrapper(pollsChoiceImpl0);
      pollsChoiceWrapper0.setChoiceId(1L);
      boolean boolean0 = pollsChoiceImpl0.equals(pollsChoiceWrapper0);
      assertEquals("Explanation message", 1L, pollsChoiceImpl0.getChoiceId());
      assertTrue("Explanation message", boolean0);
  }
}
