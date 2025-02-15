import pytest

class TestEventHandlerService:
    def test_service(self):
        ehs = _test_event_handler_service()
        assert isinstance(ehs.get_event_queue(), MemoryEventQueue)
        jobtypes = ehs.get_app_types()
        assert "workflow_job" in jobtypes
        assert "coordinator_action" in jobtypes

        services = Services.get()
        services.destroy()
        services = Services()
        conf = services.get_conf()
        conf.set(Services.CONF_SERVICE_EXT_CLASSES, "")
        services.init()
        assert not EventHandlerService.is_enabled()
