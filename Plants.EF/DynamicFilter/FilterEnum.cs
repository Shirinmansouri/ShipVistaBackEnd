using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plants.EF.DynamicFilter
{
    public class FilterEnum
    {
        public enum GroupOpEnum
        {
            And = 0,
            Or = 1
        }
        public enum OpEnum
        {
            eq = 0,
            ni = 1,
            ne = 2,
            lt = 3,
            le = 4,
            gt = 5,
            ge = 6,
            bw = 7,
            bn = 8,
            ew = 9,
            en = 10,
            cn = 11,
            nc = 12
        }
    }
}
