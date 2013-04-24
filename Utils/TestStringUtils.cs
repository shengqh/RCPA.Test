using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestStringUtils
  {
    [Test]
    public void TestLeftFill()
    {
      Assert.AreEqual("00001", StringUtils.LeftFill(1, 5, '0'));
    }

    [Test]
    public void TestMerge()
    {
      Assert.AreEqual("00001 ! 00002 ! 00003", StringUtils.Merge(new string[] { "00001", "00002", "00003" }, " ! "));
    }

    [Test]
    public void TestGetMergedHeader()
    {
      string oldHeader = "A\tB\tC";
      string[] additionalHeader = { "B", "D" };
      string expect = "A\tB\tC\tD";
      Assert.AreEqual(expect, StringUtils.GetMergedHeader(oldHeader, additionalHeader, '\t'));
    }

    [Test]
    public void TestRepeatChar()
    {
      Assert.AreEqual("AAA", StringUtils.RepeatChar('A', 3));
    }
  }
}
