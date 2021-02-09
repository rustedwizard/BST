using System;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     Binary Search Tree class
    ///     This C# class implement a generic binary search tree that support create, insert, delete operation
    /// </summary>
    /// <typeparam name="T"> The type of actual data this Binary search tree needs to hold </typeparam>
    public class Bst<T> : AbBst<T> where T : IComparable
    {
        /// <summary>
        ///     Default constructor: Creates an empty Binary Search Tree
        /// </summary>
        public Bst()
        {
            Root = null;
            TreeSize = 0;
        }

        /// <summary>
        ///     Constructor: Creates an Binary search with Root node holds specified data
        ///     since standard Binary Search does not have any re-balancing algorithm, the root node will not change.
        /// </summary>
        /// <param name="data"> Data will be hold by Root node </param>
        public Bst(T data)
        {
            Root = new BstNode<T>(data);
            TreeSize++;
        }

        /// <summary>
        ///     Internal Only: The actual Root Node
        /// </summary>
        internal BstNode<T> Root { get; set; }

        /// <summary>
        ///     The size property of this Binary Tree, gives the count of nodes inside of this Binary search tree
        /// </summary>
        public int TreeSize { get; private set; }

        /// <summary>
        ///     Clear every element of this Binary Search Tree, zero the size property.
        /// </summary>
        public void ClearTheTree()
        {
            Root = null;
            TreeSize = 0;
        }

        /// <summary>
        ///     Insert specified data into Binary search tree
        /// </summary>
        /// <param name="data"> the data need to be inserted </param>
        /// <returns> a boolean value indicate if the insertion is successful </returns>
        public override bool Insert(T data)
        {
            if (Root == null)
            {
                Root = new BstNode<T>(data);
                TreeSize++;
                return true;
            }

            var current = Root;
            while (true)
            {
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                        continue;
                    }

                    current.Left = new BstNode<T>(data);
                    TreeSize++;
                    return true;
                }

                //Duplicate data is not allowed in BST, if found, stop insertion and return false; 
                if (data.CompareTo(current.Data) == 0) return false;
                if (current.Right != null)
                {
                    current = current.Right;
                    continue;
                }

                current.Right = new BstNode<T>(data);
                TreeSize++;
                return true;
            }
        }

        /// <summary>
        ///     Delete specified data from this Binary Search Tree
        /// </summary>
        /// <param name="data"> the actual data need to be deleted</param>
        /// <returns> a boolean value indicate if deletion is successful </returns>
        public override bool Delete(T data)
        {
            if (Root == null) return false;
            var found = false;
            var current = Root;
            var prev = Root;
            //Special Case handling: Deleting Root Node
            if (Root.Data.CompareTo(data) == 0)
            {
                //Root node is the only node left in tree
                if (Root.IsLeafNode())
                {
                    Root = null;
                    TreeSize--;
                    return true;
                }

                //Root node has one child
                if (Root.HasOneChild())
                {
                    if (Root.Left != null)
                    {
                        Root = Root.Left;
                        TreeSize--;
                        return true;
                    }

                    Root = Root.Right;
                    TreeSize--;
                    return true;
                }

                //Root node has 2 child
                var toDelete = Root.Right;
                //find minimum on Right subtree
                while (toDelete.Left != null)
                {
                    prev = toDelete;
                    toDelete = toDelete.Left;
                }

                current = toDelete;
                Root.Data = toDelete.Data;
                found = true; //skip finding the node, directly go to delete
            }

            //Find the node
            while (!found)
            {
                if (current.Data.CompareTo(data) > 0)
                {
                    if (current.Left == null) return false;
                    prev = current;
                    current = current.Left;
                    continue;
                }

                if (current.Data.CompareTo(data) == 0)
                {
                    found = true;
                    continue;
                }

                if (current.Right == null) return false;
                prev = current;
                current = current.Right;
            }

            //delete the node
            while (true)
            {
                if (current.IsLeafNode())
                {
                    if (prev.Left == current)
                    {
                        prev.Left = null;
                        TreeSize--;
                        return true;
                    }

                    prev.Right = null;
                    TreeSize--;
                    return true;
                }

                if (current.HasOneChild())
                {
                    if (prev.Left == current)
                    {
                        if (current.Left != null)
                        {
                            prev.Left = current.Left;
                            TreeSize--;
                            return true;
                        }

                        prev.Left = current.Right;
                        TreeSize--;
                        return true;
                    }

                    if (current.Left != null)
                    {
                        prev.Right = current.Left;
                        TreeSize--;
                        return true;
                    }

                    prev.Right = current.Right;
                    TreeSize--;
                    return true;
                }

                prev = current;
                var toDelete = current.Right;
                while (toDelete.Left != null)
                {
                    prev = toDelete;
                    toDelete = toDelete.Left;
                }

                current.Data = toDelete.Data;
                current = toDelete;
            }
        }

        /// <summary>
        ///     The implementation of abstract method GetRoot() in AbBst Class
        /// </summary>
        /// <returns> return the Root node in IBstNode type </returns>
        internal override IBstNode<T> GetRoot()
        {
            return Root;
        }
    }
}