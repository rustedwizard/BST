using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    /// <summary>
    ///     AVLTree class.
    ///     Enables the creation, insertion, deletion, searching, and traversal(in-order, preorder and post-order) functions.
    ///     This class implement AVL Tree which after each insertion and deletion, tree is balanced to enable optimal searching
    ///     performance.
    /// </summary>
    /// <typeparam name="T">This class allows any type as long as that type implement System.IComparable Interface</typeparam>
    public partial class AvlTree<T> : AbBst<T> where T : IComparable
    {
        /// <summary>
        ///     Default constructor of AVLTree
        ///     this constructor creates an empty AVLTree with specified data type
        /// </summary>
        public AvlTree()
        {
            Root = null;
            TreeSize = 0;
        }

        /// <summary>
        ///     Constructor of AVLTree with root node's data specified
        ///     this constructor creates an AVLTree contains only root node which holds specified data
        /// </summary>
        /// <param name="data"> the actual data the root node should hold </param>
        public AvlTree(T data)
        {
            Root = new AvlNode<T>(data);
            TreeSize++;
        }

        /// <summary>
        ///     Internal only: the Root node of this AVL Tree
        /// </summary>
        internal AvlNode<T> Root { get; set; }

        /// <summary>
        ///     The size property of this AVL Tree, hold the number of Nodes in this AVL Tree
        /// </summary>
        public int TreeSize { get; private set; }

        //Partial Method
        //For declaration purpose since it is used in this file
        //To find detail implementation go to file AVLTreePrivateHelper.cs
        partial void TreeBalancing(Stack<AvlNode<T>> stack);

        /// <summary>
        ///     Empty all elements in the tree.
        /// </summary>
        public void ClearTheTree()
        {
            Root = null;
            TreeSize = 0;
        }

        /// <summary>
        ///     Insert new data into AVL Tree.
        ///     If data is actually inserted, method will return true.
        ///     If duplicate data is found, duplicate data will not be inserted and method will return false.
        /// </summary>
        /// <param name="data">The data intended to be insert into this AVL Tree.</param>
        /// <returns></returns>
        public override bool Insert(T data)
        {
            //handling an empty tree
            if (Root == null)
            {
                Root = new AvlNode<T>(data) {Height = 1};
                TreeSize++;
                return true;
            }

            var res = true;
            var current = Root;

            // use stack keep record of traverse path
            var stack = new Stack<AvlNode<T>>();
            stack.Push(current);
            while (true)
            {
                // Data to be inserted is smaller then current node, go left
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                        stack.Push(current);
                        continue;
                    }

                    // if has no left child, insert new node here
                    current.Left = new AvlNode<T>(data);
                    stack.Push(current.Left);
                    break;
                }

                // Duplicate data is not allowed in BST, if found, stop insertion and return false; 
                if (data.CompareTo(current.Data) == 0)
                {
                    res = false;
                    break;
                }

                // Data to be inserted is bigger then current node, go right
                if (current.Right != null)
                {
                    current = current.Right;
                    stack.Push(current);
                    continue;
                }

                // If has no right child, insert new node here
                current.Right = new AvlNode<T>(data);
                stack.Push(current.Right);
                break;
            }

            if (res)
            {
                // if data inserted, increase tree size and balancing the tree
                TreeSize++;
                TreeBalancing(stack);
            }

            return res;
        }

        /// <summary>
        ///     Delete data from AVL Tree
        ///     If data supplied is found in the tree, it will be deleted and method will return true.
        ///     If data is not found, tree will remain unchanged and method will return false.
        /// </summary>
        /// <param name="data">The data intended to be deleted.</param>
        /// <returns></returns>
        public override bool Delete(T data)
        {
            if (Root == null) return false;
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
                    if (current.Left == null) return false;

                    stack.Push(current);
                    current = current.Left;
                    continue;
                }

                if (current.Data.CompareTo(data) == 0)
                {
                    found = true;
                    continue;
                }

                if (current.Right == null) return false;
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
        ///     internal only method: implementation of abstract method of GetRoot in AbBst class
        /// </summary>
        /// <returns> the root node of this AVL Tree in IBstNode type </returns>
        internal override IBstNode<T> GetRoot()
        {
            return Root;
        }
    }
}