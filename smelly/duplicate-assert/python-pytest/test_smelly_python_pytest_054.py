import pytest

def test_workflow_run():
    wf_app = "<workflow-app xmlns='uri:oozie:workflow:0.1' name='test-wf'>" \
             "    <start to='end'/>" \
             "    <end name='end'/>" \
             "</workflow-app>"

    fs = get_file_system()
    app_path = Path(get_fs_test_case_dir(), "app")
    fs.mkdirs(app_path)
    fs.mkdirs(Path(app_path, "lib"))

    with fs.create(Path(app_path, "workflow.xml")) as writer:
        writer.write(wf_app)

    try:
        LocalOozie.start()
        wc = LocalOozie.get_client()
        conf = wc.create_configuration()
        conf.set_property(OozieClient.APP_PATH, str(app_path) + os.sep + "workflow.xml")
        conf.set_property(OozieClient.USER_NAME, get_test_user())
        conf.set_property(OozieClient.GROUP_NAME, get_test_group())

        job_id = wc.submit(conf)
        assert job_id is not None

        wf = wc.get_job_info(job_id)
        assert wf is not None
        assert wf.get_status() == WorkflowJob.Status.PREP

        wc.start(job_id)

        wait_for(1000, lambda: wc.get_job_info(job_id).get_status() == WorkflowJob.Status.SUCCEEDED)

        wf = wc.get_job_info(job_id)
        assert wf is not None
        assert wf.get_status() == WorkflowJob.Status.SUCCEEDED
    finally:
        LocalOozie.stop()
