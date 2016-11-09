using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class CFG_semantic : Program
    {
        bool IS_func_running = false;
        bool LeftCompablity = true;
        List<string> icg_list = new List<string>();
        int class_index = 0;
        string class_name = "";
        int method_index = -1;
        int scope = 0;
        bool is_inlcass = false;
        bool is_inmethod = false;
        bool Is_func_ok = false;
        int[] stack = new int[20];
        int stack_index = 0;
        string fn_param_types = "";
        bool in_fn_param = false;
        public bool my_main()
        {
            if (All_Tokens[index].class_part == "Main")
            {
                index++;
                if (All_Tokens[index].class_part == "(")
                {
                    index++;
                    if (All_Tokens[index].class_part == ")")
                    {
                        index++;
                        if (All_Tokens[index].class_part == "{")
                        {
                            create_scope();
                            index++;
                            if (multi_line_st())
                            {
                                if (All_Tokens[index].class_part == "}")
                                {
                                    destroy_scope();
                                    index++;
                                    if (next())
                                    {


                                        return true;

                                    }
                                }
                            }

                        }
                    }
                }
            }

            return false;
        }
        bool next()
        {
            if (class_start())
            {
                if (next())
                {
                    return true;

                }
            }
            return true;
        }
        bool single_line_st()
        {
            //if (datatype_identifier())
            //{
            //    return true;

            //}
            if (identifier())
            {

                return true;
            }
            if (Repeat_st())
            {
                return true;
            }
            if (Repeat_while())
            {
                return true;
            }
            if (class_start())
            {
                return true;
            }
            if (either())
            {
                return true;
            }
            if (shift())
            {
                return true;
            }
            if (break_st())
            {
                return true;
            }
            if (func_def())
            {
                return true;

            }

            return false;
        }
        bool multi_line_st()
        {
            if (single_line_st())
            {
                if (multi_line_st())
                {
                    return true;

                }
            }

            return true;
        }

        #region Expression
        public bool OE_equal(string right_type)
        {
            bool compablity = true;
            if (All_Tokens[index].class_part == "=")
            {
                compablity = false;
                index++;
                if (OE())
                {


                    if (stack_opr.Count > 0)
                    {
                        string left_type = stack_opr.Pop();
                        if (compatiblity(right_type, left_type, "=").ToString() != "Not Compatible" && LeftCompablity == true)
                        {
                            compablity = true;
                            if (All_Tokens[index].class_part == ";")
                            {
                                index++;
                                return true;
                            }
                        }
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }

            }
            if (All_Tokens[index].class_part == ";" && LeftCompablity == true && compablity == true)
            {
                index++;
                return true;
            }
            return false;
        }

        bool OE()
        {
            stack_opr.Clear();
            if (AE())
            {
                if (OEx())
                {
                    while (stack_opr.Count > 1)
                    {
                        int ab = index;
                        string leftOprand, rightOperand, Operator;
                        List<string> list_opr = new List<string>();
                        string[] arr_opr = new string[stack_opr.Count];
                        int temp_length = stack_opr.Count;
                        for (int i = 0; i < temp_length; i++)
                        {
                            list_opr.Add(stack_opr.Pop());
                        }

                        for (int i = 0; i < list_opr.Count; i++)
                        {
                            if (list_opr[i] == "*" || list_opr[i] == "/" || list_opr[i] == "%")
                            {
                                try
                                {
                                    string TypeReturn = compatiblity(list_opr[i - 1], list_opr[i + 1], list_opr[i]);
                                    if (TypeReturn == "Not Compatible")
                                    {
                                        LeftCompablity = false;
                                        return false;
                                    }
                                    else
                                    {
                                        list_opr[i - 1] = (TypeReturn);
                                        list_opr.RemoveAt(i);
                                        list_opr.RemoveAt(i);
                                        i = 0;

                                    }
                                }
                                catch (Exception)
                                {
                                    
                                   
                                }
                               
                              
                            }
                        }


                        for (int i = 0; i < list_opr.Count; i++)
                        {
                            if (list_opr[i] == "+" || list_opr[i] == "-")
                            {
                                try
                                {
                                    string TypeReturn = compatiblity(list_opr[i - 1], list_opr[i + 1], list_opr[i]);
                                    if (TypeReturn == "Not Compatible")
                                    {
                                        LeftCompablity = false;
                                        return false;
                                    }
                                    else
                                    {
                                        list_opr[i - 1] = (TypeReturn);
                                        list_opr.RemoveAt(i);
                                        list_opr.RemoveAt(i);
                                        i = 0;
                                    }
                                }
                                catch (Exception)
                                {
                                    
                                    
                                }
                             
                            }
                        }

                        for (int i = 0; i < list_opr.Count; i++)
                        {
                            if (list_opr[i] == "<" || list_opr[i] == ">" || list_opr[i] == ">=" || list_opr[i] == "<=" || list_opr[i] == "!=" || list_opr[i] == "==")
                            {
                                try
                                {


                                    string TypeReturn = compatiblity(list_opr[i - 1], list_opr[i + 1], list_opr[i]);
                                    if (TypeReturn == "Not Compatible")
                                    {
                                        LeftCompablity = false;
                                        return false;
                                    }
                                    else
                                    {
                                        list_opr[i - 1] = (TypeReturn);
                                        list_opr.RemoveAt(i);
                                        list_opr.RemoveAt(i);
                                        i = 0;
                                    }
                                }
                                catch (Exception)
                                {

                                    
                                }
                            }
                        }

                        for (int i = 0; i < list_opr.Count; i++)
                        {
                            if (list_opr[i] == "&&")
                            {
                                try
                                {


                                    string TypeReturn = compatiblity(list_opr[i - 1], list_opr[i + 1], list_opr[i]);
                                    if (TypeReturn == "Not Compatible")
                                    {
                                        LeftCompablity = false;
                                        return false;
                                    }
                                    else
                                    {
                                        list_opr[i - 1] = (TypeReturn);
                                        list_opr.RemoveAt(i);
                                        list_opr.RemoveAt(i);
                                        i = 0;
                                    }
                                }
                                catch (Exception)
                                {

                                   
                                }
                            }
                        }

                        for (int i = 0; i < list_opr.Count; i++)
                        {
                            if (list_opr[i] == "||")
                            {
                                try
                                {


                                    string TypeReturn = compatiblity(list_opr[i - 1], list_opr[i + 1], list_opr[i]);
                                    if (TypeReturn == "Not Compatible")
                                    {
                                        LeftCompablity = false;
                                        return false;
                                    }
                                    else
                                    {
                                        list_opr[i - 1] = (TypeReturn);
                                        list_opr.RemoveAt(i);
                                        list_opr.RemoveAt(i);
                                        i = 0;
                                    }
                                }
                                catch (Exception)
                                {

                                   
                                }
                            }
                        }

                        stack_opr.Push(list_opr[0]);
                    }
                    if (IS_func_running)
                    {
                        if (Is_func_ok)
                        {
                            return true;
                        }
                    }
                    if (!IS_func_running)
                    {
                        return true;

                    }
                }

            }
            return false;
        }

        bool OEx()
        {
            if (All_Tokens[index].class_part == "||")
            {
                icg_list.Add(All_Tokens[index].Value_part);
                stack_opr.Push(All_Tokens[index].Value_part);
                index++;

                if (AE())
                {
                    if (OEx())
                    {
                        return true;
                    }
                }
            }
            if (All_Tokens[index].class_part == ":" || All_Tokens[index].class_part == ")" || All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == "," || All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier" || All_Tokens[index].class_part == "]")
            {
                return true;
            }
            return false;
        }

        bool AE()
        {
            if (RE())
            {
                if (AEx())
                {
                    return true;
                }
            }
            if (All_Tokens[index].class_part == ":" || All_Tokens[index].class_part == ")" || All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == "," || All_Tokens[index].class_part == "]" || All_Tokens[index].class_part == "||" || All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                return true;
            }
            return false;
        }

        bool AEx()
        {
            if (All_Tokens[index].class_part == "&&")
            {
                icg_list.Add(All_Tokens[index].Value_part);

                stack_opr.Push(All_Tokens[index].Value_part);
                index++;
                if (RE())
                {
                    if (AE())
                    {
                        return true;
                    }
                }
            }
            //if (Is_func_ok)
            //{
            //    Is_func_ok = false;
            //    return true;
            //}
            if (All_Tokens[index].class_part == ":" || All_Tokens[index].class_part == ")" || All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == "," || All_Tokens[index].class_part == "]" || All_Tokens[index].class_part == "||" || All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                return true;
            }
            return false;
        }

        bool RE()
        {
            if (E())
            {
                if (REx())
                {
                    return true;
                }
            }
            return false;
        }

        bool REx()
        {
            if (All_Tokens[index].class_part == "Relation-Operator")
            {
                icg_list.Add(All_Tokens[index].Value_part);

                stack_opr.Push(All_Tokens[index].Value_part);
                index++;
                if (E())
                {
                    if (REx())
                    {
                        return true;
                    }
                }
            }
            if (All_Tokens[index].class_part == ":" || All_Tokens[index].class_part == ")" || All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == "," || All_Tokens[index].class_part == "]" || All_Tokens[index].class_part == "||" || All_Tokens[index].class_part == "&&" || All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                return true;
            }
            return false;
        }

        bool E()
        {
            if (T())
            {
                if (Ex())
                {
                    return true;
                }
            }
            return false;
        }

        bool Ex()
        {
            if (All_Tokens[index].class_part == "P-M")
            {
                icg_list.Add(All_Tokens[index].Value_part);

                stack_opr.Push(All_Tokens[index].Value_part);
                index++;
                if (T())
                {
                    if (Ex())
                    {
                        return true;
                    }
                }
            }
            if (All_Tokens[index].class_part == "Relation-Operator" || All_Tokens[index].class_part == ":" || All_Tokens[index].class_part == ")" || All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == "," || All_Tokens[index].class_part == "]" || All_Tokens[index].class_part == "||" || All_Tokens[index].class_part == "&&" || All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                return true;
            }
            return false;
        }


        bool T()
        {
            if (F())
            {
                if (Tx())
                {
                    return true;
                }
            }
            return false;
        }


        bool Tx()
        {
            if (All_Tokens[index].class_part == "D-M" || All_Tokens[index].class_part == "*")
            {
                icg_list.Add(All_Tokens[index].Value_part);

                stack_opr.Push(All_Tokens[index].Value_part);
                index++;

                if (F())
                {
                    if (Tx())
                    {
                        return true;
                    }
                }
            }
            if (All_Tokens[index].class_part == "P-M" || All_Tokens[index].class_part == "Relation-Operator" || All_Tokens[index].class_part == ":" || All_Tokens[index].class_part == ")" || All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == "," || All_Tokens[index].class_part == "]" || All_Tokens[index].class_part == "||" || All_Tokens[index].class_part == "&&" || All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                return true;
            }
            return false;
        }

        bool ID_More()
        {
            //if (func_call())
            //{
            //    return true;
            //}
            if (All_Tokens[index].class_part == "Inc-Dec")
            {
                index++;
                return true;
            }
            return false;

        }

        bool F()
        {

            if (constant())
            {
                return true;
            }
            if (All_Tokens[index].class_part == "Identifier")
            {
                icg_list.Add(All_Tokens[index].Value_part);

                string name = All_Tokens[index].Value_part;
                index++;
                string type = "";
                bool Is_found_in_mehtod = false;
                bool Is_found_in_class = false;
                bool Is_found_in_main = false;
                if (is_inmethod)
                {

                    //// inside metod
                    int scope_from_method = 0;
                    for (int i = stack_index - 1; i >= 0; i--)
                    {
                        if (Data.d_varible_lookup_inmethod(name, stack[i], class_index, method_index)) ////eistancw
                        {
                            scope_from_method = stack[i];
                            Is_found_in_mehtod = true;
                            //if (All_Tokens[index].class_part == ";")
                            //{
                            //index++;
                            //return true;
                            //}
                        }
                    }
                    if (!Is_found_in_mehtod)
                    {
                        if (Data.d_varible_lookup_inclass(name, class_index))
                        {
                            Is_found_in_class = true;
                        }
                    }
                    if (!Is_found_in_mehtod && !Is_found_in_class)
                    {
                        if (Data.d_varible_lookup_inmethod(name, 1, 0, 0))
                        {
                            Is_found_in_main = true;
                        }
                    }
                    if (Is_found_in_mehtod)
                    {
                        if ("0" != Data.get_type_inmethod(name, scope_from_method, class_index, method_index)) /// type return
                        {
                            type = Data.get_type_inmethod(name, scope_from_method, class_index, method_index);
                            stack_opr.Push(type);
                            if (in_fn_param)
                            {
                                if (All_Tokens[index].Value_part != "+" && All_Tokens[index].Value_part != "*" && All_Tokens[index].Value_part != "/" && All_Tokens[index].Value_part != "-")
                                {
                                    fn_param_types += type + ",";
                                }
                            }
                        }
                        return true;

                    }
                    if (Is_found_in_class)
                    {
                        if ("0" != Data.get_type_inclass(name, class_index))
                        {
                            type = Data.get_type_inclass(name, class_index);
                            stack_opr.Push(type);
                            if (in_fn_param)
                            {
                                if (All_Tokens[index].Value_part != "+" && All_Tokens[index].Value_part != "*" && All_Tokens[index].Value_part != "/" && All_Tokens[index].Value_part != "-")
                                {
                                    fn_param_types += type + ",";
                                }
                            }
                        }
                        return true;
                    }
                    if (Is_found_in_main)
                    {
                        if ("0" != Data.get_type_inmethod(name, 1, 0, 0)) /// type return
                        {
                            type = Data.get_type_inmethod(name, 1, 0, 0);
                            stack_opr.Push(type);
                            if (in_fn_param)
                            {
                                if (All_Tokens[index].Value_part != "+" && All_Tokens[index].Value_part != "*" && All_Tokens[index].Value_part != "/" && All_Tokens[index].Value_part != "-")
                                {
                                    fn_param_types += type + ",";
                                }
                            }
                        }
                        return true;
                    }
                }
                else
                {
                    //// outside method
                    if (Data.d_varible_lookup_inclass(name, class_index))
                    {
                        Is_found_in_class = true;
                    }
                    if (!Is_found_in_class)
                    {
                        if (Data.d_varible_lookup_inmethod(name, 1, 0, 0))
                        {
                            Is_found_in_main = true;
                        }
                    }
                    if (Is_found_in_class)
                    {
                        if ("0" != Data.get_type_inclass(name, class_index))
                        {
                            type = Data.get_type_inclass(name, class_index);
                            stack_opr.Push(type);
                            if (in_fn_param)
                            {
                                if (All_Tokens[index].Value_part != "+" && All_Tokens[index].Value_part != "*" && All_Tokens[index].Value_part != "/" && All_Tokens[index].Value_part != "-")
                                {
                                    fn_param_types += type + ",";
                                }
                            }
                        }
                        return true;
                    }


                    if (Is_found_in_main)
                    {
                        if ("0" != Data.get_type_inmethod(name, 1, 0, 0)) /// type return
                        {
                            type = Data.get_type_inmethod(name, 1, 0, 0);
                            stack_opr.Push(type);
                            if (in_fn_param)
                            {
                                if (All_Tokens[index].Value_part != "+" && All_Tokens[index].Value_part != "*" && All_Tokens[index].Value_part != "/" && All_Tokens[index].Value_part != "-")
                                {
                                    fn_param_types += type + ",";
                                }
                            }
                        }
                        return true;
                    }

                }
                if (func_call(name))
                {
                    return true;
                }
                if (func_object())
                {
                    return true;

                }
                //if (Is_found_in_mehtod||Is_found_in_class||Is_found_in_main)
                //{
                //    return true;

                //}
            }


            return false;
        }

        #endregion

        #region rules
        bool datatype_identifier()
        {
            if (All_Tokens[index].class_part == "Datatype")
            {
                string type = All_Tokens[index].Value_part.ToString();
                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    string name = All_Tokens[index].Value_part.ToString();
                    if (is_inlcass)
                    {
                        //Data.all_classes_syntax[class_index].l2_list.Add(new L2(name, type));
                    }
                    index++;
                    if (options(type, false, false))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool identifier()
        {
            if (All_Tokens[index].class_part == "Identifier" || All_Tokens[index].class_part == "Datatype")
            {
                string type_or_name = All_Tokens[index].Value_part.ToString();
                bool Is_func = false;
                bool Is_variable = true;
                index++;
                if (All_Tokens[index].class_part == "=" || All_Tokens[index].class_part == "+" || All_Tokens[index].class_part == "-")
                {

                    Is_variable = true;
                }
                if (All_Tokens[index].class_part == "(")
                {
                    Is_func = true;
                }
                if (class_object(type_or_name))
                {
                    return true;

                }
                if (options(type_or_name, Is_func, Is_variable))
                {
                    return true;
                }

            }
            return false;
        }


        bool options(string type_or_name, bool Is_func, bool Is_variable)
        {
            if (Is_variable)
            {
                string type = "";
                bool Is_found_in_mehtod = false;
                bool Is_found_in_class = false;
                bool Is_found_in_main = false;
                bool found = false;
                if (is_inmethod)
                {

                    //// inside metod
                    int scope_from_method = 0;
                    for (int i = stack_index - 1; i >= 0; i--)
                    {
                        if (Data.d_varible_lookup_inmethod(type_or_name, stack[i], class_index, method_index)) ////eistancw
                        {
                            scope_from_method = stack[i];
                            Is_found_in_mehtod = true;
                            found = true;
                            //if (All_Tokens[index].class_part == ";")
                            //{
                            //index++;
                            //return true;
                            //}
                        }
                    }
                    if (!Is_found_in_mehtod)
                    {
                        if (Data.d_varible_lookup_inclass(type_or_name, class_index))
                        {
                            Is_found_in_class = true;
                            found = true;

                        }
                    }
                    if (!Is_found_in_mehtod && !Is_found_in_class)
                    {
                        if (Data.d_varible_lookup_inmethod(type_or_name, 1, 0, 0))
                        {
                            Is_found_in_main = true;
                            found = true;

                        }
                    }
                    if (Is_found_in_mehtod)
                    {
                        if ("0" != Data.get_type_inmethod(type_or_name, scope_from_method, class_index, method_index)) /// type return
                        {
                            type_or_name = Data.get_type_inmethod(type_or_name, scope_from_method, class_index, method_index);

                        }


                    }
                    if (Is_found_in_class)
                    {
                        if ("0" != Data.get_type_inclass(type_or_name, class_index))
                        {
                            type_or_name = Data.get_type_inclass(type_or_name, class_index);

                        }

                    }
                    if (Is_found_in_main)
                    {
                        if ("0" != Data.get_type_inmethod(type_or_name, 1, 0, 0)) /// type return
                        {
                            type_or_name = Data.get_type_inmethod(type_or_name, 1, 0, 0);

                        }

                    }
                }
                else
                {
                    //// outside method
                    if (Data.d_varible_lookup_inclass(type_or_name, class_index))
                    {
                        Is_found_in_class = true;
                        found = true;

                    }
                    if (!Is_found_in_class)
                    {
                        if (Data.d_varible_lookup_inmethod(type_or_name, 1, 0, 0))
                        {
                            Is_found_in_main = true;
                            found = true;

                        }
                    }
                    if (Is_found_in_class)
                    {
                        if ("0" != Data.get_type_inclass(type_or_name, class_index))
                        {
                            type_or_name = Data.get_type_inclass(type_or_name, class_index);

                        }

                    }


                    if (Is_found_in_main)
                    {
                        if ("0" != Data.get_type_inmethod(type_or_name, 1, 0, 0)) /// type return
                        {
                            type_or_name = Data.get_type_inmethod(type_or_name, 1, 0, 0);

                        }

                    }
                }
                if (found)
                {


                    if (OE_equal(type_or_name))
                    {
                        return true;
                    }
                    if (OE())
                    {

                        return true;
                    }
                    if (INC_DEC())
                    {
                        return true;
                    }
                    if (Decl_Assign())
                    {
                        return true;
                    }
                    if (func_object())
                    {
                        if (All_Tokens[index].class_part == ";")
                        {
                            index++;
                            return true;
                        }
                    }
                }
            }

            if (Is_func)
            {
                if (func_call(type_or_name))
                {
                    if (All_Tokens[index].class_part == ";")
                    {
                        index++;
                        return true;
                    }
                }


            }
            if (!Is_func && !Is_variable)
            {
                if (OE_equal(type_or_name))
                {
                    return true;
                }
                if (OE())
                {

                    return true;
                }
                if (INC_DEC())
                {
                    return true;
                }
                if (Decl_Assign())
                {
                    return true;
                }
                if (func_call(type_or_name))
                {
                    if (All_Tokens[index].class_part == ";")
                    {
                        index++;
                        return true;
                    }
                }
                if (func_object())
                {
                    if (All_Tokens[index].class_part == ";")
                    {
                        index++;
                        return true;
                    }
                }
            }
            if (array())
            {
                return true;
            }

            return false;
        }

        public bool Decl_Assign()
        {

            if (Init())
            {
                if (list())
                {
                    return true;
                }
            }
            return false;
        }
        bool constant()
        {

            if (All_Tokens[index].class_part == "Integer" || All_Tokens[index].class_part == "Float" || All_Tokens[index].class_part == "String" || All_Tokens[index].class_part == "character")
            {
                string type = "";
                // string type1 = All_Tokens[index].class_part;

                if (All_Tokens[index].class_part == "Integer")
                {
                    type = "num";
                }
                if (All_Tokens[index].class_part == "Float")
                {
                    type = "bignum";
                }
                if (All_Tokens[index].class_part == "String")
                {
                    type = "Line";
                }
                if (All_Tokens[index].class_part == "character")
                {
                    type = "char";
                }
                //  operandDT[OperatorIndex++]= type;
                stack_opr.Push(type);
                index++;
                if (in_fn_param)
                {
                    fn_param_types += type + ",";
                }
                return true;
            }

            return false;
        }

        bool list()
        {

            if (All_Tokens[index].class_part == ";")
            {
                index++;

                return true;
            }
            if (All_Tokens[index].class_part == ",")
            {

                index++;

                if (All_Tokens[index].class_part == "Identifier")
                {
                    index++;

                    if (Init())
                    {
                        if (list())
                        {
                            return true;

                        }
                    }
                }
            }

            return false;
        }
        bool Init1()
        {
            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;

                if (Init())
                {
                    return true;
                }

            }
            if (constant())
            {
                return true;
            }
            return false;
        }
        bool Init()
        {


            if (All_Tokens[index].class_part == "=")
            {
                index++;
                if (OE())
                {
                    return true;

                }
                //if (Init1())
                //{
                //    return true;
                //}


            }
            return true;

        }

        bool Body()
        {

            if (All_Tokens[index].class_part == ";")
            {
                index++;

                return true;
            }
            if (single_line_st())
            {
                return true;
            }
            if (multi_line_st())
            {
                return true;
            }
            return false;
        }

        bool INC_DEC()
        {

            //if (All_Tokens[index].class_part == "Identifier")
            //{
            //index++;
            if (All_Tokens[index].class_part == "Inc-Dec")
            {
                index++;
                if (All_Tokens[index].class_part == ";")
                {
                    index++;

                    return true;
                }
            }
            //}
            return false;
        }

        bool IDF_const()
        {
            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;
                return true;
            }
            if (constant())
            {
                return true;
            }
            return false;
        }

        bool condition()
        {


            if (IDF_const())
            {
                //if (All_Tokens[index].class_part == "Relation-Operator")
                //{
                //index++;
                if (condition_more())
                {
                    //index++;
                    ////////////////////////////////////
                    return true;
                }
                //}
            }
            return false;
        }
        bool condition_more()
        {

            if ((All_Tokens[index].class_part == "Relation-Operator") || (All_Tokens[index].class_part == "="))
            {
                index++;
                if (IDF_const())
                {
                    if (condition_more())
                    {

                        return true;

                    }
                }
            }

            return true;
        }
        bool Repeat_st()
        {
            if (All_Tokens[index].class_part == "repeat")
            {
                index++;
                if (All_Tokens[index].class_part == "(")
                {
                    index++;
                    if ((datatype_identifier() || identifier()))
                    {
                        //if (All_Tokens[index].class_part == ";")
                        //{
                        //index++;
                        if (OE())
                        {
                            if (All_Tokens[index].class_part == ";")
                            {
                                index++;

                                if (f1())
                                {
                                    if (All_Tokens[index].class_part == ")")
                                    {
                                        index++;
                                        if (All_Tokens[index].class_part == "{")
                                        {
                                            create_scope();
                                            index++;
                                            if (multi_line_st())
                                            {
                                                if (All_Tokens[index].class_part == "}")
                                                {
                                                    destroy_scope();
                                                    index++;
                                                    return true;
                                                }
                                            }

                                        }
                                    }
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }


        bool f1()
        {

            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;
                if (f2())
                {
                    return true;
                }
            }
            return true;
        }

        bool f2()
        {
            if (INC_DEC())
            {
                return true;
            }
            return false;
        }
        bool Repeat_while()
        {
            if (All_Tokens[index].class_part == "repeatwhile")
            {
                index++;
                if (All_Tokens[index].class_part == "(")
                {
                    index++;
                    if (OE())
                    {
                        if (All_Tokens[index].class_part == ")")
                        {
                            index++;
                            if (All_Tokens[index].class_part == "{")
                            {
                                index++;
                                create_scope();
                                if (multi_line_st())
                                {
                                    if (All_Tokens[index].class_part == "}")
                                    {
                                        destroy_scope();
                                        index++;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        bool class_ini()
        {
            if (All_Tokens[index].class_part == "{")
            {
                index++;


                if (class_body())
                {
                    if (All_Tokens[index].class_part == "}")
                    {
                        index++;
                        return true;
                    }
                }
            }


            return false;
        }

        bool class_object(string type)
        {
            if (All_Tokens[index].class_part == "Identifier")
            {
                string name = All_Tokens[index].Value_part.ToString();
                index++;
                if (is_inlcass)
                {


                    if (Data.class_lookup(type, "class"))
                    {
                        if (is_inmethod)
                        {
                            if (OE_equal(type))
                            {
                                if (!Data.d_varible_lookup_inmethod(name, stack[stack_index - 1], class_index, method_index))
                                {
                                    Data.all_classes_semantic[class_index].l2_list[method_index].l3_list.Add(new L3(name, type, stack[stack_index - 1]));
                                    //if (All_Tokens[index].class_part == ";")
                                    //{
                                    //index++;
                                    return true;
                                    //}
                                }
                            }
                        }
                        else
                        {

                            if (OE_equal(type))
                            {
                                if (!Data.d_varible_lookup_inclass(name, class_index))
                                {
                                    method_index++;
                                    Data.all_classes_semantic[class_index].l2_list.Add(new L2(name, type));
                                    //if (All_Tokens[index].class_part == ";")
                                    //{
                                    //    index++;
                                    return true;
                                    //}
                                }
                            }
                        }

                    }
                }
                else
                {
                    if (Data.class_lookup(type, "class"))
                    {
                        //if (Data.d_variable_c3(name,stack[stack_index-1]))
                        //{
                        // Data.all_classes_semantic[0].l2_list[0].l3_list.Add(new L3())   

                        //}
                        if (OE_equal(type))
                        {
                            if (!Data.d_varible_lookup_inmethod(name, stack[stack_index - 1], 0, 0))
                            {
                                Data.all_classes_semantic[0].l2_list[0].l3_list.Add(new L3(name, type, stack[stack_index - 1]));
                                //if (All_Tokens[index].class_part == ";")
                                //{
                                //index++;
                                return true;
                                //}
                            }
                        }
                    }
                }
            }
            return false;
        }

        bool class_start()
        {
            if (All_Tokens[index].class_part == "class")
            {
                string type = All_Tokens[index].class_part.ToString();
                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    string name = All_Tokens[index].Value_part.ToString();
                    class_name = name;

                    //Data.all_classes.Add(new L1(name, type));
                    if (!Data.class_lookup_semantic(name, type))
                    {
                        Data.all_classes_semantic.Add(new L1(name, type));
                        class_index++;


                        is_inlcass = true;
                        index++;
                        method_index = -1;
                        if (class_ini())
                        {
                            return true;
                        }
                        //if (class_object())
                        //{
                        //    return true;


                        //}
                    }
                }
            }
            return false;
        }
        bool class_body()
        {
            //if (datatype_identifier())
            //{
            //    if (class_body())
            //    {
            //        return true;

            //    }
            //}
            if (identifier())
            {
                if (class_body())
                {
                    return true;

                }
            }
            if (func_def())
            {
                if (class_body())
                {
                    return true;

                }
            }

            return true;

        }

        bool either()
        {
            if (All_Tokens[index].class_part == "either")
            {
                index++;
                if (All_Tokens[index].class_part == "(")
                {
                    index++;
                    if (OE())
                    {
                        if (All_Tokens[index].class_part == ")")
                        {
                            index++;

                            if (All_Tokens[index].class_part == "{")
                            {
                                index++;
                                create_scope();
                                if (multi_line_st())
                                {
                                    if (All_Tokens[index].class_part == "}")
                                    {
                                        destroy_scope();
                                        index++;
                                        if (or())
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        bool or()
        {
            if (All_Tokens[index].class_part == "or")
            {
                index++;
                if (All_Tokens[index].class_part == "{")
                {
                    create_scope();
                    index++;
                    if (multi_line_st())
                    {
                        if (All_Tokens[index].class_part == "}")
                        {
                            destroy_scope();
                            index++;
                            return true;
                        }

                    }
                }
            }
            return true;
        }

        bool shift()
        {
            if (All_Tokens[index].class_part == "shift")
            {
                index++;
                if (All_Tokens[index].class_part == "(")
                {
                    index++;
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        index++;
                        if (All_Tokens[index].class_part == ")")
                        {
                            index++;
                            if (All_Tokens[index].class_part == "{")
                            {
                                create_scope();
                                index++;
                                if (bound())
                                {
                                    if (default_st())
                                    {
                                        if (All_Tokens[index].class_part == "}")
                                        {
                                            destroy_scope();
                                            index++;
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }


        bool bound()
        {
            if (All_Tokens[index].class_part == "bound")
            {
                index++;
                if (constant())
                {
                    if (All_Tokens[index].class_part == ":")
                    {
                        index++;
                        if ((All_Tokens[index].class_part == "Relation-Operator") && (All_Tokens[index].Value_part == "<"))
                        {
                            create_scope();
                            index++;
                            if (bound_body())
                            {
                                if ((All_Tokens[index].class_part == "Relation-Operator") && (All_Tokens[index].Value_part == ">"))
                                {
                                    destroy_scope();
                                    index++;

                                    if (bound())
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        bool bound_body()
        {
            if (multi_line_st())
            {

            }
            return true;
        }

        bool default_st()
        {
            if (All_Tokens[index].class_part == "default")
            {
                index++;
                if (All_Tokens[index].class_part == ":")
                {
                    index++;
                    if (multi_line_st())
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        bool break_st()
        {
            if (All_Tokens[index].class_part == "break")
            {
                index++;
            }
            return false;
        }


        bool func_def()
        {
            if (All_Tokens[index].class_part == "Define")
            {
                index++;
                if (All_Tokens[index].class_part == "Datatype")
                {
                    string type = "";

                    string parameters = "";
                    string return_type = All_Tokens[index].Value_part.ToString();
                    index++;
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        string name = All_Tokens[index].Value_part.ToString();
                        index++;
                        if (All_Tokens[index].class_part == "(")
                        {
                            is_inmethod = true;

                            index++;
                            if (func_parameter(ref type, ref parameters))
                            {
                                if (All_Tokens[index].class_part == ")")
                                {
                                    string[] param = new string[0];
                                    string[] types = new string[0];
                                    if (parameters != "")
                                    {
                                        param = new string[20];
                                        types = new string[20];
                                        param = parameters.Split(' ');
                                        types = type.Split(',');
                                    }

                                    create_scope();
                                    method_index++;
                                    string type_void = type;
                                    type += "-->" + return_type;
                                    //if (Data.class_lookup_semantic(class_name, "class"))
                                    //{
                                    if (!Data.method_lookup_semantic(name, type, class_index))
                                    {
                                        Data.all_classes_semantic[class_index].l2_list.Add(new L2(name, type));
                                        if (param.Length > 0)
                                        {


                                            for (int i = 0; i < param.Length; i++)
                                            {
                                                if (!Data.d_varible_lookup_inmethod(param[i], stack[stack_index - 1], class_index, method_index))
                                                {
                                                    Data.all_classes_semantic[class_index].l2_list[method_index].l3_list.Add(new L3(param[i], types[i], stack[stack_index - 1]));
                                                }
                                            }
                                        }
                                        if (type_void == "")
                                        {
                                            Data.all_classes_semantic[class_index].l2_list[method_index].l3_list.Add(new L3("void", "void", stack[stack_index - 1]));

                                        }

                                        //}
                                        index++;
                                        if (All_Tokens[index].class_part == "{")
                                        {

                                            index++;
                                            if (func_body())
                                            {
                                                if (All_Tokens[index].class_part == "}")
                                                {
                                                    destroy_scope();
                                                    is_inmethod = false;
                                                    index++;
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        bool func_parameter(ref string type, ref string parameters)
        {
            if (All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                type = All_Tokens[index].Value_part.ToString();
                index++;

                if (Data.class_lookup(type, "class"))
                {
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        parameters = All_Tokens[index].Value_part.ToString();
                        index++;
                        if (fn_INIT())
                        {
                            if (func_parameter_1(ref type, ref parameters))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return true;
        }

        bool func_parameter_1(ref string type, ref string parameters)
        {
            if (All_Tokens[index].class_part == ",")
            {
                index++;
                if (All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
                {
                    type += "," + All_Tokens[index].Value_part.ToString();
                    string type_check = All_Tokens[index].Value_part.ToString();
                    index++;
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        string param_name = All_Tokens[index].Value_part.ToString();

                        string[] split = parameters.Split(' ');
                        foreach (string item in split)
                        {
                            if (!(item == param_name))
                            {
                                parameters += " " + param_name;
                                index++;
                                if (Data.class_lookup(type_check, "class"))
                                {
                                    if (fn_INIT())
                                    {
                                        if (func_parameter_1(ref type, ref parameters))
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return true;
        }
        bool fn_INIT()
        {
            if (All_Tokens[index].class_part == "=")
            {
                index++;
                if (OE())
                {

                    return true;
                }
            }
            return true;
        }

        bool fn_INIT_more()
        {
            if (constant())
            {
                return true;
            }
            return false;
        }



        bool func_body()
        {
            if (multi_line_st())
            {
                //return true;
            }
            if (All_Tokens[index].class_part == "return")
            {
                index++;
                if (fn_return())
                {
                    if (All_Tokens[index].class_part == ";")
                    {
                        index++;

                        return true;
                    }
                }
            }
            return true;
        }

        bool fn_return()
        {
            if (OE())
            {
                return true;
            }

            return false;
        }

        bool array()
        {
            if (All_Tokens[index].class_part == "[")
            {
                index++;
                if (size())
                {
                    if (All_Tokens[index].class_part == "]")
                    {
                        index++;
                        if (array_detail())
                        {
                            if (All_Tokens[index].class_part == ";")
                            {
                                index++;
                                return true;
                            }
                        }

                    }
                }
            }
            return false;
        }

        bool size()
        {
            if (OE())
            {
                return true;
            }
            return true;
        }

        bool array_detail()
        {
            if (All_Tokens[index].class_part == "=")
            {
                index++;
                if (All_Tokens[index].class_part == "{")
                {
                    index++;
                    if (IDF_const())
                    {
                        if (id_const_more())
                        {
                            if (All_Tokens[index].class_part == "}")
                            {
                                index++;
                                return true;

                            }
                        }
                    }

                }

            }

            return true;
        }

        bool id_const_more()
        {
            if (All_Tokens[index].class_part == ",")
            {
                index++;
                if (IDF_const())
                {
                    if (id_const_more())
                    {
                        return true;
                    }
                }

            }
            return true;
        }

        bool func_call(string name)
        {

            if (All_Tokens[index].class_part == "(")
            {
                index++;
                in_fn_param = true;
                IS_func_running = true;
                if (fn_call_param())
                {
                    in_fn_param = false;
                    string[] a = fn_param_types.Split(',');
                    fn_param_types = "";
                    string typ = "";
                    for (int i = 0; i < a.Length; i++)
                    {
                        typ += a[i];
                    }
                    if (All_Tokens[index].class_part == ")")
                    {
                        index++;
                        if (is_inlcass)
                        {
                            if (Data.method_lookup_syntax_fn_param(name, typ, class_index + 2))
                            {
                                Is_func_ok = true;
                                return true;

                            }
                        }
                        else
                        {
                            if (Data.method_lookup_syntax_fn_param(name, typ, class_index + 3))
                            {
                                Is_func_ok = true;

                                return true;

                            }
                        }
                        //if (All_Tokens[index].class_part == ";")
                        //{
                        //index++;
                        //}
                    }

                }
            }
            return false;
        }

        bool fn_call_param()
        {
            if (OE())
            {

                if (fn_INIT())
                {
                    if (fn_call_param1())
                    {
                        return true;

                    }
                }
            }
            return true;
        }

        bool fn_call_param1()
        {
            if (All_Tokens[index].class_part == ",")
            {
                index++;
                if (fn_call_param2())
                {
                    return true;
                }
            }
            return true;
        }

        bool fn_call_param2()
        {

            if (OE())
            {

                if (fn_INIT())
                {
                    if (fn_call_param1())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool func_object()
        {
            if (All_Tokens[index].class_part == ".")
            {
                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    string name = All_Tokens[index].Value_part;
                    index++;
                    if (func_call(name))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        public void create_scope()
        {
            scope++;
            stack[stack_index] = scope;
            stack_index++;
        }
        public void destroy_scope()
        {
            stack_index--;
        }

        #region TypeReturn
        Stack<string> stack_opr = new Stack<string>();
        public static int OperatorIndex = 0;
        public static string[] operandDT = new string[2];
        public string[,] typechk = {
{ "num", "num","+", "num" }, { "num", "num", "-", "num" }, { "num", "num", "*", "num" }, { "num", "num", "/", "num" },{ "num", "num", "%", "num" },
                            {"num", "bignum","+", "bignum" },{ "num", "bignum", "-", "bignum" },{ "num", "bignum", "*", "bignum" },{ "num", "bignum", "/", "bignum" },{ "num", "bignum", "%", "bignum" } ,
                            { "bignum", "num","+", "bignum" }, { "bignum", "num", "-", "bignum" }, { "bignum", "num", "*", "bignum" }, { "bignum", "num", "/", "bignum" },{ "bignum", "num", "%", "bignum" },
                            {"bignum", "bignum","+", "bignum" },{ "bignum", "bignum", "-", "bignum" },{ "bignum", "bignum", "*", "bignum" },{ "bignum", "bignum", "/", "bignum" },{ "bignum", "bignum", "%", "bignum" } 
                           ,{ "line", "line","+", "line" },{ "bignum", "line", "+", "line" },{ "num", "line", "+", "line" }
                            
                           ,{ "num", "num", "=", "num" },{ "bignum", "bignum", "=", "bignum" },{ "bignum", "num", "=", "bignum" }
                           ,{ "line", "line", "=", "line" },{ "char", "char", "=", "char" }

                           ,{"num","num","<","Bool"},{"bignum","num","<","Bool"},{"num","bignum","<","Bool"},{"bignum","bignum","<","Bool"}
                           ,{"num","num",">","Bool"},{"bignum","num",">","Bool"},{"num","bignum",">","Bool"},{"bignum","bignum",">","Bool"}
                           ,{"num","num","<=","Bool"},{"bignum","num","<=","Bool"},{"num","bignum","<=","Bool"},{"bignum","bignum","<=","Bool"}
                           ,{"num","num",">=","Bool"},{"bignum","num",">=","Bool"},{"num","bignum",">=","Bool"},{"bignum","bignum",">=","Bool"}
                           ,{"num","num","!=","Bool"},{"bignum","num","!=","Bool"},{"num","bignum","!=","Bool"},{"bignum","bignum","!=","Bool"}
                           ,{"num","num","==","Bool"},{"bignum","num","==","Bool"},{"num","bignum","==","Bool"},{"bignum","bignum","==","Bool"}
                           ,{"Bool","Bool","&&","Bool"},{"Bool","Bool","&&","Bool"}

                           // { "int", "int","+", "int" }, { "int", "int", "-", "int" }, { "int", "int", "*", "int" }, { "int", "int", "/", "float" },{ "int", "int", "%", "int" },
                           // {"int", "float","+", "float" },{ "int", "float", "-", "float" },{ "int", "float", "*", "float" },{ "int", "float", "/", "float" },{ "int", "float", "%", "float" } ,
                           // { "float", "int","+", "float" }, { "float", "int", "-", "float" }, { "float", "int", "*", "float" }, { "float", "int", "/", "float" },{ "float", "int", "%", "float" },
                           // {"float", "float","+", "float" },{ "float", "float", "-", "float" },{ "float", "float", "*", "float" },{ "float", "float", "/", "float" },{ "float", "float", "%", "float" } 
                           //,{ "string", "string","+", "string" },{ "float", "string", "+", "string" },{ "int", "string", "+", "string" }
                            
                           //,{ "int", "int", "=", "int" },{ "float", "float", "=", "float" },{ "float", "int", "=", "float" }
                           //,{ "string", "string", "=", "string" },{ "char", "char", "=", "char" }

                           //,{"int","int","<","Bool"},{"float","int","<","Bool"},{"int","float","<","Bool"},{"float","float","<","Bool"}
                           //,{"int","int",">","Bool"},{"float","int",">","Bool"},{"int","float",">","Bool"},{"float","float",">","Bool"}
                           //,{"int","int","<=","Bool"},{"float","int","<=","Bool"},{"int","float","<=","Bool"},{"float","float","<=","Bool"}
                           //,{"int","int",">=","Bool"},{"float","int",">=","Bool"},{"int","float",">=","Bool"},{"float","float",">=","Bool"}
                           //,{"int","int","!=","Bool"},{"float","int","!=","Bool"},{"int","float","!=","Bool"},{"float","float","!=","Bool"}
                           //,{"int","int","==","Bool"},{"float","int","==","Bool"},{"int","float","==","Bool"},{"float","float","==","Bool"}

                          
                           // { "Number", "Number","+", "Number" }, { "Number", "Number", "-", "Number" }, { "Number", "Number", "*", "Number" }, { "Number", "Number", "/", "PNumber" },{ "Number", "Number", "%", "Number" },
                           // {"Number", "PNumber","+", "PNumber" },{ "Number", "PNumber", "-", "PNumber" },{ "Number", "PNumber", "*", "PNumber" },{ "Number", "PNumber", "/", "PNumber" },{ "Number", "PNumber", "%", "PNumber" } ,
                           // { "PNumber", "Number","+", "PNumber" }, { "PNumber", "Number", "-", "PNumber" }, { "PNumber", "Number", "*", "PNumber" }, { "PNumber", "Number", "/", "PNumber" },{ "PNumber", "Number", "%", "PNumber" },
                           // {"PNumber", "PNumber","+", "PNumber" },{ "PNumber", "PNumber", "-", "PNumber" },{ "PNumber", "PNumber", "*", "PNumber" },{ "PNumber", "PNumber", "/", "PNumber" },{ "PNumber", "PNumber", "%", "PNumber" } 
                           //,{ "NChar", "NChar","+", "NChar" },{ "PNumber", "NChar", "+", "NChar" },{ "Number", "NChar", "+", "NChar" }
                            
                           //,{ "Number", "Number", "=", "Number" },{ "PNumber", "PNumber", "=", "PNumber" },{ "PNumber", "Number", "=", "PNumber" }
                           //,{ "NChar", "NChar", "=", "NChar" },{ "SChar", "SChar", "=", "SChar" }

                           //,{"Number","Number","<","Bool"},{"PNumber","Number","<","Bool"},{"Number","PNumber","<","Bool"},{"PNumber","PNumber","<","Bool"}
                           //,{"Number","Number",">","Bool"},{"PNumber","Number",">","Bool"},{"Number","PNumber",">","Bool"},{"PNumber","PNumber",">","Bool"}
                           //,{"Number","Number","<=","Bool"},{"PNumber","Number","<=","Bool"},{"Number","PNumber","<=","Bool"},{"PNumber","PNumber","<=","Bool"}
                           //,{"Number","Number",">=","Bool"},{"PNumber","Number",">=","Bool"},{"Number","PNumber",">=","Bool"},{"PNumber","PNumber",">=","Bool"}
                           //,{"Number","Number","!=","Bool"},{"PNumber","Number","!=","Bool"},{"Number","PNumber","!=","Bool"},{"PNumber","PNumber","!=","Bool"}
                           //,{"Number","Number","==","Bool"},{"PNumber","Number","==","Bool"},{"Number","PNumber","==","Bool"},{"PNumber","PNumber","==","Bool"}


                            };
        public string[,] typechk_Uniary = 
                            {

                             {"int","++", "int" }, { "float", "++" ,"float"}
                            ,{"int","--", "int" }, { "float", "--" ,"float"}
                            ,{"int","!", "Bool" }, { "float", "!" ,"Bool"}
                       
                            };

        public string compatiblity(string LT, string RT, string optr)
        {
            for (int i = 0; i < typechk.Length / 4; i++)
            {
                if (typechk[i, 0] == LT && typechk[i, 1] == RT && typechk[i, 2] == optr)
                {
                    return typechk[i, 3];
                }

            }


            return "Not Compatible";

        }
        public string Uniarycompatiblity(string LT, string optr)
        {
            for (int i = 0; i < typechk_Uniary.Length; i++)
            {
                if (typechk_Uniary[i, 0] == LT && typechk_Uniary[i, 2] == optr)
                {
                    return typechk_Uniary[i, 3];
                }

            }


            return "Not Compatible";

        }

        #endregion

    }
}
