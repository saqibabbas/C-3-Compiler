using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compiler
{
    class CFG : Program
    {


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
                return true;
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
            }
            return false;
        }

        bool options()
        {
            if (Decl_Assign())
            {
                return true;
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

                if (Init1())
                {
                    return true;
                }
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

            if (All_Tokens[index].class_part == "Identifier")
            {
                index++;
                if (All_Tokens[index].class_part == "Inc-Dec")
                {
                    index++;
                    return true;
                }
            }
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
                    if (Decl_Assign())
                    {
                        if (All_Tokens[index].class_part == ";")
                        {
                            index++;
                            if (condition())
                            {
                                if (All_Tokens[index].class_part == ";")
                                {
                                    index++;
                                    if (INC_DEC())
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
                        }
                    }
                }
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
                    if (condition())
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
                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    index++;

                    if (class_ini())
                    {
                        return true;
                    }
                    if (class_object())
                    {
                                                return true;


                    }
                }
            }
            return false;
        }
        bool class_body()
        {
            if (Decl_Assign())
            {
                return true;
            }
            if (func_def())
            {
            return true;

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
                    if (condition())
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
                if (All_Tokens[index].class_part == "Identifier")
                {
                    index++;
                    if (All_Tokens[index].class_part == "(")
                    {
                        index++;
                        if (func_parameter())
                        {
                            if (All_Tokens[index].class_part == ")")
                            {
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

            return false;
        }

        bool func_parameter()
        {
            if (All_Tokens[index].class_part == "Datatype")
            {
                index++;
                if (All_Tokens[index].class_part == "Identifier")
                {
                    index++;
                    if (fn_INIT())
                    {
                        if (func_parameter_1())
                        {
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        bool func_parameter_1()
        {
            if (All_Tokens[index].class_part == ",")
            {
                index++;
                if (All_Tokens[index].class_part == "Datatype")
                {
                    index++;
                    if (All_Tokens[index].class_part == "Identifier")
                    {
                        index++;
                        if (fn_INIT())
                        {
                            if (func_parameter_1())
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
                if (fn_INIT_more())
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
            if (IDF_const())
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
            if (IDF_const())
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

    }
}
