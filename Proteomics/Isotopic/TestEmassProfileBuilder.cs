using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RCPA.emass;

namespace RCPA.Proteomics.Isotopic
{
  [TestFixture]
  public class TestEmassProfileBuilder
  {
    [Test]
    public void Calculate()
    {
      var calc = new EmassCalculator(@TestContext.CurrentContext.TestDirectory + "/../../../data//ISOTOPE.DAT");

      EmassProfileBuilder builder = new EmassProfileBuilder();

      AtomComposition light = new AtomComposition("C45H73N13O13");
      var res = builder.GetProfile(light, 2, 4);

      Console.WriteLine("light");
      foreach (var r in res)
      {
        Console.WriteLine("{0:0.0000}\t{1:0.000000}", r.Mz, r.Intensity);
      }

      AtomComposition heavy = new AtomComposition("(C13)6(N15)4C39H73N9O13");
      var resH = builder.GetProfile(heavy, 2, 4);

      Console.WriteLine("heavy");
      foreach (var r in resH)
      {
        Console.WriteLine("{0:0.0000}\t{1:0.000000}", r.Mz, r.Intensity);
      }
    }

    [Test]
    public void CalculateO18_charge1()
    {
      EmassProfileBuilder builder = new EmassProfileBuilder(@TestContext.CurrentContext.TestDirectory + "/../../../data//ISOTOPE.DAT");

      AtomComposition light = new AtomComposition("C45H73N13O13");
      var res = builder.GetProfile(light, 1, 5);

      var max = res.Max(m => m.Intensity);

      Console.WriteLine("nature");
      foreach (var r in res)
      {
        Console.WriteLine("{0:0.0000}\t{1:0.000000}", r.Mz - Atom.H.MonoMass, r.Intensity * 100 / max);
      }
    }

    [Test]
    public void CalculateO18()
    {
      EmassProfileBuilder builder = new EmassProfileBuilder(@TestContext.CurrentContext.TestDirectory + "/../../../data//ISOTOPE.DAT");

      AtomComposition light = new AtomComposition("C45H73N13O13");
      var res = builder.GetProfile(light, 2, 5);

      Console.WriteLine("nature");
      foreach (var r in res)
      {
        Console.WriteLine("{0:0.0000}\t{1:0.000000}", r.Mz, r.Intensity);
      }

      AtomComposition O181 = new AtomComposition("(O18)1C45H73N13O12");
      var resH = builder.GetProfile(O181, 2, 5);

      Console.WriteLine("O181");
      foreach (var r in resH)
      {
        Console.WriteLine("{0:0.0000}\t{1:0.000000}", r.Mz, r.Intensity);
      }

      AtomComposition O182 = new AtomComposition("(O18)2C45H73N13O11");
      var resH2 = builder.GetProfile(O182, 2, 5);

      Console.WriteLine("O182");
      foreach (var r in resH2)
      {
        Console.WriteLine("{0:0.0000}\t{1:0.000000}", r.Mz, r.Intensity);
      }

      Console.WriteLine(string.Format("O18 - O16 = {0:0.0000}", Atom.O18.MonoMass - Atom.O.MonoMass));
      Console.WriteLine(string.Format("2 * (C13 - C12) = {0:0.0000}", 2 * (Atom.C13.MonoMass - Atom.C.MonoMass)));
      Console.WriteLine(string.Format("O18 - O181, ppm = {0:0.00}ppm", PrecursorUtils.mz2ppm(res[0].Mz, resH[0].Mz - res[2].Mz)));
      Console.WriteLine(string.Format("O18 - O182, ppm = {0:0.00}ppm", PrecursorUtils.mz2ppm(res[0].Mz, resH2[0].Mz - res[4].Mz)));
      Console.WriteLine(string.Format("O181- O182, ppm = {0:0.00}ppm", PrecursorUtils.mz2ppm(resH[0].Mz, resH2[0].Mz - resH[2].Mz)));

    }
  }
}
