using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class Data
    {
       public static List<L1> all_classes_syntax = new List<L1>();
       public static List<L1> all_classes_semantic = new List<L1>();

        public static bool class_lookup(string name,string type)
       {
           foreach (L1 item in all_classes_syntax)
           {
               if (item.name==name && item.type==type)
               {
                   return true;
               }
           }

           return false;
       }

        public static bool class_lookup_semantic(string name, string type)
        {
            foreach (L1 item in all_classes_semantic)
            {
                if (item.name == name && item.type == type)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool method_lookup_syntax(string name, string type,int class_index)
        {
            foreach (L2 item in all_classes_syntax[class_index].l2_list)
            {
                if (item.name == name && item.type == type)
                {
                    return true;
                }
                
            }
            return false;
        }
        public static bool method_lookup_syntax_fn_param(string name, string type, int class_index)
        {
            foreach (L2 item in all_classes_syntax[class_index].l2_list)
            {
                string[] a = item.type.Split('-');
                string[] typ = a[0].Split(',');
                string typ_f = "";
                foreach (string typ_item in typ)
                {
                    typ_f += typ_item;
                }
                if (item.name == name && type == typ_f)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool method_lookup_semantic(string name, string type, int class_index)
        {
            foreach (L2 item in all_classes_semantic[class_index].l2_list)
            {
                if (item.name == name && item.type == type)
                {
                    return true;
                }

            }
            return false;
        }

        public static bool d_varible_lookup_inmethod(string name, int scope, int class_index, int method_index)
        {
            foreach (L3 item in all_classes_semantic[class_index].l2_list[method_index].l3_list)
            {
                if (item.name==name && item.scope==scope)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool d_varible_lookup_inclass(string name, int class_index)
        {
            foreach (L2 item in all_classes_semantic[class_index].l2_list)
            {
                if (item.name == name && item.l3_list.Count==0)
                {
                    return true;
                }
            }
            return false;
        }

        public static string get_type_inmethod(string name, int scope, int class_index, int method_index)
        {
            foreach (L3 item in all_classes_semantic[class_index].l2_list[method_index].l3_list)
            {
                if (item.name == name && item.scope == scope)
                {
                    return item.type;
                }
            }
            return "0";
        }

        public static string get_type_inclass(string name, int class_index)
        {
            foreach (L2 item in all_classes_semantic[class_index].l2_list)
            {
                if (item.name == name)
                {
                    return item.type;
                }
            }
            
            return "0";
        }


    }
}
