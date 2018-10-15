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

        public static void WriteOutlineFile(string Name, string[] Data)
        {

            System.IO.File.WriteAllLines(Name + ".txt", Data);

        }

        public static TreeGraph CreateTree(string[] Data)
        {

            TreeGraph NewTree = new TreeGraph();

            List<float> Depth = new List<float>();
            List<INode> Nodes = new List<INode>();

            // Get each string depth
            for (int i = 0; i < Data.Length; i++)
            {

                float TempDepth = 0;
                TempDepth += Data[i].Count(character => character == '\t');
                TempDepth += Data[i].Count(character => character == ' ') * 0.5f;
                TempDepth += Data[i].Count(character => character == ' ') * 0.5f;
                Depth.Add(TempDepth);

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
                Console.WriteLine(NodeToDisplay.Value);

            }
            if (NodeToDisplay.NodeChildren.Count >= 0)
            {

                for (int i = 0; i < NodeToDisplay.NodeChildren.Count; i++)
                {

                    DisplayNode(NodeToDisplay.NodeChildren[i], ref Source);

                }

            }

        }

        public static List<INode> GetNodes(string Value, TreeGraph Tree)
        {

            List<INode> ListOfNodes = new List<INode>();

            CheckGetNode(Value, Tree.RootNode, ref ListOfNodes);

            return ListOfNodes;

        }

        public static void CheckGetNode(string Value, INode Node, ref List<INode> Source)
        {

            if (Node.Value.Replace("\t", "") == Value)
            {

                Source.Add(Node);

            }
            if (Node.NodeChildren.Count >= 0)
            {

                for (int i = 0; i < Node.NodeChildren.Count; i++)
                {

                    CheckGetNode(Value, Node.NodeChildren[i], ref Source);

                }

            }

        }

        public static void GetParent(INode Node)
        {

            Console.WriteLine(Node.Value);
            if (Node.ParentNode != null)
            {

                GetParent(Node.ParentNode);

            }

        }

    }

}
