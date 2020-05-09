using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    public class ModelClientValidationDangerousRule : ModelClientValidationRule
    {
        public ModelClientValidationDangerousRule()
        {
            base.ValidationType = "dangerous";
            base.ErrorMessage = "true";
        }
    }
}
