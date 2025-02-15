import pytest

class TestChocolateyUpgrader:
    
    # @pytest.mark.skip(reason="Disabled")
    def test_should_add_skip_download_cache_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.get_settings().set_skip_download_cache(skip_download_cache)
        # When
        result = fixture.run()
        # Then
        assert result.get_args() == expected
