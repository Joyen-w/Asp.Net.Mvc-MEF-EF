using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL.Properties;
using DAL.Utilities;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public abstract class BaseAttribute : ValidationAttribute, IClientValidatable, IMetadataAware
    {
        public string DisplayName
        {
            get;
            private set;
        }
        public string Description
        {
            get;
            set;
        }
        public string TemplateHint
        {
            get;
            set;
        }
        public string Watermark
        {
            get;
            set;
        }
        public int Order
        {
            get;
            set;
        }
        public bool ShowForDisplay
        {
            get;
            set;
        }
        public bool ShowForEdit
        {
            get;
            set;
        }
        public bool ReadOnly
        {
            get;
            set;
        }
        public bool Hidden
        {
            get;
            set;
        }
        public bool RequiredFlag
        {
            get;
            set;
        }
        protected bool CustomDefaultValue
        {
            get;
            set;
        }
        protected string ColumnLabel
        {
            get;
            set;
        }
        protected Align ColumnAlign
        {
            get;
            set;
        }
        protected int ColumnWidth
        {
            get;
            set;
        }
        protected string ColumnFormatter
        {
            get;
            set;
        }
        protected string ColumnFormatOptions
        {
            get;
            set;
        }
        protected bool ColumnFixed
        {
            get;
            set;
        }
        protected bool ColumnHidden
        {
            get;
            set;
        }
        protected bool ColumnKey
        {
            get;
            set;
        }
        protected bool ColumnTitle
        {
            get;
            set;
        }
        protected bool ColumnSortable
        {
            get;
            set;
        }
        protected string FilterLabel
        {
            get;
            set;
        }
        protected string FilterItems
        {
            get;
            set;
        }
        protected ConditionalOperator SearchOperator
        {
            get;
            set;
        }
        protected BaseAttribute() : this(Resources.BaseAttribute_DisplayName)
        {
        }
        protected BaseAttribute(string displayName) : this(displayName, Resources.BaseAttribute_Invalid)
        {
        }
        protected BaseAttribute(string displayName, string errorMessage)
        {
            this.DisplayName = displayName;
            base.ErrorMessage = errorMessage;
            this.Order = 10000;
            this.ShowForDisplay = true;
            this.ShowForEdit = true;
            this.RequiredFlag = false;
            this.ColumnLabel = displayName;
            this.ColumnAlign = Align.Center;
            this.ColumnWidth = 150;
            this.ColumnSortable = true;
            this.FilterLabel = displayName;
            this.SearchOperator = ConditionalOperator.Equal;
        }
        public override bool IsValid(object value)
        {
            return true;
        }
        public virtual void OnMetadataCreated(ModelMetadata metadata)
        {
            Check.NotNull<ModelMetadata>(metadata, "metadata");
            this.MetadataAssist(metadata);
            metadata.DisplayName = this.DisplayName;
            metadata.Description = this.Description;
            metadata.TemplateHint = this.GetTemplateHint();
            metadata.ShowForDisplay = this.ShowForDisplay;
            metadata.ShowForEdit = this.ShowForEdit;
            metadata.Order = this.Order;
            metadata.Watermark = this.Watermark;
            metadata.IsReadOnly = this.ReadOnly;
            if (this.Hidden)
            {
                metadata.HideSurroundingHtml = true;
                metadata.TemplateHint = "HiddenInput";
            }
            metadata.AdditionalValues.Add("RequiredFlag", this.RequiredFlag);
            metadata.AdditionalValues.Add("Column.Label", this.ColumnLabel);
            metadata.AdditionalValues.Add("Column.Align", this.ColumnAlign);
            metadata.AdditionalValues.Add("Column.Width", this.ColumnWidth);
            metadata.AdditionalValues.Add("Column.Formatter", this.ColumnFormatter);
            metadata.AdditionalValues.Add("Column.FormatOptions", this.ColumnFormatOptions);
            metadata.AdditionalValues.Add("Column.Fixed", this.ColumnFixed);
            metadata.AdditionalValues.Add("Column.Hidden", this.ColumnHidden);
            metadata.AdditionalValues.Add("Column.Key", this.ColumnKey);
            metadata.AdditionalValues.Add("Column.Title", this.ColumnTitle);
            metadata.AdditionalValues.Add("Column.Order", this.Order);
            metadata.AdditionalValues.Add("Column.Sortable", this.ColumnSortable);
            metadata.AdditionalValues.Add("Filter.Label", this.DisplayName);
            metadata.AdditionalValues.Add("Filter.Items", this.FilterItems);
            metadata.AdditionalValues.Add("Search.Operators", this.SearchOperator.ToString());
            IDataSource dataSource = this as IDataSource;

            if (dataSource != null)
            {
                DataSourceHelper.IntoModelMetadata(metadata, dataSource.DataSource, "DataSource");
            }
        }
        public virtual System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield break;
        }
        protected virtual void MetadataAssist(ModelMetadata metadata)
        {
        }
        protected void SetFixedColumnWidth(int columnWidth)
        {
            this.ColumnWidth = columnWidth;
            this.ColumnFixed = true;
        }
        private string GetTemplateHint()
        {
            return string.IsNullOrEmpty(this.TemplateHint) ? base.GetType().Name.Replace("Attribute", string.Empty) : this.TemplateHint;
        }
    }
}
