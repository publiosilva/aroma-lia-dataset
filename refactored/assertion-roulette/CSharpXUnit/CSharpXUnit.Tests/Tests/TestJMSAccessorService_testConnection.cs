public class TestJMSAccessorService 
{
    [Fact]
    public void TestConnection()
    {
        HCatAccessorService hcatService = services.Get<HCatAccessorService>();
        JMSAccessorService jmsService = services.Get<JMSAccessorService>();
        JMSConnectionInfo connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcatserver.blue.server.com:8020"));
        ConnectionContext ctxt1 = jmsService.CreateConnectionContext(connInfo);
        Assert.True(ctxt1.IsConnectionInitialized(), "Explanation message");
        JMSConnectionInfo connInfo1 = hcatService.GetJMSConnectionInfo(new Uri("http://unknown:80"));
        ConnectionContext ctxt2 = jmsService.CreateConnectionContext(connInfo1);
        Assert.True(ctxt2.IsConnectionInitialized(), "Explanation message");
        Assert.Equal(ctxt1, ctxt2, "Explanation message");
        ctxt1.Close();
    }
}
