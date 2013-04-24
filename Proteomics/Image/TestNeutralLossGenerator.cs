using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RCPA.Proteomics.Image
{
  [TestFixture]
  public class TestNeutralLossGenerator
  {
    void AssertContain(List<INeutralLossType> types, string name)
    {
      Assert.IsTrue(types.Any(m => m.Name.Equals(name)));
    }

    [Test]
    public void TestAddToDistinctList()
    {
      List<INeutralLossType> nlTypes = new List<INeutralLossType>();
      nlTypes.Add(new CombinedNeutralLossType(new[] { NeutralLossConstants.NL_HPO3, NeutralLossConstants.NL_WATER }));

      new NeutralLossGenerator().AddToDistinctList(nlTypes, NeutralLossConstants.NL_H3PO4);

      Assert.AreEqual(1, nlTypes.Count);
      Assert.AreSame(NeutralLossConstants.NL_H3PO4, nlTypes[0]);
    }

    [Test]
    public void TestGetCombinationValues()
    {
      List<INeutralLossType> types = new List<INeutralLossType>();
      types.Add(new CombinedNeutralLossType(new[] { NeutralLossConstants.NL_H3PO4, NeutralLossConstants.NL_HPO3 }));
      types.Add(NeutralLossConstants.NL_H3PO4);
      types.Add(NeutralLossConstants.NL_H3PO4);
      types.Add(NeutralLossConstants.NL_HPO3);
      types.Add(NeutralLossConstants.NL_WATER);
      types.Add(NeutralLossConstants.NL_AMMONIA);

      NeutralLossGenerator generator = new NeutralLossGenerator();

      List<INeutralLossType> actual = generator.GetCombinationValues(types, 1);
      Assert.AreEqual(5, actual.Count);
      AssertContain(actual, "H3PO4-HPO3");
      AssertContain(actual, "H3PO4");
      AssertContain(actual, "HPO3");
      AssertContain(actual, "NH3");
      AssertContain(actual, "H2O");

      actual = generator.GetCombinationValues(types, 2);
      Assert.AreEqual(10, actual.Count);
      AssertContain(actual, "H3PO4-HPO3-H3PO4");
      AssertContain(actual, "H3PO4-HPO3-HPO3");
      AssertContain(actual, "H3PO4-HPO3-NH3");
      AssertContain(actual, "H3PO4-H3PO4");
      AssertContain(actual, "H3PO4-HPO3");
      AssertContain(actual, "H3PO4-H2O");
      AssertContain(actual, "H3PO4-NH3");
      AssertContain(actual, "HPO3-H2O");
      AssertContain(actual, "HPO3-NH3");
      AssertContain(actual, "H2O-NH3");

      actual = generator.GetTotalCombinationValues(types, 2);
      //actual.ForEach(m => Console.WriteLine(m.Name));
      Assert.AreEqual(13, actual.Count);

      //两个冗余删除
      AssertContain(actual, "H3PO4-HPO3");
      AssertContain(actual, "H3PO4");
      AssertContain(actual, "HPO3");
      AssertContain(actual, "NH3");
      AssertContain(actual, "H2O");
      AssertContain(actual, "H3PO4-HPO3-H3PO4");
      AssertContain(actual, "H3PO4-HPO3-HPO3");
      AssertContain(actual, "H3PO4-HPO3-NH3");
      AssertContain(actual, "H3PO4-H3PO4");
      AssertContain(actual, "H3PO4-H2O");
      AssertContain(actual, "H3PO4-NH3");
      AssertContain(actual, "HPO3-NH3");
      AssertContain(actual, "H2O-NH3");
    }
  }
}
