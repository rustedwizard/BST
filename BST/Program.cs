using System;
using BSTLibrary;

namespace BST
{
    class Program
    {
        static void Main(string[] args)
        {
            var bst = new BST<int>();
            bst.Insert(5);
            bst.Insert(7);
            bst.Insert(3);
            bst.Insert(1);
            bst.Insert(2);
            bst.Insert(8);
            bst.Insert(0);
            bst.Insert(10);
            bst.Insert(9);
            bst.Insert(4);
            bst.Insert(6);
            bst.Delete(5);
            bst.Delete(3);
            bst.Delete(2);
            var found = bst.TryFind(7);
            if (found.Found)
            {
                Console.WriteLine(found.Data);
            }
            foreach (var e in bst.InOrderTraverse())
            {
                Console.Write(e + " ");
            }
            Console.Out.WriteLine();
            foreach (var e in bst.PreOrderTraverse())
            {
                Console.Write(e + " ");
            }
            Console.WriteLine();
            foreach (var e in bst.PostOrderTraverse())
            {
                Console.Write(e + " ");
            }

            var avl = new AVLTree<int>();
            avl.Insert(10);
            avl.Insert(9);
            avl.Insert(8);
            avl.Insert(5);
            avl.Insert(6);
            avl.Insert(4);
            avl.Delete(8);
            avl.Delete(10);
            avl.Delete(9);
            avl.Insert(7);
            avl.Insert(8);
            avl.Insert(9);
            avl.Insert(10);
            avl.Insert(12);
            avl.Insert(11);
        }
    }
}
