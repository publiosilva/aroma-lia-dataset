public class TestCoordSubmitXCommand
{
    [Fact]
    public void TestBasicSubmitWithMultipleInstancesInputEvent1()
    {
        Configuration conf = new XConfiguration();
        string appPath = "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "coordinator.xml";

        // CASE 1: Failure case i.e. multiple data-in instances
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance1.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        conf.Set(OozieClient.COORDINATOR_APP_PATH, appPath);
        conf.Set(OozieClient.USER_NAME, GetTestUser());
        CoordSubmitXCommand sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        var exception = Assert.Throws<CommandException>(() => sc.Call());
        Assert.Equal(sc.GetJob().GetStatus(), Job.Status.FAILED);
        Assert.Equal(exception.GetErrorCode(), ErrorCode.E1021);
        Assert.Contains(sc.COORD_INPUT_EVENTS, exception.Message);
        Assert.Contains("per data-in instance", exception.Message);
    }

    [Fact]
    public void TestBasicSubmitWithMultipleInstancesInputEvent2()
    {
        Configuration conf = new XConfiguration();
        string appPath = "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "coordinator.xml";

        // CASE 1: Failure case i.e. multiple data-in instances
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance1.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        conf.Set(OozieClient.COORDINATOR_APP_PATH, appPath);
        conf.Set(OozieClient.USER_NAME, GetTestUser());
        CoordSubmitXCommand sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException) { }

        // CASE 2: Multiple data-in instances specified as separate <instance> tags, but one or more tags are empty. Check works for whitespace in the tags too
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance2.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        var exception = Assert.Throws<CommandException>(() => sc.Call());
        Assert.Equal(sc.GetJob().GetStatus(), Job.Status.FAILED);
        Assert.Equal(exception.GetErrorCode(), ErrorCode.E1021);
        Assert.Contains(sc.COORD_INPUT_EVENTS, exception.Message);
        Assert.Contains("is empty", exception.Message);
    }

    [Fact]
    public void TestBasicSubmitWithMultipleInstancesInputEvent3()
    {
        Configuration conf = new XConfiguration();
        string appPath = "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "coordinator.xml";

        // CASE 1: Failure case i.e. multiple data-in instances
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance1.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        conf.Set(OozieClient.COORDINATOR_APP_PATH, appPath);
        conf.Set(OozieClient.USER_NAME, GetTestUser());
        CoordSubmitXCommand sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException) { }

        // CASE 2: Multiple data-in instances specified as separate <instance> tags, but one or more tags are empty. Check works for whitespace in the tags too
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance2.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException) { }

        // CASE 3: Success case i.e. Multiple data-in instances specified correctly as separate <instance> tags
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance3.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException e)
        {
            Assert.True(false, "Unexpected failure: " + e);
        }
    }

    [Fact]
    public void TestBasicSubmitWithMultipleInstancesInputEvent4()
    {
        Configuration conf = new XConfiguration();
        string appPath = "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "coordinator.xml";

        // CASE 1: Failure case i.e. multiple data-in instances
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance1.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        conf.Set(OozieClient.COORDINATOR_APP_PATH, appPath);
        conf.Set(OozieClient.USER_NAME, GetTestUser());
        CoordSubmitXCommand sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException) { }

        // CASE 2: Multiple data-in instances specified as separate <instance> tags, but one or more tags are empty. Check works for whitespace in the tags too
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance2.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException) { }

        // CASE 3: Success case i.e. Multiple data-in instances specified correctly as separate <instance> tags
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance3.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException) { }

        // CASE 4: Success case i.e. Single instances for input and single instance for output, but both with ","
        using (Reader reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance4.xml", -1))
        using (Writer writer = new StreamWriter(new Uri(appPath).LocalPath))
        {
            IOUtils.CopyCharStream(reader, writer);
        }
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException e)
        {
            Assert.True(false, "Unexpected failure: " + e);
        }
    }
}
