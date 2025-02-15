import pytest

class TestFileLengthCheck:
    
    def test_extensions(self):
        check = FileLengthCheck()
        check.set_file_extensions("java")
        assert check.get_file_extensions()[0] == ".java", "extension should be the same"
        check.set_file_extensions(".java")
        assert check.get_file_extensions()[0] == ".java", "extension should be the same"

        with pytest.raises(ValueError) as exception_info:
            check.set_file_extensions(None)
        assert str(exception_info.value) == "Extensions array can not be null", "Invalid exception message"
