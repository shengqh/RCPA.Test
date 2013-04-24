using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedSpectrumPropertyConverterFactory
  {
    [Test]
    public void TestDtaselectConverter()
    {
      IPropertyConverter<IIdentifiedSpectrum> converter;
      
      converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().FindConverter("Sequence");
      Assert.AreEqual(converter.GetType(), new IdentifiedSpectrumSequenceConverter<IIdentifiedSpectrum>().GetType());

      converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().FindConverter("Sequence","Dtaselect");
      Assert.AreEqual(converter.GetType(), new IdentifiedSpectrumSequenceConverterDtaselect<IIdentifiedSpectrum>().GetType());

      converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().FindConverter("\"File, Scan(s)\"");
      Assert.AreEqual(converter.GetType(), new IdentifiedSpectrumFileScanConverter<IIdentifiedSpectrum>().GetType());

      converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().FindConverter("FileName");
      Assert.AreEqual(converter.GetType(), new IdentifiedSpectrumFileScanConverterDtaselect<IIdentifiedSpectrum>().GetType());

      converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().FindConverter("FileName","Dtaselect");
      Assert.AreEqual(converter.GetType(), new IdentifiedSpectrumFileScanConverterDtaselect<IIdentifiedSpectrum>().GetType());
    }

    [Test]
    public void TestNoredundant()
    {
      string header = "\t\"File, Scan(s)\"\tSequence\tMH+\tDiff(MH+)\tCharge\tRank\tScore\tDeltaScore\tExpectValue\tQuery\tIons\tReference\tDIFF_MODIFIED_CANDIDATE\tPI\tMissCleavage\tModification";
      IPropertyConverter<IIdentifiedSpectrum> converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().GetConverters(header, '\t');

      Assert.AreEqual(header, converter.Name);

      IIdentifiedSpectrum mphit = new IdentifiedSpectrum();
      mphit.Query.FileScan.ShortFileName = "AAA,1-2";

      IdentifiedPeptide mp1 = new IdentifiedPeptide(mphit);
      mp1.Sequence = "AAAAA";
      mp1.AddProtein("PROTEIN1");
      mp1.AddProtein("PROTEIN2");

      IdentifiedPeptide mp2 = new IdentifiedPeptide(mphit);
      mp2.Sequence = "BBBBB";
      mp2.AddProtein("PROTEIN3");

      mphit.TheoreticalMH = 1000.00102;
      mphit.ExperimentalMH = 1000.0;
      mphit.Query.Charge = 2;
      mphit.Rank = 1;
      mphit.Score = 100.2;
      mphit.DeltaScore = 0.5;
      mphit.ExpectValue = 1.1e-2;
      mphit.Query.QueryId = 10;
      mphit.NumMissedCleavages = 1;
      mphit.Modifications = "O18(1)";

      string expect = "	AAA,1 - 2	AAAAA ! BBBBB	1000.00102	0.00102	2	1	100.2	0.5	1.10E-002	10	0|0	PROTEIN1/PROTEIN2 ! PROTEIN3		0.00	1	O18(1)";
      Assert.AreEqual(expect, converter.GetProperty(mphit));

      string expectNew = "	BBB,2 - 3	BBBBB	1002.00783	-0.00200	3	2	200.2	0.6	1.20E-003	20	0|0	PROTEIN2/PROTEIN4		0.00	2	O18(2)";
      converter.SetProperty(mphit, expectNew);
      Assert.AreEqual(expectNew, converter.GetProperty(mphit));
    }

    [Test]
    public void TestDtaselect()
    {
      string header = "Unique	FileName	Score	DeltCN	M+H+	CalcM+H+	TotalIntensity	SpRank	SpScore	IonProportion	Redundancy	Sequence";
      IPropertyConverter<IIdentifiedSpectrum> converter = IdentifiedSpectrumPropertyConverterFactory.GetInstance().GetConverters(header, '\t');

      Assert.AreEqual(header, converter.Name);
    }

  }
}
