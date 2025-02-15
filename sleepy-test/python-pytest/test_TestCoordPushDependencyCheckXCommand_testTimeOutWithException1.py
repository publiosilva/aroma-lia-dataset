import pytest

class TestCoordPushDependencyCheckXCommand:
    def test_time_out_with_exception_1(self):
        new_hcat_dependency_1 = "hcat://" + server + "/nodb/notable/dt=20120430;country=brazil"
        new_hcat_dependency_2 = "hcat://" + server + "/nodb/notable/dt=20120430;country=usa"
        new_hcat_dependency = new_hcat_dependency_1 + CoordELFunctions.INSTANCE_SEPARATOR + new_hcat_dependency_2

        action_id = add_init_records(new_hcat_dependency)
        check_coord_action(action_id, new_hcat_dependency, CoordinatorAction.Status.WAITING)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(action_id, True).call()
        assert "NoSuchObjectException" in str(excinfo.value)
        
        check_coord_action(action_id, new_hcat_dependency, CoordinatorAction.Status.WAITING)
        pdms = Services.get().get(PartitionDependencyManagerService)
        hcat_service = Services.get().get(HCatAccessorService)
        assert pdms.get_waiting_actions(HCatURI(new_hcat_dependency_1)) is None
        assert not hcat_service.is_registered_for_notification(HCatURI(new_hcat_dependency_1))

        time_out_creation_time = time.time() * 1000 - (12 * 60 * 1000)
        set_coord_action_creation_time(action_id, time_out_creation_time)
        with pytest.raises(Exception) as excinfo:
            CoordPushDependencyCheckXCommand(action_id).call()
        assert "NoSuchObjectException" in str(excinfo.value)

        time.sleep(0.1)
        check_coord_action(action_id, new_hcat_dependency, CoordinatorAction.Status.TIMEDOUT)
