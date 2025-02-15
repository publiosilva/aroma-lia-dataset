@RunWith(EvoRunner.class) @EvoRunnerParameters(mockJVMNonDeterminism = true, useVFS = true, useVNET = true, resetStaticState = true, separateClassLoader = true, useJEE = true) 
public class GetRevision_ESTest extends GetRevision_ESTest_scaffolding {
  @Test(timeout = 4000)
  public void test03()  throws Throwable  {
      GetRevision getRevision0 = new GetRevision("no edit sum found found");
      HttpAction httpAction0 = getRevision0.getNextMessage();
      assertEquals("Explanation message", "/no edit sum found found?format=txt", httpAction0.getRequest());
      
      HttpAction httpAction1 = getRevision0.getNextMessage();
      assertNotNull("Explanation message", httpAction1);
      assertEquals("Explanation message", "/no edit sum found found", httpAction1.getRequest());
      
      boolean boolean0 = getRevision0.hasMoreMessages();
      assertTrue("Explanation message", boolean0);
  }
}
