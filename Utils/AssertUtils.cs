using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace RCPA.Utils
{
  public class AssertUtils
  {
    public static void AssertFileEqual(string file1, string file2)
    {
      Assert.AreEqual(new FileInfo(file1).Length, new FileInfo(file2).Length,"File lengths are not equal");

      using (StreamReader sr1 = new StreamReader(file1)) 
      {
        using (StreamReader sr2 = new StreamReader(file2))
        {
          string line1, line2;
          while ((line1 = sr1.ReadLine()) != null && (line2 = sr2.ReadLine()) != null)
          {
            Assert.AreEqual(line1, line2);
          }
        }
      }
    }
  }
}
