import pytest

@pytest.mark.skip(reason="")
class TestChocolateyUpgrader:

    def test_should_add_skipdownloadcache_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyUpgraderFixture()
        fixture.settings.skip_download_cache = skip_download_cache
        # When
        result = fixture.run()
        # Then
        assert expected == result.args
