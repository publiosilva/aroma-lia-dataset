public class TestCoordSuspendXCommand : XDataTestCase
{
    [Fact]
    public void TestCoordSuspendNegative()
    {
        CoordinatorJobBean job = AddRecordToCoordJobTable(CoordinatorJob.Status.SUCCEEDED, false, false);

        JPAService jpaService = Services.Get<JPAService>();
        Assert.NotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.Id);
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.SUCCEEDED);

        new CoordSuspendXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.SUCCEEDED);
    }
}
