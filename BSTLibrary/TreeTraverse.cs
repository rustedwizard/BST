﻿using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    internal class TreeTraverse<T> where T : IComparable
    {
        private static TreeTraverse<T> _tt;

        public static TreeTraverse<T> CreateTreeTraverse()
        {
            if (_tt != null)
            {
                return _tt;
            }
            _tt = new TreeTraverse<T>();
            return _tt;
        }
        private TreeTraverse() { }

        internal IEnumerable<T> InOrderTraversal(IBstNode<T> root)
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
                if (current.GetRight() != null)
                {
                    var data = current.Data;
                    current = current.GetRight();
                    yield return data;
                }
                else
                {
                    var data = current.Data;
                    current = null;
                    yield return data;
                }

            }
        }

        internal IEnumerable<T> PreOrderTraversal(IBstNode<T> root)
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

        internal IEnumerable<T> PostOrderTraversal(IBstNode<T> root)
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

        internal (bool Result, T Data) TryFind(T data, IBstNode<T> root)
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
