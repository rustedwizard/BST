using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    //BST class
    //This C# class implement a generic binary search tree that support create, insert, delete operation
    //Support In-Order, PreOrder and Post-Order traversal of the tree.
    //Support find operation to tell if supplied data exists in Binary Search Tree
    //All supported operations are implemented without use of Recursion. So no stack overflow can happen.
    public class Bst<T> : IBst<T> where T : IComparable
    {
        internal BstNode<T> Root { get; set; }
        public int TreeSize { get; private set; }

        public Bst()
        {
            Root = null;
            TreeSize = 0;
        }

        public Bst(T data)
        {
            Root = new BstNode<T>(data);
            TreeSize++;
        }

        public void ClearTheTree()
        {
            Root = null;
            TreeSize = 0;
        }

        public bool Insert(T data)
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
                if (data.CompareTo(current.Data) == 0)
                {
                    return false;
                }
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

        public bool Delete(T data)
        {
            if (Root == null)
            {
                return false;
            }
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
                    if (current.Left == null)
                    {
                        return false;
                    }
                    prev = current;
                    current = current.Left;
                    continue;

                }
                if (current.Data.CompareTo(data) == 0)
                {
                    found = true;
                    continue;
                }
                if (current.Right == null)
                {
                    return false;
                }
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

        //Attempt to find data in BST
        public (bool Found, T Data) TryFind(T data)
        {
            return TreeTraverse<T>.TryFind(data, Root);
        }

        public IEnumerable<T> InOrderTraverse()
        {
            return TreeTraverse<T>.InOrderTraversal(Root);
        }

        public IEnumerable<T> PreOrderTraverse()
        {
            return TreeTraverse<T>.PreOrderTraversal(Root);
        }

        public IEnumerable<T> PostOrderTraverse()
        {
            return TreeTraverse<T>.PostOrderTraversal(Root);
        }
    }
}
