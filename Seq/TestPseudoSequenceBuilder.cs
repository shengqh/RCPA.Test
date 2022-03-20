using NUnit.Framework;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestPseudoSequenceBuilder
  {
    [Test]
    public void Test()
    {
      PseudoSequenceBuilder builder = new PseudoSequenceBuilder("KR", false);
      Sequence seq = new Sequence("TEST", "EISQVFEIALKRNLPVNFEVARESGPPHMKNFVTKVSVGEFVGEGEGKSK");
      builder.Build(seq);
      Assert.AreEqual("EISQVFEIALRKNLPVNFEVAERSGPPHMNKFVTVKSVGEFVGEGEGSKK", seq.SeqString);
    }
  }
}
