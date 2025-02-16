public class TestJMSAccessorService {
    @Test
    public void testConnectionRetryMaxAttempt() throws Exception {
        services.destroy();
        services = super.setupServicesForHCatalog();
        String jndiPropertiesString = "java.naming.factory.initial#" + ActiveMQConnFactory + ";"
                + "java.naming.provider.url#" + "tcp://localhost:12345;connectionFactoryNames#ConnectionFactory";
        Configuration servicesConf = services.getConf();
        servicesConf.set(JMSAccessorService.CONF_RETRY_INITIAL_DELAY, "1");
        servicesConf.set(JMSAccessorService.CONF_RETRY_MAX_ATTEMPTS, "1");
        servicesConf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, "default=" + jndiPropertiesString);
        services.init();
        HCatAccessorService hcatService = Services.get().get(HCatAccessorService.class);
        JMSAccessorService jmsService = Services.get().get(JMSAccessorService.class);

        String publisherAuthority = "hcat.server.com:5080";
        String topic = "topic.topic1";
        JMSConnectionInfo connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://hcat.server.com:8020"));
        jmsService.registerForNotification(connInfo, topic, new HCatMessageHandler(publisherAuthority));
        assertTrue(jmsService.isConnectionInRetryList(connInfo));
        assertTrue(jmsService.isTopicInRetryList(connInfo, topic));
        assertFalse(jmsService.isListeningToTopic(connInfo, topic));
        // Thread.sleep(1100);
        // Should not retry again as max attempt is 1
        assertTrue(jmsService.isConnectionInRetryList(connInfo));
        assertTrue(jmsService.isTopicInRetryList(connInfo, topic));
        assertFalse(jmsService.isListeningToTopic(connInfo, topic));
        assertEquals(1, jmsService.getNumConnectionAttempts(connInfo));
        assertFalse(jmsService.retryConnection(connInfo));
        jmsService.destroy();
    }

}
