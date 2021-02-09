using System;
using RustedWizard.BSTLibrary;

namespace PefromanceTest
{
    internal class Program
    {
        private static void Main()
        {
            var avlTree = new AvlTree<int>();
            var rnd = new Random();
            //Generate 20 random integer
            var nits = new int[100000];
            for (var i = 0; i < 100000; i++) nits[i] = rnd.Next(-2000000, 2000000);
            foreach (var e in nits) avlTree.Insert(e);
            foreach (var e in nits)
            {
                var res = avlTree.TryFind(e);
                Console.WriteLine($"number: {e}, found: {res.Found}");
            }

            foreach (var e in nits) avlTree.Delete(e);
        }
    }
}