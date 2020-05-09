
using Entity.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("PY")]
    public  class PY : BaseEntity
    {
        [Key]
        public string HZ { get; set; }
        public string PYX { get; set; }
    }
}