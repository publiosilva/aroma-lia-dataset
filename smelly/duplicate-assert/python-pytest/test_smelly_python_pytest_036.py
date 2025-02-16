import pytest

class TestCheckstyleAntTask(AbstractPathTestSupport):

    def test_create_classpath(self):
        ant_task = CheckstyleAntTask()

        assert ant_task.create_classpath() == "", "Invalid classpath"

        ant_task.set_classpath(Path(Project(), "/path"))

        assert ant_task.create_classpath() == "", "Invalid classpath"
