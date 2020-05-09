using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public sealed class ComplexAttribute : BaseAttribute, IMetadataAware
    {
        public bool HideSurroundingHtml
        {
            get;
            set;
        }
        public ComplexAttribute(string displayName) : base(displayName)
        {
        }
        void IMetadataAware.OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("ShowComplexType", true);
            metadata.HideSurroundingHtml = this.HideSurroundingHtml; ;
            this.OnMetadataCreated(metadata);
        }
    }
}
