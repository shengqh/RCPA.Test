using NUnit.Framework;
using System;

namespace RCPA.Proteomics.Snp
{
  [TestFixture]
  public class TestSnpCode
  {
    [Test]
    public void TestIsRnaEditing()
    {
      var aas = new Aminoacids();
      var aasstr = aas.GetVisibleAminoacids();
      for (int i = 0; i < aasstr.Length; i++)
      {
        for (int j = 0; j < aasstr.Length; j++)
        {
          if (i == j)
          {
            continue;
          }

          string mutationstr;
          if (aas[aasstr[i]].IsRnaEditing(aas[aasstr[j]], out mutationstr))
          {
            Console.WriteLine(aasstr[i] + "->" + aasstr[j] + " : " + mutationstr);
          }
        }
      }
    }

    [Test]
    public void TestTransferTo()
    {
      var aas = new Aminoacids();

      int count;
      var actual = SnpCode.TransferTo(aas['I'], aas['L'], out count);
      Assert.AreEqual(1, count);
      Assert.AreEqual("AUU->CUU ! AUC->CUC ! AUA->UUA ! AUA->CUA", actual);

      actual = SnpCode.TransferTo(aas['I'], aas['.'], out count);
      Assert.AreEqual(int.MaxValue, count);
      Assert.AreEqual(string.Empty, actual);

      actual = SnpCode.TransferTo(aas['.'], aas['I'], out count);
      Assert.AreEqual(int.MaxValue, count);
      Assert.AreEqual(string.Empty, actual);
    }
  }
}
