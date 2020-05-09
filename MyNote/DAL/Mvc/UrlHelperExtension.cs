using DAL.Power;
using DAL.Utilities;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAL.Mvc
{
    public static class UrlHelperExtension
    {
        public const string AdminHomeRouteName = "Admin.Home";
        public const string AdminCommonRouteName = "Admin.Common";
        public const string AdminRouteNameFormat = "Admin.{0}";
        public const string FrontRouteNameFormat = "Front.{0}";
        public static string AdminAction(this UrlHelper urlHelper, string actionName)
        {
            return urlHelper.AdminAction(actionName, null);
        }
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return urlHelper.AdminAction(actionName, controllerName, null);
        }
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName, string areaName)
        {
            return urlHelper.AdminAction(actionName, controllerName, areaName, null);
        }
        public static string AdminAction(this UrlHelper urlHelper, string actionName, object routeValues)
        {
            return urlHelper.AdminAction(actionName, null, routeValues);
        }
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName, object routeValues)
        {
            return urlHelper.AdminAction(actionName, controllerName, null, routeValues);
        }
        public static string AdminAction(this UrlHelper urlHelper, string actionName, string controllerName, string areaName, object routeValues)
        {
            return UrlHelper.GenerateUrl(UrlHelperExtension.GetAdminRouteName(areaName), actionName, controllerName, (routeValues as RouteValueDictionary) ?? ((RouteValueDictionary)HtmlHelper.ObjectToDictionary(routeValues)), urlHelper.RouteCollection, urlHelper.RequestContext, true);
        }
        public static string AdminHomeAction(this UrlHelper urlHelper, string actionName)
        {
            return UrlHelper.GenerateUrl("Admin.Home", actionName, null, null, urlHelper.RouteCollection, urlHelper.RequestContext, false);
        }
        public static string AdminHomeAction(this UrlHelper urlHelper, string actionName, object routeValues)
        {
            return UrlHelper.GenerateUrl("Admin.Home", actionName, null, (routeValues as RouteValueDictionary) ?? ((RouteValueDictionary)HtmlHelper.ObjectToDictionary(routeValues)), urlHelper.RouteCollection, urlHelper.RequestContext, false);
        }
        public static string AdminCommonAction(this UrlHelper urlHelper, string actionName, string controller)
        {
            return urlHelper.AdminCommonAction(actionName, controller, null);
        }
        public static string AdminCommonAction(this UrlHelper urlHelper, string actionName, string controller, object routeValues)
        {
            return UrlHelper.GenerateUrl("Admin.Common", actionName, controller, (routeValues as RouteValueDictionary) ?? ((RouteValueDictionary)HtmlHelper.ObjectToDictionary(routeValues)), urlHelper.RouteCollection, urlHelper.RequestContext, false);
        }
        public static string GetAdminRouteName()
        {
            return UrlHelperExtension.GetAdminRouteName(null);
        }
        public static string GetAdminRouteName(string areaName)
        {
            return string.Format("Admin.{0}", areaName ?? HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"]);
        }
        public static string GetAdminLoginUrl()
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext).AdminHomeAction("Login");
        }
        public static string GetContentUrl(this PowerUrlHelper urlHelper, string path)
        {
            Check.NotNull<PowerUrlHelper>(urlHelper, "urlHelper");
            return PathHelper.GetContentUrl(path);
        }
        public static string ReplaceUploadPath(this PowerUrlHelper urlHelper, string body)
        {
            Check.NotNull<PowerUrlHelper>(urlHelper, "urlHelper");
            return PathHelper.ReplaceUploadPath(body);
        }
        public static string GetUploadPath(this PowerUrlHelper urlHelper, string path)
        {
            Check.NotNull<PowerUrlHelper>(urlHelper, "urlHelper");
            return PathHelper.GetUploadPath(path);
        }
        public static string AddCurrentHash(string url)
        {
            string text = HttpContext.Current.Request["state"];
            if (!string.IsNullOrWhiteSpace(text) && !Regex.IsMatch(text, "#"))
            {
                url = url.Split(new char[]
                {
                    '#'
                })[0] + "##" + text;
            }
            return url;
        }
    }
}
