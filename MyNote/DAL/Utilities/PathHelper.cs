using DAL.Config;
using DAL.Mvc;
using DAL.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DAL.Utilities
{
    public class PathHelper
    {
        public const string ManagePath = "~/Admin/";
        public const string ManagePathReplaceSymbol = "!/";
        public const string UploadPathReplaceSymbol = "$/";
        public static readonly string UploadVirtualPath = string.Format("~/{0}/", GlobalUploadConfig.Instance.UploadDirectory);
        public static string GetContentUrl(string path)
        {
            string result;
            if (string.IsNullOrEmpty(path))
            {
                result = string.Empty;
            }
            else
            {
                UrlHelper urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                Regex regex = new Regex(Resources.WebUrlAttribute_RegexPattern, RegexOptions.IgnoreCase);
                Match match = regex.Match(path);
                bool flag = false;
                if (match.Success)
                {
                    result = path;
                }
                else
                {
                    if (path.StartsWith("!/"))
                    {
                        path = path.Replace("!/", "~/Admin/");
                    }
                    else
                    {
                        if (path.StartsWith("$/"))
                        {
                            flag = true;
                            path = path.Replace("$/", PathHelper.UploadVirtualPath);
                        }
                        else
                        {
                            if (path.Trim().StartsWith("{") && path.Trim().EndsWith("}"))
                            {
                                PowerRouteData powerRouteData = JsonConvert.DeserializeObject<PowerRouteData>(path);
                                path = urlHelper.RouteUrl(powerRouteData.RouteName, powerRouteData.RouteValues);
                            }
                        }
                    }
                    if (path != null)
                    {
                        if (!path.StartsWith("~/") && !path.StartsWith("/"))
                        {
                            path = "~/" + path;
                        }
                        int num = path.LastIndexOf("?", System.StringComparison.Ordinal);
                        if (num > 0)
                        {
                            string text = path.Substring(0, num);
                            string text2 = path.Substring(num);
                            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                            string[] array = text2.Split(new char[]
                            {
                                '&'
                            });
                            for (int i = 0; i < array.Length; i++)
                            {
                                string text3 = array[i];
                                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                                string[] array2 = text3.Split(new char[]
                                {
                                    '='
                                });
                                stringBuilder.Append(array2[0].ToLower());
                                stringBuilder.Append("=");
                                stringBuilder.Append(array2[1]);
                                list.Add(stringBuilder.ToString());
                            }
                            path = text.ToLower() + string.Join("&", list);
                        }
                        else
                        {
                            path = path.ToLower();
                        }
                        path = urlHelper.Content(path);
                    }
                    result = (flag ? (GlobalUploadConfig.Instance.UploadPathPerfix + path) : path);
                }
            }
            return result;
        }
        public static string ReplaceUploadPath(string body)
        {
            string result;
            if (!string.IsNullOrEmpty(body))
            {
                string str = VirtualPathUtility.ToAbsolute(PathHelper.UploadVirtualPath);
                result = Regex.Replace(body, "((src|href)\\s*=\\s*(\"|'))\\$\\/", "$1" + GlobalUploadConfig.Instance.UploadPathPerfix + str);
            }
            else
            {
                result = body;
            }
            return result;
        }
        public static string GetUploadPath(string path)
        {
            string result;
            if (!string.IsNullOrEmpty(path))
            {
                string str = VirtualPathUtility.ToAbsolute(PathHelper.UploadVirtualPath);
                result = Regex.Replace(path, "^\\$", GlobalUploadConfig.Instance.UploadPathPerfix + str);
            }
            else
            {
                result = path;
            }
            return result;
        }
    }
}
