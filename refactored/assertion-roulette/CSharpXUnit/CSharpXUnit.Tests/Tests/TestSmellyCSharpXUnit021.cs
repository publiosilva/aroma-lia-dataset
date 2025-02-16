public class TestHCatAccessorService 
{
    [Fact]
    public void TestGetJMSConnectionInfo1()
    {
        services.Destroy();
        services = SetupServicesForHCatalog();
        var conf = services.GetConf();
        string server1 = "default=java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" +
                "java.naming.provider.url#vm://localhost?broker.persistent=false";
        string server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp://broker.${2}:61616";
        string server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp:localhost:61616";

        string jmsConnectionURL = server1 + "," + server2 + "," + server3;
        conf.Set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jmsConnectionURL);
        services.Init();

        var hcatService = services.Get<HCatAccessorService>();
        var connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcatserver.blue.server.com:8020"));
        Assert.Equal("java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp://broker.blue:61616",
                connInfo.GetJNDIPropertiesString());
    }

    [Fact]
    public void TestGetJMSConnectionInfo2()
    {
        services.Destroy();
        services = SetupServicesForHCatalog();
        var conf = services.GetConf();
        string server1 = "default=java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" +
                "java.naming.provider.url#vm://localhost?broker.persistent=false";
        string server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp://broker.${2}:61616";
        string server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp:localhost:61616";

        string jmsConnectionURL = server1 + "," + server2 + "," + server3;
        conf.Set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jmsConnectionURL);
        services.Init();

        var hcatService = services.Get<HCatAccessorService>();
        var connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcatserver.blue.server.com:8020"));

        connInfo = hcatService.GetJMSConnectionInfo(new Uri("http://unknown:9999/fs"));
        Assert.Equal(
                "java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" +
                "java.naming.provider.url#vm://localhost?broker.persistent=false",
                connInfo.GetJNDIPropertiesString());
    }

    [Fact]
    public void TestGetJMSConnectionInfo3()
    {
        services.Destroy();
        services = SetupServicesForHCatalog();
        var conf = services.GetConf();
        string server1 = "default=java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" +
                "java.naming.provider.url#vm://localhost?broker.persistent=false";
        string server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp://broker.${2}:61616";
        string server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp:localhost:61616";

        string jmsConnectionURL = server1 + "," + server2 + "," + server3;
        conf.Set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jmsConnectionURL);
        services.Init();

        var hcatService = services.Get<HCatAccessorService>();
        var connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcatserver.blue.server.com:8020"));

        connInfo = hcatService.GetJMSConnectionInfo(new Uri("http://unknown:9999/fs"));

        connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://xyz.corp.dummy.com"));
        Assert.Equal("java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp:localhost:61616",
                connInfo.GetJNDIPropertiesString());
    }
}
