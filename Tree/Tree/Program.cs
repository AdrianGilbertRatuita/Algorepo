using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] Data0 = TreeGraph.LoadText("people.txt");
            string[] Data1 = TreeGraph.LoadText("places.txt");
            string[] Data2 = TreeGraph.LoadText("unknownTaxonomy.txt");

            List<string> Output0 = new List<string>();
            List<string> Output1 = new List<string>();
            List<string> Output2 = new List<string>();

            TreeGraph Tree0 = TreeGraph.CreateTree(Data0);
            TreeGraph Tree1 = TreeGraph.CreateTree(Data1);
            TreeGraph Tree2 = TreeGraph.CreateTree(Data2);

            TreeGraph.DisplayTree(Tree0, ref Output0);
            Console.WriteLine("\n\n");
            TreeGraph.DisplayTree(Tree1, ref Output1);
            Console.WriteLine("\n\n");
            TreeGraph.DisplayTree(Tree2, ref Output2);
            Console.WriteLine("\n\n");

            TreeGraph.WriteOutlineFile("peopleTree", Output0.ToArray<string>());
            TreeGraph.WriteOutlineFile("placesTree", Output1.ToArray<string>());
            TreeGraph.WriteOutlineFile("unknownTree", Output2.ToArray<string>());

            string Enter = "";

            while (Enter.ToUpper() != "END")
            {

                Console.WriteLine("Enter what to search, or End: ");
                Enter = Console.ReadLine();

                List<INode> FirstTree = TreeGraph.GetNodes(Enter, Tree0);
                List<INode> SecondTree = TreeGraph.GetNodes(Enter, Tree1);
                List<INode> ThirdTree = TreeGraph.GetNodes(Enter, Tree2);

                for (int i = 0; i < FirstTree.Count; i++)
                {

                    TreeGraph.GetParent(FirstTree[i]);

                }

                for (int i = 0; i < SecondTree.Count; i++)
                {

                    TreeGraph.GetParent(SecondTree[i]);

                }

                for (int i = 0; i < ThirdTree.Count; i++)
                {

                    TreeGraph.GetParent(ThirdTree[i]);

                }

            }

            Console.ReadLine();

        }

    }



}
