import subprocess
import pytest

class TestStringUtils:
    
    @pytest.mark.skip(reason="")
    def test_format_arguments(self):
        complex_input = "'"
        arguments = StringUtils.format_arguments("-n", "foo", complex_input, "bar")
        result = subprocess.run(["/bin/echo"] + arguments, capture_output=True, text=True)
        assert result.stdout.strip() == f"foo {complex_input} bar"
