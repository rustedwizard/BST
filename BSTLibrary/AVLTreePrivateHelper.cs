using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    public partial class AvlTree<T> where T : IComparable
    {
        //Following methods are helper method for treeBalancing method
        //Should not be used anywhere else.
        #region Tree Balancing Helper methods
        private int GetBalancingFactor(AvlNode<T> node)
        {
            //no child, balancing factor is 0
            if (node.IsLeafNode())
            {
                return 0;
            }
            //has two child, balancing factor is height of left subtree - height of right subtree
            if (!node.HasOneChild())
            {
                return (node.Left.Height - node.Right.Height);
            }
            //only one child, return that child's height
            if (node.Left != null)//child on the left
            {
                return node.Left.Height;
            }
            return 0 - node.Right.Height;//child on the right
            
        }

        private int GetSubtreeHeight(AvlNode<T> node)
        {
            if (node.IsLeafNode())
            {
                return 0; //no nodes below, height is 0 below
            }
            //two child, return the maximum height between left and right subtree.
            if (!node.HasOneChild())
            {
                return Math.Max(node.Left.Height, node.Right.Height);
            }
            //Only one child, return that child's height
            if (node.Left != null) //child on the left
            {
                return node.Left.Height;
            }
            return node.Right.Height; //child on the right
        }

        #region Rotations
        private AvlNode<T> LeftRightRotation(AvlNode<T> node)
        {
            var left = node.Left;
            var lr = left.Right;
            left.Right = lr.Left;
            node.Left = lr.Right;
            lr.Left = left;
            lr.Right = node;
            left.Height = GetSubtreeHeight(left) + 1;
            node.Height = GetSubtreeHeight(node) + 1;
            lr.Height = GetSubtreeHeight(lr) + 1;
            return lr;
        }

        private AvlNode<T> LeftLeftRotation(AvlNode<T> node)
        {
            var left = node.Left;
            var lr = node.Left.Right;
            node.Left = lr;
            left.Right = node;
            if (lr != null)
            {
                lr.Height = GetSubtreeHeight(lr) + 1;
            }
            node.Height = GetSubtreeHeight(node) + 1;
            left.Height = GetSubtreeHeight(left) + 1;
            return left;
        }

        private AvlNode<T> RightRightRotation(AvlNode<T> node)
        {
            var right = node.Right;
            var rl = node.Right.Left;
            node.Right = rl;
            right.Left = node;
            if (rl != null)
            {
                rl.Height = GetSubtreeHeight(rl) + 1;
            }
            node.Height = GetSubtreeHeight(node) + 1;
            right.Height = GetSubtreeHeight(right) + 1;
            return right;
        }

        private AvlNode<T> RightLeftRotation(AvlNode<T> node)
        {
            var right = node.Right;
            var rl = node.Right.Left;
            node.Right = rl.Left;
            right.Left = rl.Right;
            rl.Left = node;
            rl.Right = right;
            node.Height = GetSubtreeHeight(node) + 1;
            right.Height = GetSubtreeHeight(right) + 1;
            rl.Height = GetSubtreeHeight(rl) + 1;
            return rl;
        }
        #endregion
        #endregion

        partial void TreeBalancing(Stack<AvlNode<T>> stack)
        {
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                //update the height of node first.
                node.Height = GetSubtreeHeight(node) + 1;
                //get balancing factor of left and right subtree.
                var bf = GetBalancingFactor(node);
                switch (bf)
                {
                    //when balancing factor is 2 -> LeftLeft or LeftRight rotation
                    //if left child has balancing factor of 1 -> LeftLeftRotation
                    case 2 when GetBalancingFactor(node.Left) >= 0:
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = LeftLeftRotation(node);
                                continue;
                            }
                            stack.Peek().Right = LeftLeftRotation(node);
                            continue;
                        }
                        Root = LeftLeftRotation(node);
                        continue;
                    }
                    //if left child has balancing factor of -1 -> LeftRightRotaion
                    case 2 when GetBalancingFactor(node.Left) == -1:
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = LeftRightRotation(node);
                                continue;
                            }
                            stack.Peek().Right = LeftRightRotation(node);
                            continue;
                        }
                        Root = LeftRightRotation(node);
                        continue;
                    }
                    //When balancing factor is -1 -> RightRightRotation or RightLeftRotation
                    //if Right Child has balancing factor of -1 -> RightRightRotation
                    case -2 when GetBalancingFactor(node.Right) <= 0:
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = RightRightRotation(node);
                                continue;
                            }
                            stack.Peek().Right = RightRightRotation(node);
                            continue;
                        }
                        Root = RightRightRotation(node);
                        continue;
                    }
                    //if Right Child has balancing factor of 1 -> RightLeftRotation
                    case -2 when GetBalancingFactor(node.Right) == 1:
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = RightLeftRotation(node);
                                continue;
                            }
                            stack.Peek().Right = RightLeftRotation(node);
                            continue;
                        }
                        Root = RightLeftRotation(node);
                        continue;
                    }
                }
            }
        }
    }
}
