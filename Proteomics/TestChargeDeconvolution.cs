using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestChargeDeconvolution
  {
    [Test]
    public void TestDevonvolute()
    {
      ChargeDeconvolution cd = new ChargeDeconvolution(10, 10);

      List<Peak> pl = new List<Peak>();
      pl.Add(new Peak(407.266, 462555));
      pl.Add(new Peak(408.269, 89539.4));

      pl.Add(new Peak(474.271, 2.28526e+006));
      pl.Add(new Peak(474.773, 898568));
      pl.Add(new Peak(475.268, 93755));
      pl.Add(new Peak(475.276, 98450.6));

      pl.Add(new Peak(505.303, 784262));
      pl.Add(new Peak(506.306, 169459));

      pl.Add(new Peak(516.306, 19459));

      pl.Add(new Peak(537.308, 888787));
      pl.Add(new Peak(537.494, 88787));
      pl.Add(new Peak(537.809, 266289));

      cd.Deconvolute(pl);
      Assert.AreEqual(1, pl[0].Charge);
      Assert.AreEqual(1, pl[1].Charge);

      Assert.AreEqual(2, pl[2].Charge);
      Assert.AreEqual(2, pl[3].Charge);
      Assert.AreEqual(0, pl[4].Charge);
      Assert.AreEqual(2, pl[5].Charge);

      Assert.AreEqual(1, pl[6].Charge);
      Assert.AreEqual(1, pl[7].Charge);

      Assert.AreEqual(0, pl[8].Charge);

      Assert.AreEqual(2, pl[9].Charge);
      Assert.AreEqual(0, pl[10].Charge);
      Assert.AreEqual(2, pl[11].Charge);
    }

    [Test]
    public void TestDevonvoluteClosely()
    {
      ChargeDeconvolution cd = new ChargeDeconvolution(15, 10);

      List<Peak> pl = new List<Peak>();
      pl.Add(new Peak(1148.5970, 462555));
      pl.Add(new Peak(1149.1007, 89539.4));
      pl.Add(new Peak(1149.5902, 2.28526e+006));
      pl.Add(new Peak(1150.1012, 898568));
      pl.Add(new Peak(1150.2814, 93755));
      pl.Add(new Peak(1151.2863, 98450.6));

      cd.Deconvolute(pl);
      Assert.AreEqual(2, pl[0].Charge);
      Assert.AreEqual(2, pl[1].Charge);
      Assert.AreEqual(2, pl[2].Charge);
      Assert.AreEqual(2, pl[3].Charge);
      Assert.AreEqual(1, pl[4].Charge);
      Assert.AreEqual(1, pl[5].Charge);
    }

    [Test]
    public void TestDevonvoluteDifferentChargeContinuely()
    {
      ChargeDeconvolution cd = new ChargeDeconvolution(20, 10);

      List<Peak> pl = new List<Peak>();
      pl.Add(new Peak(1146.1007, 462555));
      pl.Add(new Peak(1147.1007, 462555));
      pl.Add(new Peak(1148.1007, 89539.4));
      pl.Add(new Peak(1148.5970, 462555));
      pl.Add(new Peak(1149.1007, 89539.4));
      pl.Add(new Peak(1149.5902, 2.28526e+006));
      pl.Add(new Peak(1150.1012, 898568));

      cd.Deconvolute(pl);
      Assert.AreEqual(1, pl[0].Charge);
      Assert.AreEqual(1, pl[1].Charge);
      Assert.AreEqual(2, pl[2].Charge);
      Assert.AreEqual(2, pl[3].Charge);
      Assert.AreEqual(2, pl[4].Charge);
      Assert.AreEqual(2, pl[5].Charge);
      Assert.AreEqual(2, pl[6].Charge);
    }
  }
}
