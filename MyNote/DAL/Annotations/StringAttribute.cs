using DAL.Properties;
using DAL.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace DAL.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public abstract class StringAttribute : BaseAttribute
    {
        private int maxLength;
        private int columnMaxLength;
        public string DefaultValue
        {
            get;
            set;
        }
        public int Width
        {
            get;
            set;
        }
        public int WidthPercent
        {
            get;
            set;
        }
        public ImeModeValue ImeMode
        {
            get;
            set;
        }
        public int MaxLength
        {
            get
            {
                return this.maxLength;
            }
            set
            {
                this.maxLength = value;
                if (!this.CustomColumnMaxLengthSet)
                {
                    this.columnMaxLength = value;
                }
            }
        }
        public int ColumnMaxLength
        {
            get
            {
                return this.columnMaxLength;
            }
            set
            {
                this.columnMaxLength = value;
                this.CustomColumnMaxLengthSet = true;
            }
        }
        public string RegexPattern
        {
            get
            {
                return this.RegexPatternString;
            }
            set
            {
                this.RegexPatternString = value;
                this.CustomRegexPatternSet = true;
            }
        }
        protected string RegexPatternIgnoreCase
        {
            get
            {
                return this.RegexPatternString;
            }
            set
            {
                this.RegexPatternString = value;
                this.RegexIgnoreCase = true;
            }
        }
        protected bool RegexIgnoreCase
        {
            get;
            set;
        }
        protected string RegexPatternString
        {
            get;
            set;
        }
        protected bool CustomRegexPatternSet
        {
            get;
            set;
        }
        protected bool CustomColumnMaxLengthSet
        {
            get;
            set;
        }
        protected bool DangerousValidationEnabled
        {
            get;
            set;
        }
        protected string ClientValidationType
        {
            get;
            set;
        }
        protected StringAttribute(string displayName, int maxLength) : this(displayName, Resources.StringAttribute_Invalid, maxLength)
        {
        }
        protected StringAttribute(string displayName, string errorMessage) : this(displayName, errorMessage, 0)
        {
        }
        protected StringAttribute(string displayName, string errorMessage, int maxLength) : this(displayName, errorMessage, maxLength, true)
        {
        }
        protected StringAttribute(string displayName, string errorMessage, int maxLength, bool dangerousValidationEnabled) : base(displayName, errorMessage)
        {
            this.maxLength = maxLength;
            this.columnMaxLength = maxLength;
            this.DangerousValidationEnabled = dangerousValidationEnabled;
            this.ImeMode = ImeModeValue.Auto;
            base.RequiredFlag = true;
            base.SearchOperator = ConditionalOperator.Contains;
        }
        public override void OnMetadataCreated(ModelMetadata metadata)
        {
            base.OnMetadataCreated(metadata);
            metadata.AdditionalValues.Add("DefaultValue", this.DefaultValue);
            metadata.AdditionalValues.Add("MaxLength", this.MaxLength);
            metadata.AdditionalValues.Add("ImeMode", this.ImeMode);
            metadata.AdditionalValues.Add("Width", this.Width);
            metadata.AdditionalValues.Add("WidthPercent", this.WidthPercent);
            metadata.RequestValidationEnabled= false;
        }
        public override System.Collections.Generic.IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            Check.NotNull<ModelMetadata>(metadata, "metadata");
            System.Collections.Generic.List<ModelClientValidationRule> list = new System.Collections.Generic.List<ModelClientValidationRule>();
            if (this.MaxLength > 0)
            {
                list.Add(new ModelClientValidationStringLengthRule(string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.StringAttribute_ValidationError_MaxLength, new object[]
                {
                    metadata.GetDisplayName(),
                    this.MaxLength
                }), 0, this.MaxLength));
            }
            if (this.DangerousValidationEnabled)
            {
                list.Add(new ModelClientValidationDangerousRule());
            }
            if (!string.IsNullOrEmpty(this.RegexPattern) && this.CustomRegexPatternSet)
            {
                list.Add(new ModelClientValidationRegexRule(this.FormatErrorMessage(metadata.GetDisplayName()), this.RegexPattern));
            }
            if (!string.IsNullOrEmpty(this.RegexPattern) && !this.CustomRegexPatternSet)
            {
                System.Collections.Generic.List<ModelClientValidationRule> arg_107_0 = list;
                ModelClientValidationRule modelClientValidationRule = new ModelClientValidationRule();
                modelClientValidationRule.ValidationType = this.GetClientValidationType();
                modelClientValidationRule.ErrorMessage = this.FormatErrorMessage(metadata.GetDisplayName());
                arg_107_0.Add(modelClientValidationRule);
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
                string text = value.ToString().Trim();
                if (string.IsNullOrEmpty(text))
                {
                    result = ValidationResult.Success;
                }
                else
                {
                    if (this.MaxLength > 0 && text.Length > this.MaxLength)
                    {
                        result = new ValidationResult(string.Format(System.Globalization.CultureInfo.CurrentCulture, Resources.StringAttribute_ValidationError_MaxLength, new object[]
                        {
                            Resources.PasswordAttribute_DisplayName,
                            this.MaxLength
                        }));
                    }
                    else
                    {
                        if (this.DangerousValidationEnabled && StringAttribute.IsDangerousString(text))
                        {
                            result = new ValidationResult(Resources.StringAttribute_ValidationError_Dangerous);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.RegexPattern) && !this.IsMatched(text))
                            {
                                result = new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
                            }
                            else
                            {
                                result = ValidationResult.Success;
                            }
                        }
                    }
                }
            }
            return result;
        }
        private static bool IsDangerousString(string value)
        {
            Regex regex = new Regex("^(?!(.|\\n)*<[a-z!\\/?])(?!(.|\\n)*&#)(.|\\n)*$", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
            return regex.Match(value).Length == 0;
        }
        private bool IsMatched(string valueAsString)
        {
            Regex regex = this.RegexIgnoreCase ? new Regex(this.RegexPattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled) : new Regex(this.RegexPattern);
            Match match = regex.Match(valueAsString);
            return match.Index == 0 && match.Length == valueAsString.Length;
        }
        private string GetClientValidationType()
        {
            return string.IsNullOrEmpty(this.ClientValidationType) ? base.GetType().Name.Replace("Attribute", string.Empty).ToLowerInvariant() : this.ClientValidationType;
        }
    }
}
