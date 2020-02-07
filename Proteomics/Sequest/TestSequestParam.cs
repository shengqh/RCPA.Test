using NUnit.Framework;

namespace RCPA.Proteomics.Sequest
{
  [TestFixture]
  public class TestSequestParam
  {
    [Test]
    public void TestAminoacidToModification()
    {
      Assert.AreEqual(SequestStaticModification.add_G_Glycine, SequestParam.AminoacidToModification('G'));
      Assert.AreEqual(SequestStaticModification.add_A_Alanine, SequestParam.AminoacidToModification('A'));
      Assert.AreEqual(SequestStaticModification.add_S_Serine, SequestParam.AminoacidToModification('S'));
      Assert.AreEqual(SequestStaticModification.add_P_Proline, SequestParam.AminoacidToModification('P'));
      Assert.AreEqual(SequestStaticModification.add_V_Valine, SequestParam.AminoacidToModification('V'));
      Assert.AreEqual(SequestStaticModification.add_T_Threonine, SequestParam.AminoacidToModification('T'));
      Assert.AreEqual(SequestStaticModification.add_C_Cysteine, SequestParam.AminoacidToModification('C'));
      Assert.AreEqual(SequestStaticModification.add_L_Leucine, SequestParam.AminoacidToModification('L'));
      Assert.AreEqual(SequestStaticModification.add_I_Isoleucine, SequestParam.AminoacidToModification('I'));
      Assert.AreEqual(SequestStaticModification.add_X_LorI, SequestParam.AminoacidToModification('X'));
      Assert.AreEqual(SequestStaticModification.add_N_Asparagine, SequestParam.AminoacidToModification('N'));
      Assert.AreEqual(SequestStaticModification.add_O_Ornithine, SequestParam.AminoacidToModification('O'));
      Assert.AreEqual(SequestStaticModification.add_B_avg_NandD, SequestParam.AminoacidToModification('B'));
      Assert.AreEqual(SequestStaticModification.add_D_Aspartic_Acid, SequestParam.AminoacidToModification('D'));
      Assert.AreEqual(SequestStaticModification.add_Q_Glutamine, SequestParam.AminoacidToModification('Q'));
      Assert.AreEqual(SequestStaticModification.add_K_Lysine, SequestParam.AminoacidToModification('K'));
      Assert.AreEqual(SequestStaticModification.add_Z_avg_QandE, SequestParam.AminoacidToModification('Z'));
      Assert.AreEqual(SequestStaticModification.add_E_Glutamic_Acid, SequestParam.AminoacidToModification('E'));
      Assert.AreEqual(SequestStaticModification.add_M_Methionine, SequestParam.AminoacidToModification('M'));
      Assert.AreEqual(SequestStaticModification.add_H_Histidine, SequestParam.AminoacidToModification('H'));
      Assert.AreEqual(SequestStaticModification.add_F_Phenylalanine, SequestParam.AminoacidToModification('F'));
      Assert.AreEqual(SequestStaticModification.add_R_Arginine, SequestParam.AminoacidToModification('R'));
      Assert.AreEqual(SequestStaticModification.add_Y_Tyrosine, SequestParam.AminoacidToModification('Y'));
      Assert.AreEqual(SequestStaticModification.add_W_Tryptophan, SequestParam.AminoacidToModification('W'));
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestAminoacidToModificationException()
    {
      SequestParam.AminoacidToModification(' ');
    }

    [Test]
    public void TestModificationToAminoacid()
    {
      Assert.AreEqual(' ', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Cterm_peptide));
      Assert.AreEqual(' ', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Cterm_protein));
      Assert.AreEqual(' ', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Nterm_peptide));
      Assert.AreEqual(' ', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Nterm_protein));
      Assert.AreEqual('G', SequestParam.ModificationToAminoacid(SequestStaticModification.add_G_Glycine));
      Assert.AreEqual('A', SequestParam.ModificationToAminoacid(SequestStaticModification.add_A_Alanine));
      Assert.AreEqual('S', SequestParam.ModificationToAminoacid(SequestStaticModification.add_S_Serine));
      Assert.AreEqual('P', SequestParam.ModificationToAminoacid(SequestStaticModification.add_P_Proline));
      Assert.AreEqual('V', SequestParam.ModificationToAminoacid(SequestStaticModification.add_V_Valine));
      Assert.AreEqual('T', SequestParam.ModificationToAminoacid(SequestStaticModification.add_T_Threonine));
      Assert.AreEqual('C', SequestParam.ModificationToAminoacid(SequestStaticModification.add_C_Cysteine));
      Assert.AreEqual('L', SequestParam.ModificationToAminoacid(SequestStaticModification.add_L_Leucine));
      Assert.AreEqual('I', SequestParam.ModificationToAminoacid(SequestStaticModification.add_I_Isoleucine));
      Assert.AreEqual('X', SequestParam.ModificationToAminoacid(SequestStaticModification.add_X_LorI));
      Assert.AreEqual('N', SequestParam.ModificationToAminoacid(SequestStaticModification.add_N_Asparagine));
      Assert.AreEqual('O', SequestParam.ModificationToAminoacid(SequestStaticModification.add_O_Ornithine));
      Assert.AreEqual('B', SequestParam.ModificationToAminoacid(SequestStaticModification.add_B_avg_NandD));
      Assert.AreEqual('D', SequestParam.ModificationToAminoacid(SequestStaticModification.add_D_Aspartic_Acid));
      Assert.AreEqual('Q', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Q_Glutamine));
      Assert.AreEqual('K', SequestParam.ModificationToAminoacid(SequestStaticModification.add_K_Lysine));
      Assert.AreEqual('Z', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Z_avg_QandE));
      Assert.AreEqual('E', SequestParam.ModificationToAminoacid(SequestStaticModification.add_E_Glutamic_Acid));
      Assert.AreEqual('M', SequestParam.ModificationToAminoacid(SequestStaticModification.add_M_Methionine));
      Assert.AreEqual('H', SequestParam.ModificationToAminoacid(SequestStaticModification.add_H_Histidine));
      Assert.AreEqual('F', SequestParam.ModificationToAminoacid(SequestStaticModification.add_F_Phenylalanine));
      Assert.AreEqual('R', SequestParam.ModificationToAminoacid(SequestStaticModification.add_R_Arginine));
      Assert.AreEqual('Y', SequestParam.ModificationToAminoacid(SequestStaticModification.add_Y_Tyrosine));
      Assert.AreEqual('W', SequestParam.ModificationToAminoacid(SequestStaticModification.add_W_Tryptophan));
    }

    [Test]
    public void TestNameToModification()
    {
      Assert.AreEqual(SequestStaticModification.add_Cterm_peptide, SequestParam.NameToModification("add_Cterm_peptide"));
      Assert.AreEqual(SequestStaticModification.add_Cterm_protein, SequestParam.NameToModification("add_Cterm_protein"));
      Assert.AreEqual(SequestStaticModification.add_Nterm_peptide, SequestParam.NameToModification("add_Nterm_peptide"));
      Assert.AreEqual(SequestStaticModification.add_Nterm_protein, SequestParam.NameToModification("add_Nterm_protein"));
      Assert.AreEqual(SequestStaticModification.add_G_Glycine, SequestParam.NameToModification("add_G_Glycine"));
      Assert.AreEqual(SequestStaticModification.add_A_Alanine, SequestParam.NameToModification("add_A_Alanine"));
      Assert.AreEqual(SequestStaticModification.add_S_Serine, SequestParam.NameToModification("add_S_Serine"));
      Assert.AreEqual(SequestStaticModification.add_P_Proline, SequestParam.NameToModification("add_P_Proline"));
      Assert.AreEqual(SequestStaticModification.add_V_Valine, SequestParam.NameToModification("add_V_Valine"));
      Assert.AreEqual(SequestStaticModification.add_T_Threonine, SequestParam.NameToModification("add_T_Threonine"));
      Assert.AreEqual(SequestStaticModification.add_C_Cysteine, SequestParam.NameToModification("add_C_Cysteine"));
      Assert.AreEqual(SequestStaticModification.add_L_Leucine, SequestParam.NameToModification("add_L_Leucine"));
      Assert.AreEqual(SequestStaticModification.add_I_Isoleucine, SequestParam.NameToModification("add_I_Isoleucine"));
      Assert.AreEqual(SequestStaticModification.add_X_LorI, SequestParam.NameToModification("add_X_LorI"));
      Assert.AreEqual(SequestStaticModification.add_N_Asparagine, SequestParam.NameToModification("add_N_Asparagine"));
      Assert.AreEqual(SequestStaticModification.add_O_Ornithine, SequestParam.NameToModification("add_O_Ornithine"));
      Assert.AreEqual(SequestStaticModification.add_B_avg_NandD, SequestParam.NameToModification("add_B_avg_NandD"));
      Assert.AreEqual(SequestStaticModification.add_D_Aspartic_Acid,
                      SequestParam.NameToModification("add_D_Aspartic_Acid"));
      Assert.AreEqual(SequestStaticModification.add_Q_Glutamine, SequestParam.NameToModification("add_Q_Glutamine"));
      Assert.AreEqual(SequestStaticModification.add_K_Lysine, SequestParam.NameToModification("add_K_Lysine"));
      Assert.AreEqual(SequestStaticModification.add_Z_avg_QandE, SequestParam.NameToModification("add_Z_avg_QandE"));
      Assert.AreEqual(SequestStaticModification.add_E_Glutamic_Acid,
                      SequestParam.NameToModification("add_E_Glutamic_Acid"));
      Assert.AreEqual(SequestStaticModification.add_M_Methionine, SequestParam.NameToModification("add_M_Methionine"));
      Assert.AreEqual(SequestStaticModification.add_H_Histidine, SequestParam.NameToModification("add_H_Histidine"));
      Assert.AreEqual(SequestStaticModification.add_F_Phenylalanine,
                      SequestParam.NameToModification("add_F_Phenylalanine"));
      Assert.AreEqual(SequestStaticModification.add_R_Arginine, SequestParam.NameToModification("add_R_Arginine"));
      Assert.AreEqual(SequestStaticModification.add_Y_Tyrosine, SequestParam.NameToModification("add_Y_Tyrosine"));
      Assert.AreEqual(SequestStaticModification.add_W_Tryptophan, SequestParam.NameToModification("add_W_Tryptophan"));
    }

    [Test]
    [ExpectedException(typeof(System.ArgumentException))]
    public void TestNameToModificationException()
    {
      SequestParam.NameToModification("");
    }
  }
}