using NUnit.Framework;
using System;
using System.Linq;

namespace RCPA.Proteomics.Quantification.Srm
{
  [TestFixture]
  public class TestOutlierDetector
  {
    [Test]
    public void TestDetect()
    {
      var values = new double[] { 6.0039, 6.4048, 6.7865, 6.6615, 13.9451 };

      var loged = (from v in values select Math.Log(v)).ToList();
      Assert.AreEqual(4, OutlierDetector.Detect(loged, 0.0001));

      loged.RemoveAt(4);
      Assert.AreEqual(-1, OutlierDetector.Detect(loged, 0.0001));
    }
  }
}
