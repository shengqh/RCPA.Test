using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestNeutralLossUtils
  {
    [Test]
    public void TestCanLossWater()
    {
      foreach (char c in new Aminoacids().GetVisibleAminoacids())
      {
        switch (c)
        {
          case 'S':
          case 'T':
          case 'E':
          case 'D':
            Assert.IsTrue(NeutralLossUtils.CanLossWater(c));
            break;
          default:
            Assert.IsFalse(NeutralLossUtils.CanLossWater(c));
            break;
        }
      }
    }

    [Test]
    public void TestCanLossAmmonia()
    {
      foreach (char c in new Aminoacids().GetVisibleAminoacids())
      {
        switch (c)
        {
          case 'K':
          case 'R':
          case 'N':
          case 'Q':
            Assert.IsTrue(NeutralLossUtils.CanLossAmmonia(c));
            break;
          default:
            Assert.IsFalse(NeutralLossUtils.CanLossAmmonia(c));
            break;
        }
      }
    }
  }
}
