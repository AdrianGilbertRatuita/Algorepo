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
            TreeGraph.DisplayTree(Tree1, ref Output1);
            TreeGraph.DisplayTree(Tree2, ref Output2);

            TreeGraph.WriteText("peopleTree", Output0.ToArray<string>());
            TreeGraph.WriteText("placesTree", Output1.ToArray<string>());
            TreeGraph.WriteText("unknownTree", Output2.ToArray<string>());

            Console.ReadLine();

        }

    }

}
