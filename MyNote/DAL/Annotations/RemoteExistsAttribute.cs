using DAL.Mvc;
using DAL.Properties;
using DAL.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace DAL.Annotations
{
    public sealed class RemoteExistsAttribute : ValidationAttribute, IClientValidatable
    {
        public string HttpMethod { get; set; }
        public string Action { get; set; }
        public string OtherProperty { get; set; }
        public RemoteExistsAttribute() : this("Exists")
        {
        }
        public RemoteExistsAttribute(string action)
        {
            this.Action = action;
            base.ErrorMessage = Resources.RemoteExistsAttribute_ValidationError;
        }
        public override bool IsValid(object value)
        {
            return true;
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string text = string.Empty;
            using (IEnumerator<PropertyInfo> enumerator = (
                from propertyInfo in metadata.ContainerType.GetProperties()
                where propertyInfo.IsPrimaryKey()
                select propertyInfo).GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    PropertyInfo current = enumerator.Current;
                    text = current.Name;
                }
            }
            if (!string.IsNullOrEmpty(this.OtherProperty) && this.OtherProperty != text)
            {
                text = text + "," + this.OtherProperty;
            }
            yield return new ModelClientValidationRemoteRule(this.FormatErrorMessage(metadata.GetDisplayName()), this.GetUrl(context), this.HttpMethod, text);
            yield break;
        }
        private string GetUrl(ControllerContext controllerContext)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary["controller"] = controllerContext.RouteData.GetRequiredString("controller");
            routeValueDictionary["action"] = this.Action;
            return RouteCollectionExtensions.GetVirtualPathForArea(RouteTable.Routes, controllerContext.RequestContext, UrlHelperExtension.GetAdminRouteName(), routeValueDictionary).VirtualPath;
        }
    }
}
