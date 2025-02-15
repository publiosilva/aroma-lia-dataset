public class TestCoordResumeXCommand 
{
    [Fact]
    public void TestCoordSuspendAndResumeForRunning1()
    {
        CoordinatorJobBean job = AddRecordToCoordJobTable(CoordinatorJob.Status.RUNNING, false, false);

        JPAService jpaService = Services.Get<JPAService>();
        Assert.NotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.Id);
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(CoordinatorJob.Status.RUNNING, job.Status);

        new CoordSuspendXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(CoordinatorJob.Status.SUSPENDED, job.Status);
    }

    [Fact]
    public void TestCoordSuspendAndResumeForRunning2()
    {
        CoordinatorJobBean job = AddRecordToCoordJobTable(CoordinatorJob.Status.RUNNING, false, false);

        JPAService jpaService = Services.Get<JPAService>();
        Assert.NotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.Id);
        job = jpaService.Execute(coordJobGetCmd);

        new CoordSuspendXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(CoordinatorJob.Status.SUSPENDED, job.Status);

        new CoordResumeXCommand(job.Id).Call();
        job = jpaService.Execute(coordJobGetCmd);
        Assert.Equal(CoordinatorJob.Status.RUNNING, job.Status);
    }
}
