﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    public interface INode
    {

        int Depth { get; }
        string Value { get; }
        INode ParentNode { get; set; }
        List<INode> NodeChildren { get; }

    }

}