import unittest

class TestAddins(unittest.TestCase):
    @unittest.skip("")
    def test_should_log_warning_for_files_located_in_root(self):
        fixture = NuGetAddinContentResolverFixture(framework, runtime)
        fixture.create_clr_assembly("/Working/file.dll")
        fixture.create_clr_assembly("/Working/file2.dll")
        fixture.create_clr_assembly("/Working/file3.dll")
        
        fixture.get_files()
        
        entries = [
            x for x in fixture.log.entries
            if x.level == LogLevel.WARNING and x.message == (
                f"Could not find any assemblies compatible with {framework} in NuGet package {fixture.package.package}. "
                "Falling back to using root folder of NuGet package."
            )
        ]
        
        self.assertEqual(1, len(entries))
