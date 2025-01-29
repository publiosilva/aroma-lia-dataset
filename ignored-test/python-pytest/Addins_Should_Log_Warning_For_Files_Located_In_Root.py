import pytest

class TestAddins:

    @pytest.mark.skip(reason="")
    def test_should_log_warning_for_files_located_in_root(self):
        # Given
        fixture = NuGetAddinContentResolverFixture(framework, runtime)
        fixture.create_clr_assembly("/Working/file.dll")
        fixture.create_clr_assembly("/Working/file2.dll")
        fixture.create_clr_assembly("/Working/file3.dll")
        # When
        fixture.get_files()
        # Then
        entries = [
            x for x in fixture.log.entries
            if x.level == LogLevel.Warning and 
               x.message == (f"Could not find any assemblies compatible with {framework} in NuGet package {fixture.package.package}. "
                             "Falling back to using root folder of NuGet package.")
        ]
        assert len(entries) == 1
