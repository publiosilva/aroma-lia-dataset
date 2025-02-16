import pytest
import time

class TestMemoryLocks:

    def test_wait_write_lock(self):
        sb = []
        l1 = WriteLocker("a", 1, -1, sb)
        l2 = WriteLocker("a", 2, -1, sb)

        thread1 = threading.Thread(target=l1)
        thread1.start()
        time.sleep(0.5)
        thread2 = threading.Thread(target=l2)
        thread2.start()
        time.sleep(0.5)
        l1.finish()
        time.sleep(0.5)
        l2.finish()
        time.sleep(0.5)
        assert "a:1-L a:1-U a:2-L a:2-U" == ' '.join(sb).strip()
