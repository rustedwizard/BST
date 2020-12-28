using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;
using System;
using System.Collections.Generic;

namespace BSTLibraryTest
{
    [TestClass]
    public class BstTest
    {
        private void CallTreeValidataion(BstNode<int> node)
        {
            Utility.TreeValidation(node);
        }

        [TestMethod]
        public void InsertionTest()
        {
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20000];
            for (int i = 0; i < 20000; i++)
            {
                ints[i] = rnd.Next(-20000, 20000);
            }
            int counter = 0;
            foreach (var e in ints)
            {
                var res = bstTree.Insert(e);
                if (res)
                {
                    counter++;
                    CallTreeValidataion(bstTree.Root);
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in bstTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidataion(bstTree.Root);
        }

        [TestMethod]
        public void DeletionTest()
        {
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20000];
            for (int i = 0; i < 20000; i++)
            {
                ints[i] = rnd.Next(-20000, 20000);
            }
            int counter = 0;
            foreach (var e in ints)
            {
                var res = bstTree.Insert(e);
                if (res)
                {
                    counter++;
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in bstTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidataion(bstTree.Root);
            //Now attempt to delete one node at a time
            //and verify tree validity after each deletion
            foreach (var e in ints)
            {
                bstTree.Delete(e);
                if (bstTree.Root != null)
                {
                    CallTreeValidataion(bstTree.Root);
                }
            }
        }

        [TestMethod]
        public void FindTest()
        {
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20000];
            for (int i = 0; i < 20000; i++)
            {
                ints[i] = rnd.Next(-20000, 20000);
            }
            int counter = 0;
            foreach (var e in ints)
            {
                var res = bstTree.Insert(e);
                if (res)
                {
                    counter++;
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in bstTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidataion(bstTree.Root);
            foreach (var e in ints)
            {
                var res = bstTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            //Generate some random number outside the range of
            //the tree and try to find. Expected false return.
            for(int i=0; i<100; i++)
            {
                var res = bstTree.TryFind(rnd.Next(21000, 50000));
                Assert.IsFalse(res.Found);
            }
        }

        [TestMethod]
        public void StressTest()
        {
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[9999999];
            for (int i = 0; i < 9999999; i++)
            {
                ints[i] = rnd.Next(-2000000, 2000000);
            }
            foreach (var e in ints)
            {
                bstTree.Insert(e);
            }
            foreach (var e in ints)
            {
                var res = bstTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            foreach (var e in ints)
            {
                bstTree.Delete(e);
            }
        }
    }
}
