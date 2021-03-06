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
  public class TestClassificationOptions
  {
    [Test]
    public void Test()
    {
      ClassificationOptions option = new ClassificationOptions()
      {
        ClassifyByCharge = true,
        ClassifyByMissCleavage = true,
        ClassifyByModification = true,
        ModifiedAminoacids = "STY"
      };

      XElement root = new XElement("Root");
      option.Save(root);

      ClassificationOptions target = new ClassificationOptions();
      target.Load(root);

      Assert.AreEqual(option.ClassifyByCharge, target.ClassifyByCharge);
      Assert.AreEqual(option.ClassifyByMissCleavage, target.ClassifyByMissCleavage);
      Assert.AreEqual(option.ClassifyByModification, target.ClassifyByModification);
      Assert.AreEqual(option.ModifiedAminoacids, target.ModifiedAminoacids);
    }
  }
}
