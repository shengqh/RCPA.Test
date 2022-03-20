using NUnit.Framework;
using System.Windows.Forms;

namespace RCPA.Gui
{
  [TestFixture]
  public class TestRcpaDoubleField
  {
    TextBox txtValue;
    RcpaDoubleField field;

    [SetUp]
    public void SetUp()
    {
      txtValue = new TextBox();
      field = new RcpaDoubleField(txtValue, "KEY1", "TEST", 0.01, true);
    }

    [Test]
    public void TestConstruction()
    {
      Assert.AreEqual("0.01", txtValue.Text);
      Assert.AreEqual(0.01, field.Value);
    }

    [Test]
    [ExpectedException(typeof(System.InvalidOperationException))]
    public void TestValidateComponent()
    {
      txtValue.Text = "AAA";
      field.ValidateComponent();
    }
  }
}
