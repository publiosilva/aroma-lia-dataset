public class TestJMSAccessorService
{
    [Fact]
    public void TestConnection()
    {
        var hcatService = services.Get<HCatAccessorService>();
        var jmsService = services.Get<JMSAccessorService>();
        var connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcatserver.blue.server.com:8020"));
        var ctxt1 = jmsService.CreateConnectionContext(connInfo);
        Assert.True(ctxt1.IsConnectionInitialized());
        var connInfo1 = hcatService.GetJMSConnectionInfo(new Uri("http://unknown:80"));
        var ctxt2 = jmsService.CreateConnectionContext(connInfo1);
        Assert.True(ctxt2.IsConnectionInitialized());
        Assert.Equal(ctxt1, ctxt2);
        ctxt1.Close();
    }
}
