import pytest

class TestCoordPushDependencyCheckXCommand:
    def test_time_out(self):
        db = "default"
        table = "tablename"
        new_hcat_dependency1 = f"hcat://{server}/{db}/{table}/dt=20120430;country=brazil"
        new_hcat_dependency2 = f"hcat://{server}/{db}/{table}/dt=20120430;country=usa"
        new_hcat_dependency = f"{new_hcat_dependency1}{CoordELFunctions.INSTANCE_SEPARATOR}{new_hcat_dependency2}"
        populate_table(db, table)

        action_id = add_init_records(new_hcat_dependency)
        check_coord_action(action_id, new_hcat_dependency, CoordinatorAction.Status.WAITING)
        CoordPushDependencyCheckXCommand(action_id, True).call()
        check_coord_action(action_id, new_hcat_dependency1, CoordinatorAction.Status.WAITING)
        pdms = Services.get().get(PartitionDependencyManagerService)
        hcat_service = Services.get().get(HCatAccessorService)
        assert action_id in pdms.get_waiting_actions(HCatURI(new_hcat_dependency1))
        assert hcat_service.is_registered_for_notification(HCatURI(new_hcat_dependency1))

        time_out_creation_time = time.time() * 1000 - (12 * 60 * 1000)
        set_coord_action_creation_time(action_id, time_out_creation_time)
        CoordPushDependencyCheckXCommand(action_id).call()
        # time.sleep(0.1)
        check_coord_action(action_id, new_hcat_dependency1, CoordinatorAction.Status.TIMEDOUT)
        assert pdms.get_waiting_actions(HCatURI(new_hcat_dependency1)) is None
        assert not hcat_service.is_registered_for_notification(HCatURI(new_hcat_dependency1))
