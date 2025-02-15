[Collection("EvoRunner")]
[EvoRunnerParameters(mockJVMNonDeterminism: true, useVFS: true, useVNET: true, resetStaticState: true, separateClassLoader: true, useJEE: true)]
public class PositionFastaRecord_ESTest : PositionFastaRecord_ESTest_scaffolding
{
    [Fact(Timeout = 4000)]
    public void Test00()
    {
        var positionSequence0 = new Mock<PositionSequence>(new ViolatedAssumptionAnswer());
        positionSequence0.Setup(m => m.ToString()).Returns("pb995}/N").Verifiable();
        var positionFastaRecord0 = new PositionFastaRecord("", "", positionSequence0.Object);
        var positionSequence1 = positionFastaRecord0.GetSequence();
        var positionFastaRecord1 = new PositionFastaRecord("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", "ml9H%7|,g{C$R-jG:Hx", positionSequence1);
        positionFastaRecord1.GetHashCode();
        Assert.Equal("ml9H%7|,g{C$R-jG:Hx", positionFastaRecord1.GetComment(), "Explanation message");
        Assert.Equal("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", positionFastaRecord1.GetId(), "Explanation message");
    }
}
