public class TestParameterVerifier extends XTestCase {
    public void testVerifyParametersMissing() throws Exception {
        Configuration conf = new Configuration(false);
        
        String str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name></property>"
                + "</parameters></root>";
        try {
            ParameterVerifier.verifyParameters(conf, XmlUtils.parseXml(str));
            fail();
        } catch(ParameterVerifierException ex) {
            assertEquals(ErrorCode.E0738, ex.getErrorCode());
            assertTrue(ex.getMessage().endsWith("hello"));
            assertTrue(ex.getMessage().contains("1"));
            assertEquals(0, conf.size());
        }
        
        conf = new Configuration(false);
        
        str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name><value>world</value></property>"
                + "</parameters></root>";
        ParameterVerifier.verifyParameters(conf, XmlUtils.parseXml(str));
        assertEquals(1, conf.size());
        assertEquals("world", conf.get("hello"));
        
        conf = new Configuration(false);
        
        str = "<root xmlns=\"uri:oozie:workflow:0.4\"><parameters>"
                + "<property><name>hello</name></property>"
                + "<property><name>foo</name><value>bar</value></property>"
                + "<property><name>meh</name></property>"
                + "</parameters></root>";
        try {
            ParameterVerifier.verifyParameters(conf, XmlUtils.parseXml(str));
            fail();
        } catch(ParameterVerifierException ex) {
            assertEquals(ErrorCode.E0738, ex.getErrorCode());
            assertTrue(ex.getMessage().endsWith("hello, meh"));
            assertFalse(ex.getMessage().contains("foo"));
            assertTrue(ex.getMessage().contains("2"));
            assertEquals(1, conf.size());
            assertEquals("bar", conf.get("foo"));
        }
    }
}
