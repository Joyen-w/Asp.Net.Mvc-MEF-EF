using DAL.Config;
using DAL.Properties;
using DAL.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class GridColumnAttribute : Attribute, IMetadataAware
    {
        private readonly List<string> dirtyList = new List<string>();
        private Align align;
        private int width;
        private PredefinedFormatter formatter;
        private string formatOption;
        private bool sortable;
        private string selectOption;
        private string label;
        private bool key;
        private bool columnFixed;
        private bool hidden;
        private int order;
        private string link;
        private string complexModelInclude;
        private string complexModelLink;
        private bool treeField;
        private bool visited;
        public Align Align
        {
            get
            {
                return this.align;
            }
            set
            {
                this.align = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(Align).GetProperty("Align"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<Align>>(proerty, new ParameterExpression[0])));
            }
        }
        public bool Key
        {
            get
            {
                return this.key;
            }
            set
            {
                this.key = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(bool).GetProperty("Key"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<bool>>(proerty, new ParameterExpression[0])));
            }
        }
        public bool Fixed
        {
            get
            {
                return this.columnFixed;
            }
            set
            {
                this.columnFixed = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(bool).GetProperty("Fixed"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<bool>>(proerty, new ParameterExpression[0])));
            }
        }
        public string FormatOption
        {
            get
            {
                return this.formatOption;
            }
            set
            {
                this.formatOption = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("FormatOption"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public PredefinedFormatter Formatter
        {
            get
            {
                return this.formatter;
            }
            set
            {
                this.formatter = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(PredefinedFormatter).GetProperty("Formatter"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<PredefinedFormatter>>(proerty, new ParameterExpression[0])));
            }
        }
        public bool Hidden
        {
            get
            {
                return this.hidden;
            }
            set
            {
                this.hidden = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(bool).GetProperty("Hidden"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<bool>>(proerty, new ParameterExpression[0])));
            }
        }
        public string Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("Label"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public int Order
        {
            get
            {
                return this.order;
            }
            set
            {
                this.order = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("Order"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<int>>(proerty, new ParameterExpression[0])));
            }
        }
        public string SelectOption
        {
            get
            {
                return this.selectOption;
            }
            set
            {
                this.selectOption = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("SelectOption"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public bool Sortable
        {
            get
            {
                return this.sortable;
            }
            set
            {
                this.sortable = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("Sortable"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("Width"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<int>>(proerty, new ParameterExpression[0])));
            }
        }
        public string Link
        {
            get
            {
                return this.link;
            }
            set
            {
                this.link = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("Link"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public string ComplexModelInclude
        {
            get
            {
                return this.complexModelInclude;
            }
            set
            {
                this.complexModelInclude = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("ComplexModelInclude"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public string ComplexModelLink
        {
            get
            {
                return this.complexModelLink;
            }
            set
            {
                this.complexModelLink = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("ComplexModelLink"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<string>>(proerty, new ParameterExpression[0])));
            }
        }
        public bool TreeField
        {
            get
            {
                return this.treeField;
            }
            set
            {
                this.treeField = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("TreeField"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<bool>>(proerty, new ParameterExpression[0])));
            }
        }
        public bool Visited
        {
            get
            {
                return this.visited;
            }
            set
            {
                this.visited = value;
                ConstantExpression constantExpression = Expression.Constant(this, typeof(GridColumnAttribute));
                Expression proerty = Expression.Property(constantExpression, typeof(string).GetProperty("Visited"));
                this.dirtyList.Add(GetPropertyName(Expression.Lambda<Func<bool>>(proerty, new ParameterExpression[0])));
            }
        }
        public GridColumnAttribute()
        {
        }
        public GridColumnAttribute(string labelName)
        {
            this.Label = labelName;
        }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            Check.NotNull(metadata, "metadata");
            if (this.IsDirty(Expression.Lambda<Func<Align>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Align")), new ParameterExpression[0])))
            {
                this.AdditionalValue(metadata, "GridColumn.Align", this.Align);
            }
            if (this.IsDirty(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Width")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Width", this.Width);
            }
            if (this.IsDirty(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Label")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Label", this.Label);
            }
            if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Key")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Key", this.Key);
            }
            if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Fixed")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Fixed", this.Fixed);
            }
            if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Hidden")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Hidden", this.Hidden);
            }
            if (this.IsDirty(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("ComplexModelLink")), new ParameterExpression[0])))
            {
                this.CheckComplexModelAttribute(metadata, this.ComplexModelLink, "ComplexModelLink");
            }
            if (string.IsNullOrWhiteSpace(this.ComplexModelInclude))
            {
                if (metadata.IsComplexType)
                {
                    Type type = metadata.ModelType;
                    if (type.IsGenericType)
                    {
                        type = type.GenericTypeArguments[0].UnderlyingSystemType;
                    }
                    object obj = type.GetCustomAttributes(typeof(DisplayColumnAttribute), true).FirstOrDefault();
                    if (obj != null)
                    {
                        string displayColumn = ((DisplayColumnAttribute)obj).DisplayColumn;
                        this.ComplexModelInclude = displayColumn;
                    }
                }
            }
            if (this.IsDirty(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("ComplexModelInclude")), new ParameterExpression[0])))
            {
                this.CheckComplexModelAttribute(metadata, this.ComplexModelInclude, "ComplexModelInclude");
                if (Array.IndexOf(metadata.ModelType.GetInterfaces(), typeof(IEnumerable)) > -1 || metadata.ModelType.IsSubclassOf(typeof(IEnumerable)))
                {
                    this.AdditionalValue(metadata, "GridColumn.Formatter", PredefinedFormatter.Collection.ToString());
                }
                else
                {
                    this.AdditionalValue(metadata, "GridColumn.Formatter", PredefinedFormatter.ComplexModel.ToString());
                }
                string text = this.complexModelInclude.Split(new char[]
                {
                    ','
                }, StringSplitOptions.RemoveEmptyEntries)[0];
                string additionalValue;
                if (string.IsNullOrEmpty(this.ComplexModelLink))
                {
                    additionalValue = "{showFieldName:'" + text + "'}";
                }
                else
                {
                    Dictionary<string, string> dictionary = this.BuildLinkFormatOption(this.ComplexModelLink, false);
                    dictionary.Add("showFieldName", text);
                    dictionary.Add("isLink", "true");
                    if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Visited")), new ParameterExpression[0])) && this.Visited)
                    {
                        dictionary.Add("visited", "true");
                    }
                    this.BaseLinkUrlFormatter(dictionary);
                    additionalValue = JsonConvert.SerializeObject(dictionary);
                }
                this.AdditionalValue(metadata, "GridColumn.FormatOptions", additionalValue);
                metadata.AdditionalValues.Add("GridColumn.ComplexModelInclude", this.ComplexModelInclude);
            }
            if (this.IsDirty(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Link")), new ParameterExpression[0])))
            {
                Dictionary<string, string> dictionary = this.BuildLinkFormatOption(this.Link, true);
                this.BaseLinkUrlFormatter(dictionary);
                if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Visited")), new ParameterExpression[0])) && this.Visited)
                {
                    dictionary.Add("visited", "true");
                }
                string additionalValue = JsonConvert.SerializeObject(dictionary);
                this.AdditionalValue(metadata, "GridColumn.Formatter", PredefinedFormatter.LocalLink.ToString());
                this.AdditionalValue(metadata, "GridColumn.FormatOptions", additionalValue);
            }
            if (this.IsDirty(Expression.Lambda<Func<PredefinedFormatter>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Formatter")), new ParameterExpression[0])))
            {
                this.AdditionalValue(metadata, "GridColumn.Formatter", this.Formatter.ToString());
                if (this.Formatter == PredefinedFormatter.ShowLink || this.Formatter == PredefinedFormatter.LocalLink)
                {
                    if (this.IsDirty(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("FormatOption")), new ParameterExpression[0])))
                    {
                        this.FormatOption = this.BaseLinkUrlFormatter(this.FormatOption);
                    }
                }
                if (this.Formatter.Equals(PredefinedFormatter.Select))
                {
                    metadata.AdditionalValues.Add("GridColumn.EditType", "select");
                    metadata.AdditionalValues.Add("GridColumn.EditOptions", string.Format("{{value: '{0}'}}", this.SelectOption));
                }
            }
            if (this.IsDirty(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("FormatOption")), new ParameterExpression[0])))
            {
                this.AdditionalValue(metadata, "GridColumn.FormatOptions", this.FormatOption);
            }
            if (this.IsDirty(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Order")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Order", this.Order);
            }
            if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("Sortable")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.Sortable", this.Sortable);
            }
            if (this.IsDirty(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(GridColumnAttribute)), typeof(string).GetProperty("TreeField")), new ParameterExpression[0])))
            {
                metadata.AdditionalValues.Add("GridColumn.TreeFiled", this.TreeField);
            }
            metadata.AdditionalValues["GridColumn.IsGridColumn"] = true;
        }
        private static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            MemberExpression memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }
        private string BaseLinkUrlFormatter(string options)
        {
            string result;
            if (string.IsNullOrEmpty(options))
            {
                result = string.Empty;
            }
            else
            {
                Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(options);
                this.BaseLinkUrlFormatter(dictionary);
                result = JsonConvert.SerializeObject(dictionary);
            }
            return result;
        }
        private void BaseLinkUrlFormatter(Dictionary<string, string> options)
        {
            if (options.ContainsKey("baseLinkUrl"))
            {
                string optionsStr;
                if (options.TryGetValue("baseLinkUrl", out optionsStr))
                {
                    Regex regex = new Regex(Resources.WebUrlAttribute_RegexPattern);
                    Match match = regex.Match(optionsStr);
                    if (match.Success)
                    {
                        this.AddLinkTargetProperty(options, "_blank");
                    }
                    else
                    {
                        string text2 = optionsStr.Substring(0, 2);
                        Uri url = HttpContext.Current.Request.Url;
                        string text3 = url.IsDefaultPort ? string.Empty : (":" + Convert.ToString(url.Port, CultureInfo.InvariantCulture));
                        string text4 = VirtualPathUtility.AppendTrailingSlash(string.Concat(new string[]
                        {
                            url.Scheme,
                            Uri.SchemeDelimiter,
                            url.Host,
                            text3,
                            "/",
                            (HttpContext.Current.Request.ApplicationPath != null && HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath
                        }));
                        string text5 = text2;
                        if (text5 != null)
                        {
                            if (!(text5 == "~/"))
                            {
                                if (!(text5 == "!/"))
                                {
                                    if (text5 == "$/")
                                    {
                                        text4 += VirtualPathUtility.AppendTrailingSlash(string.Empty);
                                        optionsStr = optionsStr.Replace("$/", text4);
                                        options["baseLinkUrl"] = optionsStr;
                                        this.AddLinkTargetProperty(options, "_blank");
                                    }
                                }
                                else
                                {
                                    text4 += VirtualPathUtility.AppendTrailingSlash(SecurityConfig.Instance.ManagePath);
                                    optionsStr = optionsStr.Replace("!/", text4);
                                    options["baseLinkUrl"] = optionsStr;
                                }
                            }
                            else
                            {
                                optionsStr = optionsStr.Replace("~/", text4);
                                options["baseLinkUrl"] = optionsStr;
                                this.AddLinkTargetProperty(options, "_blank");
                            }
                        }
                    }
                }
            }
        }
        private void AddLinkTargetProperty(Dictionary<string, string> options, string property)
        {
            options["target"] = property;
        }
        private bool IsDirty<T>(Expression<Func<T>> expression)
        {
            return this.dirtyList.Contains(GetPropertyName(expression));
        }
        private void CheckComplexModelAttribute(ModelMetadata metadata, string attributeValue, string attributeName)
        {
            if (!metadata.IsComplexType)
            {
                if (!string.IsNullOrEmpty(attributeValue))
                {
                    throw new InvalidOperationException(metadata.PropertyName + "属性不是复杂类型，不能在数据特性中标注" + attributeName + "特性。");
                }
            }
        }
        private Dictionary<string, string> BuildLinkFormatOption(string linkUrl, bool isHandlerIdParam = true)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            int num = linkUrl.IndexOf('?');
            string value = linkUrl;
            if (num > 0)
            {
                value = linkUrl.Substring(0, num);
                string text = linkUrl.Substring(num, linkUrl.Length - num);
                if (isHandlerIdParam)
                {
                    dictionary.Add("isAppendIdParam", "true");
                    if (text.ToLower().Contains("?id=") || text.ToLower().Contains("&id="))
                    {
                        dictionary["isAppendIdParam"] = "false";
                    }
                }
                else
                {
                    dictionary["isAppendIdParam"] = "false";
                }
                dictionary.Add("addParam", text.TrimStart(new char[]
                {
                    '?'
                }));
            }
            dictionary.Add("baseLinkUrl", value);
            return dictionary;
        }
        private void AdditionalValue(ModelMetadata metadata, string additionalKey, object additionalValue)
        {
            metadata.AdditionalValues[additionalKey] = additionalValue;
        }
    }
}
