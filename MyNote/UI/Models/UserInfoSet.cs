using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UI.Models
{
    [Table("UserInfoSet")]
    public partial class UserInfoSet 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserCode { get; set; }
        public string UserPwd { get; set; }
        public string RegisterDate { get; set; }
        public string LoginDate { get; set; }
    }
}