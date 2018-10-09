using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraph
{
    public class TreeGraph
    {

        public int MaximumHeight;
        public INode DefaultRootNode;
        public List<INode> Nodes;

        public TreeGraph()
        {

            DefaultRootNode = new Node("Root", "RootNode", "Root", null);
            Nodes = new List<INode>();

        }

        public TreeGraph(INode Root)
        {

            DefaultRootNode = Root;
            Nodes = new List<INode>();

        }

        public string[] LoadText(string Path)
        {

            return System.IO.File.ReadAllLines(Path);

        }

        public void WriteOutLineFile(string Path)
        {

            //ystem.IO.File.WriteAllLines(Path, Lines);

        }

        public void AddNode(INode NewNode)
        {

            Nodes.Add(NewNode);
            NewNode.ChangeParentNode(DefaultRootNode);

        }

        public void AddNodeToNodeChild(string NodeName, INode NewNode)
        {

            for (int i = 0; i < Nodes.Count; i ++)
            {

                if (Nodes[i].NodeName == NodeName)
                {

                    AddNodeToNodeChild(Nodes[i], NewNode);
                }

            }

        }

        public void AddNodeToNodeChild(string NodeName, string Value, INode NewNode)
        {

            for (int i = 0; i < Nodes.Count; i++)
            {

                if (Nodes[i].NodeName == NodeName && Nodes[i])
                {

                    AddNodeToNodeChild(Nodes[i], NewNode);
                }

            }

        }

        public void AddNodeToNodeChild(string NodeName, string Value, string Key, INode NewNode)
        {

            for (int i = 0; i < Nodes.Count; i++)
            {

                if (Nodes[i].NodeName == NodeName)
                {

                    AddNodeToNodeChild(Nodes[i], NewNode);
                }

            }

        }

        public void AddNodeToNodeChild(INode ChildNode, INode NewNode)
        {

            ChildNode.AddNodeChild(NewNode);
            ChildNode.ChangeParentNode(NewNode);

        }

        public void RemoveNodeToNodeChild(INode ChildNode, INode NodeToRemove)
        {

            ChildNode.RemoveNodeChild(NodeToRemove);

        }

        public void RemoveNode(INode NodeToRemove)
        {

            if (Nodes.Contains(NodeToRemove))
            {

                Nodes.Remove(NodeToRemove);

            }

        }

        public void RemoveNode(string Name)
        {

            int NodeIndex = -1;

            for (int i = 0; i < Nodes.Count; i++)
            {

                if (Nodes[i].NodeName == Name)
                {

                    NodeIndex = i;

                }

            }

            if (NodeIndex != -1)
            {


                Nodes.RemoveAt(NodeIndex);

            }

        }

        public List<INode> GetNodes(string Nodes)
        {

            return new List<INode>();

        }

        public List<INode> GetNodes(INode Nodes)
        {

            List<INode> NodesOfTheSameName;

            return new List<INode>();

        }

        public List<INode> GetChildrenNodes(INode Node)
        {

            return Node.NodeChildren;

        }


    }

}
