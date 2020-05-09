using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL.Search
{
    [ModelBinder(typeof(SearchParametersModelBinder))]
    public class PageSearchParameters : ISearchParameters
    {
        [JsonProperty(PropertyName = "rows")]
        public int PageSize { get; set; }
        [JsonProperty(PropertyName = "page")]
        public int PageIndex { get; set; }
        [JsonProperty(PropertyName = "sidx")]
        public string SortColumn { get; set; }
        [JsonProperty(PropertyName = "sord")]
        public string SortOrder { get; set; }
        [JsonProperty(PropertyName = "filters")]
        public SearchConstraint Where { get; set; }
        [JsonProperty(PropertyName = "notPage")]
        public bool NotPage { get; set; }
        [JsonProperty(PropertyName = "queryLevel")]
        public int QueryLevel { get; set; }
        [JsonProperty(PropertyName = "expandLevel")]
        public int ExpandLevel { get; set; }
        internal SearchConstraint QuerySearch { get; set; }
        public ConstraintRule ShiftQueryRule(string fieldName)
        {
            ConstraintRule constraintRule = this.QuerySearch.Rules.Find((ConstraintRule x) => x.Field.Equals(fieldName, System.StringComparison.CurrentCultureIgnoreCase));
            ConstraintRule result;
            if (constraintRule != null)
            {
                this.QuerySearch.Rules.Remove(constraintRule);
                result = constraintRule;
            }
            else
            {
                result = null;
            }
            return result;
        }
        public ConstraintRule ShiftWhereRule(string fieldName)
        {
            List<SearchConstraint> groups = this.Where.Groups;
            ConstraintRule result;
            foreach (SearchConstraint current in groups)
            {
                ConstraintRule constraintRule = current.Rules.Find((ConstraintRule c) => c.Field.Equals(fieldName));
                if (constraintRule != null)
                {
                    this.Where.Groups.Remove(current);
                    result = constraintRule;
                    return result;
                }
            }
            result = null;
            return result;
        }
    }
}
