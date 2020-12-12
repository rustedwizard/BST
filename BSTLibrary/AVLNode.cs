using System;

namespace RustedWizard.BSTLibrary
{
    public class AVLNode<T> : IBSTNode<T> where T : IComparable
    {
        public T Data { get; set; }

        public int Height { get; set; }

        public AVLNode<T> Left { get; set; }

        public AVLNode<T> Right { get; set; }

        public AVLNode()
        {
            Data = default(T);
            Height = 0;
            Left = null;
            Right = null;
        }

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
            if ((Left == null && Right != null) || (Left != null && Right == null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
