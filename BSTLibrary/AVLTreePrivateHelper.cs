using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace RustedWizard.BSTLibrary
{
    public partial class AvlTree<T> where T : IComparable
    {
        /// <summary>
        ///     private helper method (automatic private due to partial declaration): Re-balancing the tree and insert/delete
        ///     operations
        /// </summary>
        /// <param name="stack"> the stack of visited nodes (traced back to root of the tree) </param>
        partial void TreeBalancing(Stack<AvlNode<T>> stack)
        {
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                //update the height of node first.
                node.Height = GetSubtreeHeight(node) + 1;
                //get balancing factor of left and right subtree.
                var bf = GetBalancingFactor(node);
                Func<AvlNode<T>, AvlNode<T>> func = null;
                switch (bf)
                {
                    //when balancing factor is 2 -> LeftLeft or LeftRight rotation
                    //if left child has balancing factor of 1 -> LeftLeftRotation
                    case 2 when GetBalancingFactor(node.Left) >= 0:
                    {
                        func = LeftLeftRotation;
                        break;
                    }
                    //if left child has balancing factor of -1 -> LeftRightRotation
                    case 2 when GetBalancingFactor(node.Left) == -1:
                    {
                        func = LeftRightRotation;
                        break;
                    }
                    //When balancing factor is -1 -> RightRightRotation or RightLeftRotation
                    //if Right Child has balancing factor of -1 -> RightRightRotation
                    case -2 when GetBalancingFactor(node.Right) <= 0:
                    {
                        func = RightRightRotation;
                        break;
                    }
                    //if Right Child has balancing factor of 1 -> RightLeftRotation
                    case -2 when GetBalancingFactor(node.Right) == 1:
                    {
                        func = RightLeftRotation;
                        break;
                    }
                }

                if (func != null) HookUp(stack, node, func);
            }
        }

        //Following methods are helper method for treeBalancing method
        //Should not be used anywhere else.

        #region Tree Balancing Helper methods

        /// <summary>
        ///     private helper method: Hook up the node with the tree after apply supplied rotation function rotation
        /// </summary>
        /// <param name="stack"> the stack used to retrace the node visited </param>
        /// <param name="node"> the node currently in processing</param>
        /// <param name="func"> the rotation function</param>
        private void HookUp(Stack<AvlNode<T>> stack, AvlNode<T> node, Func<AvlNode<T>, AvlNode<T>> func)
        {
            if (stack.Count > 0)
            {
                if (stack.Peek().Left != null && stack.Peek().Left.Data.CompareTo(node.Data) == 0)
                {
                    stack.Peek().Left = func(node);
                    return;
                }

                stack.Peek().Right = func(node);
                return;
            }

            Root = func(node);
        }

        /// <summary>
        ///     private helper method: calculate the balancing factor of given node
        /// </summary>
        /// <param name="node"> node need to calculate balancing factor</param>
        /// <returns></returns>
        private int GetBalancingFactor(AvlNode<T> node)
        {
            //no child, balancing factor is 0
            if (node.IsLeafNode()) return 0;
            //has two child, balancing factor is height of left subtree - height of right subtree
            if (!node.HasOneChild()) return node.Left.Height - node.Right.Height;
            //only one child, return that child's height
            if (node.Left != null) //child on the left
                return node.Left.Height;
            return 0 - node.Right.Height; //child on the right
        }

        /// <summary>
        ///     private helper method: Calculate the subtree height
        ///     this method takes the direct child's height property and return the maximum height
        /// </summary>
        /// <param name="node"> the node needs to get its subtree height </param>
        /// <returns> returns the height of the subtree</returns>
        private int GetSubtreeHeight(AvlNode<T> node)
        {
            if (node.IsLeafNode()) return 0; //no nodes below, height is 0 below
            //two child, return the maximum height between left and right subtree.
            if (!node.HasOneChild()) return Math.Max(node.Left.Height, node.Right.Height);
            //Only one child, return that child's height
            return node.Left?.Height ?? node.Right.Height;
        }

        #region Rotations

        /// <summary>
        ///     private helper method: Perform Left-Right rotation on given node
        /// </summary>
        /// <param name="node"> the root node that need to perform left-Right rotation</param>
        /// <returns> returns the new root node after the Left-Right rotation </returns>
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

        /// <summary>
        ///     private helper method: Perform Left-Left Rotation on given node
        /// </summary>
        /// <param name="node"> the root node that need to perform Left-Left rotation</param>
        /// <returns> the new root node after Left-Left rotation</returns>
        private AvlNode<T> LeftLeftRotation(AvlNode<T> node)
        {
            var left = node.Left;
            var lr = node.Left.Right;
            node.Left = lr;
            left.Right = node;
            if (lr != null) lr.Height = GetSubtreeHeight(lr) + 1;
            node.Height = GetSubtreeHeight(node) + 1;
            left.Height = GetSubtreeHeight(left) + 1;
            return left;
        }

        /// <summary>
        ///     private helper method: Perform Right-Right Rotation on given node
        /// </summary>
        /// <param name="node"> the root node that need to perform Right-Right rotation </param>
        /// <returns> the new root node after Right-Right rotation </returns>
        private AvlNode<T> RightRightRotation(AvlNode<T> node)
        {
            var right = node.Right;
            var rl = node.Right.Left;
            node.Right = rl;
            right.Left = node;
            if (rl != null) rl.Height = GetSubtreeHeight(rl) + 1;
            node.Height = GetSubtreeHeight(node) + 1;
            right.Height = GetSubtreeHeight(right) + 1;
            return right;
        }

        /// <summary>
        ///     private helper method: Perform Right-Left Rotation on given node
        /// </summary>
        /// <param name="node"> the root node that need to perform Right-Left Rotation </param>
        /// <returns> the new root node after Right-Left rotation </returns>
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
    }
}