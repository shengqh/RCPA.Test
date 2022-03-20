using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestMiscUtils
  {
    [Test]
    public void TestMatchToList()
    {
      Match m = Regex.Match("1A6B8", @"(\d)\S(\d)\S(\d)");
      List<string> s = m.ToList();
      Assert.AreEqual(3, s.Count);
      Assert.AreEqual("1", s[0]);
      Assert.AreEqual("6", s[1]);
      Assert.AreEqual("8", s[2]);
    }
  }
}
