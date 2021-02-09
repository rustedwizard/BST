using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;

namespace BSTLibraryTest
{
    [TestClass]
    public class TreeTraverseTest
    {
        [TestMethod]
        public void InOrderTraverseTest()
        {
            Console.WriteLine("In-Order Traversing Test is running...");
            var avlTree = new AvlTree<int>();
            for (var i = 1; i <= 10; i++) avlTree.Insert(i);
            var result = avlTree.InOrderTraverse().ToList();
            Assert.IsTrue(result.SequenceEqual(new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}));
            Console.WriteLine("In-Order Traversing Test has finished running...");
        }

        [TestMethod]
        public void PreOrderTraverseTest()
        {
            Console.WriteLine("Pre-Order Traversing Test is running...");
            var avlTree = new AvlTree<int>();
            for (var i = 0; i <= 10; i++) avlTree.Insert(i);
            var result = avlTree.PreOrderTraverse().ToList();
            Assert.IsTrue(result.SequenceEqual(new List<int> {3, 1, 0, 2, 7, 5, 4, 6, 9, 8, 10}));
            Console.WriteLine("Pre-Order Traversing Test is running");
        }

        [TestMethod]
        public void PostOrderTraverseTest()
        {
            Console.WriteLine("Post-Order Traversing Test is running...");
            var avlTree = new AvlTree<int>();
            for (var i = 0; i <= 10; i++) avlTree.Insert(i);
            var result = avlTree.PostOrderTraverse().ToList();
            Assert.IsTrue(result.SequenceEqual(new List<int> {0, 2, 1, 4, 6, 5, 8, 10, 9, 7, 3}));
            Console.WriteLine("Post-Order Traversing Test has finished running...");
        }
    }
}