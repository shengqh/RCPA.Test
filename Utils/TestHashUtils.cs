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
      Assert.AreEqual("7cf36c11d4a3ae49be41800cc694d0ed", HashUtils.GetMD5Hash("../../../data/md5test.xml", true, false));
      Assert.AreEqual("7cf36c11d4a3ae49be41800cc694d0ed", HashUtils.GetGzippedMD5Hash("../../../data/md5test.xml.gz", true, false));
      Assert.AreEqual("7cf36c11d4a3ae49be41800cc694d0ed", HashUtils.GetZippedMD5Hash("../../../data/md5test.xml.zip", true, false));
    }
  }
}
