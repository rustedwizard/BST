using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if DEBUG
//this is for testing purpose only
[assembly: InternalsVisibleTo("BSTLibraryTest")]
#endif
// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     The interface of Binary Search tree
    ///     This interface enforce following function:
    ///     1. Insert data into Binary Search Tree
    ///     2. Delete data from Binary Search Tree
    ///     3. Search(try find) for specific data in Binary Search Tree
    ///     4. In-Order traversal of Binary Search tree
    ///     5. Pre-Order traversal of Binary Search tree
    ///     6. Post-Order traversal fo Binary Search tree
    ///     this interface pose following constraint:
    ///     To implement this interface, the type of data must already implemented the IComparable interface
    /// </summary>
    /// <typeparam name="T">the type of data the Binary Search tree needs hold</typeparam>
    public interface IBst<T> where T : IComparable
    {
        /// <summary>
        ///     Insert function of Binary Search Tree
        /// </summary>
        /// <param name="data"> the data need to be inserted </param>
        /// <returns> boolean value indicate if insertion is successful </returns>
        public bool Insert(T data);

        /// <summary>
        ///     Delete function of Binary Search Tree
        /// </summary>
        /// <param name="data"> the data need to be deleted from Binary Search Tree </param>
        /// <returns> boolean value indicate if deletion is successful </returns>
        public bool Delete(T data);

        /// <summary>
        ///     Search function of Binary Search Tree
        /// </summary>
        /// <param name="data"> the data need to be searched for existence in Binary Search Tree</param>
        /// <returns> a tuple of a boolean value indicate if supplied data is found and the actual value </returns>
        public (bool Found, T Data) TryFind(T data);

        /// <summary>
        ///     In-Order Traversal function
        /// </summary>
        /// <returns> Enumerable collection of all data inside Binary Search Tree </returns>
        public IEnumerable<T> InOrderTraverse();

        /// <summary>
        ///     Pre-Order Traversal function
        /// </summary>
        /// <returns> Enumerable collection of all data inside Binary Search Tree </returns>
        public IEnumerable<T> PreOrderTraverse();

        /// <summary>
        ///     Post-Order Traversal function
        /// </summary>
        /// <returns> Enumerable collection of all data inside Binary Search Tree </returns>
        public IEnumerable<T> PostOrderTraverse();
    }
}