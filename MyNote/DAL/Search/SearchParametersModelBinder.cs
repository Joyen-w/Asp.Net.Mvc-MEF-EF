using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DAL.Search
{
    public class SearchParametersModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object result;
            try
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;
                int num = int.Parse(request["page"] ?? "1");
                num = ((num <= 0) ? 1 : num);
                int num2 = int.Parse(request["rows"] ?? "10");
                num2 = ((num2 <= 0) ? 10 : num2);
                string searchparameters = request["search"];
                SearchConstraint searchConstraint = (request.Unvalidated["filters"] == null) ? null : SearchConstraint.Create(request.Unvalidated["filters"]);
                SearchConstraint searchConstraint2 = new QueryStringRuleParser(searchparameters).ParserRules();
                if (searchConstraint2 != null)
                {
                    if (searchConstraint != null)
                    {
                        searchConstraint.Groups.Add(searchConstraint2);
                    }
                    else
                    {
                        searchConstraint = new SearchConstraint
                        {
                            GroupOperator = "AND"
                        };
                        searchConstraint.Groups.Add(searchConstraint2);
                    }
                }
                result = new PageSearchParameters
                {
                    PageIndex = num,
                    PageSize = num2,
                    SortColumn = request["sidx"] ?? string.Empty,
                    SortOrder = request["sord"] ?? "asc",
                    Where = searchConstraint,
                    QuerySearch = searchConstraint2,
                    NotPage = request["notPage"] != null && bool.Parse(request["notPage"]),
                    QueryLevel = int.Parse(request["queryLevel"] ?? "1"),
                    ExpandLevel = int.Parse(request["expandLevel"] ?? "1")
                };
            }
            catch
            {
                result = null;
            }
            return result;
        }
    }
}
