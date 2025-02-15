public class TestActionCheckerService : XDataTestCase
{
    [Fact]
    public void TestActionCheckerServiceDelay()
    {
        using var reader = IOUtils.GetResourceAsReader("wf-ext-schema-valid.xml", -1);
        using var writer = new StreamWriter(Path.Combine(GetTestCaseDir(), "workflow.xml"));
        IOUtils.CopyCharStream(reader, writer);

        var engine = new DagEngine("u", "a");
        var conf = new XConfiguration();
        conf.Set(OozieClient.APP_PATH, "file://" + Path.Combine(GetTestCaseDir(), "workflow.xml"));
        conf.SetStrings(WorkflowAppService.HADOOP_USER, GetTestUser());
        conf.SetStrings(OozieClient.GROUP_NAME, GetTestGroup());

        conf.Set(OozieClient.LOG_TOKEN, "t");

        conf.Set("external-status", "ok");
        conf.Set("signal-value", "based_on_action_status");
        conf.Set("running-mode", "async");

        var jobId = engine.SubmitJob(conf, true);
        Thread.Sleep(200);

        WaitFor(5000, () => engine.GetJob(jobId).GetStatus() == WorkflowJob.Status.RUNNING);

        Thread.Sleep(100);

        var jpaService = Services.Get().Get<JPAService>();
        Assert.NotNull(jpaService);
        var actionsGetExecutor = new WorkflowActionsGetForJobJPAExecutor(jobId);
        var actions = jpaService.Execute(actionsGetExecutor);
        WorkflowActionBean action = null;
        foreach (var bean in actions)
        {
            if (bean.GetType() == "test")
            {
                action = bean;
                break;
            }
        }
        Assert.NotNull(action);
        Assert.Equal(WorkflowActionBean.Status.RUNNING, action.GetStatus());

        action.SetLastCheckTime(DateTime.Now);
        jpaService.Execute(new WorkflowActionUpdateJPAExecutor(action));

        var actionCheckDelay = 20;

        var actionCheckRunnable = new ActionCheckRunnable(actionCheckDelay);
        actionCheckRunnable.Run();

        Thread.Sleep(3000);

        var actions2 = jpaService.Execute(actionsGetExecutor);
        WorkflowActionBean action2 = null;
        foreach (var bean in actions2)
        {
            if (bean.GetType() == "test")
            {
                action2 = bean;
                break;
            }
        }
        Assert.NotNull(action);
        Assert.Equal(WorkflowActionBean.Status.RUNNING, action2.GetStatus());
        Assert.Equal(WorkflowJob.Status.RUNNING, engine.GetJob(jobId).GetStatus());
    }
}
