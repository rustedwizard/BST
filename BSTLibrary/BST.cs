using System;
using System.Collections.Generic;

namespace BSTLibrary
{
    //BST class
    //This C# class implement a generic binary search tree that support create, insert, delete operation
    //Support In-Order, Pre-Order and Post-Order traversal of the tree.
    //Support find operation to tell if supplied data exists in Binary serchtree
    //All supported operations are implemented without use of Recursion. So no stack overflow can happen.
    public class BST<T> where T : IComparable
    {
        public BSTNode<T> Root { get; set; }

        public BST()
        {
            Root = null;
        }

        public BST(T data)
        {
            Root = new BSTNode<T>(data);
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
            //if tree is empty, stop and return false;
            if(Root == null)
            {
                return false;
            }
            var prev = Root;
            var node = Root;
            //if the node we are looking for is not the root node
            //try to find it. Also use prev variable to keep track of parent node.
            //We can not reuse Find method because we need extra step to keep track of parent node.
            if(data.CompareTo(Root.Data) != 0)
            {
                var current = Root;
                while (true)
                {
                    if (current.Data.CompareTo(data) > 0)
                    {
                        //if data we search for is smaller then current node
                        //and current node has no left child, that means node 
                        //we are searching for does not exists, return false;
                        if(current.Left == null)
                        {
                            return false;
                        }
                        else
                        {
                            prev = current;
                            current = current.Left;
                        }
                    }
                    if (current.Data.CompareTo(data) == 0)
                    {
                        node = current;
                        break;
                    }
                    if(current.Data.CompareTo(data) < 0)
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
                        }
                    }
                }
            }
            while (true)
            {
                //if node to be deleted is leaf node
                if (node.IsLeafNode())
                {
                    if (prev.Left.Data.CompareTo(node.Data) == 0)
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
                //if node to be deleted is a one child node
                if (node.HasOneChild())
                {
                    if(prev.Left.Data.CompareTo(node.Data) == 0)
                    {
                        if(node.Left != null)
                        {
                            prev.Left = node.Left;
                            return true;
                        }
                        else
                        {
                            prev.Left = node.Right;
                            return true;
                        }
                    }
                    else
                    {
                        if (node.Left != null)
                        {
                            prev.Right = node.Left;
                            return true;
                        }
                        else
                        {
                            prev.Right = node.Right;
                            return true;
                        }
                    }
                }
                else //if node to be deleted has two child nodes.
                {
                    var minOnRight = node.Right;
                    var prevToMin = node.Right;
                    while(minOnRight.Left != null)
                    {
                        prevToMin = minOnRight;
                        minOnRight = minOnRight.Left;
                    }
                    //set the value of the node to be delete to the min value of its right subtree.
                    //a.k.a the left most node of right subtree
                    node.Data = minOnRight.Data;
                    //set up prev and node, delete its min value node from right subtree.
                    //since it is left most node, it can only be leaf node or one child node.
                    //so the loop will end at next iteration
                    //Special case: if the left most node is the root node of this right subtree (has no left child)
                    if(minOnRight.Data.CompareTo(prevToMin.Data)==0)
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
