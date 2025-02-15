public class TestJMSAccessorService 
{
    [Fact]
    public void TestConnectionRetryMaxAttempt()
    {
        services.Destroy();
        services = base.SetupServicesForHCatalog();
        string jndiPropertiesString = "java.naming.factory.initial#" + ActiveMQConnFactory + ";" +
                "java.naming.provider.url#" + "tcp://localhost:12345;connectionFactoryNames#ConnectionFactory";
        Configuration servicesConf = services.GetConf();
        servicesConf.Set(JMSAccessorService.CONF_RETRY_INITIAL_DELAY, "1");
        servicesConf.Set(JMSAccessorService.CONF_RETRY_MAX_ATTEMPTS, "1");
        servicesConf.Set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, "default=" + jndiPropertiesString);
        services.Init();
        HCatAccessorService hcatService = Services.Get().Get<HCatAccessorService>();
        JMSAccessorService jmsService = Services.Get().Get<JMSAccessorService>();

        string publisherAuthority = "hcat.server.com:5080";
        string topic = "topic.topic1";
        JMSConnectionInfo connInfo = hcatService.GetJMSConnectionInfo(new Uri("hcat://hcat.server.com:8020"));
        jmsService.RegisterForNotification(connInfo, topic, new HCatMessageHandler(publisherAuthority));
        Assert.True(jmsService.IsConnectionInRetryList(connInfo));
        Assert.True(jmsService.IsTopicInRetryList(connInfo, topic));
        Assert.False(jmsService.IsListeningToTopic(connInfo, topic));
        Thread.Sleep(1100);
        // Should not retry again as max attempt is 1
        Assert.True(jmsService.IsConnectionInRetryList(connInfo));
        Assert.True(jmsService.IsTopicInRetryList(connInfo, topic));
        Assert.False(jmsService.IsListeningToTopic(connInfo, topic));
        Assert.Equal(1, jmsService.GetNumConnectionAttempts(connInfo));
        Assert.False(jmsService.RetryConnection(connInfo));
        jmsService.Destroy();
    }
}
