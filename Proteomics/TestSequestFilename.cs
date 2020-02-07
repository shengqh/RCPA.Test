using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestSequestFilename
  {
    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestParseThrowException()
    {
      SequestFilename.Parse("ddd.dd.d1.d.dta");
    }

    [Test]
    public void TestParse()
    {
      string expectName = "ddd.dd.1.2.3.dta";
      SequestFilename sf = SequestFilename.Parse(expectName);
      Assert.AreEqual("ddd.dd", sf.Experimental);
      Assert.AreEqual(1, sf.FirstScan);
      Assert.AreEqual(2, sf.LastScan);
      Assert.AreEqual(3, sf.Charge);
      Assert.AreEqual("dta", sf.Extension);
      Assert.AreEqual(expectName, sf.ToString());
    }

    [Test]
    public void TestSetShortFilename1()
    {
      SequestFilename sf = new SequestFilename();
      sf.ShortFileName = "JWH_SAX_25_050906,13426 - 13428";
      Assert.AreEqual("JWH_SAX_25_050906", sf.Experimental);
      Assert.AreEqual(13426, sf.FirstScan);
      Assert.AreEqual(13428, sf.LastScan);
    }

    [Test]
    public void TestSetShortFilename2()
    {
      SequestFilename sf = new SequestFilename();
      sf.ShortFileName = "JWH_SAX_25_050906,13426";
      Assert.AreEqual("JWH_SAX_25_050906", sf.Experimental);
      Assert.AreEqual(13426, sf.FirstScan);
      Assert.AreEqual(13426, sf.LastScan);
    }

    [Test]
    public void TestSetShortFilename3()
    {
      SequestFilename sf = new SequestFilename();
      sf.ShortFileName = "\"JWH_SAX_25_050906,13426\"";
      Assert.AreEqual("JWH_SAX_25_050906", sf.Experimental);
      Assert.AreEqual(13426, sf.FirstScan);
      Assert.AreEqual(13426, sf.LastScan);
    }


  }
}
