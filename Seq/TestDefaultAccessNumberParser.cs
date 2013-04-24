using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Seq
{
  [TestFixture]
  public class TestDefaultAccessNumberParser
  {
    [Test]
    public void TestGetValue()
    {
      Assert.AreEqual("ABCDE", DefaultAccessNumberParser.GetInstance().GetValue("ABCDE"));
      Assert.AreEqual("ABCDE", DefaultAccessNumberParser.GetInstance().GetValue("ABCDE   GEHDKG"));
    }
  }
}
