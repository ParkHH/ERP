using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripERP.Common
{
    class CommonCodeItem
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public int Sort { get; set; }
        public string Desc { get; set; }

        public CommonCodeItem(string NameParam, object ValueParam, int SortParam, string DescParam)
        {
            Name = NameParam;
            Value = ValueParam;
            Sort = SortParam;
            Desc = DescParam;
        }
    }
}
