
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunkyNodeIds;
using FunkyNodeIds.Bitmap;
using System.Linq;

namespace FunkyNodeIdsTest
{
    [TestClass]
    public class FunkyNodeIdsTests
    {

        [TestMethod]
        public void TestParse()
        {
            Node myNode = Node.Parse("foo/123");
            Assert.AreEqual("foo",myNode.Name);
            Assert.AreEqual(123, myNode.Number);
        }

        [TestMethod]
        public void TestSet()
        {
            MySet myset = new MySet(SimpleBitmap.BitmapOf);
            var node = new Node() { Name = "Name", Number = 1 };
            myset.Add(node);
            Assert.IsTrue(myset.Contains(node));
            Assert.IsFalse(myset.Contains(new Node() { Name = "Name", Number = 2 }));
            Assert.IsFalse(myset.Contains(new Node() { Name = "Name_", Number = 1 }));
        }

        [TestMethod]
        public void TestSetMerge()
        {
            MySet myset1 = new MySet(SimpleBitmap.BitmapOf);
            myset1.Add(new Node() { Name = "a", Number = 1 });
            myset1.Add(new Node() { Name = "a", Number = 3 });

            MySet myset2 = new MySet(SimpleBitmap.BitmapOf);
            myset2.Add(new Node() { Name = "a", Number = 2 });
            myset2.Add(new Node() { Name = "b", Number = 1 });


            MySet myset3 = MySet.Merge(myset1, myset2, SimpleBitmap.BitmapOf);
            Assert.IsTrue(myset3.Contains(new Node() { Name = "a", Number = 1 }));
            Assert.IsTrue(myset3.Contains(new Node() { Name = "a", Number = 2 }));
            Assert.IsTrue(myset3.Contains(new Node() { Name = "a", Number = 3 }));
            Assert.IsTrue(myset3.Contains(new Node() { Name = "b", Number = 1 }));
        }
    }
}
