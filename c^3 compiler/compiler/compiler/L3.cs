using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class L3
    {
      public  string name;
      public string type;
      public int scope;
        public L3 (string name,string type,int scope)
        {

            this.name = name;

            this.scope = scope;
            this.type = type;
        }
    }
}
