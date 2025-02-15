public class TestHCatAccessorService extends XTestCase {
    @Test
    public void testGetJMSConnectionInfoNoDefault() throws Exception {
        services.destroy();
        services = super.setupServicesForHCatalog();
        Configuration conf = services.getConf();
        String server2 = "hcat://${1}.${2}.server.com:8020=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp://broker.${2}:61616";
        String server3 = "hcat://xyz.corp.dummy.com=java.naming.factory.initial#Dummy.Factory;" +
                "java.naming.provider.url#tcp:localhost:61616";

        String jmsConnectionURL = server2 + "," + server3;
        conf.set(HCatAccessorService.JMS_CONNECTIONS_PROPERTIES, jmsConnectionURL);
        services.init();

        HCatAccessorService hcatService = services.get(HCatAccessorService.class);
        // No default JMS mapping
        JMSConnectionInfo connInfo = hcatService.getJMSConnectionInfo(new URI("http://unknown:9999/fs"));
        assertNull(connInfo);
        connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://server1.colo1.server.com:8020/db/table/pk1=val1;pk2=val2"));
        assertEquals("java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp://broker.colo1:61616",
                connInfo.getJNDIPropertiesString());
        connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://xyz.corp.dummy.com/db/table"));
        assertEquals("java.naming.factory.initial#Dummy.Factory;java.naming.provider.url#tcp:localhost:61616",
                connInfo.getJNDIPropertiesString());
    }
}
