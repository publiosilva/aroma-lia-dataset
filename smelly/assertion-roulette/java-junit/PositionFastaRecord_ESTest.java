@RunWith(EvoRunner.class) @EvoRunnerParameters(mockJVMNonDeterminism = true, useVFS = true, useVNET = true, resetStaticState = true, separateClassLoader = true, useJEE = true) 
public class PositionFastaRecord_ESTest extends PositionFastaRecord_ESTest_scaffolding {
  @Test(timeout = 4000)
  public void test00()  throws Throwable  {
      PositionSequence positionSequence0 = mock(PositionSequence.class, new ViolatedAssumptionAnswer());
      doReturn("pb995}/N", "pb995}/N").when(positionSequence0).toString();
      PositionFastaRecord positionFastaRecord0 = new PositionFastaRecord("", "", positionSequence0);
      PositionSequence positionSequence1 = positionFastaRecord0.getSequence();
      PositionFastaRecord positionFastaRecord1 = new PositionFastaRecord("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", "ml9H%7|,g{C$R-jG:Hx", positionSequence1);
      positionFastaRecord1.hashCode();
      assertEquals("ml9H%7|,g{C$R-jG:Hx", positionFastaRecord1.getComment());
      assertEquals("PositionSequenceFastaRecord [identifier=, comments=, positions=pb995}/N]", positionFastaRecord1.getId());
  }
}
