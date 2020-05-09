using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Web
{
    public class TreeSetNode
    {
        public int TreeSetNodeId { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public IList<TreeSetNode> Children { get; set; }
        public string IconSkin { get; set; }
        public TreeSetNode()
        {
            this.Children = new List<TreeSetNode>();
        }
    }
}
