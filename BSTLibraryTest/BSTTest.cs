using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;

namespace BSTLibraryTest
{
    [TestClass]
    public class BstTest
    {
        private static void ValidateTheTree(BstNode<int> node)
        {
            Utility.TreeValidation(node);
        }

        [TestMethod]
        public void InsertionTest()
        {
            Console.WriteLine("BSTTree Insertion Test is running...");
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var ints = new int[20000];
            for (var i = 0; i < 20000; i++) ints[i] = rnd.Next(-20000, 20000);
            var counter = 0;
            foreach (var e in ints)
            {
                var res = bstTree.Insert(e);
                if (res)
                {
                    counter++;
                    Assert.AreEqual(counter, bstTree.TreeSize);
                    Assert.AreEqual(bstTree.InOrderTraverse().Count(), bstTree.TreeSize);
                    ValidateTheTree(bstTree.Root);
                }
            }

            Assert.AreEqual(counter, bstTree.TreeSize);
            var inOrderList = new List<int>();
            foreach (var e in bstTree.InOrderTraverse()) inOrderList.Add(e);
            Assert.AreEqual(inOrderList.Count, counter);
            ValidateTheTree(bstTree.Root);
            bstTree.ClearTheTree();
            Assert.IsNull(bstTree.Root);
            Console.WriteLine("BSTTree Insertion Test has finished running...");
        }

        [TestMethod]
        public void DeletionTest()
        {
            Console.WriteLine("BSTTree Deletion Test is running...");
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var ints = new int[20000];
            for (var i = 0; i < 20000; i++) ints[i] = rnd.Next(-20000, 20000);
            var counter = 0;
            foreach (var e in ints)
            {
                var res = bstTree.Insert(e);
                if (res) counter++;
            }

            var inOrderList = new List<int>();
            foreach (var e in bstTree.InOrderTraverse()) inOrderList.Add(e);
            Assert.AreEqual(inOrderList.Count, counter);
            ValidateTheTree(bstTree.Root);
            //Now attempt to delete one node at a time
            //and verify tree validity after each deletion
            foreach (var e in ints)
            {
                var res = bstTree.Delete(e);
                if (res)
                {
                    counter--;
                    Assert.AreEqual(counter, bstTree.TreeSize);
                    Assert.AreEqual(bstTree.InOrderTraverse().Count(), bstTree.TreeSize);
                    Assert.IsFalse(bstTree.TryFind(e).Found);
                }

                if (bstTree.Root != null) ValidateTheTree(bstTree.Root);
            }

            Assert.IsNull(bstTree.Root);
            Assert.AreEqual(0, bstTree.TreeSize);
            Console.WriteLine("BSTTree Deletion Test has finished running...");
        }

        [TestMethod]
        public void FindTest()
        {
            Console.WriteLine("BSTTree Finding Test is running...");
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var ints = new int[20000];
            for (var i = 0; i < 20000; i++) ints[i] = rnd.Next(-20000, 20000);
            var counter = 0;
            foreach (var e in ints)
            {
                var res = bstTree.Insert(e);
                if (res) counter++;
            }

            var inOrderList = new List<int>();
            foreach (var e in bstTree.InOrderTraverse()) inOrderList.Add(e);
            Assert.AreEqual(inOrderList.Count, counter);
            ValidateTheTree(bstTree.Root);
            foreach (var e in ints)
            {
                var res = bstTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }

            //Generate some random number outside the range of
            //the tree and try to find. Expected false return.
            for (var i = 0; i < 100; i++)
            {
                var res = bstTree.TryFind(rnd.Next(21000, 50000));
                Assert.IsFalse(res.Found);
            }

            Console.WriteLine("BSTTree Finding Test has finished running...");
        }

        [TestMethod]
        public void StressTest()
        {
            Console.WriteLine("BSTTree Stress Test is running...");
            var bstTree = new Bst<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var ints = new int[9999999];
            for (var i = 0; i < 9999999; i++) ints[i] = rnd.Next(-2000000, 2000000);
            foreach (var e in ints) bstTree.Insert(e);
            foreach (var e in ints)
            {
                var res = bstTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }

            foreach (var e in ints) bstTree.Delete(e);
            Console.WriteLine("BSTTree Stress Test has finished running...");
        }
    }
}