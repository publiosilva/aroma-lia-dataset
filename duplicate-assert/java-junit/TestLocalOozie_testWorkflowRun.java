public class TestLocalOozie extends XFsTestCase {
    public void testWorkflowRun() throws Exception {
        String wfApp = "<workflow-app xmlns='uri:oozie:workflow:0.1' name='test-wf'>" +
                "    <start to='end'/>" +
                "    <end name='end'/>" +
                "</workflow-app>";

        FileSystem fs = getFileSystem();
        Path appPath = new Path(getFsTestCaseDir(), "app");
        fs.mkdirs(appPath);
        fs.mkdirs(new Path(appPath, "lib"));

        Writer writer = new OutputStreamWriter(fs.create(new Path(appPath, "workflow.xml")));
        writer.write(wfApp);
        writer.close();

        try {
            LocalOozie.start();
            final OozieClient wc = LocalOozie.getClient();
            Properties conf = wc.createConfiguration();
            conf.setProperty(OozieClient.APP_PATH, appPath.toString() + File.separator + "workflow.xml");
            conf.setProperty(OozieClient.USER_NAME, getTestUser());
            conf.setProperty(OozieClient.GROUP_NAME, getTestGroup());


            final String jobId = wc.submit(conf);
            assertNotNull(jobId);

            WorkflowJob wf = wc.getJobInfo(jobId);
            assertNotNull(wf);
            assertEquals(WorkflowJob.Status.PREP, wf.getStatus());

            wc.start(jobId);

            waitFor(1000, new Predicate() {
                public boolean evaluate() throws Exception {
                    WorkflowJob wf = wc.getJobInfo(jobId);
                    return wf.getStatus() == WorkflowJob.Status.SUCCEEDED;
                }
            });

            wf = wc.getJobInfo(jobId);
            assertNotNull(wf);
            assertEquals(WorkflowJob.Status.SUCCEEDED, wf.getStatus());
        }
        finally {
            LocalOozie.stop();
        }
    }
}
