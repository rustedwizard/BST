using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;
using System;
using System.Collections.Generic;

namespace BSTLibraryTest
{
    [TestClass]
    public class AVLTreeTest
    {
        //Verify the Height Property of each node AVLTree contains correct value.
        private int TreeHeightVerification(AVLNode<int> node)
        {
            int h;
            if (node.IsLeafNode())
            {
                Console.WriteLine($"Node: {node.Data}, Height: {node.Height}, Expected Height: 1");
                Assert.AreEqual(node.Height, 1);
                return 1;
            }
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    h = TreeHeightVerification(node.Left) + 1;
                    Console.WriteLine($"Node: {node.Data}, Height: {node.Height}, Expected Height: {h}");
                }
                else
                {
                    h = TreeHeightVerification(node.Right) + 1;
                    Console.WriteLine($"Node: {node.Data}, Height: {node.Height}, Expected Height: {h}");
                }
                Assert.AreEqual(node.Height, h);
                return h;
            }
            h = Math.Max(TreeHeightVerification(node.Left), TreeHeightVerification(node.Right)) + 1;
            Console.WriteLine($"Node: {node.Data}, Height: {node.Height}, Expected Height: {h}");
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
            string output;
            if(node == null)
            {
                return;
            }
            if (node.IsLeafNode())
            {
                Console.WriteLine($"Node: {node.Data}, Balancing Factor: 0, With the Range: Yes (Leaf node!) ");
                return;
            }
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    bf = node.Left.Height - 0;
                    res = (bf > -2 && bf < 2);
                    output = (res ? "Yes" : "No");
                    Console.WriteLine($"Node: {node.Data}, Balancing Factor: {bf}, With the Range: {output} ");
                    Assert.IsTrue(res);
                    BalacningFactorVerification(node.Left);
                    return;
                }
                else
                {
                    bf = 0 - node.Right.Height;
                    res = (bf > -2 && bf < 2);
                    output = (res ? "Yes" : "No");
                    Console.WriteLine($"Node: {node.Data}, Balancing Factor: {bf}, With the Range: {output} ");
                    Assert.IsTrue(res);
                    BalacningFactorVerification(node.Right);
                    return;
                }
            }
            bf = node.Left.Height - node.Right.Height;
            res = (bf > -2 && bf < 2);
            output = (res ? "Yes" : "No");
            Console.WriteLine($"Node: {node.Data}, Balancing Factor: {bf}, With the Range: {output} ");
            Assert.IsTrue(res);
            BalacningFactorVerification(node.Left);
            BalacningFactorVerification(node.Right);
            return;
        }

        [TestMethod]
        public void SimpleInsertionTest()
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20];
            for (int i = 0; i < 20; i++)
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
                    Console.WriteLine($"{counter} Elements Inserted");
                    _ = TreeHeightVerification(AVLTree.Root);
                    BalacningFactorVerification(AVLTree.Root);
                }
            }
            var inOrderList = new List<int>();
            foreach (var e in AVLTree.InOrderTraverse())
            {
                Console.Write($"{e}, ");
                Console.WriteLine();
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            _ = TreeHeightVerification(AVLTree.Root);
            BalacningFactorVerification(AVLTree.Root);
        }

        [TestMethod]
        public void SimpleDeletionTest()
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[20];
            for (int i = 0; i < 20; i++)
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
                Console.Write($"{e}, ");
                Console.WriteLine();
                inOrderList.Add(e);
            }
            Assert.AreEqual(inOrderList.Count, counter);
            _ = TreeHeightVerification(AVLTree.Root);
            BalacningFactorVerification(AVLTree.Root);
            //Now attempt to delete one node at a time
            //and verify tree validity after each deletion
            Console.WriteLine("Following are deletion test");
            counter = 1;
            foreach(var e in ints)
            {
                AVLTree.Delete(e);
                _ = TreeHeightVerification(AVLTree.Root);
                BalacningFactorVerification(AVLTree.Root);
                Console.WriteLine($"{counter} Element Deleted");
                counter++;
            }
        }
    }
}
