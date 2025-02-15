import pytest
from datetime import datetime

class TestCoordActionInputCheckXCommand:
    @pytest.mark.asyncio
    async def test_action_input_check_latest_current_time(self):
        Services.get().getConf().setBoolean(CoordELFunctions.LATEST_EL_USE_CURRENT_TIME, True)

        job_id = "0000000-" + str(int(datetime.now().timestamp() * 1000)) + "-TestCoordActionInputCheckXCommand-C"
        start_time = DateUtils.parse_date_oozie_tz("2009-02-15T23:59" + TZ)
        end_time = DateUtils.parse_date_oozie_tz("2009-02-16T23:59" + TZ)
        job = add_record_to_coord_job_table(job_id, start_time, end_time, "latest")
        await CoordMaterializeTransitionXCommand(job.getId(), 3600).call()

        jpa_service = Services.get().get(JPAService.class)
        action = await jpa_service.execute(CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"))
        assert action.getMissingDependencies() == CoordCommandUtils.RESOLVED_UNRESOLVED_SEPARATOR + "${coord:latestRange(-3,0)}"

        action_xml = action.getActionXml()
        action_creation_time = "2009-02-15T01:00" + TZ
        action_xml = re.sub(r'action-actual-time=".*?">', f'action-actual-time="{action_creation_time}">', action_xml)
        action.setActionXml(action_xml)
        action.setCreatedTime(DateUtils.parse_date_oozie_tz(action_creation_time))
        await jpa_service.execute(CoordActionUpdateForInputCheckJPAExecutor(action))
        action = await jpa_service.execute(CoordActionGetForInputCheckJPAExecutor(job.getId() + "@1"))
        assert "action-actual-time=\"2009-02-15T01:00" in action.getActionXml()

        create_dir(get_test_case_dir() + "/2009/03/05/")
        create_dir(get_test_case_dir() + "/2009/02/19/")
        create_dir(get_test_case_dir() + "/2009/02/12/")
        create_dir(get_test_case_dir() + "/2009/02/05/")
        create_dir(get_test_case_dir() + "/2009/01/22/")
        create_dir(get_test_case_dir() + "/2009/01/08/")

        await CoordActionInputCheckXCommand(job.getId() + "@1", job.getId()).call()
        # await asyncio.sleep(1)

        action = await jpa_service.execute(CoordActionGetJPAExecutor(job.getId() + "@1"))
        action_xml = action.getActionXml()
        assert action.getMissingDependencies() == ""
        
        resolved_list = "file://" + get_test_case_dir() + "/2009/03/05" + CoordELFunctions.INSTANCE_SEPARATOR + \
                        "file://" + get_test_case_dir() + "/2009/02/19" + CoordELFunctions.INSTANCE_SEPARATOR + \
                        "file://" + get_test_case_dir() + "/2009/02/12" + CoordELFunctions.INSTANCE_SEPARATOR + \
                        "file://" + get_test_case_dir() + "/2009/02/05"
        assert resolved_list == action_xml[action_xml.index("<uris>") + 6: action_xml.index("</uris>")]
