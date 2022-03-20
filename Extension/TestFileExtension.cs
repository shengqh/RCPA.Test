﻿using NUnit.Framework;
using System.IO;

namespace RCPA.Extension
{
  [TestFixture]
  public class TestFileExtension
  {
    [Test]
    public void TestReadLine()
    {
      var datafile = TestContext.CurrentContext.TestDirectory + "/../../../data//TestReadLine.txt";

      using (FileStream fs = new FileStream(datafile, FileMode.Open))
      {
        Assert.AreEqual("Start\tLength\tKey", fs.ReadLine());
        Assert.AreEqual("55\t1279\tS1,255", fs.ReadLine());
        Assert.AreEqual("1334\t1383\tS2,355", fs.ReadLine());
        Assert.AreEqual(null, fs.ReadLine());
      }
    }
  }
}
