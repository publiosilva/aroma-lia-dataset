import time
import pytest

class TestActionCheckerService:
    def test_action_checker_service_delay(self):
        reader = IOUtils.getResourceAsReader("wf-ext-schema-valid.xml", -1)
        writer = open(f"{getTestCaseDir()}/workflow.xml", "w")
        IOUtils.copyCharStream(reader, writer)

        engine = DagEngine("u", "a")
        conf = XConfiguration()
        conf.set(OozieClient.APP_PATH, f"file://{getTestCaseDir()}/workflow.xml")
        conf.setStrings(WorkflowAppService.HADOOP_USER, getTestUser())
        conf.setStrings(OozieClient.GROUP_NAME, getTestGroup())

        conf.set(OozieClient.LOG_TOKEN, "t")

        conf.set("external-status", "ok")
        conf.set("signal-value", "based_on_action_status")
        conf.set("running-mode", "async")

        job_id = engine.submitJob(conf, True)
        time.sleep(0.2)

        waitFor(5, lambda: engine.getJob(job_id).getStatus() == WorkflowJob.Status.RUNNING)

        time.sleep(0.1)

        jpa_service = Services.get().get(JPAService)
        assert jpa_service is not None
        actions_get_executor = WorkflowActionsGetForJobJPAExecutor(job_id)
        actions = jpa_service.execute(actions_get_executor)
        action = next((bean for bean in actions if bean.getType() == "test"), None)
        assert action is not None
        assert action.getStatus() == WorkflowActionBean.Status.RUNNING

        action.setLastCheckTime(Date())
        jpa_service.execute(WorkflowActionUpdateJPAExecutor(action))

        action_check_delay = 20

        action_check_runnable = ActionCheckRunnable(action_check_delay)
        action_check_runnable.run()

        time.sleep(3)

        actions2 = jpa_service.execute(actions_get_executor)
        action2 = next((bean for bean in actions2 if bean.getType() == "test"), None)
        assert action2 is not None
        assert action2.getStatus() == WorkflowActionBean.Status.RUNNING
        assert engine.getJob(job_id).getStatus() == WorkflowJob.Status.RUNNING
