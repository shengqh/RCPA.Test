using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestSequence
  {
    [Test]
    public void TestConstruction()
    {
      Sequence seq = new Sequence("test description of test", "ABCDE");
      Assert.AreEqual("test", seq.Name);
      Assert.AreEqual("test description of test", seq.Reference);
      Assert.AreEqual("description of test", seq.Description);
      Assert.AreEqual("ABCDE", seq.SeqString);
    }
  }
}
