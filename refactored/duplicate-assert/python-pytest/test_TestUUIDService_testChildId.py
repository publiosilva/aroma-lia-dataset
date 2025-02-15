import pytest

def test_child_id_1():
    set_system_property(UUIDService.CONF_GENERATOR, "counter")
    services = Services()
    services.init()
    try:
        uuid = services.get(UUIDService)
        id = uuid.generate_id(ApplicationType.WORKFLOW)
        child_id = uuid.generate_child_id(id, "a")
        assert id == uuid.get_id(child_id)
        assert "a" == uuid.get_child_name(child_id)
    finally:
        services.destroy()

def test_child_id_2():
    set_system_property(UUIDService.CONF_GENERATOR, "counter")
    services = Services()
    services.init()
    try:
        uuid = services.get(UUIDService)
        id = uuid.generate_id(ApplicationType.WORKFLOW)
        child_id = uuid.generate_child_id(id, "a")
    finally:
        services.destroy()

    set_system_property(UUIDService.CONF_GENERATOR, "random")
    services = Services()
    services.init()
    try:
        uuid = services.get(UUIDService)
        id = uuid.generate_id(ApplicationType.WORKFLOW)
        child_id = uuid.generate_child_id(id, "a")
        assert id == uuid.get_id(child_id)
        assert "a" == uuid.get_child_name(child_id)
    finally:
        services.destroy()
