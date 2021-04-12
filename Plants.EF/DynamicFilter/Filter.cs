using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plants.EF.DynamicFilter
{


    public class SearchFilter
    {
        public FilterEnum.GroupOpEnum groupOp { set; get; }
        public List<SearchGroup> groups { set; get; }
        public List<SearchRule> rules { set; get; }
    }

    public class SearchRule
    {
        public string field { set; get; }
        public FilterEnum.OpEnum op { set; get; }
        public string data { set; get; }

        public override string ToString()
        {
            return string.Format("'{0}' {1} '{2}'", field, op.ToString(), data);
        }
    }

    public class SearchGroup
    {
        public string groupOp { set; get; }
        public List<SearchRule> rules { set; get; }
        public List<SearchGroup> groups { set; get; }
    }
}

