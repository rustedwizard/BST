﻿using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     abstract class: Represent a Binary search tree
    ///     This is an abstract class (can never be instantiated) and it meant to be inherited
    /// </summary>
    /// <typeparam name="T"> the type of data this Binary tree should contain </typeparam>
    public abstract class AbBst<T> : IBst<T> where T : IComparable
    {
        /// <summary>
        ///     abstract method: Insert data into this binary tree
        /// </summary>
        /// <param name="data"> the actual data needed to insert into this binary tree</param>
        /// <returns> return a boolean value indicate if the insertion is successful </returns>
        public abstract bool Insert(T data);

        /// <summary>
        ///     abstract method: Delete data from this binary tree
        /// </summary>
        /// <param name="data"> the actual data needed to be removed from this binary tree</param>
        /// <returns> return a boolean value indicate if the deletion is successful </returns>
        public abstract bool Delete(T data);

        /// <summary>
        ///     Searching function of Binary Tree
        ///     Attempt to find supplied data in the tree.
        ///     If found return result as true along with the actual data
        ///     If nothing is found return result as false along with null value.
        /// </summary>
        /// <param name="data">The data intended to be searched</param>
        /// <returns>
        ///     Returns a named tuple contains two item Found and Data.
        ///     Found: bool value indicate if supplied Data is found in the tree
        ///     Data: the actual data found in the tree.
        /// </returns>
        public virtual (bool Found, T Data) TryFind(T data)
        {
            var root = GetRoot();
            if (root == null) return (false, default);
            var current = root;
            while (true)
            {
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.GetLeft() == null) return (false, default);
                    current = current.GetLeft();
                    continue;
                }

                if (data.CompareTo(current.Data) == 0) return (true, current.Data);
                if (current.GetRight() == null) return (false, default);
                current = current.GetRight();
            }
        }

        /// <summary>
        ///     In order traversal of AVL Tree
        ///     Standard In order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in In-Order sequence.</returns>
        public virtual IEnumerable<T> InOrderTraverse()
        {
            var root = GetRoot();
            if (root == null) yield break;
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

        /// <summary>
        ///     Pre-Order Traversal Function of AVL Tree
        ///     Standard Pre-order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in Pre-Order sequence.</returns>
        public virtual IEnumerable<T> PreOrderTraverse()
        {
            var root = GetRoot();
            if (root == null) yield break;
            var stack = new Stack<IBstNode<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.GetRight() != null) stack.Push(current.GetRight());
                if (current.GetLeft() != null) stack.Push(current.GetLeft());
                yield return current.Data;
            }
        }

        /// <summary>
        ///     Post-Order Traversal Function of AVL Tree
        ///     Standard Post-order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in Post-Order sequence.</returns>
        public virtual IEnumerable<T> PostOrderTraverse()
        {
            var root = GetRoot();
            if (root == null) yield break;
            var stack = new Stack<IBstNode<T>>();
            var res = new Stack<IBstNode<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                res.Push(current);
                if (current.GetLeft() != null) stack.Push(current.GetLeft());
                if (current.GetRight() != null) stack.Push(current.GetRight());
            }

            while (res.Count > 0) yield return res.Pop().Data;
        }

        /// <summary>
        ///     abstract method: get the root node of this binary search tree
        /// </summary>
        /// <returns> return an instance Node type which implement IBstNode interface</returns>
        internal abstract IBstNode<T> GetRoot();
    }
}