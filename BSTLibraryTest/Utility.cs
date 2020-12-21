using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;
using System;
using System.Collections.Generic;

namespace BSTLibraryTest
{
    class Utility
    {
        public static void TreeValidation(IBSTNode<int> n, Type type)
        {
            dynamic node = Convert.ChangeType(n, type);
            var t = typeof(Stack<>).MakeGenericType(type);
            dynamic stack = Activator.CreateInstance(t);
            stack.Push(node);
            while(stack.Count > 0)
            {
                dynamic current = stack.Pop();
                bool res;
                if (current.IsLeafNode())
                {
                    continue;
                }
                if (current.HasOneChild())
                {
                    if (current.Left != null)
                    {
                        res = current.Left.Data < current.Data;
                        Assert.IsTrue(res);
                        stack.Push(current.Left);
                        continue;
                    }
                    else
                    {
                        res = current.Right.Data > current.Data;
                        Assert.IsTrue(res);
                        stack.Push(current.Right);
                        continue;
                    }
                }
                res = current.Right.Data > current.Data && current.Left.Data < current.Data;
                Assert.IsTrue(res);
                stack.Push(current.Left);
                stack.Push(current.Right);
                continue;
            }
        }
    }
}
