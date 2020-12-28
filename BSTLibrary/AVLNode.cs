using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    internal class AvlNode<T> : IBstNode<T> where T : IComparable
    {
        public T Data { get; set; }

        public int Height { get; set; }

        public AvlNode<T> Left { get; set; }

        public AvlNode<T> Right { get; set; }

        public AvlNode(T data)
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
