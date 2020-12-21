using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;

namespace PefromanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var AVLTree = new AVLTree<int>();
            var Rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[100000];
            for (int i = 0; i < 100000; i++)
            {
                ints[i] = Rnd.Next(-2000000, 2000000);
            }
            foreach (var e in ints)
            {
                AVLTree.Insert(e);
            }
            foreach (var e in ints)
            {
                var res = AVLTree.TryFind(e);
                Console.WriteLine($"number: {e}, found: {res.Found}");
            }
            foreach (var e in ints)
            {
                AVLTree.Delete(e);
            }
        }
    }
}
