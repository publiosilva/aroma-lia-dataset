import time
import pytest

TOLERANCE = 10  # Define this based on your requirement
INTERVAL = 100  # Define this based on your requirement

class TestInstrumentation:

    def test_cron1(self):
        cron = Instrumentation.Cron()
        start = int(time.time() * 1000)
        assert abs(0 - cron.get_start()) <= TOLERANCE
        assert abs(0 - cron.get_end()) <= TOLERANCE
        assert cron.get_start() == cron.get_end()
        assert cron.get_own() == 0
        assert cron.get_total() == 0

        cron.start()
        s = int(time.time() * 1000)
        time.sleep(INTERVAL / 1000.0)
        cron.stop()
        real_own_delay = int(time.time() * 1000) - s
        now = int(time.time() * 1000)
        assert abs(start - cron.get_start()) <= TOLERANCE
        assert abs(now - cron.get_end()) <= TOLERANCE
        assert abs(INTERVAL - cron.get_total()) <= TOLERANCE
        assert abs(INTERVAL - cron.get_own()) <= TOLERANCE
        assert abs(cron.get_total() - cron.get_own()) <= TOLERANCE

        real_total_delay = int(time.time() * 1000) - s
        s = int(time.time() * 1000)
        time.sleep(INTERVAL / 1000.0)

        cron.start()

        real_total_delay += int(time.time() * 1000) - s

        s = int(time.time() * 1000)
        time.sleep(INTERVAL / 1000.0)
        cron.stop()
        now = int(time.time() * 1000)

        real_total_delay += int(time.time() * 1000) - s
        real_own_delay += int(time.time() * 1000) - s

        assert abs(real_total_delay - cron.get_total()) <= TOLERANCE
        assert abs(real_own_delay - cron.get_own()) <= TOLERANCE

    def test_cron2(self):
        cron = Instrumentation.Cron()
        start = int(time.time() * 1000)
        assert abs(0 - cron.get_start()) <= TOLERANCE
        assert abs(0 - cron.get_end()) <= TOLERANCE
        assert cron.get_start() == cron.get_end()
        assert cron.get_own() == 0
        assert cron.get_total() == 0

        cron.start()
        s = int(time.time() * 1000)
        time.sleep(INTERVAL / 1000.0)
        cron.stop()
        real_own_delay = int(time.time() * 1000) - s
        now = int(time.time() * 1000)
        assert abs(INTERVAL - cron.get_total()) <= TOLERANCE
        assert abs(INTERVAL - cron.get_own()) <= TOLERANCE
        assert abs(cron.get_total() - cron.get_own()) <= TOLERANCE

        real_total_delay = int(time.time() * 1000) - s
        s = int(time.time() * 1000)
        time.sleep(INTERVAL / 1000.0)

        cron.start()

        real_total_delay += int(time.time() * 1000) - s

        s = int(time.time() * 1000)
        time.sleep(INTERVAL / 1000.0)
        cron.stop()
        now = int(time.time() * 1000)

        real_total_delay += int(time.time() * 1000) - s
        real_own_delay += int(time.time() * 1000) - s

        assert abs(start - cron.get_start()) <= TOLERANCE
        assert abs(now - cron.get_end()) <= TOLERANCE
        assert abs(real_total_delay - cron.get_total()) <= TOLERANCE
        assert abs(real_own_delay - cron.get_own()) <= TOLERANCE
