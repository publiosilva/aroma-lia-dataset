import pytest

class TestCoordSubmitXCommand:
    
    def test_basic_submit_with_multiple_instances_input_event(self):
        conf = XConfiguration()
        app_path = "file://" + get_test_case_dir() + "/coordinator.xml"

        # CASE 1: Failure case i.e. multiple data-in instances
        with open("coord-multiple-input-instance1.xml", 'r') as reader, open(app_path[7:], 'w') as writer:
            writer.write(reader.read())
        conf.set(OozieClient.COORDINATOR_APP_PATH, app_path)
        conf.set(OozieClient.USER_NAME, get_test_user())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        with pytest.raises(CommandException) as excinfo:
            sc.call()
        assert sc.get_job().get_status() == Job.Status.FAILED
        assert excinfo.value.error_code == ErrorCode.E1021
        assert sc.COORD_INPUT_EVENTS in str(excinfo.value) and "per data-in instance" in str(excinfo.value)

        # CASE 2: Multiple data-in instances specified as separate <instance> tags, but one or more tags are empty.
        with open("coord-multiple-input-instance2.xml", 'r') as reader, open(app_path[7:], 'w') as writer:
            writer.write(reader.read())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        with pytest.raises(CommandException) as excinfo:
            sc.call()
        assert sc.get_job().get_status() == Job.Status.FAILED
        assert excinfo.value.error_code == ErrorCode.E1021
        assert sc.COORD_INPUT_EVENTS in str(excinfo.value) and "is empty" in str(excinfo.value)

        # CASE 3: Success case i.e. Multiple data-in instances specified correctly as separate <instance> tags
        with open("coord-multiple-input-instance3.xml", 'r') as reader, open(app_path[7:], 'w') as writer:
            writer.write(reader.read())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException as e:
            pytest.fail(f"Unexpected failure: {e}")

        # CASE 4: Success case i.e. Single instances for input and single instance for output, but both with ","
        with open("coord-multiple-input-instance4.xml", 'r') as reader, open(app_path[7:], 'w') as writer:
            writer.write(reader.read())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException as e:
            pytest.fail(f"Unexpected failure: {e}")
