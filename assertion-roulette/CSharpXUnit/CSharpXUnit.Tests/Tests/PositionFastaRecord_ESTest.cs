[Collection("EvoRunner")]
public class PositionFastaRecord_ESTest : PositionFastaRecord_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void Test00()
    {
        var positionSequence0 = new Mock<PositionSequence>();
        positionSequence0.Setup(seq => seq.ToString()).Returns("pb995}/N").Returns("pb995}/N");
        var positionFastaRecord0 = new PositionFastaRecord("", "", positionSequence0.Object);
        var positionSequence1 = positionFastaRecord0.GetSequence();
        var positionFastaRecord1 = new PositionFastaRecord("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", "ml9H%7|,g{C$R-jG:Hx", positionSequence1);
        positionFastaRecord1.GetHashCode();
        Assert.Equal("ml9H%7|,g{C$R-jG:Hx", positionFastaRecord1.GetComment());
        Assert.Equal("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", positionFastaRecord1.GetId());
    }
}
