using NUnit.Framework;

namespace RCPA.Proteomics.Snp
{
  [TestFixture]
  public class TestMutationUtils
  {
    [Test]
    public void TestIsAminoacidEquals()
    {
      Assert.IsTrue(MutationUtils.IsAminoacidEquals('I', 'L'));

      Assert.IsTrue(MutationUtils.IsAminoacidEquals('L', 'I'));

      Assert.IsFalse(MutationUtils.IsAminoacidEquals('L', 'A'));
    }

    [Test]
    public void TestIsMutationOne()
    {
      //I -> L
      Assert.IsTrue(MutationUtils.IsMutationOne("ABCDEIR", "ABCDELR"));

      //no mutation
      Assert.IsFalse(MutationUtils.IsMutationOne("ABCDEIR", "ABCDEIR"));

      //two mutation
      Assert.IsFalse(MutationUtils.IsMutationOne("ABCDEIR", "ABCDELK"));

      int site = -1;
      MutationUtils.IsMutationOne("ABCDEIR", "ABCDELR", ref site);
      Assert.AreEqual(5, site);
    }

    [Test]
    public void TestIsMutationOneIL()
    {
      int site = 0;
      //C -> N
      Assert.IsTrue(MutationUtils.IsMutationOneIL("ABCDEIR", "ABNDEIR", ref site));
      Assert.AreEqual(2, site);

      //C -> N, I -> L
      Assert.IsTrue(MutationUtils.IsMutationOneIL("ABCDEIR", "ABNDELR", ref site));
      Assert.AreEqual(2, site);

      //I -> L
      Assert.IsFalse(MutationUtils.IsMutationOneIL("ABCDEIR", "ABCDELR", ref site));

      //no mutation
      Assert.IsFalse(MutationUtils.IsMutationOneIL("ABCDEIR", "ABCDEIR", ref site));

      //two mutation
      Assert.IsFalse(MutationUtils.IsMutationOneIL("ABCDEIR", "ABCDEDK", ref site));
    }


    [Test]
    public void TestIsMutationOneIgnoreNTerminal()
    {
      int site = -1;

      Assert.IsTrue(MutationUtils.IsMutationOne2("ABCDEIR", "BBCDEIR", ref site, false, false, false));

      Assert.IsFalse(MutationUtils.IsMutationOne2("ABCDEIR", "BBCDEIR", ref site, true, false, false));

      Assert.IsTrue(MutationUtils.IsMutationOneIL2("ABCDEIR", "BBCDELR", ref site, false, false, false));

      Assert.IsTrue(MutationUtils.IsMutationOneIL2("ABCDELR", "BBCDEIR", ref site, false, false, false));

      Assert.IsFalse(MutationUtils.IsMutationOneIL2("ABCDELR", "BBCDEIR", ref site, true, false, false));
    }

    [Test]
    public void TestIsMutationOneIgnoreDeamidated()
    {
      int site = -1;

      //N->D
      Assert.IsTrue(MutationUtils.IsMutationOne2("ABNDEIR", "ABDDEIR", ref site, false, false, false));

      //N->D
      Assert.IsFalse(MutationUtils.IsMutationOne2("ABNDEIR", "ABDDEIR", ref site, false, true, false));

      //Q->E
      Assert.IsTrue(MutationUtils.IsMutationOne2("ABQDEIR", "ABEDEIR", ref site, false, false, false));

      //Q->E
      Assert.IsFalse(MutationUtils.IsMutationOne2("ABQDEIR", "ABEDEIR", ref site, false, true, false));

      //N->D
      Assert.IsTrue(MutationUtils.IsMutationOneIL2("ABNDEIR", "ABDDELR", ref site, false, false, false));

      //N->D
      Assert.IsFalse(MutationUtils.IsMutationOneIL2("ABNDEIR", "ABDDELR", ref site, false, true, false));

      //Q->E
      Assert.IsTrue(MutationUtils.IsMutationOneIL2("ABQDEIR", "ABEDELR", ref site, false, false, false));

      //Q->E
      Assert.IsFalse(MutationUtils.IsMutationOneIL2("ABQDEIR", "ABEDELR", ref site, false, true, false));
    }

    [Test]
    public void TestReplaceLToI()
    {
      Assert.AreEqual("A.DKDKDFLEI*DR.C", MutationUtils.ReplaceLToI("A.DKDKDFLEL*DR.C", "DKIKDFLEIDR"));
    }
  }
}
