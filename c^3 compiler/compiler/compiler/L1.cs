using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class L1
    {
        public string name;
        public string type;
     public   List<L2> l2_list;
        public L1(string name,string type)
        {
            this.name = name;
            this.type = type;
            this.l2_list = new List<L2>();

        }
    }
}
