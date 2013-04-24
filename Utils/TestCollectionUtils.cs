using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace RCPA.Utils
{
  [TestFixture]
  public class TestCollectionUtils
  {
    [Test]
    public void TestValueEquals()
    {
      Assert.IsTrue(CollectionUtils.ValueEquals<int>(null, null));

      List<int> l1 = new List<int>();
      l1.Add(1);
      l1.Add(2);

      Assert.IsFalse(CollectionUtils.ValueEquals<int>(null, l1));

      List<int> l2 = new List<int>();
      l2.Add(1);
      l2.Add(2);

      Assert.IsTrue(CollectionUtils.ValueEquals( l1, l2));

      l2.Add(3);
      Assert.IsFalse(CollectionUtils.ValueEquals(l1, l2));

      l1.Add(4);
      Assert.IsFalse(CollectionUtils.ValueEquals(l1, l2));
    }

    [Test]
    public void TestContainsAll()
    {
      Assert.IsFalse(CollectionUtils.ContainsAll<int>(null, null));

      List<int> l1 = new List<int>();
      l1.Add(1);

      Assert.IsFalse(CollectionUtils.ContainsAll<int>(l1, null));
      Assert.IsFalse(CollectionUtils.ContainsAll<int>(null, l1));

      List<int> l2 = new List<int>();
      l2.Add(1);
      l2.Add(2);

      Assert.IsFalse(CollectionUtils.ContainsAll(l1, l2));
      Assert.IsTrue(CollectionUtils.ContainsAll(l2, l1));

      l1.Add(2);
      Assert.IsTrue(CollectionUtils.ContainsAll(l1, l2));
      Assert.IsTrue(CollectionUtils.ContainsAll(l2, l1));

      l1.Add(3);
      Assert.IsTrue(CollectionUtils.ContainsAll(l1, l2));
      Assert.IsFalse(CollectionUtils.ContainsAll(l2, l1));

      l2.Add(4);
      Assert.IsFalse(CollectionUtils.ContainsAll(l1, l2));
      Assert.IsFalse(CollectionUtils.ContainsAll(l2, l1));
    }

    [Test]
    public void TestFindMaxIndex()
    {
      double[] values = { 1, 2, 3, 2.5 };
      List<double> vv = new List<double>(values);
      int actual = CollectionUtils.FindMaxIndex(vv);
      Assert.AreEqual(2, actual);
    }

    [Test]
    [ExpectedException("System.ArgumentNullException")]
    public void TestFindMaxIndexException1()
    {
      CollectionUtils.FindMaxIndex(null);
    }

    [Test]
    [ExpectedException("System.ArgumentException")]
    public void TestFindMaxIndexException()
    {
      CollectionUtils.FindMaxIndex(new List<double>());
    }
  }
}
