using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    /// AVLTree class.
    /// Enables the creation, insertion, deletion, searching, and traversal(in-order, preorder and post-order) functions.
    /// This class implement AVL Tree which after each insertion and deletion, tree is balanced to enable optimal searching performance.
    /// </summary>
    /// <typeparam name="T">This class allows any type as long as that type implement System.IComparable Interface</typeparam>
    public partial class AvlTree<T> : IBst<T> where T : IComparable
    {
        internal AvlNode<T> Root { get; set; }
        public int TreeSize { get; private set; }

        public AvlTree()
        {
            Root = null;
            TreeSize = 0;
        }

        public AvlTree(T data)
        {
            Root = new AvlNode<T>(data);
            TreeSize++;
        }

        //Partial Method
        //For declaration purpose since it is used in this file
        //To find detail implementation go to file AVLTreePrivateHelper.cs
        partial void TreeBalancing(Stack<AvlNode<T>> stack);

        /// <summary>
        /// Empty all elements in the tree.
        /// </summary>
        public void ClearTheTree()
        {
            Root = null;
            TreeSize = 0;
        }

        /// <summary>
        /// Insert new data into AVL Tree.
        /// If data is actually inserted, method will return true.
        /// If duplicate data is found, duplicate data will not be inserted and method will return false.
        /// </summary>
        /// <param name="data">The data intended to be insert into this AVL Tree.</param>
        /// <returns></returns>
        public bool Insert(T data)
        {
            //handling an empty tree
            if (Root == null)
            {
                Root = new AvlNode<T>(data) { Height = 1 };
                TreeSize++;
                return true;
            }
            var res = true;
            var current = Root;
            //use stack keep record of traverse path
            var stack = new Stack<AvlNode<T>>();
            stack.Push(current);
            while (true)
            {
                //Data to be inserted is smaller then current node, go left
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                        stack.Push(current);
                        continue;
                    }
                    //if has no left child, insert new node here
                    current.Left = new AvlNode<T>(data);
                    stack.Push(current.Left);
                    break;

                }
                //Duplicate data is not allowed in BST, if found, stop insertion and return false; 
                if (data.CompareTo(current.Data) == 0)
                {
                    res = false;
                    break;
                }
                //Data to be inserted is bigger then current node, go right
                if (current.Right != null)
                {
                    current = current.Right;
                    stack.Push(current);
                    continue;
                }
                //If has no right child, insert new node here
                current.Right = new AvlNode<T>(data);
                stack.Push(current.Right);
                break;
            }
            if (res)
            {
                //if data inserted, increase tree size and balancing the tree
                TreeSize++;
                TreeBalancing(stack);
            }
            return res;
        }

        /// <summary>
        /// Delete data from AVL Tree
        /// If data supplied is found in the tree, it will be deleted and method will return true.
        /// If data is not found, tree will remain unchanged and method will return false.
        /// </summary>
        /// <param name="data">The data intended to be deleted.</param>
        /// <returns></returns>
        public bool Delete(T data)
        {
            if (Root == null)
            {
                return false;
            }
            var stack = new Stack<AvlNode<T>>();
            var found = false;
            var current = Root;
            if (Root.Data.CompareTo(data) == 0)
            {
                if (Root.IsLeafNode())
                {
                    Root = null;
                    TreeSize--;
                    return true;
                }
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
                stack.Push(Root);
                var toDelete = Root.Right;
                while (toDelete.Left != null)
                {
                    stack.Push(toDelete);
                    toDelete = toDelete.Left;
                }
                current = toDelete;
                Root.Data = toDelete.Data;
                found = true;
            }
            while (!found)
            {
                if (current.Data.CompareTo(data) > 0)
                {
                    if (current.Left == null)
                    {
                        return false;
                    }
                    else
                    {
                        stack.Push(current);
                        current = current.Left;
                        continue;
                    }
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
                stack.Push(current);
                current = current.Right;
            }
            while (true)
            {
                if (current.IsLeafNode())
                {
                    //Left child detected, delete!
                    if (stack.Peek().Left == current)
                    {
                        stack.Peek().Left = null;
                        break;
                    }
                    //Otherwise delete right child.
                    stack.Peek().Right = null;
                    break;
                }
                if (current.HasOneChild())
                {
                    //Left child detected
                    if (stack.Peek().Left == current)
                    {
                        //Node to be deleted has left child
                        if (current.Left != null)
                        {
                            stack.Peek().Left = current.Left;
                            break;
                        }
                        //Node to be deleted has right child
                        stack.Peek().Left = current.Right;
                        break;
                    }
                    //Right child detected
                    //Node to be deleted has left child
                    if (current.Left != null)
                    {
                        stack.Peek().Right = current.Left;
                        break;
                    }
                    //node to be deleted has right child
                    stack.Peek().Right = current.Right;
                    break;
                }
                //Node to be deleted has two children 
                stack.Push(current);
                //find left most node of right subtree of node to be deleted
                var toDelete = current.Right;
                while (toDelete.Left != null)
                {
                    stack.Push(toDelete);
                    toDelete = toDelete.Left;
                }
                //set data of original node to be delete to the data of left most node
                current.Data = toDelete.Data;
                //set the left most node to be the node to delete
                current = toDelete;
            }
            //if program ever reaches here
            //means that deletion is successful, decrease tree size and balance the tree
            TreeSize--;
            TreeBalancing(stack);
            return true;
        }

        /// <summary>
        /// Searching function of AVL Tree
        /// Attempt to find supplied data in the tree.
        /// If found return result as true along with the actual data
        /// If nothing is found return result as false along with null value.
        /// </summary>
        /// <param name="data">The data intended to be searched</param>
        /// <returns>
        /// Returns a named tuple contains two item Found and Data.
        /// Found: bool value indicate if supplied Data is found in the tree
        /// Data: the actual data found in the tree.
        /// </returns>
        public (bool Found, T Data) TryFind(T data)
        {
            return TreeTraverse<T>.CreateTreeTraverse().TryFind(data, Root);
        }

        /// <summary>
        /// In order traversal of AVL Tree
        /// Standard In order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in In-Order sequence.</returns>
        public IEnumerable<T> InOrderTraverse()
        {
            return TreeTraverse<T>.CreateTreeTraverse().InOrderTraversal(Root);
        }

        /// <summary>
        /// Pre-Order Traversal Function of AVL Tree
        /// Standard Pre-order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in Pre-Order sequence.</returns>
        public IEnumerable<T> PreOrderTraverse()
        {
            return TreeTraverse<T>.CreateTreeTraverse().PreOrderTraversal(Root);
        }

        /// <summary>
        /// Post-Order Traversal Function of AVL Tree
        /// Standard Post-order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in Post-Order sequence.</returns>
        public IEnumerable<T> PostOrderTraverse()
        {
            return TreeTraverse<T>.CreateTreeTraverse().PostOrderTraversal(Root);
        }
    }
}
