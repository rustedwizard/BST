# BSTLibrary ![Tests Status](https://github.com/rustedwizard/BST/workflows/.NET/badge.svg)  ![Code Scanning](https://github.com/rustedwizard/BST/workflows/CodeQL/badge.svg)

## Quick Intro

This repo contains .Net Standard 2.1 project (in C# language) which implement Standard Binary search tree and AVL Tree.

## Usage

Compiled code is available as Nuget project at https://www.nuget.org/packages/BSTLibrary/.

To use it in your .Net project simply install the Nuget package into your project and using the namespace RustedWizard.BSTLibrary.

Both Binary Search Tree (BST is the class name) and AVL Tree (AVLTree is the class name) provide following functionality:

1. Create a empty tree.

2. Create a new tree with one node.

3. Both tree implementation are generic, with only one constraint which is that the type must implement IComparable interface.

4. Both tree implementation provide Creation, Insertion, Deletion, Searching, and Traversing(in-order, pre-order, post-order).

For detailed usage, please read this commented code of [AVLTree class](https://github.com/rustedwizard/BST/blob/master/BSTLibrary/AVLTree.cs)

## Note

To protect the validity of BST and AVLTree, both BSTNode(Node class for Binary search tree) and AVLNode(node class for AVL Tree) are declared internal. All the root node for both tree implementation are internal. All the creation and deletion of node are handled withing the assembly. You can only pass in and taking away the Data. You CAN NOT Create and pass in the Node yourself.
