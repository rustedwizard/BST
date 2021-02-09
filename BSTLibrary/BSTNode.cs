using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     Internal only: Binary Search Tree Node Class, Node for AVL Tree
    /// </summary>
    /// <typeparam name="T"> The actual type for the data  of this node needs to hold </typeparam>
    internal class BstNode<T> : IBstNode<T> where T : IComparable
    {
        /// <summary>
        ///     Internal Only: Constructor, construct a new instance of Binary search tree node holds specified data
        /// </summary>
        /// <param name="data"> the data which newly created node should hold </param>
        public BstNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        /// <summary>
        ///     Internal Only: The left child property, point to the left child of this node
        /// </summary>
        public BstNode<T> Left { get; set; }

        /// <summary>
        ///     Internal Only: The right child property point to the right child of this node
        /// </summary>
        public BstNode<T> Right { get; set; }

        /// <summary>
        ///     Internal Only: Data property, holds the actual data and provide direct access to it.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     Internal only: Determine if this node is a leaf node (has no child)
        /// </summary>
        /// <returns> a boolean value indicating the result (true: the node is a leaf node, false: the node is not a leaf node)</returns>
        public bool IsLeafNode()
        {
            return Left == null && Right == null;
        }

        /// <summary>
        ///     Internal only: Determine if this node has and only has one child
        /// </summary>
        /// <returns> a boolean value indicating the result </returns>
        public bool HasOneChild()
        {
            return Left == null && Right != null || Left != null && Right == null;
        }

        /// <summary>
        ///     Internal only: return the Left child in type of IBstNode
        /// </summary>
        /// <returns> return the left child as IBstNode </returns>
        public IBstNode<T> GetLeft()
        {
            return Left;
        }

        /// <summary>
        ///     return the Right child in type of IBstNode
        /// </summary>
        /// <returns> return the right child as IBstNode </returns>
        public IBstNode<T> GetRight()
        {
            return Right;
        }
    }
}