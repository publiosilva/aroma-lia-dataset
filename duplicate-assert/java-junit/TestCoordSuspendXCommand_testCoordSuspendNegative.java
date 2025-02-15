public class TestCoordSuspendXCommand extends XDataTestCase {
    public void testCoordSuspendNegative() throws Exception {
        CoordinatorJobBean job = addRecordToCoordJobTable(CoordinatorJob.Status.SUCCEEDED, false, false);

        JPAService jpaService = Services.get().get(JPAService.class);
        assertNotNull(jpaService);
        CoordJobGetJPAExecutor coordJobGetCmd = new CoordJobGetJPAExecutor(job.getId());
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.SUCCEEDED);

        new CoordSuspendXCommand(job.getId()).call();
        job = jpaService.execute(coordJobGetCmd);
        assertEquals(job.getStatus(), CoordinatorJob.Status.SUCCEEDED);
    }
}
