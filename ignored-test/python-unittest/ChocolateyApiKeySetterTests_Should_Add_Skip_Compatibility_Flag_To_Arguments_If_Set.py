import unittest

class TestChocolateyApiKeySetter(unittest.TestCase):
    @unittest.skip("")
    def test_should_add_skip_compatibility_flag_to_arguments_if_set(self):
        fixture = ChocolateyApiKeySetterFixture()
        fixture.settings.skip_compatibility_checks = skip_compatibiity
        
        result = fixture.run()
        
        self.assertEqual(expected, result.args)
