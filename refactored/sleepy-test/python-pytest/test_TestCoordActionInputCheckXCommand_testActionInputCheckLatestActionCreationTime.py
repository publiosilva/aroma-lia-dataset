import pytest
from datetime import datetime

@pytest.mark.parametrize("TZ", ["Z"])  # Adjust for timezone as needed
def test_action_input_check_latest_action_creation_time(TZ):
    Services.get().getConf().setBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, False)

    job_id = "0000000-{}-TestCoordActionInputCheckXCommand-C".format(int(datetime.now().timestamp()))
    start_time = DateUtils.parseDateOozieTZ("2009-02-15T23:59" + TZ)
    end_time = DateUtils.parseDateOozieTZ("2009-02-16T23:59" + TZ)
    job = add_record_to_coord_job_table(job_id, start_time, end_time, "latest")
    CoordMaterializeTransitionXCommand(job.getId(), 3600).call()

    jpa_service = Services.get().get(JPAService)
    action = jpa_service.execute(CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"))
    assert action.getMissingDependencies() == CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}"

    action_xml = action.getActionXml()
    action_creation_time = "2009-02-15T01:00" + TZ
    action_xml = action_xml.replace("action-actual-time=\".*\">", "action-actual-time=\"{}\">".format(action_creation_time))
    action.setActionXml(action_xml)
    action.setCreatedTime(DateUtils.parseDateOozieTZ(action_creation_time))
    jpa_service.execute(CoordActionUpdateForInputCheckJPAExecutor(action))
    action = jpa_service.execute(CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"))
    assert "action-actual-time=\"2009-02-15T01:00" in action.getActionXml()

    create_dir(get_test_case_dir() + "/2009/03/05/")
    create_dir(get_test_case_dir() + "/2009/02/19/")
    create_dir(get_test_case_dir() + "/2009/02/12/")
    create_dir(get_test_case_dir() + "/2009/02/05/")
    create_dir(get_test_case_dir() + "/2009/01/22/")
    create_dir(get_test_case_dir() + "/2009/01/08/")

    CoordActionInputCheckXCommand(job.getId() + "@1", job.getId()).call()
    # time.sleep(1)

    action = jpa_service.execute(CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"))
    action_xml = action.getActionXml()
    assert action.getMissingDependencies() == ""
    resolved_list = "file://{}{}".format(get_test_case_dir() + "/2009/02/12", CoordELFunctions.INSTANCE_SEPARATOR) + \
                    "file://{}{}".format(get_test_case_dir() + "/2009/02/05", CoordELFunctions.INSTANCE_SEPARATOR) + \
                    "file://{}{}".format(get_test_case_dir() + "/2009/01/22", CoordELFunctions.INSTANCE_SEPARATOR) + \
                    "file://{}".format(get_test_case_dir() + "/2009/01/08")
    print("Expected: " + resolved_list)
    print("Actual: " + action_xml[action_xml.index("<uris>") + 6:action_xml.index("</uris>")])
    assert resolved_list == action_xml[action_xml.index("<uris>") + 6:action_xml.index("</uris>")]
