using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Power
{
    public static class EngineHelper
    {
        public static readonly string LabelParamsKey = "PE_LabelParams";
        public static readonly string ViewParametersKey = "PE_ViewParametersKey";
        public static readonly string PageModelKey = "PE_PageModel";
        public static readonly string GenerateStaticHtmlFlagKey = "PE_GenerateStaticHtml";
        public static readonly string GenerateStaticHtmlFileDirectory = "StaticHtmlFile";
        public static readonly string DefaultNoDataMessage = "没有任何数据！";
        public static readonly string DefaultListLinkOpenType = "_self";
        public static readonly int DefaultListTitleLength = 10;
        public static readonly string AjaxLabelController = "Ajax";
        public static readonly string AjaxLabelAction = "AjaxLabel";
        public static readonly string AjaxLabelPath = "/" + EngineHelper.AjaxLabelController + "/" + EngineHelper.AjaxLabelAction;
        public static readonly Regex SqlKeywordRegex = new Regex("(SELECT|UPDATE|INSERT|DELETE|DECLARE|@|EXEC|DBCC|ALTER|DROP|CREATE|BACKUP|IF|ELSE|END|AND|OR|ADD|SET|OPEN|CLOSE|USE|BEGIN|RETUN|AS|GO|EXISTS|KILL|&)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        public static string FilterSqlKeyword(string content)
        {
            string result;
            if (string.IsNullOrEmpty(content))
            {
                result = string.Empty;
            }
            else
            {
                bool flag = false;
                if (EngineHelper.SqlKeywordRegex.IsMatch(content))
                {
                    content = EngineHelper.SqlKeywordRegex.Replace(content, string.Empty);
                    flag = true;
                }
                if (flag)
                {
                    EngineHelper.FilterSqlKeyword(content);
                }
                result = content;
            }
            return result;
        }
    }
}
