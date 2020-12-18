using System;
using System.Collections.Generic;

namespace RustedWizard.BSTLibrary
{
    /// <summary>
    /// AVLTree class.
    /// Enables the creation, insertion, deletion, searching, and traversal(in-order, pre-order and post-order) functions.
    /// This class implement AVL Tree which after each insertion and deletion, tree is balanced to enable optimal searching performance.
    /// </summary>
    /// <typeparam name="T">This class allows any type as long as that type implement System.IComparable Interface</typeparam>
    public partial class AVLTree<T> : IBST<T> where T : IComparable
    {
        internal AVLNode<T> Root { get; set; }

        //Partial Method
        //For declaration purpose since it is used in this file
        //To find detail implementation go to file AVLTreePrivateHelper.cs
        partial void treeBalancing(Stack<AVLNode<T>> stack);

        public void ClearTheTree()
        {
            Root = null;
        }

        /// <summary>
        /// Insert new data into AVL Tree.
        /// If data is actually inserted, method will return true.
        /// If duplicate data is found, duplicate data will not be inserted and method will return false.
        /// </summary>
        /// <param name="Data">The data intended to be insert into this AVL Tree.</param>
        /// <returns></returns>
        public bool Insert(T Data)
        {
            if (Root == null)
            {
                Root = new AVLNode<T>(Data);
                Root.Height = 1;
                return true;
            }
            var res = true;
            var current = Root;
            //use stack keep record of traverse path
            var stack = new Stack<AVLNode<T>>();
            stack.Push(current);
            while (true)
            {
                //Data to be inserted is smaller then current node, go left
                if (Data.CompareTo(current.Data) < 0)
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                        stack.Push(current);
                        continue;
                    }
                    else
                    {
                        current.Left = new AVLNode<T>(Data);
                        stack.Push(current.Left);
                        break;
                    }
                }
                //Duplicate data is not allowed in BST, if found, stop insertion and return false; 
                if (Data.CompareTo(current.Data) == 0)
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
                else
                {
                    current.Right = new AVLNode<T>(Data);
                    stack.Push(current.Right);
                    break;
                }
            }
            if (res)
            {
                //if data inserted, balancing the tree
                treeBalancing(stack);
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
            //if tree is empty, stop and return false;
            if (Root == null)
            {
                return false;
            }
            #region find the node and its parent
            var prev = Root;
            var node = Root;
            var res = true;
            var stack = new Stack<AVLNode<T>>();
            stack.Push(node);
            //if the node we are looking for is not the root node
            //try to find it. Also use prev variable to keep track of parent node.
            //We can not reuse Find method because we need extra step to keep track of parent node.
            if (data.CompareTo(Root.Data) != 0)
            {
                var current = Root;
                stack.Pop();
                while (true)
                {
                    if (current.Data.CompareTo(data) > 0)
                    {
                        //if data we search for is smaller then current node
                        //and current node has no left child, that means node 
                        //we are searching for does not exists, return false;
                        if (current.Left == null)
                        {
                            return false;
                        }
                        else
                        {
                            prev = current;
                            current = current.Left;
                            stack.Push(prev);
                        }
                    }
                    if (current.Data.CompareTo(data) == 0)
                    {
                        node = current;
                        break;
                    }
                    if (current.Data.CompareTo(data) < 0)
                    {
                        //if data we search for is larger then current node
                        //and current node has no right child, that means node 
                        //we are searching for does not exists, return false.
                        if (current.Right == null)
                        {
                            return false;
                        }
                        else
                        {
                            prev = current;
                            current = current.Right;
                            stack.Push(prev);
                        }
                    }
                }
            }
            #endregion
            #region actual delete
            while (true)
            {
                //if node to be deleted is leaf node
                if (node.IsLeafNode())
                {
                    if (prev.Left!=null && prev.Left.Data.CompareTo(node.Data) == 0)
                    {
                        prev.Left = null;
                        break;
                    }
                    else
                    {
                        prev.Right = null;
                        break;
                    }
                }
                //if node to be deleted is a one child node
                if (node.HasOneChild())
                {
                    if (prev.Left != null && prev.Left.Data.CompareTo(node.Data) == 0)
                    {
                        if (node.Left != null)
                        {
                            prev.Left = node.Left;
                            break;
                        }
                        else
                        {
                            prev.Left = node.Right;
                            break;
                        }
                    }
                    else
                    {
                        if (node.Left != null)
                        {
                            prev.Right = node.Left;
                            break;
                        }
                        else
                        {
                            prev.Right = node.Right;
                            break;
                        }
                    }
                }
                else //if node to be deleted has two child nodes.
                {
                    var minOnRight = node.Right;
                    var prevToMin = node;
                    stack.Push(prevToMin);
                    while (minOnRight.Left != null)
                    {
                        prevToMin = minOnRight;
                        minOnRight = minOnRight.Left;
                        stack.Push(prevToMin);
                    }
                    //set the value of the node to be delete to the min value of its right subtree.
                    //a.k.a the left most node of right subtree
                    node.Data = minOnRight.Data;
                    //set up prev and node, delete its min value node from right subtree.
                    //since it is left most node, it can only be leaf node or one child node.
                    //so the loop will end at next iteration
                    //Special case: if the left most node is the root node of this right subtree (has no left child)
                    if (minOnRight.Data.CompareTo(prevToMin.Data) == 0)
                    {
                        //prev node is the current node
                        prev = node;
                        node = minOnRight;
                    }
                    else
                    {
                        prev = prevToMin;
                        node = minOnRight;
                    }
                }
            }
            #endregion
            if (res)
            {
                treeBalancing(stack);
            }
            return res;
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
            if (Root == null)
            {
                return (false, default(T));
            }
            var current = Root;
            while (true)
            {
                if (data.CompareTo(current.Data) < 0)
                {
                    if (current.Left == null)
                    {
                        return (false, default(T));
                    }
                    else
                    {
                        current = current.Left;
                    }
                }
                if (data.CompareTo(current.Data) == 0)
                {
                    return (true, current.Data);
                }
                if (current.Right == null)
                {
                    return (false, default(T));
                }
                current = current.Right;
            }
        }


