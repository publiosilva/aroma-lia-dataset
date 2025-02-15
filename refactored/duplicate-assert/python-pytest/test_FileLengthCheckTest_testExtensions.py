import pytest

class TestFileLengthCheck:

    def test_extensions_1(self):
        check = FileLengthCheck()
        check.set_file_extensions("java")
        assert check.get_file_extensions()[0] == ".java", "extension should be the same"

        with pytest.raises(IllegalArgumentException) as excinfo:
            check.set_file_extensions(None)
        assert str(excinfo.value) == "Extensions array can not be null", "Invalid exception message"

    def test_extensions_2(self):
        check = FileLengthCheck()
        check.set_file_extensions(".java")
        assert check.get_file_extensions()[0] == ".java", "extension should be the same"

        with pytest.raises(IllegalArgumentException) as excinfo:
            check.set_file_extensions(None)
        assert str(excinfo.value) == "Extensions array can not be null", "Invalid exception message"
