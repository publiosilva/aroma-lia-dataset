using Xunit;

public class TestCoordSubmitXCommand : XDataTestCase
{
    [Fact]
    public void TestBasicSubmitWithMultipleInstancesInputEvent()
    {
        var conf = new XConfiguration();
        string appPath = "file://" + GetTestCaseDir() + Path.DirectorySeparatorChar + "coordinator.xml";

        // CASE 1: Failure case i.e. multiple data-in instances
        using var reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance1.xml", -1);
        using var writer = new FileWriter(new Uri(appPath).AbsolutePath);
        IOUtils.CopyCharStream(reader, writer);
        conf.Set(OozieClient.COORDINATOR_APP_PATH, appPath);
        conf.Set(OozieClient.USER_NAME, GetTestUser());
        var sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
            Assert.True(false, "Expected to catch errors due to incorrectly specified input data set instances");
        }
        catch (CommandException e)
        {
            Assert.Equal(Job.Status.FAILED, sc.GetJob().GetStatus());
            Assert.Equal(ErrorCode.E1021, e.GetErrorCode());
            Assert.True(e.Message.Contains(sc.COORD_INPUT_EVENTS) && e.Message.Contains("per data-in instance"));
        }

        // CASE 2: Multiple data-in instances specified as separate <instance> tags, but one or more tags are empty.
        reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance2.xml", -1);
        writer = new FileWriter(new Uri(appPath).AbsolutePath);
        IOUtils.CopyCharStream(reader, writer);
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
            Assert.True(false, "Expected to catch errors due to incorrectly specified input data set instances");
        }
        catch (CommandException e)
        {
            Assert.Equal(Job.Status.FAILED, sc.GetJob().GetStatus());
            Assert.Equal(ErrorCode.E1021, e.GetErrorCode());
            Assert.True(e.Message.Contains(sc.COORD_INPUT_EVENTS) && e.Message.Contains("is empty"));
        }

        // CASE 3: Success case i.e. Multiple data-in instances specified correctly as separate <instance> tags
        reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance3.xml", -1);
        writer = new FileWriter(new Uri(appPath).AbsolutePath);
        IOUtils.CopyCharStream(reader, writer);
        sc = new CoordSubmitXCommand(conf, "UNIT_TESTING");

        try
        {
            sc.Call();
        }
        catch (CommandException e)
        {
            Assert.True(false, "Unexpected failure: " + e);
        }

        // CASE 4: Success case i.e. Single instances for input and single instance for output, but both with ","
        reader = IOUtils.GetResourceAsReader("coord-multiple-input-instance4.xml", -1);
        writer = new FileWriter(new Uri(appPath).AbsolutePath);
        IOUtils.CopyCharStream(reader, writer);
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
