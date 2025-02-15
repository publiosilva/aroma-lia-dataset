import pytest
from time import sleep

class TestMemoryLocks:
    def test_timeout_timing_out_write_lock(self):
        sb = []
        l1 = WriteLocker("a", 1, 0, sb)
        l2 = WriteLocker("a", 2, 50, sb)

        Thread(target=l1).start()
        # sleep(0.5)
        Thread(target=l2).start()
        # sleep(0.5)
        l1.finish()
        # sleep(0.5)
        l2.finish()
        # sleep(0.5)
        assert "a:1-L a:2-N a:1-U" == ''.join(sb).strip()
