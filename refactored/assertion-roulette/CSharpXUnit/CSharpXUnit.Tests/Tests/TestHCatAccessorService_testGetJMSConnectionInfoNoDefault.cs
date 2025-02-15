public class TestHCatAccessorService
{
    [Fact]
    public void TestGetJMSConnectionInfoNoDefault()
    {
        services.Destroy();
        services = SetupServicesForHCatalog();
        Configuration conf = services.GetConf();
        string server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp://broker.${2}:61616";
        string server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp:localhost:61616";

        string jmsConnectionURL = server2 + "," + server3;
        conf.Set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jmsConnectionURL);
        services.Init();

        HCatAccessorService hcatService = services.Get<HCatAccessorService>();
        // No default JMS mapping
        JMSConnectionInfo connInfo = hcatService.GetJMSConnectionInfo(new Uri("http://unknown:9999/fs"));
        Assert.Null(connInfo, "Explanation message");
        connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://server1.colo1.server.com:8020/db/table/pk1=val1;pk2=val2"));
        Assert.Equal("java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp://broker.colo1:61616", 
                     connInfo.GetJNDIPropertiesString(), "Explanation message");
        connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://xyz.corp.dummy.com/db/table"));
        Assert.Equal("java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp:localhost:61616", 
                     connInfo.GetJNDIPropertiesString(), "Explanation message");
    }
}
