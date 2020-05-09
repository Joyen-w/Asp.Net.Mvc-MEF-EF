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
    public class TextAttribute : StringAttribute
    {
        public bool AllowHtml
        {
            get;
            private set;
        }
        public TextAttribute() : this(Resources.TextAttribute_DisplayName)
        {
        }
        public TextAttribute(string displayName) : this(displayName, 100, false)
        {
        }
        public TextAttribute(string displayName, int maxLength) : this(displayName, maxLength, false)
        {
        }
        public TextAttribute(string displayName, bool allowHtml) : this(displayName, 100, allowHtml)
        {
        }
        public TextAttribute(string displayName, int maxLength, bool allowHtml) : base(displayName, maxLength)
        {
            this.AllowHtml = allowHtml;
            base.DangerousValidationEnabled = !this.AllowHtml;
            base.ColumnTitle = true;
        }
        public override void OnMetadataCreated(ModelMetadata metadata)
        {
            base.OnMetadataCreated(metadata);
            metadata.AdditionalValues.Add("AllowHtml", this.AllowHtml);
        }
    }
}
