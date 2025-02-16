import pytest

class TestCoordELFunctions:
    def test_hours_1(self):
        self.init("coord-job-submit-freq")
        expr = "${coord:hours(1)}"
        assert "60" == CoordELFunctions.eval_and_wrap(self.eval, expr)
        assert TimeUnit.MINUTE == self.eval.get_variable("timeunit")

    def test_hours_2(self):
        self.init("coord-job-submit-freq")
        expr = "${coord:hours(coord:hours(1))}"
        assert "3600" == CoordELFunctions.eval_and_wrap(self.eval, expr)
        assert TimeUnit.MINUTE == self.eval.get_variable("timeunit")
