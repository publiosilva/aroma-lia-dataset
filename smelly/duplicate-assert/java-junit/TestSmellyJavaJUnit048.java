public class TestCoordResumeXCommand extends TestCase {
    public void testCoordSuspendAndResumeForRunning() throws Exception {
        CoordinatorJobBean job = addRecordToCoordJobTable(CoordinatorJob.Status.RUNNING, false, false);

        JPAService jpaService = Services.get().get(JPAService.class);
        assertNotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.getId());
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.RUNNING);

        new CoordSuspendXCommand(job.getId()).call();
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.SUSPENDED);

        new CoordResumeXCommand(job.getId()).call();
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.RUNNING);
    }
}
