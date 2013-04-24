using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Utils
{
  [TestFixture]
  public class TestPeptideUtils
  {
    [Test]
    public void TestGetPureSequence()
    {
      string expect = "DDDD";
      Assert.AreEqual(expect, PeptideUtils.GetPureSequence("A.DDDD.A"));
      Assert.AreEqual(expect, PeptideUtils.GetPureSequence(".DDDD.A"));
      Assert.AreEqual(expect, PeptideUtils.GetPureSequence("A.DDDD."));
      Assert.AreEqual(expect, PeptideUtils.GetPureSequence("DDDD"));
      Assert.AreEqual(expect, PeptideUtils.GetPureSequence("DDDpD"));
    }

    [Test]
    public void TestGetMatchedSequence()
    {
      string expect = "DD*DD";
      Assert.AreEqual(expect, PeptideUtils.GetMatchedSequence("A.DD*DD.A"));
      Assert.AreEqual(expect, PeptideUtils.GetMatchedSequence(".DD*DD.A"));
      Assert.AreEqual(expect, PeptideUtils.GetMatchedSequence("A.DD*DD."));
      Assert.AreEqual(expect, PeptideUtils.GetMatchedSequence("DD*DD"));
    }

    [Test]
    public void TestIsModified()
    {
      Assert.IsFalse(PeptideUtils.IsModified("DD*DD", 0));
      
      Assert.IsTrue(PeptideUtils.IsModified("DD*DD", 1));
      Assert.IsTrue(PeptideUtils.IsModified("DTpDD", 1));

      Assert.IsFalse(PeptideUtils.IsModified("DD*DD", 2));
      Assert.IsFalse(PeptideUtils.IsModified("DD*DD", 3));
      Assert.IsFalse(PeptideUtils.IsModified("DD*DD", 4));
    }
  }
}
