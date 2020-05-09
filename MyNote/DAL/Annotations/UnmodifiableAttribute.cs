using DAL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class UnmodifiableAttribute : System.Attribute, IMetadataAware
    {
        public bool ShowForModify
        {
            get;
            set;
        }
        public UnmodifiableAttribute()
        {
            this.ShowForModify = true;
        }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            Check.NotNull<ModelMetadata>(metadata, "metadata");
            metadata.AdditionalValues.Add("Unmodifiable", true);
            metadata.AdditionalValues.Add("ShowForModify", this.ShowForModify);
        }
    }
}
