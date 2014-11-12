using NUnit.Framework;
using RCPA.Seq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Omssa
{
  [TestFixture]
  public class TestOmssaConsts
  {
    [Test]
    public void TestEnzymeMap()
    {
      Assert.AreEqual(27, OmssaConsts.EnzymeMap.Count);
      Assert.AreEqual("trypsin", OmssaConsts.EnzymeMap["0"]);
      Assert.AreEqual("none", OmssaConsts.EnzymeMap["255"]);
    }
  }
}
