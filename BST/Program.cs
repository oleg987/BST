using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BST
{
    class Program
    {        

        // ASP.NET Core 3.1
        static void Main(string[] args)
        {
            var tree = new AVLTree<int>();

            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);
            tree.Add(5);
            tree.Add(6);
            tree.Add(7);
            tree.Add(8);

            tree.Remove(4);

            Console.WriteLine();
        }
    }
}