        #region tree traversal
        /// <summary>
        /// In order traversal of AVL Tree
        /// Standard In order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in In-Order sequence.</returns>
        public IEnumerable<T> InOrderTraverse()
        {
            if (Root == null)
            {
                yield break;
            }
            //use stack to keep tack all node instead of recursion
            var stack = new Stack<AVLNode<T>>();
            var current = Root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current = stack.Pop();
                if (current.Right != null)
                {
                    var data = current.Data;
                    current = current.Right;
                    yield return data;
                }
                else
                {
                    var data = current.Data;
                    current = null;
                    yield return data;
                }

            }
        }

        /// <summary>
        /// Pre-Order Traversal Function of AVL Tree
        /// Standard Pre-order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in Pre-Order sequence.</returns>
        public IEnumerable<T> PreOrderTraverse()
        {
            if (Root == null)
            {
                yield break;
            }
            var stack = new Stack<AVLNode<T>>();
            stack.Push(Root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Right != null)
                {
                    stack.Push(current.Right);
                }
                if (current.Left != null)
                {
                    stack.Push(current.Left);
                }
                yield return current.Data;
            }
        }

        /// <summary>
        /// Post-Order Traversal Function of AVL Tree
        /// Standard Post-order traversal of Binary tree (In this case this AVL Tree)
        /// </summary>
        /// <returns>Enumerable Collection of Data in Post-Order sequence.</returns>
        public IEnumerable<T> PostOrderTraverse()
        {
            if (Root == null)
            {
                yield break;
            }
            var stack = new Stack<AVLNode<T>>();
            var res = new Stack<AVLNode<T>>();
            stack.Push(Root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                res.Push(current);
                if (current.Left != null)
                {
                    stack.Push(current.Left);
                }
                if (current.Right != null)
                {
                    stack.Push(current.Right);
                }
            }
            while (res.Count > 0)
            {
                yield return res.Pop().Data;
            }
        }
        #endregion


    }
}
