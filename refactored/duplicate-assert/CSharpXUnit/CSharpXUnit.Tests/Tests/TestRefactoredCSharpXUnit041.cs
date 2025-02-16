public class MasterClientTest
{
    [Fact]
    public void OpenCloseTest1()
    {
        MasterClient masterClient = new MasterClient(mMasterInfo.GetMasterAddress(), mExecutorService);
        Assert.False(masterClient.IsConnected());
        masterClient.Connect();
        Assert.True(masterClient.IsConnected());
        masterClient.UserCreateFile("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, true);
        Assert.NotNull(masterClient.GetFileStatus(-1, "/file"));
    }

    [Fact]
    public void OpenCloseTest2()
    {
        MasterClient masterClient = new MasterClient(mMasterInfo.GetMasterAddress(), mExecutorService);
        masterClient.Connect();
        masterClient.UserCreateFile("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, true);
        masterClient.Close();
        Assert.False(masterClient.IsConnected());
        masterClient.Connect();
        Assert.True(masterClient.IsConnected());
        Assert.NotNull(masterClient.GetFileStatus(-1, "/file"));
    }
}
