using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Snp
{
  [TestFixture]
  public class TestSnpCode
  {
    public void TestIsRnaEditing()
    {
      var aas = new Aminoacids();
      var aasstr = aas.GetVisibleAminoacids();
      for (int i = 0; i < aasstr.Length; i++)
      {
        for (int j = 0; j < aasstr.Length; j++)
        {
          if(i == j)
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
  }
}
