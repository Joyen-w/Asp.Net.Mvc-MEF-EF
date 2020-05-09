using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public abstract class NumericAttribute : BaseAttribute
    {
        public int MaxLength { get; set; }
        public string Unit { get; set; }
        public int Width { get; set; }
        public string RegexPattern { get; set; }
        protected ImeModeValue ImeMode { get; set; }
        protected NumericAttribute(string displayName, string errorMessage) : base(displayName, errorMessage)
        {
            this.ImeMode = ImeModeValue.Inactive;
            base.RequiredFlag = true;
        }
        public override void OnMetadataCreated(ModelMetadata metadata)
        {
            base.OnMetadataCreated(metadata);
            metadata.AdditionalValues.Add("ImeMode", this.ImeMode);
            metadata.AdditionalValues.Add("MaxLength", this.MaxLength);
            metadata.AdditionalValues.Add("Unit", this.Unit);
            metadata.AdditionalValues.Add("Width", this.Width);
        }
        protected bool IsMatched(string value)
        {
            bool result;
            if (value == null)
            {
                result = true;
            }
            else
            {
                Regex regex = new Regex(this.RegexPattern);
                Match match = regex.Match(value);
                result = (match.Index == 0 && match.Length == value.Length);
            }
            return result;
        }
    }
}
