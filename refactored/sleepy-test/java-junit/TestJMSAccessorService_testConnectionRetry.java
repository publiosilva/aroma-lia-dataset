public class TestJMSAccessorService {
    @Test
    public void testConnectionRetry() throws Exception {
        services.destroy();
        services = super.setupServicesForHCatalog();
        int randomPort = 30000 + random.nextInt(10000);
        String brokerURl = "tcp://localhost:" + randomPort;
        Configuration servicesConf = services.getConf();
        servicesConf.set(JMSAccessorService.CONF_RETRY_INITIAL_DELAY, "1");
        servicesConf.set(JMSAccessorService.CONF_RETRY_MAX_ATTEMPTS, "3");
        servicesConf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, "default=java.naming.factory.initial#"
                + ActiveMQConnFactory + ";" + "java.naming.provider.url#" + brokerURl + ";" + "connectionFactoryNames#"
                + "ConnectionFactory");
        services.init();
        HCatAccessorService hcatService = Services.get().get(HCatAccessorService.class);
        JMSAccessorService jmsService = Services.get().get(JMSAccessorService.class);

        String publisherAuthority = "hcat.server.com:5080";
        String topic = "topic.topic1";
        JMSConnectionInfo connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://hcat.server.com:8020"));
        jmsService.registerForNotification(connInfo, topic, new HCatMessageHandler(publisherAuthority));
        assertFalse(jmsService.isListeningToTopic(connInfo, topic));
        assertTrue(jmsService.isConnectionInRetryList(connInfo));
        assertTrue(jmsService.isTopicInRetryList(connInfo, topic));
        // Start the broker and check if listening to topic now
        BrokerService broker = new BrokerService();
        broker.addConnector(brokerURl);
        broker.start();
        // Thread.sleep(1000);
        assertTrue(jmsService.isListeningToTopic(connInfo, topic));
        assertFalse(jmsService.isConnectionInRetryList(connInfo));
        assertFalse(jmsService.isTopicInRetryList(connInfo, topic));
        broker.stop();
        jmsService.destroy();

    }
}
