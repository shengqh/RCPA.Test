using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Parser;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestAccessNumberParserFactory
  {
    [Test]
    public void Test()
    {
      ParserFormatList lstFormat = new ParserFormatList();
      lstFormat.ReadFromFile(@"../../../data/MiscOptions.xml", AccessNumberParserFactory.SECTION_NAME);
      List<IAccessNumberParser> acParser = AccessNumberParserFactory.GetParsers(lstFormat);
      Assert.AreEqual(5, acParser.Count);
    }
  }
}
