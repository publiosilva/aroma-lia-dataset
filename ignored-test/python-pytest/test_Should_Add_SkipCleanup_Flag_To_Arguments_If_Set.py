import pytest

class TestChocolateyPusher:
    
    @pytest.mark.skip(reason="")
    def test_should_add_skipcleanup_flag_to_arguments_if_set(self):
        # Given
        fixture = ChocolateyPusherFixture()
        fixture.settings.skip_cleanup = skip_cleanup
        # When
        result = fixture.run()
        # Then
        assert expected == result.args
