using DAL.Properties;
using DAL.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IntegerAttribute : NumericAttribute
    {
        private string defaultValue;
        public string DefaultValue
        {
            get{return this.defaultValue;}
            set{this.defaultValue = value; CustomDefaultValue = true;}
        }
        public int MinValue{get;private set;}
        public int MaxValue{get;private set;}
        public string ListItems{get;set;}
        public IntegerAttribute() : this(Resources.IntegerAttribute_DisplayName)
        {
        }
        public IntegerAttribute(string displayName) : this(displayName, 0)
        {
        }
        public IntegerAttribute(string displayName, int minValue) : this(displayName, minValue, 2147483647)
        {
        }
        public IntegerAttribute(string displayName, int minValue, int maxValue) : base(displayName, Resources.IntegerAttribute_Invalid)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            int num = this.MaxValue;
            int length = num.ToString(System.Globalization.CultureInfo.InvariantCulture).Length;
            num = this.MinValue;
            MaxLength = Math.Max(length, num.ToString(System.Globalization.CultureInfo.InvariantCulture).Length);
            RegexPattern = Resources.IntegerAttribute_RegexPattern;
            ImeMode = ImeModeValue.Inactive;
            SetFixedColumnWidth(55);
        }
        public override void OnMetadataCreated(ModelMetadata metadata)
        {
            Check.NotNull(metadata, "metadata");
            base.OnMetadataCreated(metadata);
            if (this.MinValue > this.MaxValue)
            {
                throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.Global_SettingError_MinGreaterThanMax, new object[]
                {
                    metadata.GetDisplayName()
                }));
            }
            if (!metadata.IsNullableValueType || CustomDefaultValue)
            {
                metadata.AdditionalValues.Add("DefaultValue", this.DefaultValue);
            }
            metadata.AdditionalValues.Add("MaxValue", this.MaxValue);
            metadata.AdditionalValues.Add("MinValue", this.MinValue);
            metadata.AdditionalValues.Add("ListItems", this.ListItems);
            metadata.AdditionalValues.Add("GridFilter.Property", GridFilterPropertyType.Number);
        }
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            Check.NotNull(metadata, "metadata");
            List<ModelClientValidationRule> list = new List<ModelClientValidationRule>
            {
                new ModelClientValidationRangeRule(this.FormatRangeErrorMessage(metadata.DisplayName), this.MinValue, this.MaxValue)
            };
            if (!string.IsNullOrEmpty(RegexPattern))
            {
                list.Add(new ModelClientValidationRegexRule(this.FormatErrorMessage(metadata.DisplayName), RegexPattern));
            }
            return list;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult result;
            if (value == null || validationContext == null)
            {
                result = ValidationResult.Success;
            }
            else
            {
                int num = Convert.ToInt32(value, System.Globalization.CultureInfo.InvariantCulture);
                if (num < this.MinValue || num > this.MaxValue)
                {
                    result = new ValidationResult(this.FormatRangeErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    if (!string.IsNullOrEmpty(RegexPattern) && !IsMatched(num.ToString(System.Globalization.CultureInfo.InvariantCulture)))
                    {
                        result = new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                    }
                    else
                    {
                        result = ValidationResult.Success;
                    }
                }
            }
            return result;
        }
        private string FormatRangeErrorMessage(string name)
        {
            string result;
            if (this.MinValue == -2147483648 && this.MaxValue == 2147483647)
            {
                result = string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.IntegerAttribute_ValidationError, new object[]
                {
                    name
                });
            }
            else
            {
                if (this.MinValue != -2147483648 && this.MaxValue != 2147483647)
                {
                    result = string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.IntegerAttribute_ValidationError_Range, new object[]
                    {
                        name,
                        this.MinValue,
                        this.MaxValue
                    });
                }
                else
                {
                    if (this.MinValue != -2147483648)
                    {
                        result = string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.IntegerAttribute_ValidationError_MinValue, new object[]
                        {
                            name,
                            this.MinValue
                        });
                    }
                    else
                    {
                        result = string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.IntegerAttribute_ValidationError_MaxValue, new object[]
                        {
                            name,
                            this.MaxValue
                        });
                    }
                }
            }
            return result;
        }
    }
}
