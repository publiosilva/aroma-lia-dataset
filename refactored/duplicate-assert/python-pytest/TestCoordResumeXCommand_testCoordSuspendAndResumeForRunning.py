import pytest

class TestCoordResumeXCommand:
    def test_coord_suspend_and_resume_for_running_1(self):
        job = add_record_to_coord_job_table(CoordinatorJob.Status.RUNNING, False, False)

        jpa_service = Services.get().get(JPAService)
        assert jpa_service is not None
        coord_job_get_cmd = CoordJobGetJPAExecutor(job.get_id())
        job = jpa_service.execute(coord_job_get_cmd)
        assert job.get_status() == CoordinatorJob.Status.RUNNING

        CoordSuspendXCommand(job.get_id()).call()
        job = jpa_service.execute(coord_job_get_cmd)
        assert job.get_status() == CoordinatorJob.Status.SUSPENDED

    def test_coord_suspend_and_resume_for_running_2(self):
        job = add_record_to_coord_job_table(CoordinatorJob.Status.RUNNING, False, False)

        jpa_service = Services.get().get(JPAService)
        assert jpa_service is not None
        coord_job_get_cmd = CoordJobGetJPAExecutor(job.get_id())
        job = jpa_service.execute(coord_job_get_cmd)

        CoordSuspendXCommand(job.get_id()).call()
        job = jpa_service.execute(coord_job_get_cmd)
        assert job.get_status() == CoordinatorJob.Status.SUSPENDED

        CoordResumeXCommand(job.get_id()).call()
        job = jpa_service.execute(coord_job_get_cmd)
        assert job.get_status() == CoordinatorJob.Status.RUNNING
