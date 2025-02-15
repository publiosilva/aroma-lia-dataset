using System;
using System.IO;
using System.Collections.Generic;
using Xunit;

public class TestActionCheckerService {
    [Fact]
    public void TestActionCheckerServiceDelay1() {
        using (Reader reader = IOUtils.GetResourceAsReader("wf-ext-schema-valid.xml", -1))
        using (Writer writer = new FileWriter(GetTestCaseDir() + "/workflow.xml")) {
            IOUtils.CopyCharStream(reader, writer);
        }

        DagEngine engine = new DagEngine("u", "a");
        Configuration conf = new XConfiguration();
        conf.Set(OozieClient.APP_PATH, "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "workflow.xml");
        conf.SetStrings(WorkflowAppService.HADOOP_USER, GetTestUser());
        conf.SetStrings(OozieClient.GROUP_NAME, GetTestGroup());

        conf.Set(OozieClient.LOG_TOKEN, "t");

        conf.Set("external-status", "ok");
        conf.Set("signal-value", "based_on_action_status");
        conf.Set("running-mode", "async");

        string jobId = engine.SubmitJob(conf, true);
        Sleep(200);

        WaitFor(5000, () => engine.GetJob(jobId).GetStatus() == WorkflowJob.Status.RUNNING);

        Sleep(100);

        JPAService jpaService = Services.Get().Get<JPAService>();
        Assert.NotNull(jpaService);
        WorkflowActionsGetForJobJPAExecutor actionsGetExecutor = new WorkflowActionsGetForJobJPAExecutor(jobId);
        List<WorkflowActionBean> actions = jpaService.Execute(actionsGetExecutor);
        WorkflowActionBean action = null;
        foreach (WorkflowActionBean bean in actions) {
            if (bean.GetType().Equals("test")) {
                action = bean;
                break;
            }
        }
        Assert.NotNull(action);
        Assert.Equal(WorkflowActionBean.Status.RUNNING, action.GetStatus());
    }

    [Fact]
    public void TestActionCheckerServiceDelay2() {
        using (Reader reader = IOUtils.GetResourceAsReader("wf-ext-schema-valid.xml", -1))
        using (Writer writer = new FileWriter(GetTestCaseDir() + "/workflow.xml")) {
            IOUtils.CopyCharStream(reader, writer);
        }

        DagEngine engine = new DagEngine("u", "a");
        Configuration conf = new XConfiguration();
        conf.Set(OozieClient.APP_PATH, "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "workflow.xml");
        conf.SetStrings(WorkflowAppService.HADOOP_USER, GetTestUser());
        conf.SetStrings(OozieClient.GROUP_NAME, GetTestGroup());

        conf.Set(OozieClient.LOG_TOKEN, "t");

        conf.Set("external-status", "ok");
        conf.Set("signal-value", "based_on_action_status");
        conf.Set("running-mode", "async");

        string jobId = engine.SubmitJob(conf, true);
        Sleep(200);

        WaitFor(5000, () => engine.GetJob(jobId).GetStatus() == WorkflowJob.Status.RUNNING);

        Sleep(100);

        JPAService jpaService = Services.Get().Get<JPAService>();
        Assert.NotNull(jpaService);
        WorkflowActionsGetForJobJPAExecutor actionsGetExecutor = new WorkflowActionsGetForJobJPAExecutor(jobId);
        List<WorkflowActionBean> actions = jpaService.Execute(actionsGetExecutor);
        WorkflowActionBean action = null;
        foreach (WorkflowActionBean bean in actions) {
            if (bean.GetType().Equals("test")) {
                action = bean;
                break;
            }
        }

        action.SetLastCheckTime(new DateTime());
        jpaService.Execute(new WorkflowActionUpdateJPAExecutor(action));

        int actionCheckDelay = 20;

        ActionCheckRunnable actionCheckRunnable = new ActionCheckRunnable(actionCheckDelay);
        actionCheckRunnable.Run();

        Sleep(3000);

        List<WorkflowActionBean> actions2 = jpaService.Execute(actionsGetExecutor);
        WorkflowActionBean action2 = null;
        foreach (WorkflowActionBean bean in actions2) {
            if (bean.GetType().Equals("test")) {
                action2 = bean;
                break;
            }
        }
        Assert.NotNull(action);
        Assert.Equal(WorkflowActionBean.Status.RUNNING, action2.GetStatus());
        Assert.Equal(WorkflowJob.Status.RUNNING, engine.GetJob(jobId).GetStatus());    
    }
}
