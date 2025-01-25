import unittest

class FileLengthCheckTest(unittest.TestCase):

    def test_extensions(self):
        check = FileLengthCheck()
        check.set_file_extensions("java")
        self.assertEqual(".java", check.get_file_extensions()[0], "extension should be the same")
        check.set_file_extensions(".java")
        self.assertEqual(".java", check.get_file_extensions()[0], "extension should be the same")
        with self.assertRaises(IllegalArgumentException) as context:
            check.set_file_extensions(None)
        self.assertEqual("Extensions array can not be null", str(context.exception), "Invalid exception message")
