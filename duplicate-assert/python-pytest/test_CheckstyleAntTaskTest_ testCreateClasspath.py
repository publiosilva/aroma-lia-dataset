from pytest import mark

@mark.parametrize("ant_task", [CheckstyleAntTask()])
def test_create_classpath(ant_task):
    assert ant_task.createClasspath().toString() == "", "Invalid classpath"
    
    ant_task.setClasspath(Path(Project(), "/path"))
    
    assert ant_task.createClasspath().toString() == "", "Invalid classpath"
