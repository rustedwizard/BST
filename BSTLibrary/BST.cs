using System;
using System.Collections.Generic;

namespace RustedWizard.BSTLibrary
{
    //BST class
    //This C# class implement a generic binary search tree that support create, insert, delete operation
    //Support In-Order, Pre-Order and Post-Order traversal of the tree.
    //Support find operation to tell if supplied data exists in Binary Search Tree
    //All supported operations are implemented without use of Recursion. So no stack overflow can happen.
    public class BST<T> : IBST<T> where T : IComparable
    {
        internal BSTNode<T> Root { get; set; }

        public BST()
        {
            Root = null;
        }

        public BST(T data) 
        {
            Root = new BSTNode<T>(data);
        }

        public void ClearTheTree()
        {
            Root = null;
        }

        public bool Insert(T Data)
        {
            if (Root == null)
            {
                Root = new BSTNode<T>(Data);
                return true;
            }

            var current = Root;
            while (true)
            {
                if (Data.CompareTo(current.Data) < 0)
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                        continue;
                    }
                    else
                    {
                        current.Left = new BSTNode<T>(Data);
                        return true;
                    }
                }
                //Duplicate data is not allowed in BST, if found, stop insertion and return false; 
                if (Data.CompareTo(current.Data) == 0)
                {
                    return false;
                }
                if (current.Right != null)
                {
                    current = current.Right;
                    continue;
                }
                else
                {
                    current.Right = new BSTNode<T>(Data);
                    return true;
                }
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
                    return true;
                }
                //Root node has one child
                if (Root.HasOneChild())
                {
                    if (Root.Left != null)
                    {
                        Root = Root.Left;
                        return true;
                    }
                    else
                    {
                        Root = Root.Right;
                        return true;
                    }
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
                    else
                    {
                        prev = current;
                        current = current.Left;
                        continue;
                    }
                }
                if (current.Data.CompareTo(data) == 0)
                {
                    found = true;
                    continue;
                }
                if (current.Data.CompareTo(data) < 0)
                {
                    if (current.Right == null)
                    {
                        return false;
                    }
                    else
                    {
                        prev = current;
                        current = current.Right;
                        continue;
                    }
                }
            }
            //delete the node
            while (true)
            {
                if (current.IsLeafNode())
                {
                    if (prev.Left == current)
                    {
                        prev.Left = null;
                        return true;
                    }
                    else
                    {
                        prev.Right = null;
                        return true;
                    }
                }
                if (current.HasOneChild())
                {
                    if (prev.Left == current)
                    {
                        if (current.Left != null)
                        {
                            prev.Left = current.Left;
                            return true;
                        }
                        else
                        {
                            prev.Left = current.Right;
                            return true;
                        }
                    }
                    else
                    {
                        if (current.Left != null)
                        {
                            prev.Right = current.Left;
                            return true;
                        }
                        else
                        {
                            prev.Right = current.Right;
                            return true;
                        }
                    }
                }
                prev = current;
                var toDelete = current.Right;
                while (toDelete.Left != null)
                {
                    prev = toDelete;
                    toDelete = toDelete.Left;
                }
                current = toDelete;
                current.Data = toDelete.Data;
            }
        }

        //Attempt to find data in BST
        public (bool Found, T Data) TryFind(T data)
        {
            if(Root == null)
            {
                return (false, default(T));
            }
            var current = Root;
            while (true)
            {
                if(data.CompareTo(current.Data) < 0)
                {
                    if(current.Left == null)
                    {
                        return (false, default(T)); 
                    }
                    else
                    {
                        current = current.Left;
                        continue;
                    }
                }
                if (data.CompareTo(current.Data) == 0)
                {
                    return (true, current.Data);
                }
                if(current.Right == null)
                {
                    return (false, default(T));
                }
                current = current.Right;
                continue;
            }
        }

        public IEnumerable<T> InOrderTraverse()
        {
            if(Root == null)
            {
                yield break;
            }
            //use stack to keep tack all node instead of recursion
            var stack = new Stack<BSTNode<T>>();
            var current = Root;
            while (stack.Count > 0 || current != null) 
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current = stack.Pop();
                if(current.Right != null)
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

        public IEnumerable<T> PreOrderTraverse()
        {
            if (Root == null)
            {
                yield break;
            }
            var stack = new Stack<BSTNode<T>>();
            stack.Push(Root);
            while(stack.Count>0)
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

        public IEnumerable<T> PostOrderTraverse()
        {
            if (Root == null)
            {
                yield break;
            }
            var stack = new Stack<BSTNode<T>>();
            var res = new Stack<BSTNode<T>>();
            stack.Push(Root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                res.Push(current);
                if(current.Left != null)
                {
                    stack.Push(current.Left);
                }
                if(current.Right != null)
                {
                    stack.Push(current.Right);
                }
            }
            while(res.Count > 0)
            {
                yield return res.Pop().Data;
            }
        }
    }
}
