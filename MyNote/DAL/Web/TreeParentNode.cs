using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Web
{
    public class TreeParentNode
    {
        public int TreeParentNodeId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public bool IsLeaf { get; set; }
        public string IconSkin { get; set; }
        public bool Enable { get; set; }
        public bool Visible { get; set; }
    }
}
