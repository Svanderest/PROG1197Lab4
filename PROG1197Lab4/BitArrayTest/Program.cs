using System;
using System.Collections;
using System.Linq;
using PROG1197Lab4Objects;

namespace BitArrayTest
{
    class Program
    {
        static void Main(string[] args)
        {            
            var tree = MovieTree.Create();
            for(int i = 1; i <= 14; i++)
            {
                Console.WriteLine(tree[i].Title);
            }
        }
    }
}
