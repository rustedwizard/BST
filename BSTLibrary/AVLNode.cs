using System;

namespace RustedWizard.BSTLibrary
{
    internal class AVLNode<T> : IBSTNode<T> where T : IComparable
    {
        public T Data { get; set; }

        public int Height { get; set; }

        public AVLNode<T> Left { get; set; }

        public AVLNode<T> Right { get; set; }

        public AVLNode(T data)
        {
            Data = data;
            Height = 1;
            Left = null;
            Right = null;
        }

        public bool IsLeafNode()
        {
            return Left == null && Right == null;
        }

        public bool HasOneChild()
        {
            return (Left == null && Right != null) || (Left != null && Right == null);
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
