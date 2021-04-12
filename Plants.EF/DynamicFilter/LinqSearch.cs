using Plants.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plants.EF.DynamicFilter
{
  public  class LinqSearch
    {
        private static readonly Dictionary<string, string> _whereOperation =
           new Dictionary<string, string>
{
                 {"in" , " {0} = @{1} "},//is in
                        {"eq" , " {0} = @{1} "},
                        {"ni" , " {0} != @{1} "},//is not in
                        {"ne" , " {0} != @{1} "},
                        {"lt" , " {0} < @{1} "},
                        {"le" , " {0} <= @{1} "},
                        {"gt" , " {0} > @{1} "},
                        {"ge" , " {0} >= @{1} "},
                        {"bw" , " {0}.StartsWith(@{1}) "},//begins with
                        {"bn" , " !{0}.StartsWith(@{1}) "},//does not begin with
                        {"ew" , " {0}.EndsWith(@{1}) "},//ends with
                        {"en" , " !{0}.EndsWith(@{1}) "},//does not end with
                        {"cn" , " {0}.Contains(@{1}) "},//contains
                        {"nc" , " !{0}.Contains(@{1}) "}//does not contain
           };


        private static readonly Dictionary<string, string> _validOperators =
                     new Dictionary<string, string>
     {
     {"Object",""},
     {"Boolean","eq:ne:"},
     {"Char",""},
     {"String","eq:ne:lt:le:gt:ge:bw:bn:cn:nc:"},
     {"SByte",""},
     {"Byte","eq:ne:lt:le:gt:ge:"},
     {"Int16","eq:ne:lt:le:gt:ge:"},
     {"UInt16",""},
     {"Int32","eq:ne:lt:le:gt:ge:"},
     {"UInt32",""},
     {"Int64","eq:ne:lt:le:gt:ge:cn:nc"},
     {"UInt64",""},
     {"Decimal","eq:ne:lt:le:gt:ge:"},
     {"Single","eq:ne:lt:le:gt:ge:"},
     {"Double","eq:ne:lt:le:gt:ge:"},
     {"DateTime","eq:ne:lt:le:gt:ge:"},
     {"TimeSpan",""},
     {"Guid",""},
     { "Nullable`1","eq:ne:lt:le:gt:ge:"}
     };

        private int _parameterIndex;

        public IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string filters)
        {
            //if (filters != string.Empty)
            //    return query;
            return MultiFieldSearch(query, filters);
        }

        private Tuple<string, object> GetPredicate<T>(string searchField, string searchOper, string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
                return null;

            var type = typeof(T).FindFieldType(searchField);
            if (type == null)
                throw new InvalidOperationException(searchField + " is not defined.");

            if (!_validOperators[type.Name].Contains(searchOper + ":"))
            {
   
                return null;
            }
            if (type.Name == "Nullable`1")
            {
                type = type.GenericTypeArguments[0];
            }
            var resultValue = Convert.ChangeType(searchValue, type);
            return new Tuple<string, object>(GetSearchOperator(searchOper, searchField), resultValue);
        }

        private string GetSearchOperator(string ruleSearchOperator, string searchField)
        {
            string whereOperation;
            if (!_whereOperation.TryGetValue(ruleSearchOperator, out whereOperation))
            {
                throw new NotSupportedException(string.Format("{0} is not supported.", ruleSearchOperator));
            }

            var searchOperator = string.Format(whereOperation, searchField, _parameterIndex);
            _parameterIndex++;
            return searchOperator;
        }


        private IQueryable<T> MultiFieldSearch<T>(IQueryable<T> query, string filter)
        {
            var filters = JsonConvert.DeserializeObject<SearchFilter>(filter);

            var groupOperator = filters.groupOp.ToString().ToLower();
            if (filters.rules == null) // rules
                return query;

            var valuesList = new List<object>();
            var filterExpression = String.Empty;
            var filterExpressionLocal = String.Empty;

            foreach (var rule in filters.rules)
                SetFilterRule(query, rule, groupOperator, ref filterExpressionLocal, ref valuesList);

            if (filters.groups != null)
                SetFilterGroups(query, filters.groups, ref filterExpressionLocal, ref valuesList, filters.groupOp.ToString().ToLower());

            if (string.IsNullOrWhiteSpace(filterExpressionLocal))
                return query;

            //ToDo: jqGrid ReDo core
            filterExpression = filterExpressionLocal.Remove(filterExpressionLocal.Length - filters.groupOp.ToString().Length - 2); //filtersArray.groupOp.Length - 2);
            query = query.Where(filterExpression, valuesList.ToArray());
            return query;
        }

        //By Ghadiri test
        private IQueryable<T> SetFilterGroups<T>(IQueryable<T> query, List<SearchGroup> groups, ref string filterExpression, ref List<object> valuesList, string headerOp)
        {
            var filterExpressionLocal = string.Empty;
            foreach (var group in groups)
            {
                if (string.IsNullOrEmpty(group.groupOp))
                    break;

                var groupOperator = group.groupOp.ToLower() == "and" ? " and " : " or ";

                filterExpressionLocal = " ( ";
                foreach (var rule in group.rules)
                    SetFilterRule(query, rule, groupOperator, ref filterExpressionLocal, ref valuesList);

                if (group.groups != null)
                    SetFilterGroups(query, group.groups, ref filterExpressionLocal, ref valuesList, group.groupOp.ToLower().ToString());

                //ToDo: jqGrid ReDo core
                if (filterExpressionLocal.Length > 3)
                {
                    filterExpressionLocal = filterExpressionLocal.Remove(filterExpressionLocal.Length - groupOperator.Length - 2);
                    filterExpressionLocal += " ) " + headerOp + " ";

                    filterExpression += filterExpressionLocal;
                }
                else
                    filterExpressionLocal = string.Empty;
            }

            return query;
        }

        public static string RemoveFieldFromFilter(string filter, string searchField)
        {
            if (string.IsNullOrEmpty(filter)) return string.Empty;
            var searchFilter = JsonConvert.DeserializeObject<SearchFilter>(filter);
            for (int i = 0; i < searchFilter.rules.Count; i++)
            {
                var rule = searchFilter.rules[i];
                if (searchFilter.rules[i].field == searchField)
                    searchFilter.rules.Remove(rule);
            }
            var newfilter = JsonConvert.SerializeObject(searchFilter);
            return newfilter;
        }

        private IQueryable<T> SetFilterRule<T>(IQueryable<T> query, SearchRule rule, string groupOperator, ref string filterExpression, ref List<object> valuesList)
        {
            bool isTwoPartFilterDate = false;
          //  var ChangeRule = ChangeField(rule.field, rule.data);
            rule.field = rule.field;
            rule.data = rule.data;
            if (typeof(T).FindFieldType(rule.field) == typeof(DateTime) && rule.op.ToString() == "eq")
            {
                isTwoPartFilterDate = true;
                rule.op = FilterEnum.OpEnum.ge;
            }


            var predicate = GetPredicate<T>(rule.field, rule.op.ToString().ToLower(), rule.data);
            if (predicate != null)
            {
                valuesList.Add(predicate.Item2);
                filterExpression = filterExpression + predicate.Item1 + " " + groupOperator + " ";

                if (isTwoPartFilterDate)
                {
                    var date2 = DateTime.Parse(rule.field).AddDays(1).ToShortDateString();
                    predicate = GetPredicate<T>(rule.data, "lt", date2);
                    if (predicate != null)
                    {

                        valuesList.Add(predicate.Item2);
                        filterExpression = filterExpression + predicate.Item1 + " " + groupOperator + " ";
                    }
                }
            }

            return query;
        }

         
        public static string GetFieldValueFromFilter(string filter, string searchField)
        {
            if (string.IsNullOrEmpty(filter)) return string.Empty;
            var rules = JsonConvert.DeserializeObject<SearchFilter>(filter).rules;
            if (rules.Count == 0 || !rules.Any(s => s.field.Contains(searchField)))
                return "";

            var value = rules.Where(s => s.field == searchField).Select(s => s.data).First();
            return value;
        }
    }
}
