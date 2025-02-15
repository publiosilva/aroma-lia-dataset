public class TestCoordResumeXCommand : XDataTestCase
{
    [Fact]
    public void TestCoordSuspendAndResumeForRunning()
    {
        var job = AddRecordToCoordJobTable(CoordinatorJob.Status.RUNNING, false, false);

        var jpaService = Services.Get<JPAService>();
        Assert.NotNull(jpaService);
        var coordJobGetCmd = new CoordJobGetJPAExecutor(job.Id);
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.RUNNING);

        new CoordSuspendXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.SUSPENDED);

        new CoordResumeXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.RUNNING);
    }
}
