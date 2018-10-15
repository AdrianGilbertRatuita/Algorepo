using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public class Node : INode
    {
        public float Depth { get; set; }
        public string Value { get; set;}
        public INode ParentNode { get; set; }
        public List<INode> NodeChildren { get; private set; }

        public Node(string _Value, float _Depth)
        {

            Depth = _Depth;
            Value = _Value;
            ParentNode = null;
            NodeChildren = new List<INode>();

        }
        public static void ChangeParentNode(INode Child, INode NewParent)
        {

            Child.ParentNode = NewParent;
            NewParent.NodeChildren.Add(Child);

        }

    }

}
