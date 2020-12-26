using System;

namespace RustedWizard.BSTLibrary
{
    internal class BSTNode<T> : IBSTNode<T> where T : IComparable
    {
        public T Data { get; set; }
        public BSTNode<T> Left { get; set; }
        public BSTNode<T> Right { get; set; }

        public BSTNode(T data)
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

        public IBSTNode<T> GetLeft()
        {
            return Left;
        }

        public IBSTNode<T> GetRight()
        {
            return Right;
        }
    }
}
