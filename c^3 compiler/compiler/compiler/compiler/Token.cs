using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Token
    {
       public string class_part;
       public string Value_part;
       public int line_no = 0;
       public Token()
       {


       }
        public Token(string class_part, string Value_part,int line_no=0)
        {
            this.class_part = class_part;
            this.Value_part = Value_part;
            this.line_no = line_no;
        
        }
    }
}
