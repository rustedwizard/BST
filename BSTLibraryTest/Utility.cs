using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;

namespace BSTLibraryTest
{
    class Utility
    {
        public static void TreeValidation(AVLNode<int> node)
        {
            if (node.IsLeafNode())
            {
                return;
            }
            bool res;
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    res = node.Left.Data < node.Data;
                    Assert.IsTrue(res);
                    TreeValidation(node.Left);
                    return;
                }
                else
                {
                    res = node.Right.Data > node.Data;
                    Assert.IsTrue(res);
                    TreeValidation(node.Right);
                    return;
                }
            }
            res = node.Right.Data > node.Data && node.Left.Data < node.Data;
            Assert.IsTrue(res);
            TreeValidation(node.Left);
            TreeValidation(node.Right);
        }

        public static void TreeValidation(BSTNode<int> node)
        {
            if (node.IsLeafNode())
            {
                return;
            }
            bool res;
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    res = node.Left.Data < node.Data;
                    Assert.IsTrue(res);
                    TreeValidation(node.Left);
                    return;
                }
                else
                {
                    res = node.Right.Data > node.Data;
                    Assert.IsTrue(res);
                    TreeValidation(node.Right);
                    return;
                }
            }
            res = node.Right.Data > node.Data && node.Left.Data < node.Data;
            Assert.IsTrue(res);
            TreeValidation(node.Left);
            TreeValidation(node.Right);
        }
    }
}
