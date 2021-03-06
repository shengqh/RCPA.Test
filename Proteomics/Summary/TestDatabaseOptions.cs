﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using RCPA.Proteomics.Summary.Uniform;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestDatabaseOptions
  {
    [Test]
    public void Test()
    {
      DatabaseOptions option = new DatabaseOptions()
      {
        Location = "c:\\d",
        AccessNumberPattern = "ddd",
        ContaminationNamePattern = "ccc",
        DecoyPattern = "eee",
        RemovePeptideFromDecoyDB = true
      };

      XElement root = new XElement("Root");
      option.Save(root);

      DatabaseOptions target = new DatabaseOptions();
      target.Load(root);

      root.RemoveAll();
      target.Save(root);

      Assert.AreEqual(option.Location, target.Location);
      Assert.AreEqual(option.AccessNumberPattern, target.AccessNumberPattern);
      Assert.AreEqual(option.ContaminationNamePattern, target.ContaminationNamePattern);
      Assert.AreEqual(option.DecoyPattern, target.DecoyPattern);
      Assert.AreEqual(option.RemovePeptideFromDecoyDB, target.RemovePeptideFromDecoyDB);
    }
  }
}
