public class MasterClientTest {
  @Test
  public void openCloseTest1() throws FileAlreadyExistException, InvalidPathException, TException,
      IOException {
    MasterClient masterClient = new MasterClient(mMasterInfo.getMasterAddress(), mExecutorService);
    Assert.assertFalse(masterClient.isConnected());
    masterClient.connect();
    Assert.assertTrue(masterClient.isConnected());
    masterClient.user_createFile("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, true);
    Assert.assertTrue(masterClient.getFileStatus(-1, "/file") != null);
  }

  @Test
  public void openCloseTest2() throws FileAlreadyExistException, InvalidPathException, TException,
      IOException {
    MasterClient masterClient = new MasterClient(mMasterInfo.getMasterAddress(), mExecutorService);
    masterClient.connect();
    masterClient.user_createFile("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, true);
    masterClient.close();
    Assert.assertFalse(masterClient.isConnected());
    masterClient.connect();
    Assert.assertTrue(masterClient.isConnected());
    Assert.assertTrue(masterClient.getFileStatus(-1, "/file") != null);
  }
}
