import pytest

class TestCoordSubmitXCommand:
    def test_basic_submit_with_multiple_instances_input_event_1(self):
        conf = XConfiguration()
        app_path = f"file://{get_test_case_dir()}/coordinator.xml"

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance1.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        conf.set(OozieClient.COORDINATOR_APP_PATH, app_path)
        conf.set(OozieClient.USER_NAME, get_test_user())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        with pytest.raises(CommandException) as excinfo:
            sc.call()
        assert sc.get_job().get_status() == Job.Status.FAILED
        assert excinfo.value.error_code == ErrorCode.E1021
        assert sc.COORD_INPUT_EVENTS in excinfo.value.message
        assert "per data-in instance" in excinfo.value.message

    def test_basic_submit_with_multiple_instances_input_event_2(self):
        conf = XConfiguration()
        app_path = f"file://{get_test_case_dir()}/coordinator.xml"

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance1.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        conf.set(OozieClient.COORDINATOR_APP_PATH, app_path)
        conf.set(OozieClient.USER_NAME, get_test_user())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException:
            pass

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance2.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        with pytest.raises(CommandException) as excinfo:
            sc.call()
        assert sc.get_job().get_status() == Job.Status.FAILED
        assert excinfo.value.error_code == ErrorCode.E1021
        assert sc.COORD_INPUT_EVENTS in excinfo.value.message
        assert "is empty" in excinfo.value.message

    def test_basic_submit_with_multiple_instances_input_event_3(self):
        conf = XConfiguration()
        app_path = f"file://{get_test_case_dir()}/coordinator.xml"

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance1.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        conf.set(OozieClient.COORDINATOR_APP_PATH, app_path)
        conf.set(OozieClient.USER_NAME, get_test_user())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException:
            pass

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance2.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException:
            pass

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance3.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException as e:
            pytest.fail(f"Unexpected failure: {e}")

    def test_basic_submit_with_multiple_instances_input_event_4(self):
        conf = XConfiguration()
        app_path = f"file://{get_test_case_dir()}/coordinator.xml"

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance1.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        conf.set(OozieClient.COORDINATOR_APP_PATH, app_path)
        conf.set(OozieClient.USER_NAME, get_test_user())
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException:
            pass

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance2.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException:
            pass

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance3.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException:
            pass

        reader = IOUtils.get_resource_as_reader("coord-multiple-input-instance4.xml", -1)
        writer = open(URI(app_path).path, 'w')
        IOUtils.copy_char_stream(reader, writer)
        sc = CoordSubmitXCommand(conf, "UNIT_TESTING")

        try:
            sc.call()
        except CommandException as e:
            pytest.fail(f"Unexpected failure: {e}")
