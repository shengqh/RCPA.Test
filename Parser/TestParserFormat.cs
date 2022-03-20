using NUnit.Framework;

namespace RCPA.Parser
{
  [TestFixture]
  public class TestParserFormat
  {
    [Test]
    public void TestToString()
    {
      ParserFormat pf = new ParserFormat() { FormatName = "TEST" };
      Assert.AreEqual("TEST", pf.ToString());

      pf.Sample = "SAMPLE";
      Assert.IsTrue(pf.ToString().Contains("SAMPLE"));
    }
  }
}
