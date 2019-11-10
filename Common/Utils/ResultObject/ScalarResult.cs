using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils.ResultObject
{
    public class ScalarResult<T> where T : struct
    {
        private T _value;
        public T Value
        {
            get { return _value; }
            private set { _value = value; }
        }


        public ScalarResult(T value)
        {
            Value = value;
        }
    }
}
