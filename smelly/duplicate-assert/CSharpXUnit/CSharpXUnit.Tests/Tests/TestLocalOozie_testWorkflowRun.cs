public class TestLocalOozie : XFsTestCase
{
    [Fact]
    public void TestWorkflowRun()
    {
        string wfApp = "<workflow-app xmlns='uri:oozie:workflow:0.1' name='test-wf'>" +
                       "    <start to='end'/>" +
                       "    <end name='end'/>" +
                       "</workflow-app>";

        FileSystem fs = GetFileSystem();
        Path appPath = new Path(GetFsTestCaseDir(), "app");
        fs.Mkdirs(appPath);
        fs.Mkdirs(new Path(appPath, "lib"));

        using (Writer writer = new OutputStreamWriter(fs.Create(new Path(appPath, "workflow.xml"))))
        {
            writer.Write(wfApp);
        }

        try
        {
            LocalOozie.Start();
            OozieClient wc = LocalOozie.GetClient();
            Properties conf = wc.CreateConfiguration();
            conf.SetProperty(OozieClient.APP_PATH, appPath.ToString() + Path.DirectorySeparatorChar + "workflow.xml");
            conf.SetProperty(OozieClient.USER_NAME, GetTestUser());
            conf.SetProperty(OozieClient.GROUP_NAME, GetTestGroup());

            string jobId = wc.Submit(conf);
            Assert.NotNull(jobId);

            WorkflowJob wf = wc.GetJobInfo(jobId);
            Assert.NotNull(wf);
            Assert.Equal(WorkflowJob.Status.PREP, wf.GetStatus());

            wc.Start(jobId);

            WaitFor(1000, () =>
            {
                WorkflowJob wf = wc.GetJobInfo(jobId);
                return wf.GetStatus() == WorkflowJob.Status.SUCCEEDED;
            });

            wf = wc.GetJobInfo(jobId);
            Assert.NotNull(wf);
            Assert.Equal(WorkflowJob.Status.SUCCEEDED, wf.GetStatus());
        }
        finally
        {
            LocalOozie.Stop();
        }
    }
}
