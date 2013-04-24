using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RCPA.Extension
{
  [TestFixture]
 public class TestFileExtension
  {
    public void TestReadLine()
    {
      var datafile = "../../data/TestReadLine.txt";

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
