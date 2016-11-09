using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class CFG_syntax : Program
    {

        int class_index = 2;
        int method_index = 0;
        string class_name = "";
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
                            index++;
                            if (multi_line_st())
                            {
                                if (All_Tokens[index].class_part == "}")
                                {
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
            if (datatype_identifier())
            {
                return true;

            }
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

        bool OE()
        {
            if (AE())
            {
                if (OEx())
                {
                    return true;
                }

            }
            return false;
        }

        bool OEx()
        {

            if (All_Tokens[index].class_part == "||")
            {
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
            return false;
        }

        bool AEx()
        {
            if (All_Tokens[index].class_part == "&&")
            {
                index++;
                if (RE())
                {
                    if (AE())
                    {
                        return true;
                    }
                }
            }
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

        //bool ID_More()
        //{
        //    if (func_call())
        //    {
        //        return true;
        //    }
        //    if (All_Tokens[index].class_part == "Inc-Dec")
        //    {
        //        index++;
        //        return true;
        //    }
        //    return false;

        //}

        bool F()
        {

            if (constant())
            {
                return true;
            }
            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;
                if (All_Tokens[index].class_part == ";")
                {
                    index++;
                    return true;
                }
                if (func_call())
                {
                    return true;
                }
                if (func_object())
                {
                    return true;

                }
                if (array())
                {
                    return true;
                }
                return true;
            }


            return false;
        }

        #endregion

        #region rules
        bool datatype_identifier()
        {
            if (All_Tokens[index].class_part == "Datatype")
            {
                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    index++;
                    if (options())
                    {
                        return true;

                    }
                }
            }
            return false;
        }
        bool identifier()
        {
            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;
                if (options())
                {
                    return true;
                }
                //if (class_object())
                //{
                //    return true;

                //}
            }
            return false;
        }


        public bool OE_equal()
        {
            if (All_Tokens[index].class_part == "=")
            {
                index++;
                if (OE())
                {
                    if (All_Tokens[index].class_part == ";")
                    {
                        index++;
                        return true;
                    }
                }
            }
            return false;
        }
        bool options()
        {
            if (OE_equal())
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
            if (array())
            {
                if (All_Tokens[index].class_part == ";")
                {
                    index++;
                    return true;
                }
            }
            if (func_call())
            {
                if (All_Tokens[index].class_part == ";")
                {
                    index++;
                    return true;
                }
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
                index++;

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
            if (All_Tokens[index].class_part == ";" || All_Tokens[index].class_part == ",")
            {
                return true;
            }
            return false;

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
                    if (datatype_identifier() || identifier())
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
                                            index++;
                                            if (multi_line_st())
                                            {
                                                if (All_Tokens[index].class_part == "}")
                                                {
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

        // Yahan tak kar deya
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
                                if (multi_line_st())
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

        bool class_object()
        {
            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;
                if (All_Tokens[index].class_part == ";")
                {
                    index++;
                    return true;
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
                    //bool a=Data.class_lookup(name,type);
                    if (!Data.class_lookup(name, type))
                    {
                        Data.all_classes_syntax.Add(new L1(name, type));
                        class_index++;

                    }
                    index++;

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
            return false;
        }

        //ye bhi ho gay

        bool class_body()
        {
            if (datatype_identifier())
            {
                if (class_body())
                {
                    return true;

                }
            }
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

            if (All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "either" || All_Tokens[index].class_part == "Identifier" || All_Tokens[index].class_part == "Define" || All_Tokens[index].class_part == "}")
            {
                return true;
            }
            return false;

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
                                if (multi_line_st())
                                {
                                    if (All_Tokens[index].class_part == "}")
                                    {
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
                    index++;
                    if (multi_line_st())
                    {
                        if (All_Tokens[index].class_part == "}")
                        {
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
                                index++;
                                if (bound())
                                {
                                    if (default_st())
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
                            index++;
                            if (bound_body())
                            {
                                if ((All_Tokens[index].class_part == "Relation-Operator") && (All_Tokens[index].Value_part == ">"))
                                {
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
                    //string type = All_Tokens[index].class_part.ToString();
                    string type = "";
                    string return_type = All_Tokens[index].Value_part.ToString();
                    index++;
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        string name = All_Tokens[index].Value_part.ToString();
                        index++;
                        if (All_Tokens[index].class_part == "(")
                        {
                            index++;
                            if (func_parameter(ref type))
                            {
                                if (All_Tokens[index].class_part == ")")
                                {
                                    type += "-->" + return_type;
                                    if (Data.class_lookup(class_name, "class"))
                                    {
                                        if (!Data.method_lookup_syntax(name, type, class_index))
                                        {
                                            Data.all_classes_syntax[class_index].l2_list.Add(new L2(name, type));
                                        }
                                    }
                                    index++;
                                    if (All_Tokens[index].class_part == "{")
                                    {
                                        index++;
                                        if (func_body())
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
                        }
                    }
                }
            }
            return false;
        }

        bool func_parameter(ref string type)
        {
            if (All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
            {
                type = All_Tokens[index].Value_part.ToString();

                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    index++;
                    if (fn_INIT())
                    {
                        if (func_parameter_1(ref type))
                        {
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        bool func_parameter_1(ref string type)
        {
            if (All_Tokens[index].class_part == ",")
            {
                index++;
                if (All_Tokens[index].class_part == "Datatype" || All_Tokens[index].class_part == "Identifier")
                {
                    type += "," + All_Tokens[index].Value_part.ToString();

                    index++;
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        index++;
                        if (fn_INIT())
                        {
                            if (func_parameter_1(ref type))
                            {
                                return true;
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
                            // if (All_Tokens[index].class_part == ";")
                            {
                                ///  index++;
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

        bool func_call()
        {
            if (All_Tokens[index].class_part == "(")
            {
                index++;
                if (fn_call_param())
                {

                    if (All_Tokens[index].class_part == ")")
                    {
                        index++;
                        //if (All_Tokens[index].class_part == ";")
                        //{
                        //index++;
                        return true;
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
                    index++;
                    if (func_call())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
