using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2Java.Util
{
    public abstract class SuperDb
    {
        public abstract void InitDbInfo(DbParamEntity dBparm);
        public abstract List<List<string>> ExecuteReader(string cmdText);
    }
}
