using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class SuperordinateIdAttribute : System.Attribute, IMetadataAware
    {
        public bool Modifiable { get; set; }
        public bool ShowForEdit { get; set; }
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.ShowForDisplay = false;
            metadata.ShowForEdit = this.ShowForEdit;
            metadata.AdditionalValues.Add("ShowForModify", this.ShowForEdit);
            if (!this.Modifiable)
            {
                metadata.AdditionalValues.Add("Unmodifiable", true);
            }
        }
    }
}
