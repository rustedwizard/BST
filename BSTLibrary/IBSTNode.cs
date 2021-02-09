using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     The interface of Binary Search Tree Node
    ///     This interface enforce following function:
    ///     1. Data property which provide access to the data this node shall holds
    ///     2. Get the Left child in IBstNode type
    ///     3. Get the Right Child in IBstNode type
    ///     4. determine if Node has and only has one child, indicate the result by return a boolean value
    ///     5. determine if Node is a leaf node, indicate the result by return a boolean value
    ///     this interface pose following constraint:
    ///     To implement this interface, the type of data must already implemented the IComparable interface
    /// </summary>
    /// <typeparam name="T">the type of data the Binary Search tree needs hold</typeparam>
    internal interface IBstNode<T> where T : IComparable
    {
        /// <summary>
        ///     Data property
        ///     this property shall provide direct access to the data holds by node.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        ///     Get Left child of the node
        /// </summary>
        /// <returns> return left child node in IBstNode type </returns>
        public IBstNode<T> GetLeft();

        /// <summary>
        ///     Get Right child of the node
        /// </summary>
        /// <returns> return right child node in IBstNode type </returns>
        public IBstNode<T> GetRight();

        /// <summary>
        ///     Determine if node has and only has one child
        /// </summary>
        /// <returns> a boolean value indicating if node has and has only one child </returns>
        public bool HasOneChild();

        /// <summary>
        ///     Determine if node is a leaf node
        /// </summary>
        /// <returns> a boolean value indicating if node is a leaf node or not </returns>
        public bool IsLeafNode();
    }
}