using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestStreamUtils
  {
    [Test]
    public void TestGetCharpos()
    {
      using (StreamReader sr = new StreamReader(@"../../../data/20030428_4_29L_15.outs"))
      {
        Assert.AreEqual(0, StreamUtils.GetCharpos(sr));

        char[] buffer = new char[100];
        sr.Read(buffer, 0, 90);

        Assert.AreEqual(90, StreamUtils.GetCharpos(sr));
      }
    }
  }
}
