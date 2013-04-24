using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Drawing;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestRectangleTransform
  {
    [Test]
    public void Test()
    {
      Rectangle rec = new Rectangle(100, 10000, 500, 400);
      RectangleTransform rt = new RectangleTransform(rec, 2000, 2000);

      Assert.AreEqual(100 + 250, rt.GetTransformX(1000));
      Assert.AreEqual(10000 + 400 - 100, rt.GetTransformY(500));
    }
  }
}
