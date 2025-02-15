import pytest
from time import sleep

def test_action_checker_service_delay_1():
    reader = IOUtils.get_resource_as_reader("wf-ext-schema-valid.xml", -1)
    writer = open(get_test_case_dir() + "/workflow.xml", 'w')
    IOUtils.copy_char_stream(reader, writer)

    engine = DagEngine("u", "a")
    conf = XConfiguration()
    conf.set(OozieClient.APP_PATH, "file://" + get_test_case_dir() + "/" + "workflow.xml")
    conf.set_strings(WorkflowAppService.HADOOP_USER, get_test_user())
    conf.set_strings(OozieClient.GROUP_NAME, get_test_group())
    
    conf.set(OozieClient.LOG_TOKEN, "t")
    
    conf.set("external-status", "ok")
    conf.set("signal-value", "based_on_action_status")
    conf.set("running-mode", "async")
    
    job_id = engine.submit_job(conf, True)
    sleep(0.2)

    wait_for(5000, lambda: engine.get_job(job_id).get_status() == WorkflowJob.Status.RUNNING)

    sleep(0.1)

    jpa_service = Services.get().get(JPAService)
    assert jpa_service is not None
    actions_get_executor = WorkflowActionsGetForJobJPAExecutor(job_id)
    actions = jpa_service.execute(actions_get_executor)
    action = next((bean for bean in actions if bean.get_type() == "test"), None)
    
    assert action is not None
    assert action.get_status() == WorkflowActionBean.Status.RUNNING

def test_action_checker_service_delay_2():
    reader = IOUtils.get_resource_as_reader("wf-ext-schema-valid.xml", -1)
    writer = open(get_test_case_dir() + "/workflow.xml", 'w')
    IOUtils.copy_char_stream(reader, writer)

    engine = DagEngine("u", "a")
    conf = XConfiguration()
    conf.set(OozieClient.APP_PATH, "file://" + get_test_case_dir() + "/" + "workflow.xml")
    conf.set_strings(WorkflowAppService.HADOOP_USER, get_test_user())
    conf.set_strings(OozieClient.GROUP_NAME, get_test_group())
    
    conf.set(OozieClient.LOG_TOKEN, "t")
    
    conf.set("external-status", "ok")
    conf.set("signal-value", "based_on_action_status")
    conf.set("running-mode", "async")

    job_id = engine.submit_job(conf, True)
    sleep(0.2)

    wait_for(5000, lambda: engine.get_job(job_id).get_status() == WorkflowJob.Status.RUNNING)

    sleep(0.1)

    jpa_service = Services.get().get(JPAService)
    assert jpa_service is not None
    actions_get_executor = WorkflowActionsGetForJobJPAExecutor(job_id)
    actions = jpa_service.execute(actions_get_executor)
    action = next((bean for bean in actions if bean.get_type() == "test"), None)

    action.set_last_check_time(Date())
    jpa_service.execute(WorkflowActionUpdateJPAExecutor(action))

    action_check_delay = 20

    action_check_runnable = ActionCheckRunnable(action_check_delay)
    action_check_runnable.run()

    sleep(3)

    actions2 = jpa_service.execute(actions_get_executor)
    action2 = next((bean for bean in actions2 if bean.get_type() == "test"), None)
    assert action is not None
    assert action2.get_status() == WorkflowActionBean.Status.RUNNING
    assert engine.get_job(job_id).get_status() == WorkflowJob.Status.RUNNING
