using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestScaleTransform
  {
    [Test]
    public void Test()
    {
      ScaleTransform st = new ScaleTransform(100, 1000);

      Assert.AreEqual(500, st.AtoB(50));
      Assert.AreEqual(50, st.BtoA(500));
    }
  }
}
