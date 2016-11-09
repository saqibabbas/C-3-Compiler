using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class L2
    {
        public string name;
        public string type;
    public    List<L3> l3_list;
         public L2(string name,string type)
        {
            this.name = name;
            this.type = type;
            this.l3_list = new List<L3>();

        }
    }
}
