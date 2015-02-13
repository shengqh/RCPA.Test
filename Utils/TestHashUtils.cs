using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestHashUtils
  {
    [Test]
    public void TestGetMD5Hash()
    {
      Assert.AreEqual("528ac4773ba82468b8c78a2799d32485", HashUtils.GetMD5Hash("../../data/md5test.txt", true, false));
    }
  }
}
