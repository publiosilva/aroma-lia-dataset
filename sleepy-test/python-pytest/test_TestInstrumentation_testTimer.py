import pytest

class TestInstrumentation:
    @pytest.fixture(autouse=True)
    def setup(self):
        self.timer = Instrumentation.Timer()

    def test_timer(self):
        assert self.timer.getTicks() == 0
        assert self.timer.getTotal() == 0
        assert self.timer.getOwn() == 0
        assert self.timer.getOwnAvg() == 0
        assert self.timer.getTotalAvg() == 0
        assert self.timer.getOwnSquareSum() == 0
        assert self.timer.getTotalSquareSum() == 0
        assert self.timer.getOwnMin() == 0
        assert self.timer.getOwnMax() == 0
        assert self.timer.getTotalMin() == 0
        assert self.timer.getTotalMax() == 0

        assert self.timer.getValue().getTicks() == 0
        assert self.timer.getValue().getTotal() == 0
        assert self.timer.getValue().getOwn() == 0
        assert self.timer.getValue().getOwnAvg() == 0
        assert self.timer.getValue().getTotalAvg() == 0
        assert self.timer.getValue().getOwnSquareSum() == 0
        assert self.timer.getValue().getTotalSquareSum() == 0
        assert self.timer.getValue().getOwnMin() == 0
        assert self.timer.getValue().getOwnMax() == 0
        assert self.timer.getValue().getTotalMin() == 0
        assert self.timer.getValue().getTotalMax() == 0

        cron1 = Instrumentation.Cron()
        cron1.start()
        time.sleep(INTERVAL)
        cron1.stop()
        self.timer.addCron(cron1)

        assert self.timer.getTicks() == 1
        assert self.timer.getTotal() == cron1.getTotal()
        assert self.timer.getOwn() == cron1.getOwn()
        assert self.timer.getOwnAvg() == cron1.getOwn()
        assert self.timer.getTotalAvg() == cron1.getTotal()
        assert self.timer.getOwnSquareSum() == cron1.getOwn() * cron1.getOwn()
        assert self.timer.getTotalSquareSum() == cron1.getTotal() * cron1.getTotal()
        assert self.timer.getOwnMin() == cron1.getOwn()
        assert self.timer.getOwnMax() == cron1.getOwn()
        assert self.timer.getTotalMin() == cron1.getTotal()
        assert self.timer.getTotalMax() == cron1.getTotal()

        assert self.timer.getValue().getTicks() == 1
        assert self.timer.getValue().getTotal() == cron1.getTotal()
        assert self.timer.getValue().getOwn() == cron1.getOwn()
        assert self.timer.getValue().getOwnAvg() == cron1.getOwn()
        assert self.timer.getValue().getTotalAvg() == cron1.getTotal()
        assert self.timer.getValue().getOwnSquareSum() == cron1.getOwn() * cron1.getOwn()
        assert self.timer.getValue().getTotalSquareSum() == cron1.getTotal() * cron1.getTotal()
        assert self.timer.getValue().getOwnMin() == cron1.getOwn()
        assert self.timer.getValue().getOwnMax() == cron1.getOwn()
        assert self.timer.getValue().getTotalMin() == cron1.getTotal()
        assert self.timer.getValue().getTotalMax() == cron1.getTotal()

        cron2 = Instrumentation.Cron()
        cron2.start()
        time.sleep(INTERVAL * 2)
        cron2.stop()
        self.timer.addCron(cron2)

        assert self.timer.getTicks() == 2
        assert self.timer.getTotal() == cron1.getTotal() + cron2.getTotal()
        assert self.timer.getOwn() == cron1.getOwn() + cron2.getOwn()
        assert self.timer.getOwnAvg() == (cron1.getOwn() + cron2.getOwn()) / 2
        assert self.timer.getTotalAvg() == (cron1.getTotal() + cron2.getTotal()) / 2
        assert self.timer.getOwnSquareSum() == cron1.getOwn() * cron1.getOwn() + cron2.getOwn() * cron2.getOwn()
        assert self.timer.getTotalSquareSum() == cron1.getTotal() * cron1.getTotal() + cron2.getTotal() * cron2.getTotal()
        assert self.timer.getOwnMin() == cron1.getOwn()
        assert self.timer.getOwnMax() == cron2.getOwn()
        assert self.timer.getTotalMin() == cron1.getTotal()
        assert self.timer.getTotalMax() == cron2.getTotal()
