import subprocess

def test_format_arguments():
    p = subprocess.Popen(["/bin/echo", "-n", "foo", "'", "bar"], stdout=subprocess.PIPE, stderr=subprocess.STDOUT)
    output, _ = p.communicate()
    assert output.decode().strip() == "foo ' bar"
