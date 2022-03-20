using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestProtease
  {
    [Test]
    public void TestIsCleavageSite()
    {
      Protease trypsin = new Protease("Trypsin", true, "KR", "P");
      Assert.AreEqual(true, trypsin.IsCleavageSite('K', 'A', '-'));
      Assert.AreEqual(true, trypsin.IsCleavageSite('R', 'A', '-'));
      Assert.AreEqual(true, trypsin.IsCleavageSite('K', '-', '-'));
      Assert.AreEqual(true, trypsin.IsCleavageSite('R', '-', '-'));
      Assert.AreEqual(false, trypsin.IsCleavageSite('K', 'P', '-'));
      Assert.AreEqual(false, trypsin.IsCleavageSite('R', 'P', '-'));
      Assert.AreEqual(false, trypsin.IsCleavageSite('N', 'A', '-'));
    }

    [Test]
    public void TestGetMissCleavageSiteCount()
    {
      Protease trypsin = new Protease("Trypsin", true, "KR", "P");

      Assert.AreEqual(0, trypsin.GetMissCleavageSiteCount("EGEABDR"));
      Assert.AreEqual(0, trypsin.GetMissCleavageSiteCount("EGEKPABDR"));
      Assert.AreEqual(0, trypsin.GetMissCleavageSiteCount("EGERPABDR"));
      Assert.AreEqual(1, trypsin.GetMissCleavageSiteCount("EGEKABDR"));
      Assert.AreEqual(1, trypsin.GetMissCleavageSiteCount("EGERABDR"));
      Assert.AreEqual(2, trypsin.GetMissCleavageSiteCount("EGEKARBDR"));

      Protease noenzyme = new Protease("Noenzyme", true, "", "");
      Assert.AreEqual(0, noenzyme.GetMissCleavageSiteCount("EGEABDR"));
    }

    [Test]
    public void TestGetNumProteaseTermini()
    {
      Protease trypsin = new Protease("Trypsin", true, "KR", "P");
      Assert.AreEqual(0, trypsin.GetNumProteaseTermini('M', "ABCDE", 'N', '-', 3));
      Assert.AreEqual(1, trypsin.GetNumProteaseTermini('M', "ABCDE", 'N', '-', 2));
      Assert.AreEqual(2, trypsin.GetNumProteaseTermini('M', "ABCDR", 'N', '-', 2));
    }
  }
}
