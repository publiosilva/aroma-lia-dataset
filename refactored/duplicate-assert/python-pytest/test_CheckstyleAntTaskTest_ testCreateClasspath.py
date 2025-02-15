import pytest

class TestCheckstyleAntTask:

    def test_create_classpath_1(self):
        ant_task = CheckstyleAntTask()

        assert ant_task.CreateClasspath().toString() == "", "Invalid classpath"

    def test_create_classpath_2(self):
        ant_task = CheckstyleAntTask()

        ant_task.setClasspath(Path(Project(), "/path"))

        assert ant_task.CreateClasspath().toString() == "", "Invalid classpath"
