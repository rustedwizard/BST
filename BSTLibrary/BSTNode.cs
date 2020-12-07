using System;
using System.Collections.Generic;
using System.Text;

namespace BSTLibrary
{
    public class BSTNode<T>where T: IComparable
    {
        public T Data { get; set; }
        public BSTNode<T> Left { get; set; }
        public BSTNode<T> Right { get; set; }

        public BSTNode()
        {
            Data = default(T);
            Left = null;
            Right = null;
        }

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
            if ((Left==null && Right != null)||(Left!=null && Right == null))
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
