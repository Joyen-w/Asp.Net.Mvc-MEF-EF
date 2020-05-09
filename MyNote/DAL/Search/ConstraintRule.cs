using DAL.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    [DataContract]
    public class ConstraintRule
    {
        [Text("约束字段"), DataMember(Name = "field")]
        public string Field { get; set; }

        [Enum("运算符", SelectUIType.DropDownList), DataMember(Name = "op")]
        public ConditionalOperator Operator { get; set; }
        [Text("约束值"), DataMember(Name = "data")]
        public string Data { get; set; }
        public object GetValue(System.Type dataType)
        {
            return TypeDescriptor.GetConverter(dataType).ConvertFromInvariantString(this.Data);
        }
        public string GetPredicate(int parameterIndex)
        {
            return string.Format(ConstraintRule.GetOperatorFormatExpression(this.Operator), this.Field, ConstraintRule.GetLinqOperator(this.Operator), parameterIndex);
        }
        private static string GetLinqOperator(ConditionalOperator constraintOperator)
        {
            string result = string.Empty;
            if (constraintOperator > ConditionalOperator.BeginsWith)
            {
                if (constraintOperator <= ConditionalOperator.Contains)
                {
                    if (constraintOperator == ConditionalOperator.NotBeginsWith)
                        result = "StartsWith";

                    return result;
                }
                else
                {
                    if (constraintOperator != ConditionalOperator.NotContains)
                    {
                        if (constraintOperator != ConditionalOperator.EndsWith && constraintOperator != ConditionalOperator.NotEndsWith)
                            return result;

                        result = "EndsWith";
                        return result; ;
                    }
                }
                result = "Contains";
                return result;
            }
            if (constraintOperator <= ConditionalOperator.LessThanOrEqual)
            {
                switch (constraintOperator)
                {
                    case ConditionalOperator.Equal:
                        result = "=";
                        return result;
                    case ConditionalOperator.NotEqual:
                        result = "!=";
                        return result;
                    case ConditionalOperator.Equal | ConditionalOperator.NotEqual:
                        return result;
                    case ConditionalOperator.LessThan:
                        result = "<";
                        return result;
                    default:
                        if (constraintOperator != ConditionalOperator.LessThanOrEqual)
                        {
                            return result;
                        }
                        result = "<=";
                        return result;
                }

            }
            else
            {
                if (constraintOperator == ConditionalOperator.GreaterThan)
                {
                    result = ">";
                }
                if (constraintOperator == ConditionalOperator.GreaterThanOrEqual)
                {
                    result = ">=";
                }
                if (constraintOperator != ConditionalOperator.BeginsWith)
                {

                }
                return result;
            }
        }
        private static string GetOperatorFormatExpression(ConditionalOperator constraintOperator)
        {
            string result = "it.{0}.toLower().{1}(@{2})";

            if (constraintOperator <= ConditionalOperator.Contains)
            {
                if (constraintOperator != ConditionalOperator.BeginsWith)
                {
                    if (constraintOperator == ConditionalOperator.NotBeginsWith)
                        result = "!it.{0}.toLower().{1}(@{2})";
                    if (constraintOperator != ConditionalOperator.Contains)
                        result = "it.{0} {1} @{2}";
                }
            }
            else
            {
                if (constraintOperator == ConditionalOperator.NotContains)
                    result = "!it.{0}.toLower().{1}(@{2})";
                else if (constraintOperator != ConditionalOperator.EndsWith)
                {
                    if (constraintOperator != ConditionalOperator.NotEndsWith)
                        result = "it.{0} {1} @{2}";
                    else
                        result = "!it.{0}.toLower().{1}(@{2})";
                }
            }
            return result;
        }
    }
}
