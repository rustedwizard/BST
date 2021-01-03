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
            var avlTree = new AvlTree<int>();
            for (int i = 1; i <= 10; i++)
            {
                avlTree.Insert(i);
            }
            var result = TreeTraverse<int>.InOrderTraversal(avlTree.Root).ToList();
            Assert.IsTrue(result.SequenceEqual(new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}));
        }

        [TestMethod]
        public void PreOrderTraverseTest()
        {
            var avlTree = new AvlTree<int>();
            for (var i = 0; i <= 10; i++)
            {
                avlTree.Insert(i);
            }
            var result = TreeTraverse<int>.PreOrderTraversal(avlTree.Root).ToList();
            Assert.IsTrue(result.SequenceEqual(new List<int>(){3, 1, 0, 2, 7, 5, 4, 6, 9, 8 ,10}));
        }

        [TestMethod]
        public void PostOrderTraverseTest()
        {
            var avlTree = new AvlTree<int>();
            for (var i = 0; i <= 10; i++)
            {
                avlTree.Insert(i);
            }
            var result = TreeTraverse<int>.PostOrderTraversal(avlTree.Root).ToList();
            Assert.IsTrue(result.SequenceEqual(new List<int>(){0, 2, 1, 4, 6, 5, 8, 10, 9, 7, 3}));
        }
    }
}