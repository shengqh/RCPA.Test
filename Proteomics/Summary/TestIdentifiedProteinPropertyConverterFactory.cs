using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedProteinPropertyConverterFactory
  {
    [Test]
    public void TestNoredundant()
    {
      string header = "	Reference	PepCount	UniquePepCount	CoverPercent	MW	PI	IdentifiedName";
      IPropertyConverter<IIdentifiedProtein> converter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(header, '\t');
      Assert.AreEqual(header, converter.Name);

      string line = "\tIPI:IPI00784154.1|SWISS-PROT:P10809|TREMBL:B2R5M6;Q53QD5;Q53SE2;Q96RI4;Q9UCR6|ENSEMBL:ENSP00000340019;ENSP00000373620|REFSEQ:NP_002147;NP_955472|H-INV:HIT000031088 Tax_Id=9606 Gene_Symbol=HSPD1 60 kDa heat shock protein, mitochondrial	84	19	43.46%	61054.43	5.70	IPI:IPI00784154.1|SWISS-PROT:P10809|TREMBL:B2R5M6;Q53QD5;Q53SE2;Q96RI4;Q9UCR6|ENSEMBL:ENSP00000340019;ENSP00000373620|REFSEQ:NP_002147;NP_955472|H-INV:HIT000031088 Tax_Id=9606 Gene_Symbol=HSPD1 60 kDa heat shock protein, mitochondrial";
      IIdentifiedProtein protein = new IdentifiedProtein();
      converter.SetProperty(protein, line);


      Assert.AreEqual("IPI:IPI00784154.1|SWISS-PROT:P10809|TREMBL:B2R5M6;Q53QD5;Q53SE2;Q96RI4;Q9UCR6|ENSEMBL:ENSP00000340019;ENSP00000373620|REFSEQ:NP_002147;NP_955472|H-INV:HIT000031088", protein.Name);
      Assert.AreEqual("Tax_Id=9606 Gene_Symbol=HSPD1 60 kDa heat shock protein, mitochondrial", protein.Description);
      Assert.AreEqual(19, protein.UniquePeptideCount);
      Assert.AreEqual(43.46, protein.Coverage);
      Assert.AreEqual(61054.43, protein.MolecularWeight);
      Assert.AreEqual(5.7, protein.IsoelectricPoint);

      for (int i = 0; i < 84; i++)
      {
        protein.Peptides.Add(new IdentifiedPeptide( new IdentifiedSpectrum()));
      }
      Assert.AreEqual(line, converter.GetProperty(protein));
    }

    [Test]
    public void TestDtaselect()
    {
      string header = "Locus	Sequence Count	Spectrum Count	Sequence Coverage	Length	MolWt	pI	Validation Status	Descriptive Name";
      IPropertyConverter<IIdentifiedProtein> converter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(header, '\t');
      Assert.AreEqual(header, converter.Name);

      string line = "YDR050C	495	495	81.10%	249	26795.41	5.74	U	YDR050C TPI1 SGDID:S000002457, Chr IV from 556470-555724, reverse complement, Verified ORF, \"Triose phosphate isomerase, abundant glycolytic enzyme; mRNA half-life is regulated by iron availability; transcription is controlled by activators Reb1p, Gcr1p, and Rap1p through binding sites in the 5' non-coding region\"";
      IIdentifiedProtein protein = new IdentifiedProtein();
      converter.SetProperty(protein, line);

      Assert.AreEqual("YDR050C", protein.Name);
      Assert.AreEqual("TPI1 SGDID:S000002457, Chr IV from 556470-555724, reverse complement, Verified ORF, \"Triose phosphate isomerase, abundant glycolytic enzyme; mRNA half-life is regulated by iron availability; transcription is controlled by activators Reb1p, Gcr1p, and Rap1p through binding sites in the 5' non-coding region\"", protein.Description);
      Assert.AreEqual(495, protein.UniquePeptideCount);
      Assert.AreEqual(81.1, protein.Coverage);
      Assert.AreEqual(26795.41, protein.MolecularWeight);
      Assert.AreEqual(5.74, protein.IsoelectricPoint);

      for (int i = 0; i < 495; i++)
      {
        protein.Peptides.Add(new IdentifiedPeptide( new IdentifiedSpectrum()));
      }
      Assert.AreEqual(line, converter.GetProperty(protein));
    }
  }
}
