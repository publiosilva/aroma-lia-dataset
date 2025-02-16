public class TestJMSAccessorService extends XTestCase {
    @Test
    public void testConnection() throws Exception {
        HCatAccessorService hcatService = services.get(HCatAccessorService.class);
        JMSAccessorService jmsService = services.get(JMSAccessorService.class);
        // both servers should connect to default JMS server
        JMSConnectionInfo connInfo = hcatService.getJMSConnectionInfo(new URI("hcat://hcatserver.blue.server.com:8020"));
        ConnectionContext ctxt1 = jmsService.createConnectionContext(connInfo);
        assertTrue("Explanation message", ctxt1.isConnectionInitialized());
        JMSConnectionInfo connInfo1 = hcatService.getJMSConnectionInfo(new URI("http://unknown:80"));
        ConnectionContext ctxt2 = jmsService.createConnectionContext(connInfo1);
        assertTrue("Explanation message", ctxt2.isConnectionInitialized());
        assertEquals("Explanation message", ctxt1, ctxt2);
        ctxt1.close();
    }
}
