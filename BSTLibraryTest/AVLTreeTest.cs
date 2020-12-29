using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;
using System;
using System.Linq;

namespace BSTLibraryTest
{
    [TestClass]
    public class AvlTreeTest
    {
        private static void CallTreeValidation(AvlNode<int> node)
        {
            Utility.TreeValidation(node);
        }

        //Verify the Height Property of each node AVLTree contains correct value.
        private static int TreeHeightVerification(AvlNode<int> node)
        {
            int h;
            if (node.IsLeafNode())
            {
                Assert.AreEqual(node.Height, 1);
                return 1;
            }
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    h = TreeHeightVerification(node.Left) + 1;
                }
                else
                {
                    h = TreeHeightVerification(node.Right) + 1;
                }
                Assert.AreEqual(node.Height, h);
                return h;
            }
            h = Math.Max(TreeHeightVerification(node.Left), TreeHeightVerification(node.Right)) + 1;
            Assert.AreEqual(node.Height, h);
            return h;
        }

        //Verify that every subtree at given node has balancing factor with range of -2(exclusive) to 2(exclusive)
        //This test relies on the Height property of AVL Tree, so it shall only be performed after the Height property 
        //has been verified by TreeHeightVerification method to ensure the correctness of this test.
        private static void BalancingFactorVerification(AvlNode<int> node)
        {
            int bf;
            bool res;
            if (node == null)
            {
                return;
            }
            if (node.IsLeafNode())
            {
                return;
            }
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    bf = node.Left.Height - 0;
                    res = (bf > -2 && bf < 2);
                    Assert.IsTrue(res);
                    BalancingFactorVerification(node.Left);
                    return;
                }
                bf = 0 - node.Right.Height;
                res = (bf > -2 && bf < 2);
                Assert.IsTrue(res);
                BalancingFactorVerification(node.Right);
                return;
            }
            bf = node.Left.Height - node.Right.Height;
            res = (bf > -2 && bf < 2);
            Assert.IsTrue(res);
            BalancingFactorVerification(node.Left);
            BalancingFactorVerification(node.Right);
        }

        [TestMethod]
        public void InsertionTest()
        {
            var avlTree = new AvlTree<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var testData = new int[20000];
            for (var i = 0; i < 20000; i++)
            {
                testData[i] = rnd.Next(-20000, 20000);
            }
            var counter = 0;
            foreach (var e in testData)
            {
                var res = avlTree.Insert(e);
                if (res)
                {
                    counter++;
                    CallTreeValidation(avlTree.Root);
                    _ = TreeHeightVerification(avlTree.Root);
                    BalancingFactorVerification(avlTree.Root);
                    Assert.AreEqual(counter, avlTree.TreeSize);
                }
            }
            var inOrderList = avlTree.InOrderTraverse().ToList();
            Assert.AreEqual(inOrderList.Count, counter);
            Assert.AreEqual(counter, avlTree.TreeSize);
            CallTreeValidation(avlTree.Root);
            _ = TreeHeightVerification(avlTree.Root);
            BalancingFactorVerification(avlTree.Root);
            //Test clear the tree here
            avlTree.ClearTheTree();
            Assert.AreEqual(0, avlTree.TreeSize);
            Assert.IsNull(avlTree.Root);
        }

        [TestMethod]
        public void DeletionTest()
        {
            var avlTree = new AvlTree<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var testData = new int[20000];
            for (var i = 0; i < 20000; i++)
            {
                testData[i] = rnd.Next(-20000, 20000);
            }
            var counter = testData.Select(e => avlTree.Insert(e)).Count(res => res);
            var inOrderList = avlTree.InOrderTraverse().ToList();
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidation(avlTree.Root);
            _ = TreeHeightVerification(avlTree.Root);
            BalancingFactorVerification(avlTree.Root);
            //Now attempt to delete one node at a time
            //and verify tree validity after each deletion
            foreach (var e in testData)
            {
                var res =avlTree.Delete(e);
                if (res)
                {
                    counter--;
                    Assert.AreEqual(counter, avlTree.TreeSize);
                }
                if (avlTree.Root == null) continue;
                CallTreeValidation(avlTree.Root);
                _ = TreeHeightVerification(avlTree.Root);
                BalancingFactorVerification(avlTree.Root);
            }
            Assert.IsNull(avlTree.Root);
            Assert.AreEqual(0, avlTree.TreeSize);
        }


        [TestMethod]
        public void FindTest()
        {
            var avlTree = new AvlTree<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var testData = new int[20000];
            for (int i = 0; i < 20000; i++)
            {
                testData[i] = rnd.Next(-20000, 20000);
            }
            var counter = testData.Select(e => avlTree.Insert(e)).Count(res => res);
            var inOrderList = avlTree.InOrderTraverse().ToList();
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidation(avlTree.Root);
            _ = TreeHeightVerification(avlTree.Root);
            BalancingFactorVerification(avlTree.Root);
            foreach (var e in testData)
            {
                var res = avlTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            //Generate some random number outside the range of
            //the tree and try to find. Expected false return.
            for (int i = 0; i < 100; i++)
            {
                var res = avlTree.TryFind(rnd.Next(21000, 50000));
                Assert.IsFalse(res.Found);
            }
        }

        [TestMethod]
        public void StressTest()
        {
            var avlTree = new AvlTree<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var testData = new int[9999999];
            for (var i = 0; i < 9999999; i++)
            {
                testData[i] = rnd.Next(-2000000, 2000000);
            }
            foreach (var e in testData)
            {
                avlTree.Insert(e);
            }
            foreach (var e in testData)
            {
                var res = avlTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            foreach (var e in testData)
            {
                avlTree.Delete(e);
            }
        }
    }
}
