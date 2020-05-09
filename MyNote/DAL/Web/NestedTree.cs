using DAL.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Web
{
    public class NestedTree
    {
        private bool isLeaf;
        [NotMapped]
        public virtual int? ParentId { get; set; }
        [Unmodifiable, GridColumn(Hidden = true), Integer("节点左标识", ShowForDisplay = false, ShowForEdit = false)]
        public int Left { get; set; }
        [Integer("节点右标识", ShowForDisplay = false, ShowForEdit = false), Unmodifiable, GridColumn(Hidden = true)]
        public int Right { get; set; }
        [Unmodifiable, GridColumn(Hidden = true), Integer("节点在树中的深度", ShowForDisplay = false, ShowForEdit = false)]
        public int Depth { get; set; }
        [NotMapped, ScaffoldColumn(false), GridColumn(TreeField = true)]
        public bool IsLeaf
        {
            get { return this.isLeaf ? this.isLeaf : (this.Left == this.Right - 1); }
            set { this.isLeaf = value; }
        }
        [NotMapped, GridColumn(Hidden = true), ScaffoldColumn(false)]
        public bool Expanded { get; set; }
        public virtual bool ComparisonObject(NestedTree nestedTree)
        {
            throw new System.NotSupportedException();
        }
    }
}
