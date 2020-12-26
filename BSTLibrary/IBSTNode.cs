﻿using System;

namespace RustedWizard.BSTLibrary
{
    //implement this interface to enforce comparable data type
    public interface IBSTNode<T> where T : IComparable
    {
        public T Data { get; set; }

        public IBSTNode<T> GetLeft();

        public IBSTNode<T> GetRight();

        public bool HasOneChild();

        public bool IsLeafNode();
    }
}
