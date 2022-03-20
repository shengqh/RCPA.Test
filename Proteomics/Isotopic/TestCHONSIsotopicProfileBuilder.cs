using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Isotopic
{
  [TestFixture]
  public class TestCHONSIsotopicProfileBuilder
  {
    private readonly CHONSIsotopicProfileBuilder ip = new CHONSIsotopicProfileBuilder();
    private readonly AtomComposition ac = new AtomComposition("C112H184O80N8");

    //[Test]
    public void TestWuyin()
    {
      List<String> lines = FileUtils.ReadFile(@"C:\Documents and Settings\sheng\Desktop\formulas.txt");
      foreach (String line in lines)
      {
        var ac = new AtomComposition(line);
        List<double> profile = this.ip.GetProfile(ac, 10);
        Console.WriteLine(line);
        foreach (double d in profile)
        {
          Console.WriteLine(d);
        }
        Console.WriteLine();
      }
    }

    //[Test]
    public void TestWuyinPeptide()
    {
      List<String> lines = FileUtils.ReadFile(@"C:\Documents and Settings\sheng\Desktop\sequences.txt");
      var aas = new Aminoacids();

      foreach (String line in lines)
      {
        AtomComposition ac = aas.GetPeptideAtomComposition(line);
        ac.Add(new AtomComposition("H2O"));
        List<double> profile = this.ip.GetProfile(ac, 10);
        Console.WriteLine(line);
        Console.WriteLine(ac);
        foreach (double d in profile)
        {
          Console.WriteLine(d);
        }
        Console.WriteLine();
      }
    }

    [Test]
    public void TestGetProfile1()
    {
      List<double> result = this.ip.GetProfile(this.ac, 3);
      Assert.AreEqual(0.2241, result[0], 0.0001);
      Assert.AreEqual(0.2942, result[1], 0.0001);
      Assert.AreEqual(0.2274, result[2], 0.0001);
    }

    [Test]
    public void TestGetProfile2()
    {
      List<double> result = this.ip.GetProfile(this.ac);
      Assert.Less(AbstractIsotopicProfileBuilder.PERCENT_TOLERANCE, result[result.Count - 2]);
      Assert.Less(result[result.Count - 1], AbstractIsotopicProfileBuilder.PERCENT_TOLERANCE);
    }
  }
}