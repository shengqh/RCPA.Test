using System;
using System.IO;
using NUnit.Framework;
using RCPA.Proteomics.Fragmentation;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;

namespace RCPA.Proteomics.Score
{
  [TestFixture]
  public class TestXCorrelationSpectrumScore
  {
    [Test]
    public void Test()
    {
      using (var sr = new StreamReader("../../data/Ribo B 645.txt"))
      {
        var iter = new MascotGenericFormatIterator<Peak>(sr);
        PeakList<Peak> expPkl = iter.Next();
        var score = new XCorrelationSpectrumScore<Peak>(expPkl);

        var builder = new ETDPeptideCSeriesBuilder<IonTypePeak>(2000, 3);

        builder.CurAminoacids['*'].ResetMass(1217.117, 1217.117);
        List<IonTypePeak> thePkl = builder.Build("SRN*LTK");

        double Score = score.Calculate(thePkl);

        Console.Out.WriteLine("Score={0:0.0000}", Score);
      }
    }
  }
}