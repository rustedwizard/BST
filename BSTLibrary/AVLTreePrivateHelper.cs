using System;
using System.Collections.Generic;

namespace RustedWizard.BSTLibrary
{
    public partial class AVLTree<T> : IBST<T> where T : IComparable
    {
        //Following methods are helper method for treeBalancing method
        //Should not be used anywhere else.
        #region Tree Balancing Helper methods
        private int getBalancingFactor(AVLNode<T> node)
        {
            //no child, balancing factor is 0
            if (node.IsLeafNode())
            {
                return 0;
            }
            //one child
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    //had left child, balancing factor is the height of left subtree
                    return node.Left.Height;
                }
                else
                {
                    //had right child, balancing factor is 0 - height of right subtree
                    return 0 - node.Right.Height;
                }
            }
            //has two child, balancing factor is height of left subtree - height of right subtree
            return (node.Left.Height - node.Right.Height);
        }

        private int getSubtreeHeight(AVLNode<T> node)
        {
            if (node.IsLeafNode())
            {
                return 0; //no nodes below, height is 0 below
            }
            //only one child return that child's height
            if (node.HasOneChild())
            {
                if (node.Left != null)
                {
                    return node.Left.Height;
                }
                else
                {
                    return node.Right.Height;
                }
            }
            //two child, return the maximum height between left and right subtree.
            return Math.Max(node.Left.Height, node.Right.Height);
        }

        #region Rotations
        private AVLNode<T> LeftRightRotation(AVLNode<T> node)
        {
            var left = node.Left;
            var lr = left.Right;
            left.Right = lr.Left;
            node.Left = lr.Right;
            lr.Left = left;
            lr.Right = node;
            left.Height = getSubtreeHeight(left) + 1;
            node.Height = getSubtreeHeight(node) + 1;
            lr.Height = getSubtreeHeight(lr) + 1;
            return lr;
        }

        private AVLNode<T> LeftLeftRotation(AVLNode<T> node)
        {
            var left = node.Left;
            var lr = node.Left.Right;
            node.Left = lr;
            left.Right = node;
            if (lr != null)
            {
                lr.Height = getSubtreeHeight(lr) + 1;
            }
            node.Height = getSubtreeHeight(node) + 1;
            left.Height = getSubtreeHeight(left) + 1;
            return left;
        }

        private AVLNode<T> RightRightRotation(AVLNode<T> node)
        {
            var right = node.Right;
            var rl = node.Right.Left;
            node.Right = rl;
            right.Left = node;
            if (rl != null)
            {
                rl.Height = getSubtreeHeight(rl) + 1;
            }
            node.Height = getSubtreeHeight(node) + 1;
            right.Height = getSubtreeHeight(right) + 1;
            return right;
        }

        private AVLNode<T> RightLeftRotation(AVLNode<T> node)
        {
            var right = node.Right;
            var rl = node.Right.Left;
            node.Right = rl.Left;
            right.Left = rl.Right;
            rl.Left = node;
            rl.Right = right;
            node.Height = getSubtreeHeight(node) + 1;
            right.Height = getSubtreeHeight(right) + 1;
            rl.Height = getSubtreeHeight(rl) + 1;
            return rl;
        }
        #endregion
        #endregion

        partial void treeBalancing(Stack<AVLNode<T>> stack)
        {
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                //update the height of node first.
                node.Height = getSubtreeHeight(node) + 1;
                //get balancing factor of left and right subtree.
                var bf = getBalancingFactor(node);
                //when balancing factor is 2 -> LeftLeft or LeftRight rotation
                if (bf == 2)
                {
                    //if left child has balancing factor of 1 -> LeftLeftRotation
                    if (getBalancingFactor(node.Left) >= 0)
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = LeftLeftRotation(node);
                            }
                            else
                            {
                                stack.Peek().Right = LeftLeftRotation(node);
                            }
                            continue;
                        }
                        else
                        {
                            Root = LeftLeftRotation(node);
                            continue;
                        }
                    }
                    //if left child has balancing factor of -1 -> LeftRightRotaion
                    if (getBalancingFactor(node.Left) == -1)
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = LeftRightRotation(node);
                            }
                            else
                            {
                                stack.Peek().Right = LeftRightRotation(node);
                            }
                            continue;
                        }
                        else
                        {
                            Root = LeftRightRotation(node);
                            continue;
                        }

                    }
                }
                //When balancing factor is -1 -> RightRightRotation or RightLeftRotation
                if (bf == -2)
                {
                    //if Right Child has balancing factor of -1 -> RightRightRotation
                    if (getBalancingFactor(node.Right) <=0)
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = RightRightRotation(node);
                            }
                            else
                            {
                                stack.Peek().Right = RightRightRotation(node);
                            }
                            continue;
                        }
                        else
                        {
                            Root = RightRightRotation(node);
                            continue;
                        }
                    }
                    //if Right Child has balancing factor of 1 -> RightLeftRotation
                    if (getBalancingFactor(node.Right) == 1)
                    {
                        if (stack.Count > 0)
                        {
                            if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                            {
                                stack.Peek().Left = RightLeftRotation(node);
                            }
                            else
                            {
                                stack.Peek().Right = RightLeftRotation(node);
                            }
                            continue;
                        }
                        else
                        {
                            Root = RightLeftRotation(node);
                            continue;
                        }
                    }
                }
            }
        }
    }
}
