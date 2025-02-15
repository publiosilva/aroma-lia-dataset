public class TestEventHandlerService extends XDataTestCase {
    @Test
    public void testService() throws Exception {
        EventHandlerService ehs = _testEventHandlerService();
        // check default initializations
        assertTrue(ehs.getEventQueue() instanceof MemoryEventQueue);
        Set<String> jobtypes = ehs.getAppTypes();
        assertTrue(jobtypes.contains("workflow_job"));
        assertTrue(jobtypes.contains("coordinator_action"));

        Services services = Services.get();
        services.destroy();
        services = new Services();
        Configuration conf = services.getConf();
        conf.set(Services.CONF_SERVICE_EXT_CLASSES, "");
        services.init();
        assertFalse(EventHandlerService.isEnabled());
    }
}
