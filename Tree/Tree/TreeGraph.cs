﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class TreeGraph
    {

        private INode RootNode;
        public bool IsReady { get; private set; }

        // Constructor
        public TreeGraph()
        {

            RootNode = new Node("Root", 0, "ROOT");

        }

        // Read from a file
        public static string[] LoadText(string path)
        {

            return System.IO.File.ReadAllLines(path);

        }

        // Write out to a file
        public static void WriteOutlineFile(string Name, string[] Data)
        {

            System.IO.File.WriteAllLines(Name + ".txt", Data);

        }

        // Initial tree creation
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
                TempDepth += Data[i].Substring(0, ReturnCharacterFirstOccurence(Data[i])).Count(character => character == ' ') * 0.125f;
                Depth.Add(TempDepth);

                // Create Temporary String
                string Value = Data[i];

                Value = Value.Replace("\t", "");
                //Value = Value.Remove(0, ReturnCharacterFirstOccurence(Data[i]));

                // Add Node
                Nodes.Add(new Node(Value, TempDepth, Value));

            }

            if (Depth[0] > 0)
            {

                Console.WriteLine("FILE IS NOT IN CORRECT FORMAT");
                return null;

            }

            for (int i = 0; i < Nodes.Count; i++)
            {

                // If Depth is 0 then add to root node
                if (Nodes[i].Depth == 0)
                {

                    Node.ChangeParentNode(Nodes[i], NewTree.RootNode);

                }
                // If Depth is the same as previous node, add to that node's parent
                else if (Nodes[i].Depth == Nodes[i - 1].Depth)
                {

                    Node.ChangeParentNode(Nodes[i], Nodes[i -1].ParentNode);

                }
                // If Node depth is greater than previous node, add as a child to the last
                else if (Nodes[i].Depth > Nodes[i - 1].Depth)
                {

                    Node.ChangeParentNode(Nodes[i], Nodes[i - 1]);

                }
                // if node depth is less than previous, loop through node list and determine close lowest
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

        // Display the whole tree
        public static void DisplayTree(TreeGraph TreeToDisplay, ref List<string> Output)
        {

            DisplayNode(TreeToDisplay.RootNode, ref Output);

        }

        public static void AddNode(INode NewNode, string Identifier, string Value, TreeGraph Tree)
        {

            if (NewNode.IsReady && !IdentifierCheck(NewNode.Identifier, Tree.RootNode))
            {

                Node.ChangeParentNode(NewNode, GetNode(Identifier, Value, Tree));

            }

        }

        public static void AddNode(INode NewNode, INode NodeToAddTo, TreeGraph Tree)
        {

            if (NewNode.IsReady && !IdentifierCheck(NewNode.Identifier, Tree.RootNode))
            {

                Node.ChangeParentNode(NewNode, NodeToAddTo);

            }

        }

        public static void RemoveNode(INode NodeToRemove, TreeGraph Tree)
        {

            if (TreeGraph.GetNode(NodeToRemove.Identifier, NodeToRemove.Value, Tree) != null)
            {

                NodeToRemove.ParentNode.NodeChildren.Remove(NodeToRemove);        

            }

        }

        public static INode GetNode(string Identifier, string Value, TreeGraph Tree)
        {

            INode Node = null;

            CheckGetNode(Identifier, Value, Tree.RootNode, ref Node);

            if (Node != null)
            {

                return Node;
                
            }

            //
            Console.WriteLine("No matching node was found");
            return Node;

        }

        // Get Nodes that match the value
        public static List<INode> GetNodes(string Value, TreeGraph Tree)
        {

            List<INode> ListOfNodes = new List<INode>();

            CheckGetNodes(Value, Tree.RootNode, ref ListOfNodes);

            return ListOfNodes;

        }

        // Get the parent of the node
        public static List<INode> GetParent(INode Node)
        {

            List<INode> NodeOutput = new List<INode>();

            GetParentRoot(Node, ref NodeOutput);

            return NodeOutput;

        }

        #region Recursive Calls

        // Recursively go back up the tree until the root node
        private static void GetParentRoot(INode Node, ref List<INode> Nodes)
        {

            Nodes.Add(Node);
            if (Node.ParentNode != null)
            {

                GetParentRoot(Node.ParentNode, ref Nodes);

            }

        }

        // Recursively search nodes for the matching value
        private static void CheckGetNodes(string Value, INode Node, ref List<INode> Source)
        {

            if (Node.Value.Replace("\t", "") == Value)
            {

                Source.Add(Node);

            }
            if (Node.NodeChildren.Count != 0)
            {

                for (int i = 0; i < Node.NodeChildren.Count; i++)
                {

                    CheckGetNodes(Value, Node.NodeChildren[i], ref Source);

                }

            }

        }

        // Recursively search for node matching value and identifier
        private static void CheckGetNode(string Value, string Identifier, INode Node, ref INode FinalNode)
        {

            //Console.WriteLine(Node.Identifier + "," + Node.Value);
            if (Node.Value == Value)
            {

                FinalNode = Node;
               
            }
            else if (Node.NodeChildren.Count != 0)
            {

                for (int i = 0; i < Node.NodeChildren.Count; i++)
                {

                    CheckGetNode(Value, Identifier, Node.NodeChildren[i], ref FinalNode);

                }

            }

        }

        // Recursively check if identifier has been taken
        private static bool IdentifierCheck(string Identifer, INode Node)
        {

            if (Node.Identifier == Identifer)
            {

                return true;

            }
            else if (Node.NodeChildren.Count != 0)
            {

                for (int i = 0; i < Node.NodeChildren.Count; i++)
                {

                    return IdentifierCheck(Identifer, Node.NodeChildren[i]);
                    
                }
                
            }

            return false;

        }

        // Recursively display all nodes in a tree
        private static void DisplayNode(INode NodeToDisplay, ref List<string> Source)
        {

            if (NodeToDisplay.Value != "Root")
            {

                Source.Add(NodeToDisplay.Value);
                Console.WriteLine(ReturnStringDepth(NodeToDisplay.Depth) + NodeToDisplay.Value);

            }
            if (NodeToDisplay.NodeChildren.Count != 0)
            {

                for (int i = 0; i < NodeToDisplay.NodeChildren.Count; i++)
                {

                    DisplayNode(NodeToDisplay.NodeChildren[i], ref Source);

                }

            }

        }

        #endregion

        #region String Parsing Functions

        // Return the depth of the read string
        private static string ReturnStringDepth(float Depth)
        {

            string StringDepth = string.Empty;

            //
            float Tabs = Depth;
            float Spaces = Tabs - (int)Tabs;
            Tabs -= Spaces;

            for (int i = 0; i < Tabs; i++)
            {

                StringDepth += "\t";

            }

            for (float i = 0; i < Spaces; i += 0.125f)
            {

                StringDepth += " ";

            }

            return StringDepth;

        }

        // Return the index of the first character
        // occurence of a character that is not a space or a tab
        private static int ReturnCharacterFirstOccurence(string Character)
        {

            int j = 0;

            for (int i = 0; i < Character.Length; i++)
            {

                if (Character[i] != '\t' || Character[i] != ' ')
                {

                    j = i;
                    break;

                }

            }

            return j;

        }

        #endregion

    }

}
