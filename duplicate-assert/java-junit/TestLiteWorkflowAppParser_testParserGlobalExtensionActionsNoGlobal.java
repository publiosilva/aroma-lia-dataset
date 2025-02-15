public class TestLiteWorkflowAppParser extends XTestCase {
    public void testParserGlobalExtensionActionsNoGlobal() throws Exception {
        LiteWorkflowAppParser parser = new LiteWorkflowAppParser(null,
            LiteWorkflowStoreService.LiteControlNodeHandler.class,
            LiteWorkflowStoreService.LiteDecisionHandler.class,
            LiteWorkflowStoreService.LiteActionHandler.class);

        // If no global section is defined, some extension actions (e.g. hive) must still have name-node and job-tracker elements
        // or the handleGlobal() method will throw an exception

        parser.validateAndParse(IOUtils.getResourceAsReader("wf-schema-valid-global-ext-no-global.xml", -1), new Configuration());

        try {
            parser.validateAndParse(IOUtils.getResourceAsReader("wf-schema-invalid-global-ext-no-global.xml", -1),
                    new Configuration());
            fail();
        }
        catch (WorkflowException ex) {
            assertEquals(ErrorCode.E0701, ex.getErrorCode());
        }
        catch (Exception ex) {
            fail();
        }
    }
}
