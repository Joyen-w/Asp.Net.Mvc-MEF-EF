using DAL.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Search
{
    internal class QueryStringRuleParser
    {
        private readonly string searchparameters;
        public QueryStringRuleParser(string searchparameters)
        {
            this.searchparameters = searchparameters;
        }
        public SearchConstraint ParserRules()
        {
            SearchConstraint result;
            if (!string.IsNullOrEmpty(this.searchparameters))
            {
                SearchConstraint searchConstraint = new SearchConstraint();
                string[] array = this.searchparameters.Split(new char[]
                {
                    '&'
                });
                for (int i = 0; i < array.Length; i++)
                {
                    string text = array[i];
                    searchConstraint.Rules.Add(new ConstraintRule
                    {
                        Data = text.Split(new char[]
                        {
                            ':'
                        })[1],
                        Field = text.Split(new char[]
                        {
                            ':'
                        })[0],
                        Operator = ConditionalOperator.Equal
                    });
                }
                result = searchConstraint;
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
