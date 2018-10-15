using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class TreeGraph
    {

        INode RootNode;

        public TreeGraph()
        {

            RootNode = new Node("Root", 0);

        }

        public void AddNode(INode NewNode)
        {

            RootNode.NodeChildren.Add(NewNode);

        }

        public static string[] LoadText(string path)
        {

            return System.IO.File.ReadAllLines(path);

        }

        public static void WriteText(string Name, string[] Data)
        {

            System.IO.File.WriteAllLines(Name + ".txt", Data);

        }

        public static TreeGraph CreateTree(string[] Data)
        {

            TreeGraph NewTree = new TreeGraph();

            List<int> Depth = new List<int>();
            List<INode> Nodes = new List<INode>();

            // Get each string depth
            for (int i = 0; i < Data.Length; i++)
            {

                Depth.Add(Data[i].Count(character => character == '\t'));

            }

            // Create node
            for (int i = 0; i < Depth.Count; i++)
            {

                Nodes.Add(new Node(Data[i], Depth[i]));

            }

            for (int i = 0; i < Nodes.Count; i++)
            {

                if (Nodes[i].Depth == 0)
                {

                    Node.ChangeParentNode(Nodes[i], NewTree.RootNode);

                }
                else if (Nodes[i].Depth == Nodes[i - 1].Depth)
                {

                    Node.ChangeParentNode(Nodes[i], Nodes[i -1].ParentNode);

                }
                else if (Nodes[i].Depth > Nodes[i - 1].Depth)
                {

                    Node.ChangeParentNode(Nodes[i], Nodes[i - 1]);

                }
                else if(Nodes[i].Depth < Nodes[i - 1].Depth)
                {

                    for (int j = 0; j < Nodes.Count; j++)
                    {

                        if (Nodes[i].Depth == Nodes[j].Depth)
                        {

                            Node.ChangeParentNode(Nodes[i], Nodes[i - 1].ParentNode);
                            break;
                        }

                    }

                }

            }

            return NewTree;

        }

        public static void DisplayTree(TreeGraph TreeToDisplay, ref List<string> Output)
        {

            DisplayNode(TreeToDisplay.RootNode, ref Output);

        }

        public static void DisplayNode(INode NodeToDisplay, ref List<string> Source)
        {

            if (NodeToDisplay.Value != "Root")
            {

                Source.Add(NodeToDisplay.Value);

            }
            if (NodeToDisplay.NodeChildren.Count >= 0)
            {

                for (int i = 0; i < NodeToDisplay.NodeChildren.Count; i++)
                {

                    DisplayNode(NodeToDisplay.NodeChildren[i], ref Source);

                }

            }

        }

    }

}
