using NUnit.Framework;

namespace RCPA.Proteomics
{
  [TestFixture]
  public class TestAminoacids
  {
    [Test]
    public void TestGetCarbonPercentAminoacid()
    {
      Aminoacids aas = new Aminoacids();

      Aminoacid maxAA = aas.GetMaxCarbonPercentAminoacid();
      Assert.AreEqual("Phe", maxAA.ThreeName);

      Aminoacid minAA = aas.GetMinCarbonPercentAminoacid();
      Assert.AreEqual("Cys", minAA.ThreeName);
    }

    public void TestBuildInfo()
    {
      var aas = new Aminoacids();
      var addedMass1 = 20.3847;
      var addedMass2 = 40.3847;
      aas['*'].ResetMass(addedMass1, addedMass1);
      aas['#'].ResetMass(addedMass2, addedMass2);
      var s1 = aas.BuildInfo("R.A#D*K.L");
      Assert.AreEqual(3, s1.Count);
      Assert.AreEqual('A', s1[0].Aminoacid);
      Assert.AreEqual(aas['A'].MonoMass + addedMass2, s1[0].Mass, 0.0001);
      Assert.AreEqual(aas['D'].MonoMass + addedMass1, s1[1].Mass, 0.0001);
      Assert.AreEqual(aas['K'].MonoMass, s1[2].Mass, 0.0001);
    }
  }
}
