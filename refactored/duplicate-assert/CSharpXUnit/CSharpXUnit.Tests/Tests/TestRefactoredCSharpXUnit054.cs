using Xunit;

public class TestLocalOozie
{
    [Fact]
    public void TestWorkflowRun1()
    {
        string wfApp = "<workflow-app xmlns='uri:oozie:workflow:0.1' name='test-wf'>" +
                       "    <start to='end'/>" +
                       "    <end name='end'/>" +
                       "</workflow-app>";

        var fs = GetFileSystem();
        var appPath = new Path(GetFsTestCaseDir(), "app");
        fs.Mkdirs(appPath);
        fs.Mkdirs(new Path(appPath, "lib"));

        using (var writer = new OutputStreamWriter(fs.Create(new Path(appPath, "workflow.xml"))))
        {
            writer.Write(wfApp);
        }

        try
        {
            LocalOozie.Start();
            var wc = LocalOozie.GetClient();
            var conf = wc.CreateConfiguration();
            conf.SetProperty(OozieClient.APP_PATH, appPath.ToString() + Path.DirectorySeparatorChar + "workflow.xml");
            conf.SetProperty(OozieClient.USER_NAME, GetTestUser());
            conf.SetProperty(OozieClient.GROUP_NAME, GetTestGroup());

            var jobId = wc.Submit(conf);
            Assert.NotNull(jobId);

            var wf = wc.GetJobInfo(jobId);
            Assert.NotNull(wf);
            Assert.Equal(WorkflowJob.Status.PREP, wf.Status);

            wc.Start(jobId);

            WaitFor(1000, () =>
            {
                var wfInfo = wc.GetJobInfo(jobId);
                return wfInfo.Status == WorkflowJob.Status.SUCCEEDED;
            });

            wf = wc.GetJobInfo(jobId);
            Assert.Equal(WorkflowJob.Status.SUCCEEDED, wf.Status);
        }
        finally
        {
            LocalOozie.Stop();
        }
    }

    [Fact]
    public void TestWorkflowRun2()
    {
        string wfApp = "<workflow-app xmlns='uri:oozie:workflow:0.1' name='test-wf'>" +
                       "    <start to='end'/>" +
                       "    <end name='end'/>" +
                       "</workflow-app>";

        var fs = GetFileSystem();
        var appPath = new Path(GetFsTestCaseDir(), "app");
        fs.Mkdirs(appPath);
        fs.Mkdirs(new Path(appPath, "lib"));

        using (var writer = new OutputStreamWriter(fs.Create(new Path(appPath, "workflow.xml"))))
        {
            writer.Write(wfApp);
        }

        try
        {
            LocalOozie.Start();
            var wc = LocalOozie.GetClient();
            var conf = wc.CreateConfiguration();
            conf.SetProperty(OozieClient.APP_PATH, appPath.ToString() + Path.DirectorySeparatorChar + "workflow.xml");
            conf.SetProperty(OozieClient.USER_NAME, GetTestUser());
            conf.SetProperty(OozieClient.GROUP_NAME, GetTestGroup());

            var jobId = wc.Submit(conf);
            Assert.NotNull(jobId);

            var wf = wc.GetJobInfo(jobId);
            Assert.Equal(WorkflowJob.Status.PREP, wf.Status);

            wc.Start(jobId);

            WaitFor(1000, () =>
            {
                var wfInfo = wc.GetJobInfo(jobId);
                return wfInfo.Status == WorkflowJob.Status.SUCCEEDED;
            });

            wf = wc.GetJobInfo(jobId);
            Assert.NotNull(wf);
            Assert.Equal(WorkflowJob.Status.SUCCEEDED, wf.Status);
        }
        finally
        {
            LocalOozie.Stop();
        }
    }
}
