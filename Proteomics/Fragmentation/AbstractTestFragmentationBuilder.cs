using NUnit.Framework;
using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Fragmentation
{
  public abstract class AbstractTestFragmentationBuilder
  {
    public void AssertPeak(IonTypePeak peak, IonType ionType, int ionIndex, double mz)
    {
      Assert.AreEqual(ionType, peak.PeakType);
      Assert.AreEqual(ionIndex, peak.PeakIndex);
      Assert.AreEqual(mz, peak.Mz, 0.0001);
    }

    public void Output(List<IonTypePeak> pkl, IonType ionType)
    {
      Console.WriteLine("Assert.AreEqual({0}, pkl.Count);", pkl.Count);
      for (int i = 0; i < pkl.Count; i++)
      {
        Console.WriteLine(MyConvert.Format("AssertPeak(pkl[{0}], IonType.{1}, {2}, {3:0.0000});", i, ionType.ToString(), i + 1, pkl[i].Mz));
      }
    }
  }
}
