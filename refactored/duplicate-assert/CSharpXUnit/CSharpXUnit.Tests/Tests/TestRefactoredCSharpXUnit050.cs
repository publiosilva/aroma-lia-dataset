public class TestCoordSuspendXCommand
{
    [Fact]
    public void TestCoordSuspendNegative1()
    {
        CoordinatorJobBean job = AddRecordToCoordJobTable(CoordinatorJob.Status.SUCCEEDED, false, false);

        JPAService jpaService = Services.Get<JPAService>();
        Assert.NotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.Id);
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.SUCCEEDED);
    }

    [Fact]
    public void TestCoordSuspendNegative2()
    {
        CoordinatorJobBean job = AddRecordToCoordJobTable(CoordinatorJob.Status.SUCCEEDED, false, false);

        JPAService jpaService = Services.Get<JPAService>();
        Assert.NotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.Id);
        job = jpaService.Execute(coordJobGetCmd);
        new CoordSuspendXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(job.Status, CoordinatorJob.Status.SUCCEEDED);
    }
}
