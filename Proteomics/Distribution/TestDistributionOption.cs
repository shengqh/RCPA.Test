﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Distribution
{
  [TestFixture]
  public class TestDistributionOption
  {
    [Test]
    public void TestGetClassifiedNames()
    {
      DistributionOption option = new DistributionOption();
      option.ClassificationSet["A"] = new List<string> (new string[]{"1","2","3"});
      option.ClassificationSet["B"] = new List<string> (new string[]{"4","5","6"});

      string[] actual = option.GetClassifiedNames();
      Assert.AreEqual(new string[] { "A", "B" }, actual);
    }
  }
}
