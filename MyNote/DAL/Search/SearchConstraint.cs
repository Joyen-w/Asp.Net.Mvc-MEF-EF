using DAL.Annotations;
using DAL.DBContext;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    [DataContract]
    public class SearchConstraint : BaseJson
    {
        [NotMapped, Complex("子约束组", TemplateHint = "SearchConstraint"), DataMember(Name = "groups")]
        public System.Collections.Generic.List<SearchConstraint> Groups { get; set; }
        [DataMember(Name = "groupOp"), Text("组运算符"), NotMapped]
        public string GroupOperator { get; set; }
        [Complex("约束条件", TemplateHint = "ConstraintRule"), NotMapped, DataMember(Name = "rules")]
        public System.Collections.Generic.List<ConstraintRule> Rules { get; set; }
        public SearchConstraint()
        {
            this.Groups = new System.Collections.Generic.List<SearchConstraint>();
            this.Rules = new System.Collections.Generic.List<ConstraintRule>();
        }
        public static SearchConstraint Create(string jsonData)
        {
            return JsonConvert.DeserializeObject(jsonData, typeof(SearchConstraint)) as SearchConstraint;
        }
        public bool ShouldSerializeRules()
        {
            return this.Rules.Count != 0;
        }
        public bool ShouldSerializeGroups()
        {
            return this.Groups.Count != 0;
        }
        public SearchConstraint AddRule(string fieldName, object data, ConditionalOperator conditionalOperator)
        {
            this.Rules.Add(new ConstraintRule
            {
                Field = fieldName,
                Data = data.ToString(),
                Operator = conditionalOperator
            });
            return this;
        }
    }
}
