using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    internal class BstNode<T> : IBstNode<T> where T : IComparable
    {
        public T Data { get; set; }
        public BstNode<T> Left { get; set; }
        public BstNode<T> Right { get; set; }

        public BstNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public bool IsLeafNode()
        {
            return Left == null && Right == null;
        }

        public bool HasOneChild()
        {
            return ((Left == null && Right != null) || (Left != null && Right == null));
        }

        public IBstNode<T> GetLeft()
        {
            return Left;
        }

        public IBstNode<T> GetRight()
        {
            return Right;
        }
    }
}
