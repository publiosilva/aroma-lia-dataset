public class TestUUIDService extends TestCase {
    public void testChildId() throws Exception {
        setSystemProperty(UUIDService.CONF_GENERATOR, "counter");
        Services services = new Services();
        services.init();
        try {
            UUIDService uuid = services.get(UUIDService.class);
            String id = uuid.generateId(ApplicationType.WORKFLOW);
            String childId = uuid.generateChildId(id, "a");
            assertEquals(id, uuid.getId(childId));
            assertEquals("a", uuid.getChildName(childId));
        }
        finally {
            services.destroy();
        }

        setSystemProperty(UUIDService.CONF_GENERATOR, "random");
        services = new Services();
        services.init();
        try {
            UUIDService uuid = services.get(UUIDService.class);
            String id = uuid.generateId(ApplicationType.WORKFLOW);
            String childId = uuid.generateChildId(id, "a");
            assertEquals(id, uuid.getId(childId));
            assertEquals("a", uuid.getChildName(childId));
        }
        finally {
            services.destroy();
        }
    }

}
