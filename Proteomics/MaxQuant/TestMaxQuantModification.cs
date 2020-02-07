using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Mascot;
using RCPA.Utils;

namespace RCPA.Proteomics.MaxQuant
{
  [TestFixture]
  public class TestMaxQuantModification
  {
    [Test]
    public void TestRead()
    {
      var mods = MaxQuantModificationList.ReadFromFile(TestContext.CurrentContext.TestDirectory + "/../../../data/maxquant_modifications.xml");
      Assert.AreEqual(550, mods.Count);
      Assert.AreEqual("Acetyl (K)", mods[0].FullName);
      Assert.AreEqual("(ac)", mods[0].ShortName);
      Assert.AreEqual("C2H2O", mods[0].Composition.ToString());
      Assert.AreEqual("Cysteinyl - carbamidomethyl", mods[549].FullName);
      Assert.AreEqual("(cy)", mods[549].ShortName);
      Assert.AreEqual("CH2OS", mods[549].Composition.ToString());
    }
  }
}
