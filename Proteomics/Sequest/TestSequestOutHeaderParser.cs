using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestOutHeaderParser
  {
    [Test]
    public void TestParseDynamicModification()
    {
      var dm = new SequestOutHeaderParser().ParseDynamicModification("  (STY* +79.9663) (M# -15.9949) C=160.0307  Enzyme:Trypsin(KR) (2)");
      Assert.AreEqual(dm['*'], 79.9663, 0.0001);
      Assert.AreEqual(dm['#'], -15.9949, 0.0001);
    }


    [Test]
    public void TestParseModification()
    {
      var dm = new SequestOutHeaderParser().ParseStaticModification("  (STY* +79.9663) (M# -15.9949) C=160.0307  Enzyme:Trypsin(KR) (2)");
      Assert.AreEqual(dm['C'], 160.0307, 0.0001);
    }

    [Test]
    public void TestParseMassType()
    {
      bool precursorIsMono, peakIsMono;

      Assert.IsFalse(new SequestOutHeaderParser().ParseMassType(" TEMP ", out precursorIsMono, out peakIsMono));

      Assert.IsTrue(new SequestOutHeaderParser().ParseMassType("  (M+H)+ mass = 2387.91135 ~ 1.1940 (+3), fragment tol = 1.0000 , MONO/AVG", out precursorIsMono, out peakIsMono));
      Assert.IsTrue(precursorIsMono);
      Assert.IsFalse(peakIsMono);
      
      Assert.IsTrue(new SequestOutHeaderParser().ParseMassType("  (M+H)+ mass = 2387.91135 ~ 1.1940 (+3), fragment tol = 1.0000 , AVG/MONO", out precursorIsMono, out peakIsMono));
      Assert.IsFalse(precursorIsMono);
      Assert.IsTrue(peakIsMono);
    }
  }
}
