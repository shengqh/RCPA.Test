using NUnit.Framework;
using RCPA.Parser;
using System.Collections.Generic;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestAccessNumberParserFactory
  {
    [Test]
    public void Test()
    {
      ParserFormatList lstFormat = new ParserFormatList();
      lstFormat.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/MiscOptions.xml", AccessNumberParserFactory.SECTION_NAME);
      List<IAccessNumberParser> acParser = AccessNumberParserFactory.GetParsers(lstFormat);
      Assert.AreEqual(5, acParser.Count);
    }
  }
}
