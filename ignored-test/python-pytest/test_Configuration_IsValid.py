import pytest
from your_module import MapperConfiguration, DefaultProfile

@pytest.mark.skip(reason="")
def test_configuration_is_valid():
    config = MapperConfiguration(lambda cfg: cfg.add_profile(DefaultProfile()))
    config.assert_configuration_is_valid()
