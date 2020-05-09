using Entity;
using Models.Entity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL.DBContext
{
    public class DbWecareContext : DbContext
    {
        #region /// 构造函数

        public DbWecareContext() : base("wecare_debug")
        {
           
        }

        #endregion /// 构造函数

        #region /// 属性}

        public DbSet<PY> py { get; set; }

        #endregion /// 属性
    }
}