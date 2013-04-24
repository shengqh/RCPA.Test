using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Quantification.ITraq;

namespace RCPA.Proteomics.Quantification
{
  [TestFixture]
  public class TestITraqPhosphoRatioCalculator
  {
    [Test]
    public void Test()
    {
      ITraqPhosphoRatioCalculator calc = new ITraqPhosphoRatioCalculator(116, 117, 114, 115);
      IsobaricItem item = new IsobaricItem()
      {
        PlexType =  IsobaricType.PLEX4
      };

      item[114] = 1;
      item[115] = 1;
      item[116] = 1;
      item[117] = 1;

      Assert.IsTrue(calc.Valid(item));

      item[116] = ITraqConsts.NULL_INTENSITY;
      Assert.IsFalse(calc.Valid(item));

      item[116] = 1;
      item[117] = ITraqConsts.NULL_INTENSITY;
      Assert.IsFalse(calc.Valid(item));
    }
  }
}
