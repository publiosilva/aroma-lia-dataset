public class TestCoordELFunctions
{
    [Fact]
    public void TestHours()
    {
        Init("coord-job-submit-freq");
        string expr = "${coord:hours(1)}";
        Assert.Equal("60", CoordELFunctions.EvalAndWrap(eval, expr));
        Assert.Equal(TimeUnit.Minute, (TimeUnit)eval.GetVariable("timeunit"));

        expr = "${coord:hours(coord:hours(1))}";
        Assert.Equal("3600", CoordELFunctions.EvalAndWrap(eval, expr));
        Assert.Equal(TimeUnit.Minute, (TimeUnit)eval.GetVariable("timeunit"));
    }
}
