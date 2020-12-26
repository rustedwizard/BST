using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;
using System;
using System.Collections.Generic;

namespace BSTLibraryTest
{
    [TestClass]
    public class AVLTreeTest
    {
        private void CallTreeValidation(AVLNode<int> node)
        {
            Utility.TreeValidation(node);
        }

        //Verify the Height Property of each node AVLTree contains correct value.
        private int TreeHeightVerification(AVLNode<int> node)
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
        private void BalacningFactorVerification(AVLNode<int> node)
        {
            int bf;
            bool res;
            if(node == null)
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
                    BalacningFactorVerification(node.Left);
                    return;
                }
                else
                {
                    bf = 0 - node.Right.Height;
                    res = (bf > -2 && bf < 2);
                    Assert.IsTrue(res);
                    BalacningFactorVerification(node.Right);
                    return;
                }
            }
            bf = node.Left.Height - node.Right.Height;
            res = (bf > -2 && bf < 2);
            Assert.IsTrue(res);
            BalacningFactorVerification(node.Left);
            BalacningFactorVerification(node.Right);
            return;
        }

        [TestMethod]
        public void InsertionTest()
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20000];
            for (int i = 0; i < 20000; i++)
            {
                ints[i] = Rnd.Next(-20000, 20000);
            }
            int counter = 0;
            foreach (var e in ints)
            {
                var res = AVLTree.Insert(e);
                if (res)
                {
                    counter++;
                    CallTreeValidation(AVLTree.Root);
                    _ = TreeHeightVerification(AVLTree.Root);
                    BalacningFactorVerification(AVLTree.Root);
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in AVLTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidation(AVLTree.Root);
            _ = TreeHeightVerification(AVLTree.Root);
            BalacningFactorVerification(AVLTree.Root);
        }

        [TestMethod]
        public void DeletionTest()
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20000];
            for (int i = 0; i < 20000; i++)
            {
                ints[i] = Rnd.Next(-20000, 20000);
            }
            int counter = 0;
            foreach (var e in ints)
            {
                var res = AVLTree.Insert(e);
                if (res)
                {
                    counter++;
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in AVLTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidation(AVLTree.Root);
            _ = TreeHeightVerification(AVLTree.Root);
            BalacningFactorVerification(AVLTree.Root);
            //Now attempt to delete one node at a time
            //and verify tree validity after each deletion
            foreach (var e in ints)
            {
                AVLTree.Delete(e);
                if (AVLTree.Root != null)
                {
                    CallTreeValidation(AVLTree.Root);
                    _ = TreeHeightVerification(AVLTree.Root);
                    BalacningFactorVerification(AVLTree.Root);
                }
            }
        }

        [TestMethod]
        public void FindTest()
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20000];
            for (int i = 0; i < 20000; i++) 
            {
                ints[i] = Rnd.Next(-20000, 20000);
            }
            int counter = 0;
            foreach (var e in ints)
            {
                var res = AVLTree.Insert(e);
                if (res)
                {
                    counter++;
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in AVLTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            CallTreeValidation(AVLTree.Root);
            _ = TreeHeightVerification(AVLTree.Root);
            BalacningFactorVerification(AVLTree.Root);
            foreach (var e in ints)
            {
                var res = AVLTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            //Generate some random number outside the range of
            //the tree and try to find. Expected false return.
            for (int i = 0; i < 100; i++)
            {
                var res = AVLTree.TryFind(Rnd.Next(21000, 50000));
                Assert.IsFalse(res.Found);
            }
        }

        [TestMethod]
        public void StressTest()
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[9999999];
            for (int i = 0; i < 9999999; i++)
            {
                ints[i] = Rnd.Next(-2000000, 2000000);
            }
            foreach (var e in ints)
            {
                AVLTree.Insert(e);
            }
            foreach (var e in ints)
            {
                var res = AVLTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            foreach (var e in ints)
            {
                AVLTree.Delete(e);
            }
        }
    }
}
