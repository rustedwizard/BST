using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    //Implement this interface to enforce comparable Generic Data type
    //and few fundamental method of binary search tree
    public interface IBST<T> where T : IComparable 
    {
        public bool Insert(T data);

        public bool Delete(T Data);

        public (bool Found, T Data) TryFind(T Data);

        public IEnumerable<T> InOrderTraverse();

        public IEnumerable<T> PreOrderTraverse();

        public IEnumerable<T> PostOrderTraverse();
    }
}
