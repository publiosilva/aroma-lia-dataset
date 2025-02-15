import time
import pytest

TOLERANCE = 10  # example tolerance value
INTERVAL = 100  # example interval value

class TestInstrumentation:
    def test_cron(self):
        cron = Instrumentation.Cron()
        start = time.time() * 1000
        assert abs(cron.get_start() - 0) <= TOLERANCE
        assert abs(cron.get_end() - 0) <= TOLERANCE
        assert cron.get_start() == cron.get_end()
        assert cron.get_own() == 0
        assert cron.get_total() == 0

        cron.start()
        s = time.time() * 1000
        time.sleep(INTERVAL / 1000)
        cron.stop()
        real_own_delay = time.time() * 1000 - s
        now = time.time() * 1000
        assert abs(cron.get_start() - start) <= TOLERANCE
        assert abs(cron.get_end() - now) <= TOLERANCE
        assert abs(cron.get_total() - INTERVAL) <= TOLERANCE
        assert abs(cron.get_own() - INTERVAL) <= TOLERANCE
        assert abs(cron.get_total() - cron.get_own()) <= TOLERANCE

        real_total_delay = time.time() * 1000 - s
        s = time.time() * 1000
        time.sleep(INTERVAL / 1000)

        cron.start()

        real_total_delay += time.time() * 1000 - s

        s = time.time() * 1000
        time.sleep(INTERVAL / 1000)
        cron.stop()
        now = time.time() * 1000

        real_total_delay += time.time() * 1000 - s
        real_own_delay += time.time() * 1000 - s

        assert abs(cron.get_start() - start) <= TOLERANCE
        assert abs(cron.get_end() - now) <= TOLERANCE
        assert abs(cron.get_total() - real_total_delay) <= TOLERANCE
        assert abs(cron.get_own() - real_own_delay) <= TOLERANCE
