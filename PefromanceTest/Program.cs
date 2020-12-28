using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RustedWizard.BSTLibrary;

namespace PefromanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var avlTree = new AvlTree<int>();
            var rnd = new Random();
            //Generate 20 random integer
            int[] ints = new int[100000];
            for (int i = 0; i < 100000; i++)
            {
                ints[i] = rnd.Next(-2000000, 2000000);
            }
            foreach (var e in ints)
            {
                avlTree.Insert(e);
            }
            foreach (var e in ints)
            {
                var res = avlTree.TryFind(e);
                Console.WriteLine($"number: {e}, found: {res.Found}");
            }
            foreach (var e in ints)
            {
                avlTree.Delete(e);
            }
        }
    }
}
