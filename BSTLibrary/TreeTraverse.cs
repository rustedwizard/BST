using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    internal static class TreeTraverse<T> where T : IComparable
    {

        internal static IEnumerable<T> InOrderTraversal(IBstNode<T> root)
        {
            if (root == null)
            {
                yield break;
            }
            var stack = new Stack<IBstNode<T>>();
            var current = root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.GetLeft();
                }
                current = stack.Pop();
                var data = current.Data;
                current = current.GetRight();
                yield return data;
            }
        }

        internal static IEnumerable<T> PreOrderTraversal(IBstNode<T> root)
        {
            if (root == null)
            {
                yield break;
            }
            var stack = new Stack<IBstNode<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.GetRight() != null)
                {
                    stack.Push(current.GetRight());
                }
                if (current.GetLeft() != null)
                {
                    stack.Push(current.GetLeft());
                }
                yield return current.Data;
            }
        }

        internal static IEnumerable<T> PostOrderTraversal(IBstNode<T> root)
        {
            if (root == null)
            {
                yield break;
            }
            var stack = new Stack<IBstNode<T>>();
            var res = new Stack<IBstNode<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                res.Push(current);
                if (current.GetLeft() != null)
                {
                    stack.Push(current.GetLeft());
                }
                if (current.GetRight() != null)
                {
                    stack.Push(current.GetRight());
                }
            }
            while (res.Count > 0)
            {
                yield return res.Pop().Data;
            }
        }

        internal static (bool Result, T Data) TryFind(T data, IBstNode<T> root)
        {
            if (root == null)
            {
                return (false, default(T));
            }
            var current = root;
            while (true)
            {
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.GetLeft() == null)
                    {
                        return (false, default(T));
                    }
                    current = current.GetLeft();
                    continue;
                }
                if (data.CompareTo(current.Data) == 0)
                {
                    return (true, current.Data);
                }
                if (current.GetRight() == null)
                {
                    return (false, default(T));
                }
                current = current.GetRight();
            }
        }
    }
}
