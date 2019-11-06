using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripERP.Common
{
    class KeyValue
    {
        public string key { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return key;
        }

        public KeyValue(string KeyParam, object ValueParam)
        {
            key = KeyParam;
            Value = ValueParam;
        }
    }
}
