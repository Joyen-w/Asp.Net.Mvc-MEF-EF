using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAL.Power
{
    public class PowerUrlHelper
    {
        internal static ViewContext ViewContext
        {
            get;
            set;
        }
        internal static ViewDataDictionary ViewData
        {
            get
            {
                return PowerUrlHelper.ViewDataContainer.ViewData;
            }
        }
        internal static RouteCollection RouteCollection
        {
            get;
            set;
        }
        internal static IViewDataContainer ViewDataContainer
        {
            get;
            set;
        }
        public static string RelpaceParameter(string parameterName, object value)
        {
            HttpRequest request = HttpContext.Current.Request;
            string text = request.Url.Query;
            if (string.IsNullOrEmpty(text))
            {
                text = string.Format("?{0}={1}", parameterName, value);
            }
            else
            {
                if (text.Contains(parameterName))
                {
                    text = text.Replace(parameterName + "=" + request.QueryString[parameterName], parameterName + "=" + value);
                }
                else
                {
                    text += string.Format("&{0}={1}", parameterName, value);
                }
            }
            string result;
            if (request.RequestContext.HttpContext.Request.Url != null)
            {
                result = request.RequestContext.HttpContext.Request.Url.AbsolutePath + text;
            }
            else
            {
                result = text;
            }
            return result;
        }
        public string Url()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        public string Url(string actionName)
        {
            return this.Url(null, actionName, null, null);
        }
        public string Url(string actionName, object routeValues)
        {
            return this.Url(null, actionName, null, new RouteValueDictionary(routeValues));
        }
        public string Url(string actionName, RouteValueDictionary routeValues)
        {
            return this.Url(null, actionName, null, routeValues);
        }
        public string Url(string actionName, string controllerName)
        {
            return this.Url(null, actionName, controllerName, null);
        }
        public string Url(string actionName, string controllerName, object routeValues)
        {
            return this.Url(null, actionName, controllerName, new RouteValueDictionary(routeValues));
        }
        public string Url(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return this.Url(null, actionName, controllerName, routeValues);
        }
        public string Url(string actionName, string controllerName, object routeValues, string protocol)
        {
            return UrlHelper.GenerateUrl(null, actionName, controllerName, protocol, null, null, new RouteValueDictionary(routeValues), PowerUrlHelper.RouteCollection, PowerUrlHelper.ViewContext.RequestContext, true);
        }
        public string Url(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName)
        {
            return UrlHelper.GenerateUrl(null, actionName, controllerName, protocol, hostName, null, routeValues, PowerUrlHelper.RouteCollection, PowerUrlHelper.ViewContext.RequestContext, true);
        }
        private string Url(string routeName, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            bool flag = PowerUrlHelper.ViewData.GetViewDataInfo(EngineHelper.GenerateStaticHtmlFlagKey) != null;
            string result;
            if (flag)
            {
                if (routeValues.ContainsKey("ContentId"))
                {
                    string text = routeValues["ContentId"].ToString();
                    string text2 = string.Concat(new string[]
                    {
                        "/",
                        EngineHelper.GenerateStaticHtmlFileDirectory,
                        "/Article_",
                        text,
                        ".html"
                    });
                    result = text2;
                    return result;
                }
                if (routeValues.ContainsKey("CategoryId"))
                {
                    string text3 = routeValues["CategoryId"].ToString();
                    string text4 = string.Concat(new string[]
                    {
                        "/",
                        EngineHelper.GenerateStaticHtmlFileDirectory,
                        "/Category_",
                        text3,
                        ".html"
                    });
                    result = text4;
                    return result;
                }
            }
            result = UrlHelper.GenerateUrl(routeName, actionName, controllerName, routeValues, PowerUrlHelper.RouteCollection, PowerUrlHelper.ViewContext.RequestContext, true);
            return result;
        }
    }
}
