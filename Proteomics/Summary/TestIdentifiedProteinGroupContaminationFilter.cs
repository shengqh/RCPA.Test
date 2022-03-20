using NUnit.Framework;

namespace RCPA.Proteomics.Summary
{
  [TestFixture]
  public class TestIdentifiedProteinGroupContaminationFilter
  {
    [Test]
    public void Run()
    {
      IdentifiedProteinGroupContaminationDescriptionFilter filter = new IdentifiedProteinGroupContaminationDescriptionFilter("KERATIN");
      IdentifiedProteinGroup group = new IdentifiedProteinGroup();
      group.Add(new IdentifiedProtein("P1")
      {
        Description = "P1 Keratin"
      });

      Assert.IsTrue(filter.Accept(group));
    }
  }
}
