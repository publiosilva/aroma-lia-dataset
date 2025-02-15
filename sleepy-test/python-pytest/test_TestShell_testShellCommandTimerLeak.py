import pytest
import time

def test_shell_command_timer_leak():
    quick_command = ["/bin/sleep", "100"]
    
    timers_before = count_timer_threads()
    print("before:", timers_before)

    for i in range(10):
        shexec = Shell.ShellCommandExecutor(quick_command, None, None, 1)
        with pytest.raises(Exception):
            shexec.execute()
    
    time.sleep(1)
    timers_after = count_timer_threads()
    print("after:", timers_after)
    assert timers_before == timers_after
