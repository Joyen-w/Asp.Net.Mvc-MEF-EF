using DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UI.Models;

namespace UI.DataAccess
{
    public class UiDbContext : BaseDbContext<UiDbContext>
    {
        public DbSet<UserInfoSet> UserInfoSet { get; set; }
    }
}