using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     Internal only: AVL Node Class, Node for AVL Tree
    /// </summary>
    /// <typeparam name="T"> The actual type for the data  of this node needs to hold </typeparam>
    internal class AvlNode<T> : IBstNode<T> where T : IComparable
    {
        /// <summary>
        ///     Internal only: Constructor of AVLNode with data supplied
        ///     Creates an instance of AVL node which holds specified data
        ///     Since AVL Tree has re-balancing function, the root node is subject to change.
        /// </summary>
        /// <param name="data"> the actual data this AVL node should hold </param>
        public AvlNode(T data)
        {
            Data = data;
            Height = 1;
            Left = null;
            Right = null;
        }

        /// <summary>
        ///     Internal only: the Height of this node stays within the AVL Tree
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     Internal only: the left child of this node
        /// </summary>
        public AvlNode<T> Left { get; set; }

        /// <summary>
        ///     Internal only: the right child of this node
        /// </summary>
        public AvlNode<T> Right { get; set; }

        /// <summary>
        ///     the actual data this AVL Node holds
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     Internal Only: test if this node is a leaf node
        /// </summary>
        /// <returns></returns>
        public bool IsLeafNode()
        {
            return Left == null && Right == null;
        }

        /// <summary>
        ///     Internal only: test if this node has and only has one child
        /// </summary>
        /// <returns></returns>
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