using DAL.DBContext;
using DAL.IDal;
using Entity;
using Models.Entity;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace DAL.Dal
{
    //[Export(typeof(IUserDal))]
    //public partial class UserDal : Base<UserInfoSet>, IUserDal
    //{
    //    /// <summary>
    //    /// 用户登录
    //    /// </summary>
    //    /// <param name="userName">帐号</param>
    //    /// <param name="userPwd">密码</param>
    //    /// <returns></returns>
    //    public UserInfoSet UserLogin(string userCode, string userPwd)
    //    {
    //        using (DbContextBase db = new DbContextBase())
    //        {
    //            var resUserModel = db.UserInfoSet.FirstOrDefault(a => a.UserCode == userCode && a.UserPwd == userPwd);

    //            if (resUserModel != null)
    //            {
    //                resUserModel.LoginDate = DateTime.Now.ToString();

    //                db.SaveChanges();
    //            }

    //            return resUserModel;
    //        }
    //    }
    //}
}