using DAL.Properties;
using DAL.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class EnumAttribute : BaseAttribute, IDataSource
    {
        private int defaultValue;
        public int DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
            set
            {
                this.defaultValue = value;
                base.CustomDefaultValue = true;
            }
        }
        public SelectUIType UIType
        {
            get;
            private set;
        }
        public string DataSource { get; set; }
        public EnumAttribute() : this(Resources.EnumAttribute_DisplayName)
        {
        }
        public EnumAttribute(string displayName) : this(displayName, SelectUIType.ButtonSet)
        {
        }
        public EnumAttribute(string displayName, SelectUIType uiType) : base(displayName)
        {
            this.UIType = uiType;
            base.SetFixedColumnWidth(60);
            base.ColumnFormatter = PredefinedFormatter.LocalSelect.ToString();
        }
        public override void OnMetadataCreated(ModelMetadata metadata)
        {
            base.OnMetadataCreated(metadata);
            if (!(metadata.IsNullableValueType ? System.Nullable.GetUnderlyingType(metadata.ModelType) : metadata.ModelType).IsEnum)
            {
                throw new System.InvalidOperationException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.EnumAttribute_SettingError_EnumType, new object[]
                {
                    Resources.EnumAttribute_DisplayName,
                    metadata.PropertyName
                }));
            }
            if (!metadata.IsNullableValueType || base.CustomDefaultValue)
            {
                System.Enum value = System.Enum.ToObject(metadata.ModelType, this.DefaultValue) as System.Enum;
                metadata.AdditionalValues.Add("DefaultValue", value);
            }
            System.Type type = metadata.IsNullableValueType ? System.Nullable.GetUnderlyingType(metadata.ModelType) : metadata.ModelType;
            metadata.AdditionalValues.Add("ListItems", EnumAttribute.GetListItems(type));
            metadata.AdditionalValues.Add("UIType", this.UIType);
        }
        protected override void MetadataAssist(ModelMetadata metadata)
        {
            Check.NotNull<ModelMetadata>(metadata, "metadata");
            base.MetadataAssist(metadata);
            System.Collections.Generic.IEnumerable<SelectListItem> listItems = EnumAttribute.GetListItems(metadata.IsNullableValueType ? System.Nullable.GetUnderlyingType(metadata.ModelType) : metadata.ModelType);
            System.Collections.Generic.Dictionary<string, string> dictionary = listItems.ToDictionary((SelectListItem item) => item.Value, (SelectListItem item) => item.Text);
            string text = JsonConvert.SerializeObject(dictionary);
            base.ColumnFormatOptions = text;
            base.FilterItems = text;
        }
        private static System.Collections.Generic.IEnumerable<SelectListItem> GetListItems(System.Type type)
        {
            return System.Enum.GetNames(type).Select(delegate (string name)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Text = type.GetDisplayName(name);
                selectListItem.Value = System.Enum.Format(type, System.Enum.Parse(type, name), "d");
                return selectListItem;
            }
            );
        }
    }
}
