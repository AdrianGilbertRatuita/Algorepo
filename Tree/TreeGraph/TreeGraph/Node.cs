using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraph
{
    public class Node<T> : INode
    {

        #region Properties
        //=======================================
        public int Depth { get; set; }

        public int Height { get; set; }

        public string NodeName { get; private set; }

        public T NodeValue { get; set; }

        public string KeyIdentifier { get; private set; }

        public INode<T> ParentNode { get; private set; }

        public List<INode<T>> NodeChildren { get; private set; }
        //=======================================
        #endregion

        #region Constructors
        //=======================================
        public Node(string Name, T Value) :this(Name, Value, "", null) { }
        public Node(string Name, T Value, string Key) :this(Name, Value, Key, null) { }
        public Node(string Name, T Value, string Key, INode<T> Parent)
        {

            NodeName = Name;
            NodeValue = Value;
            KeyIdentifier = Key;
            ParentNode = Parent;

        }
        //=======================================
        #endregion

        #region Functions
        //=======================================
        public void ChangeParentNode(INode<T> NewParent)
        {
            
            if (ParentNode != null)
            {

                ParentNode.RemoveNodeChild(this);

            }
            ParentNode = NewParent;

            //
            CalculateDepth();
            CalculateHeight();

        }

        public void CalculateDepth()
        {

            int Count = 0;
            INode<T> Checker = ParentNode;
            List<INode<T>> ParentNodeList = new List<INode<T>>();

            while (Checker != null || Checker.NodeName == "Root")
            {

                Checker = Checker.ParentNode;
                GetParentNode(Checker);
                Count++;

            }

            Depth = Count;

        }

        public void CalculateHeight()
        {

            int Count = 0;  


        }
        public INode<T> GetParentNode(INode<T> Node)
        {

            return Node.ParentNode;

        }


        public void AddNodeChild(INode<T> NewNode)
        {

            NewNode.ChangeParentNode(NewNode);
            NodeChildren.Add(NewNode);
            
        }

        public void RemoveNodeChild(INode<T> RemoveNode)
        {
            
            if (NodeChildren.Contains(RemoveNode))
            {

                RemoveNodeChild(RemoveNode.NodeName);

            }

        }

        public void RemoveNodeChild(string Name)
        {

            int NodeIndex = -1;

            for (int i = 0; i < NodeChildren.Count; i++)
            {

                if (NodeChildren[i].NodeName == Name)
                {

                    NodeIndex = i;

                }

            }

            if (NodeIndex != -1)
            {

                NodeChildren[NodeIndex].ChangeParentNode(null);
                NodeChildren.RemoveAt(NodeIndex);

            }

        }

        //=======================================
        #endregion

    }
}
