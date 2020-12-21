﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RustedWizard.BSTLibrary;

namespace BSTLibraryTest
{
    [TestClass]
    public class BSTTest
    {
        [TestMethod]
        public void InsertionTest()
        {
            var BSTTree = new BST<int>();
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
                var res = BSTTree.Insert(e);
                if (res)
                {
                    counter++;
                    Utility.TreeValidation(BSTTree.Root, typeof(BSTNode<int>));
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in BSTTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            Utility.TreeValidation(BSTTree.Root, typeof(BSTNode<int>));
        }

        [TestMethod]
        public void DeletionTest()
        {
            var BSTTree = new BST<int>();
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
                var res = BSTTree.Insert(e);
                if (res)
                {
                    counter++;
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in BSTTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            Utility.TreeValidation(BSTTree.Root, typeof(BSTNode<int>));
            //Now attempt to delete one node at a time
            //and verify tree validity after each deletion
            foreach (var e in ints)
            {
                BSTTree.Delete(e);
                if (BSTTree.Root != null)
                {
                    Utility.TreeValidation(BSTTree.Root, typeof(BSTNode<int>));
                }
            }
        }

        [TestMethod]
        public void FindTest()
        {
            var BSTTree = new BST<int>();
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
                var res = BSTTree.Insert(e);
                if (res)
                {
                    counter++;
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in BSTTree.InOrderTraverse())
            {
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            Utility.TreeValidation(BSTTree.Root, typeof(BSTNode<int>));
            foreach (var e in ints)
            {
                var res = BSTTree.TryFind(e);
                Assert.IsTrue(res.Found);
                Assert.AreEqual(res.Data, e);
            }
            //Generate some random number outside the range of
            //the tree and try to find. Expected false return.
            for(int i=0; i<100; i++)
            {
                var res = BSTTree.TryFind(Rnd.Next(21000, 50000));
                Assert.IsFalse(res.Found);
            }
        }
    }
}
