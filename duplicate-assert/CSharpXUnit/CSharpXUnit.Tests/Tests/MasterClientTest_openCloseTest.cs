public class MasterClientTest
{
    [Fact]
    public void OpenCloseTest()
    {
        var masterClient = new MasterClient(mMasterInfo.GetMasterAddress(), mExecutorService);
        Assert.False(masterClient.IsConnected());
        masterClient.Connect();
        Assert.True(masterClient.IsConnected());
        masterClient.UserCreateFile("/file", "", Constants.DEFAULT_BLOCK_SIZE_BYTE, true);
        Assert.NotNull(masterClient.GetFileStatus(-1, "/file"));
        masterClient.Close();
        Assert.False(masterClient.IsConnected());
        masterClient.Connect();
        Assert.True(masterClient.IsConnected());
        Assert.NotNull(masterClient.GetFileStatus(-1, "/file"));
    }
}
