public class TestCoordSuspendXCommand extends TestCase {
    public void testCoordSuspendNegative1() throws Exception {
        CoordinatorJobBean job = addRecordToCoordJobTable(CoordinatorJob.Status.SUCCEEDED, false, false);

        JPAService jpaService = Services.get().get(JPAService.class);
        assertNotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.getId());
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.SUCCEEDED);
    }

    public void testCoordSuspendNegative2() throws Exception {
        CoordinatorJobBean job = addRecordToCoordJobTable(CoordinatorJob.Status.SUCCEEDED, false, false);

        JPAService jpaService = Services.get().get(JPAService.class);
        assertNotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.getId());
        job = jpaService.execute(coordJobGetCmd);
        new CoordSuspendXCommand(job.getId()).call();
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.SUCCEEDED);
    }
}
