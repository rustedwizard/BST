using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("BSTLibraryTest")]
#endif
// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    //Implement this interface to enforce comparable Generic Data type
    //and few fundamental method of binary search tree
    public interface IBst<T> where T : IComparable
    {
        public bool Insert(T data);

        public bool Delete(T data);

        public (bool Found, T Data) TryFind(T data);

        public IEnumerable<T> InOrderTraverse();

        public IEnumerable<T> PreOrderTraverse();

        public IEnumerable<T> PostOrderTraverse();
    }
}
