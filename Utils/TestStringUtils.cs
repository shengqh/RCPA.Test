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

    [Test]
    public void TestTake()
    {
      string oldHeader = "A\tB\tC";

      AssertUtils.AssertArrayEqual(new[] { "A", "B" }, oldHeader.Take('\t', 2));

      AssertUtils.AssertArrayEqual(new[] { "A", "B", "C" }, oldHeader.Take('\t', 4));
    }

    [Test]
    public void TestConcatOverlapByPercentage()
    {
      Assert.IsNull(StringUtils.ConcatOverlapByPercentage("ABCDE", "HIJKL", 0.5));
      Assert.IsNull(StringUtils.ConcatOverlapByPercentage("ABCDE", "ABCDF", 0.5));
      Assert.AreEqual("ABCDEFG", StringUtils.ConcatOverlapByPercentage("ABCDE", "CDEFG", 0.5));
      Assert.AreEqual("ABCDEFG", StringUtils.ConcatOverlapByPercentage("CDEFG", "ABCDE", 0.5));
      Assert.AreEqual("ABCDE", StringUtils.ConcatOverlapByPercentage("ABCDE", "BCD", 0.5));
      Assert.AreEqual("ABCDE", StringUtils.ConcatOverlapByPercentage("BCD", "ABCDE", 0.5));
    }

    [Test]
    public void TestConcatOverlapByExtensionNumber()
    {
      //no overlap
      Assert.IsNull(StringUtils.ConcatOverlapByExtensionNumber("ABCDE", "HIJKL", 1));
      //cannot mismatch
      Assert.IsNull(StringUtils.ConcatOverlapByExtensionNumber("ABCDE", "ABCDF", 1));
      //overlap 3 out of 5 characters
      Assert.AreEqual("ABCDEFG", StringUtils.ConcatOverlapByExtensionNumber("ABCDE", "CDEFG", 2));
      Assert.AreEqual("ABCDEFG", StringUtils.ConcatOverlapByExtensionNumber("CDEFG", "ABCDE", 2));
      //failed to extend if only 1 base allowed
      Assert.IsNull(StringUtils.ConcatOverlapByExtensionNumber("ABCDE", "CDEFG", 1));
      Assert.IsNull(StringUtils.ConcatOverlapByExtensionNumber("CDEFG", "ABCDE", 1));
      //contain
      Assert.AreEqual("ABCDE", StringUtils.ConcatOverlapByExtensionNumber("ABCDE", "BCD", 1));
      Assert.AreEqual("ABCDE", StringUtils.ConcatOverlapByExtensionNumber("BCD", "ABCDE", 1));

    }
  }
}
