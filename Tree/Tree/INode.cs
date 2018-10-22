using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public interface INode
    {

        float Depth { get; }
        string Value { get; }
        string Identifier { get; }
        bool IsReady { get; }
        INode ParentNode { get; set; }
        List<INode> NodeChildren { get; }

    }

}
