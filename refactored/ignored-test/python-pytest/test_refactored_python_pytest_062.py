import pytest

class TestAutoMapperConfiguration:

    # @pytest.mark.skip(reason="")
    def test_configuration_is_valid(self):
        config = MapperConfiguration(lambda cfg: cfg.add_profile(DefaultProfile()))
        config.assert_configuration_is_valid()
