using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Distribution
{
  [TestFixture]
  public class TestModifiedPeptideDistributionCalculator
  {
    private ModifiedPeptideDistributionCalculator calc = new ModifiedPeptideDistributionCalculator("STY", true);

    [Test]
    public void TestIsModified()
    {
      Mock<IIdentifiedSpectrum> spectrum = new Mock<IIdentifiedSpectrum>();
      spectrum.SetupGet(x => x.Sequence).Returns("-.M#AS*YSLVESNSFGS*ENWCLK.L");

      Assert.IsTrue(calc.IsModified(spectrum.Object));

      spectrum.SetupGet(x => x.Sequence).Returns("K.AADALLLK.V");
      Assert.IsFalse(calc.IsModified(spectrum.Object));
    }
  }
}
