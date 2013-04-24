using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Sequest;
using RCPA.Seq;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentificationBuilder
  {
    [Test]
    public void TestBuild()
    {
      List<IIdentifiedSpectrum> spectra = new SequestPeptideTextFormat().ReadFromFile(@"..\..\data\TestBuilder.peptides");
      Assert.AreEqual(4, spectra.Count);

      IAccessNumberParser parser = AccessNumberParserFactory.FindOrCreateParser(@"(IPI\d+)", "IPI");

      List<IIdentifiedProtein> proteins = new IdentifiedProteinBuilder().Build(spectra);
      Assert.AreEqual(4, proteins.Count);

      List<IIdentifiedProteinGroup> groups = new IdentifiedProteinGroupBuilder().Build(proteins);
      Assert.AreEqual(2, groups.Count);

      Assert.AreEqual(1, groups[0].Count);
      Assert.AreEqual("IPI:IPI00784154.1|SW", groups[0][0].Name);

      Assert.AreEqual(2, groups[1].Count);
      Assert.AreEqual("REVERSED_00000001", groups[1][0].Name);
      Assert.AreEqual("REVERSED_00000002", groups[1][1].Name);

      IIdentifiedResult result = new IdentifiedResultBuilder(parser,"").Build(groups);
    }
  }
}
