using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeGraph
{
    public interface INode<T>
    {

        int Depth { get; }
        int Height { get; }
        string NodeName { get; }
        T Nodevalue { get; }
        string KeyIdentifier { get; }
        INode<T> ParentNode { get; }
        List<INode<T>> NodeChildren { get; }
        void AddNodeChild(INode<T> NewNode);
        void ChangeParentNode(INode<T> Parent);
        void RemoveNodeChild(INode<T> RemoveNode);
        void RemoveNodeChild(string Name);
        void CalculateDepth();
        void CalculateHeight();

    }
}
