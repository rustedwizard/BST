using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    //implement this interface to enforce comparable data type
    internal interface IBstNode<T> where T : IComparable
    {
        public T Data { get; set; }

        public IBstNode<T> GetLeft();

        public IBstNode<T> GetRight();

        public bool HasOneChild();

        public bool IsLeafNode();
    }
}
