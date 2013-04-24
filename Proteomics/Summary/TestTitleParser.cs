using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Parser;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestTitleParser
  {
    [Test]
    public void TestCmpd1()
    {
      ParserItem item = new ParserItem();
      ParserFormat format = new ParserFormat();
      format.FormatName = "TurboRAW2MGF, Raw+Cmpd";
      format.Add(new ParserItem("rawFile", @"(.+?)\s*Cmpd"));
      format.Add(new ParserItem("scanNumber", @"Cmpd\s*(\d+)\s*,"));

      TitleParser parser = new TitleParser(format);
      SequestFilename sf = parser.GetValue("TEST Cmpd 2345, xxxxx");
      Assert.AreEqual("TEST", sf.Experimental);
      Assert.AreEqual(2345, sf.FirstScan);
      Assert.AreEqual(2345, sf.LastScan);
    }

    [Test]
    public void TestCmpd2()
    {
      ParserItem item = new ParserItem();
      ParserFormat format = new ParserFormat();
      format.FormatName = "TurboRAW2MGF, Cmpd";
      format.Add(new ParserItem("rawFile", ""));
      format.Add(new ParserItem("scanNumber", @"Cmpd\s*(\d+)\s*,"));

      TitleParser parser = new TitleParser(format);
      SequestFilename sf = parser.GetValue("Cmpd 2345, xxxxx");
      Assert.AreEqual(2345, sf.FirstScan);
      Assert.AreEqual(2345, sf.LastScan);
    }

    [Test]
    public void TestDta()
    {
      ParserItem item = new ParserItem();
      ParserFormat format = new ParserFormat();
      format.FormatName = "TurboRAW2MGF, DTA Format";
      format.Add(new ParserItem("rawFile", @"(.+)\.\d+\.\d+\.\d\.(?:dta|DTA)"));
      format.Add(new ParserItem("scanNumber", @".+\.(\d+)\.(\d+)\.\d\.(?:dta|DTA)"));

      TitleParser parser = new TitleParser(format);
      SequestFilename sf = parser.GetValue("TEST.2345.2346.1.dta");
      Assert.AreEqual("TEST", sf.Experimental);
      Assert.AreEqual(2345, sf.FirstScan);
      Assert.AreEqual(2346, sf.LastScan);
    }
  }
}
