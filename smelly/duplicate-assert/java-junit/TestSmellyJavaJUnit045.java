public class TestActionCheckerService extends TestCase {
    public void testActionCheckerServiceDelay() throws Exception {
        Reader reader = IOUtils.getResourceAsReader("wf-ext-schema-valid.xml", -1);
        Writer writer = new FileWriter(getTestCaseDir() + "/workflow.xml");
        IOUtils.copyCharStream(reader, writer);

        final DagEngine engine = new DagEngine("u", "a");
        Configuration conf = new XConfiguration();
        conf.set(OozieClient.APP_PATH, "file://" + getTestCaseDir() + File.separator + "workflow.xml");
        conf.setStrings(WorkflowAppService.HADOOP_USER, getTestUser());
        conf.setStrings(OozieClient.GROUP_NAME, getTestGroup());

        conf.set(OozieClient.LOG_TOKEN, "t");

        conf.set("external-status", "ok");
        conf.set("signal-value", "based_on_action_status");
        conf.set("running-mode", "async");

        final String jobId = engine.submitJob(conf, true);
        sleep(200);

        waitFor(5000, new Predicate() {
            public boolean evaluate() throws Exception {
                return (engine.getJob(jobId).getStatus() == WorkflowJob.Status.RUNNING);
            }
        });

        sleep(100);

        JPAService jpaService = Services.get().get(JPAService.class);
        assertNotNull(jpaService);
        WorkflowActionsGetForJobJPAExecutor actionsGetExecutor = new WorkflowActionsGetForJobJPAExecutor(jobId);
        List<WorkflowActionBean> actions = jpaService.execute(actionsGetExecutor);
        WorkflowActionBean action = null;
        for (WorkflowActionBean bean : actions) {
            if (bean.getType().equals("test")) {
                action = bean;
                break;
            }
        }
        assertNotNull(action);
        assertEquals(WorkflowActionBean.Status.RUNNING, action.getStatus());

        action.setLastCheckTime(new Date());
        jpaService.execute(new WorkflowActionUpdateJPAExecutor(action));

        int actionCheckDelay = 20;

        Runnable actionCheckRunnable = new ActionCheckRunnable(actionCheckDelay);
        actionCheckRunnable.run();

        sleep(3000);

        List<WorkflowActionBean> actions2 = jpaService.execute(actionsGetExecutor);
        WorkflowActionBean action2 = null;
        for (WorkflowActionBean bean : actions2) {
            if (bean.getType().equals("test")) {
                action2 = bean;
                break;
            }
        }
        assertNotNull(action);
        assertEquals(WorkflowActionBean.Status.RUNNING, action2.getStatus());
        assertEquals(WorkflowJob.Status.RUNNING, engine.getJob(jobId).getStatus());
    }
}
