import pytest

def test_coord_suspend_negative():
    job = add_record_to_coord_job_table(CoordinatorJob.Status.SUCCEEDED, False, False)

    jpa_service = Services.get().get(JPAService)
    assert jpa_service is not None
    coord_job_get_cmd = CoordJobGetJPAExecutor(job.get_id())
    job = jpa_service.execute(coord_job_get_cmd)
    assert job.get_status() == CoordinatorJob.Status.SUCCEEDED

    CoordSuspendXCommand(job.get_id()).call()
    job = jpa_service.execute(coord_job_get_cmd)
    assert job.get_status() == CoordinatorJob.Status.SUCCEEDED
