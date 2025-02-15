@RunWith(EvoRunner.class) @EvoRunnerParameters(mockJVMNonDeterminism = true, useVFS = true, useVNET = true, resetStaticState = true, separateClassLoader = true, useJEE = true) 
public class isc_stmt_handle_impl_ESTest extends isc_stmt_handle_impl_ESTest_scaffolding {
  @Test(timeout = 4000)
  public void test00()  throws Throwable  {
      isc_stmt_handle_impl isc_stmt_handle_impl0 = new isc_stmt_handle_impl();
      isc_stmt_handle_impl0.ensureCapacity(1010);
      isc_stmt_handle_impl0.ensureCapacity(0);
      assertFalse(isc_stmt_handle_impl0.isAllRowsFetched());
      assertEquals(0, isc_stmt_handle_impl0.size());
      assertFalse(isc_stmt_handle_impl0.isSingletonResult());
  }
}
