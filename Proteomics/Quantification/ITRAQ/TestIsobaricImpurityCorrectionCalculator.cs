using NUnit.Framework;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  [TestFixture]
  public class TestIsobaricImpurityCorrectionCalculator
  {
    [Test]
    public void TestParsePlex8()
    {
      double[][] a = IsobaricImpurityCorrectionCalculator.ParseTable(IsobaricType.PLEX8, @TestContext.CurrentContext.TestDirectory + "/../../../data//itraq-8plex.csv");

      // OutputArray(a);

      double[][] expect = new double[][] {
        new double[]{0.9287,0.0689,0.0024,0.0000,0.0000,0.0000,0.0000,0.0000},
        new double[]{0.0094,0.9300,0.0590,0.0016,0.0000,0.0000,0.0000,0.0000},
        new double[]{0.0000,0.0188,0.9312,0.0490,0.0010,0.0000,0.0000,0.0000},
        new double[]{0.0000,0.0000,0.0282,0.9321,0.0390,0.0007,0.0000,0.0000},
        new double[]{0.0000,0.0000,0.0006,0.0377,0.9329,0.0288,0.0000,0.0000},
        new double[]{0.0000,0.0000,0.0000,0.0009,0.0471,0.9329,0.0191,0.0000},
        new double[]{0.0000,0.0000,0.0000,0.0000,0.0014,0.0566,0.9333,0.0000},
        new double[]{0.0000,0.0000,0.0000,0.0000,0.0000,0.0000,0.0027,0.9211}
      };

      for (int i = 0; i < a.Length; i++)
      {
        for (int j = 0; j < a[i].Length; j++)
        {
          Assert.AreEqual(a[i][j], expect[i][j], 0.0001);
        }
      }
    }

    [Test]
    public void TestParseTMT6()
    {
      double[][] a = IsobaricImpurityCorrectionCalculator.ParseTable(IsobaricType.TMT6, @TestContext.CurrentContext.TestDirectory + "/../../../data//TMT-6plex.csv");

      //OutputArray(a);

      double[][] expect = new double[][] {
        new double[]{0.9232,0.0768,0.0000,0.0000,0.0000,0.0000},
        new double[]{0.0000,0.9342,0.0658,0.0000,0.0000,0.0000},
        new double[]{0.0000,0.0000,0.9462,0.0538,0.0000,0.0000},
        new double[]{0.0000,0.0000,0.0140,0.9406,0.0454,0.0000},
        new double[]{0.0000,0.0000,0.0000,0.0000,0.9656,0.0344},
        new double[]{0.0000,0.0000,0.0000,0.0000,0.0167,0.9454}
      };

      for (int i = 0; i < a.Length; i++)
      {
        for (int j = 0; j < a[i].Length; j++)
        {
          Assert.AreEqual(a[i][j], expect[i][j], 0.0001);
        }
      }
    }

    [Test]
    public void TestParsePlex4()
    {
      double[][] a = IsobaricImpurityCorrectionCalculator.ParseTable(IsobaricType.PLEX4, @TestContext.CurrentContext.TestDirectory + "/../../../data//itraq-4plex.csv");

      //OutputArray(a);

      double[][] expect = new double[][] {
        new double[]{0.9290,0.0590,0.0020,0.0000},
        new double[]{0.0200,0.9230,0.0560,0.0010},
        new double[]{0.0000,0.0300,0.9240,0.0450},
        new double[]{0.0000,0.0010,0.0400,0.9230}
      };

      for (int i = 0; i < a.Length; i++)
      {
        for (int j = 0; j < a[i].Length; j++)
        {
          Assert.AreEqual(a[i][j], expect[i][j], 0.0001);
        }
      }

    }

    public void InitializePlex4()
    {
      var lines = File.ReadAllLines(@TestContext.CurrentContext.TestDirectory + "/../../../data//ITraqIsotope.txt");
      List<double[]> vs = new List<double[]>();
      for (int i = 1; i < lines.Length; i++)
      {
        var parts = lines[i].Split();
        var name = parts[1];
        parts = parts.Skip(1).ToArray();
        var values = (from p in parts
                      let d = MyConvert.ToDouble(p)
                      select d).ToList();
        var v0 = 100 - values.Sum();
        values.Insert(2, v0);
        vs.Add(values.ToArray());
      }
      OutputArray(vs.ToArray());
    }

    private static void OutputArray(double[][] a)
    {
      for (int i = 0; i < a.Length; i++)
      {
        for (int j = 0; j < a[i].Length; j++)
        {
          if (j == 0)
          {
            Console.Write("new double[]{");
          }
          else
          {
            Console.Write(",");
          }

          Console.Write("{0:0.0000}", a[i][j]);
          if (j == a[i].Length - 1)
          {
            Console.WriteLine("},");
          }
        }
      }
    }

    [Test]
    public void TestCorrectPlex4()
    {
      var calc = new IsobaricImpurityCorrectionCalculator(IsobaricType.PLEX4, @TestContext.CurrentContext.TestDirectory + "/../../../data//itraq-4plex.csv");
      double[] b = new double[] { 0.949, 1.013, 1.022, 0.969 };
      double[] x = calc.Correct(b);
      foreach (var v in x)
      {
        Assert.AreEqual(1, v, 0.001);
      }

      //repeat again to ensure that the matrix (a) in calc has not been changed.
      b = new double[] { 0.949, 1.013, 1.022, 0.969 };
      x = calc.Correct(b);
      foreach (var v in x)
      {
        Assert.AreEqual(1, v, 0.001);
      }
    }

    [Test]
    public void TestCorrectPlex8()
    {
      var calc = new IsobaricImpurityCorrectionCalculator(IsobaricType.PLEX8, @TestContext.CurrentContext.TestDirectory + "/../../../data//itraq-8plex.csv");
      var b = new List<double>();
      var m = calc.CloneTable();
      for (int i = 0; i < m[0].Length; i++)
      {
        b.Add((from mm in m select mm[i]).Sum());
      }

      var bb = b.ToArray();
      double[] x = calc.Correct(bb);
      foreach (var v in x)
      {
        Assert.AreEqual(1, v, 0.001);
      }

      //repeat again to ensure that the matrix (a) in calc has not been changed.
      bb = b.ToArray();
      x = calc.Correct(bb);
      foreach (var v in x)
      {
        Assert.AreEqual(1, v, 0.001);
      }
    }
  }
}
