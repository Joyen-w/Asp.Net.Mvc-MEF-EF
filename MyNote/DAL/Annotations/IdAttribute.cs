using DAL.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class IdAttribute : BaseAttribute
    {
        public IdAttribute() : this(Resources.IdAttribute_DisplayName)
        {
        }
        public IdAttribute(string displayName) : base(displayName)
        {
            base.Order = 1;
            base.ColumnKey = true;
            base.SetFixedColumnWidth(45);
        }
        public override void OnMetadataCreated(ModelMetadata metadata)
        {
            base.OnMetadataCreated(metadata);
            metadata.HideSurroundingHtml = true;
            metadata.AdditionalValues.Add("DefaultValue", (typeof(int) == metadata.ModelType) ? "0" : string.Empty);
            metadata.AdditionalValues.Add("GridFilter.Property", GridFilterPropertyType.Number);
            metadata.AdditionalValues["GridColumn.IsGridColumn"] = true;
        }
    }
}
