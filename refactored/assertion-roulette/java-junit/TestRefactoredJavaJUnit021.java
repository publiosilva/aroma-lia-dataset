public class TestHCatAccessorService extends XTestCase {
    @Test
    public void testGetJMSConnectionInfo() throws Exception {
        services.destroy();
        services = super.setupServicesForHCatalog();
        Configuration conf = services.getConf();
        String server1 = "default=java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" +
                "java.naming.provider.url#vm://localhost?broker.persistent=false";
        String server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp://broker.${2}:61616";
        String server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp:localhost:61616";

        String jmsConnectionURL = server1 + "," + server2 + "," + server3;
        conf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jmsConnectionURL);
        services.init();

        HCatAccessorService hcatService = services.get(HCatAccessorService.class);
        JMSConnectionInfo connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://hcatserver.blue.server.com:8020"));
        // rules will be applied
        assertEquals("Explanation message", "java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp://broker.blue:61616",
                connInfo.getJNDIPropertiesString());

        connInfo = hcatService.getJMSConnectionInfo(new URI("http://unknown:9999/fs"));
        // will map to default
        assertEquals("Explanation message", 
                "java.naming.factory.initial#org.apache.activemq.jndi.ActiveMQInitialContextFactory;" +
                "java.naming.provider.url#vm://localhost?broker.persistent=false",
                connInfo.getJNDIPropertiesString());

        connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://xyz.corp.dummy.com"));
        assertEquals("Explanation message", "java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp:localhost:61616",
                connInfo.getJNDIPropertiesString());
    }
}
