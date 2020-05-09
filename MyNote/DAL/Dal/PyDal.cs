using DAL.DBContext;
using DAL.IDal;
using Entity;
using Models.Entity;
using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace DAL.Dal
{
    //[Export(typeof(IPyDal))]
    //public partial class PyDal : Base<PY>, IPyDal
    //{
    //      /// <summary>
    //      /// 
    //      /// </summary>
    //      /// <returns></returns>
    //    public PY GetPY()
    //    {
    //        using (DbWecareContext db = new DbWecareContext())
    //        {
    //            var resUserModel = db.py.FirstOrDefault(a => a.HZ == "磅  ");

    //            return resUserModel;
    //        }
    //    }
    //}
}