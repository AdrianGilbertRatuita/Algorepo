using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraph
{
    class Program
    {
        static void Main(string[] args)
        {

            TreeGraph NewTree = new TreeGraph();
            NewTree.AddNode(new Node("Hello"));
            //NewTree.AddNodeToNodeChild();

            Console.WriteLine();

        }
    }
}
