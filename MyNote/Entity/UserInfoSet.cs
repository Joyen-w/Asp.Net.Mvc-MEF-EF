
using Entity.BaseEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    [Table("UserInfoSet")]
    public partial class UserInfoSet : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string UserPwd { get; set; }
        public string RegisterDate { get; set; }
        public string LoginDate { get; set; }
    }
}