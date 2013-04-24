using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestRangeLocation
  {
    [Test]
    public void TestParse()
    {
      RangeLocation expect = new RangeLocation(30, 600);
      Assert.AreEqual(expect, RangeLocation.Parse("30-600"));
      Assert.AreEqual(expect, RangeLocation.Parse("30.5, 600.1"));
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestParseExceptionRegex()
    {
      RangeLocation.Parse("3");
    }

    [Test]
    [ExpectedException(typeof(ArgumentException))]
    public void TestParseExceptionDouble()
    {
      RangeLocation.Parse("3.1.1, 200");
    }
  }
}
