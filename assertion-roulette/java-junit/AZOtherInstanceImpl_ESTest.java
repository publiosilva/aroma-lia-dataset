@RunWith(EvoRunner.class) @EvoRunnerParameters(mockJVMNonDeterminism = true, useVFS = true, useVNET = true, resetStaticState = true, separateClassLoader = true, useJEE = true) 
public class AZOtherInstanceImpl_ESTest extends AZOtherInstanceImpl_ESTest_scaffolding {
  @Test(timeout = 4000)
  public void test44()  throws Throwable  {
      InetAddress inetAddress0 = InetAddress.getLoopbackAddress();
      HashMap<String, Object> hashMap0 = new HashMap<String, Object>();
      AZOtherInstanceImpl aZOtherInstanceImpl0 = new AZOtherInstanceImpl("so>]Ju~Kc2J5H@", "z@4}", inetAddress0, inetAddress0, 0, 121, 0, hashMap0);
      boolean boolean0 = aZOtherInstanceImpl0.update(aZOtherInstanceImpl0);
      assertFalse(boolean0);
      assertEquals(121, aZOtherInstanceImpl0.getUDPListenPort());
      assertEquals(0, aZOtherInstanceImpl0.getUDPNonDataListenPort());
      assertEquals(0, aZOtherInstanceImpl0.getTCPListenPort());
  }
}
