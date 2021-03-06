﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;

namespace BSTLibraryTest
{
    internal class Utility
    {
        public static void TreeValidation(IBstNode<int> n)
        {
            var stack = new Stack<IBstNode<int>>();
            stack.Push(n);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                bool res;
                if (current.IsLeafNode()) continue;
                if (current.HasOneChild())
                {
                    if (current.GetLeft() != null)
                    {
                        res = current.GetLeft().Data < current.Data;
                        Assert.IsTrue(res);
                        stack.Push(current.GetLeft());
                        continue;
                    }

                    res = current.GetRight().Data > current.Data;
                    Assert.IsTrue(res);
                    stack.Push(current.GetRight());
                    continue;
                }

                res = current.GetRight().Data > current.Data && current.GetLeft().Data < current.Data;
                Assert.IsTrue(res);
                stack.Push(current.GetLeft());
                stack.Push(current.GetRight());
            }
        }
    }
}