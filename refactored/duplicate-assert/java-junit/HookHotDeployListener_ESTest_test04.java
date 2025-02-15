@RunWith(EvoRunner.class) @EvoRunnerParameters(mockJVMNonDeterminism = true, useVFS = true, useVNET = true, resetStaticState = true, separateClassLoader = true, useJEE = true) 
public class HookHotDeployListener_ESTest extends HookHotDeployListener_ESTest_scaffolding {
  @Test(timeout = 4000)
  public void test001()  throws Throwable  {
      HookHotDeployListener hookHotDeployListener0 = new HookHotDeployListener();
      assertNotNull(hookHotDeployListener0);
      
      MockServletContext mockServletContext0 = new MockServletContext();
      assertNotNull(mockServletContext0);
      assertNull(mockServletContext0.getServerInfo());
      assertEquals(0, mockServletContext0.getMajorVersion());
      assertEquals(0, mockServletContext0.getMinorVersion());
      assertNull(mockServletContext0.getServletContextName());
  }

  @Test(timeout = 4000)
  public void test002()  throws Throwable  {
      HookHotDeployListener hookHotDeployListener0 = new HookHotDeployListener();
      MockServletContext mockServletContext0 = new MockServletContext();
      HotDeployEvent hotDeployEvent0 = new HotDeployEvent(mockServletContext0, (ClassLoader) null);
      assertNotNull(hotDeployEvent0);
      assertNull(mockServletContext0.getServerInfo());
      assertEquals(0, mockServletContext0.getMajorVersion());
      assertEquals(0, mockServletContext0.getMinorVersion());
      assertNull(mockServletContext0.getServletContextName());
  }

  @Test(timeout = 4000)
  public void test003()  throws Throwable  {
      HookHotDeployListener hookHotDeployListener0 = new HookHotDeployListener();
      MockServletContext mockServletContext0 = new MockServletContext();
      HotDeployEvent hotDeployEvent0 = new HotDeployEvent(mockServletContext0, (ClassLoader) null);
      hookHotDeployListener0.doInvokeUndeploy(hotDeployEvent0);
      assertNull(mockServletContext0.getServerInfo());
      assertEquals(0, mockServletContext0.getMajorVersion());
      assertEquals(0, mockServletContext0.getMinorVersion());
      assertNull(mockServletContext0.getServletContextName());
  }
}
