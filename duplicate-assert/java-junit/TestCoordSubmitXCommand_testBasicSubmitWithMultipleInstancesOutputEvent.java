public class TestCoordSubmitXCommand extends XDataTestCase {
    public void testBasicSubmitWithMultipleInstancesInputEvent() throws Exception {
        Configuration conf = new XConfiguration();
        String appPath = "file://" + getTestCaseDir() + File.separator + "coordinator.xml";

        // CASE 1: Failure case i.e. multiple data-in instances
        Reader reader = IOUtils.getResourceAsReader("coord-multiple-input-instance1.xml", -1);
        Writer writer = new FileWriter(new URI(appPath).getPath());
        IOUtils.copyCharStream(reader, writer);
        conf.set(OozieClient.COORDINATOR_APP_PATH, appPath);
        conf.set(OozieClient.USER_NAME, getTestUser());
        CoordSubmitXCommand sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try {
            sc.call();
            fail("Expected to catch errors due to incorrectly specified input data set instances");
        }
        catch (CommandException e) {
            assertEquals(sc.getJob().getStatus(), Job.Status.FAILED);
            assertEquals(e.getErrorCode(), ErrorCode.E1021);
            assertTrue(e.getMessage().contains(sc.COORD_INPUT_EVENTS) && e.getMessage().contains("per data-in instance"));
        }

        // CASE 2: Multiple data-in instances specified as separate <instance> tags, but one or more tags are empty. Check works for whitespace in the tags too
        reader = IOUtils.getResourceAsReader("coord-multiple-input-instance2.xml", -1);
        writer = new FileWriter(new URI(appPath).getPath());
        IOUtils.copyCharStream(reader, writer);
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try {
            sc.call();
            fail("Expected to catch errors due to incorrectly specified input data set instances");
        }
        catch (CommandException e) {
            assertEquals(sc.getJob().getStatus(), Job.Status.FAILED);
            assertEquals(e.getErrorCode(), ErrorCode.E1021);
            assertTrue(e.getMessage().contains(sc.COORD_INPUT_EVENTS) && e.getMessage().contains("is empty"));
        }

        // CASE 3: Success case i.e. Multiple data-in instances specified correctly as separate <instance> tags
        reader = IOUtils.getResourceAsReader("coord-multiple-input-instance3.xml", -1);
        writer = new FileWriter(new URI(appPath).getPath());
        IOUtils.copyCharStream(reader, writer);
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try {
            sc.call();
        }
        catch (CommandException e) {
            fail("Unexpected failure: " + e);
        }

        // CASE 4: Success case i.e. Single instances for input and single instance for output, but both with ","
        reader = IOUtils.getResourceAsReader("coord-multiple-input-instance4.xml", -1);
        writer = new FileWriter(new URI(appPath).getPath());
        IOUtils.copyCharStream(reader, writer);
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try {
            sc.call();
        }
        catch (CommandException e) {
            fail("Unexpected failure: " + e);
        }
    }
}
