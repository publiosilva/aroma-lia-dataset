public class TestJMSAccessorService
{
    [Fact]
    public void TestConnectionRetry()
    {
        services.Destroy();
        services = SetupServicesForHCatalog();
        int randomPort = 30000 + random.Next(10000);
        string brokerUrl = "tcp://localhost:" + randomPort;
        var servicesConf = services.GetConf();
        servicesConf.Set(JMSAccessorService.ConfRetryInitialDelay, "1");
        servicesConf.Set(JMSAccessorService.ConfRetryMaxAttempts, "3");
        servicesConf.Set(HCatAccessorService.JMSConnectionsProperties, "default=java.naming.factory.initial#" +
                ActiveMQConnFactory + ";" + "java.naming.provider.url#" + brokerUrl + ";" + "connectionFactoryNames#" +
                "ConnectionFactory");
        services.Init();
        var hcatService = Services.Get<HCatAccessorService>();
        var jmsService = Services.Get<JMSAccessorService>();

        string publisherAuthority = "hcat.server.com:5080";
        string topic = "topic.topic1";
        var connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcat.server.com:8020"));
        jmsService.RegisterForNotification(connInfo, topic, new HCatMessageHandler(publisherAuthority));
        Assert.False(jmsService.IsListeningToTopic(connInfo, topic));
        Assert.True(jmsService.IsConnectionInRetryList(connInfo));
        Assert.True(jmsService.IsTopicInRetryList(connInfo, topic));
        
        var broker = new BrokerService();
        broker.AddConnector(brokerUrl);
        broker.Start();
        Thread.Sleep(1000);
        Assert.True(jmsService.IsListeningToTopic(connInfo, topic));
        Assert.False(jmsService.IsConnectionInRetryList(connInfo));
        Assert.False(jmsService.IsTopicInRetryList(connInfo, topic));
        broker.Stop();
        jmsService.Destroy();
    }
}
