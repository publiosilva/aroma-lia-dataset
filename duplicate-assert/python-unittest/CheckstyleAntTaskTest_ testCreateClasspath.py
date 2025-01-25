import unittest

class CheckstyleAntTaskTest(unittest.TestCase):

    def test_create_classpath(self):
        ant_task = CheckstyleAntTask()

        self.assertEqual("", ant_task.create_classpath().__str__(), "Invalid classpath")

        ant_task.set_classpath(Path(Project(), "/path"))

        self.assertEqual("", ant_task.create_classpath().__str__(), "Invalid classpath")
