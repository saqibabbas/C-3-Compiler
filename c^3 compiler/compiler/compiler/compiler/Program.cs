
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using DFA_saqib;
using FA;

namespace compiler
{
    class Program
    {
        static List<Token> All_tokens = new List<Token>();
        protected static List<Token> All_Tokens = new List<Token>();
        protected static int index = 0;

        static void Main(string[] args)
        {
            int b = 3, e = 2;

            switch (b)
            {
                case 'f':
                    break;
                case'k':
                    int sf;
                    break;
                default:
                    break;
            }

           
            string class_part = "";
            string Value_part = "";
            int line_no = 1;

            StreamReader reader = new StreamReader("code.txt");
            string body = reader.ReadToEnd();
            char[] code = body.ToCharArray();
            string[] check = new string[code.Length];
            int check_Counter = 0;
            for (int i = 0; i < code.Length; i++)
            {
                #region start
                #region line
                if (code[i] == '\n')
                {
                    line_no++;
                }
                #endregion
                #region number constants
                try
                {
                    bool ist = false;

                    if ((code[i] >= '0' && code[i] <= '9') || (code[i] == '.'))
                    {


                        do
                        {
                            if (code[i] == '.')
                            {
                                if (ist)
                                {
                                    i--;
                                    break;
                                }
                                else
                                {
                                    ist = true;

                                }
                            }
                            Value_part += code[i];
                            i++;
                            if (code.Length == i)
                            {
                                break;
                            }

                        }
                        //while ((code[i] >= 'A' && code[i] <= 'Z') || (code[i] >= 'a' && code[i] <= 'z') || (code[i] >= '0' && code[i] <= '9') || (code[i] == '_') || (code[i] == '.') || (code[i] == '+') || (code[i] == '-'));
                        while ((code[i] >= '0' && code[i] <= '9') || (code[i] == '.'));
                        class_part = "Invalid";

                        if (Is_integer(Value_part))
                        {
                            class_part = "Integer";
                        }
                        if (Is_float(Value_part))
                        {
                            class_part = "Float";

                        }
                        Make_Token(class_part, Value_part, line_no);
                        class_part = "";
                        Value_part = "";

                    }
                }
                catch (Exception)
                {


                }
                #endregion
                #region keyword and identifier
                try
                {


                    if ((code[i] >= 'A' && code[i] <= 'Z') || (code[i] >= 'a' && code[i] <= 'z') || (code[i] == '_'))
                    {
                        do
                        {
                            Value_part += code[i];
                            i++;
                            if (code.Length == i)
                            {
                                break;
                            }
                            //} while (((code[i] >= 'A' && code[i] <= 'Z') || (code[i] >= 'a' && code[i] <= 'z') || (code[i] >= '0' && code[i] <= '9') || (code[i] == '_')) && ((code[i] != ';') || (code[i] != '\n') || (code[i] != '\r')));
                        }
                        while ((code[i] >= 'A' && code[i] <= 'Z') || (code[i] >= 'a' && code[i] <= 'z') || (code[i] >= '0' && code[i] <= '9')  || (code[i] == '_'));
                        class_part = Key_word(Value_part);
                        if (class_part == "Invalid")
                        {
                            //if (Is_Array(Value_part))
                            //{
                            //    class_part = "Array";

                            //}
                            if (Is_identifier(Value_part))
                            {
                                class_part = "Identifier";
                            }
                            //if (!Is_identifier(Value_part) && !Is_Array(Value_part))
                            //{
                            //    class_part = "Invalid";

                            //}

                        }
                        Make_Token(class_part, Value_part, line_no);
                        class_part = "";
                        Value_part = "";
                    }
                }
                catch
                {


                }
                #endregion
                #region punctuators
                try
                {

                    if (code[i] == ';' || code[i] == ':' || code[i] == ',' || code[i] == '{' || code[i] == '}' || code[i] == '(' || code[i] == ')' || code[i] == '[' || code[i] == ']')
                    {
                        Make_Token(code[i].ToString(), code[i].ToString(), line_no);
                    }

                }
                catch
                {

                }

                #endregion
                #region operators
                try
                {
                    if ((code[i] == '=') || (code[i] == '!') || (code[i] == '%') || (code[i] == '*') || (code[i] == '-') || (code[i] == '+') || (code[i] == '<') || (code[i] == '>') || (code[i] == '/') || (code[i] == '&') || (code[i] == '|') || (code[i] == '~'))
                    {

                        Value_part += code[i];

                        if (code[i + 1] == '=')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '+' && code[i] == '+')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '-' && code[i] == '-')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '<' && code[i] == '<')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '>' && code[i] == '>')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '&' && code[i] == '&')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '|' && code[i] == '|')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '!')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '/')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '%')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '*')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '-')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '+')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '<')
                        {
                            i++;
                            Value_part += code[i];
                        }

                        else if (code[i + 1] == '=' && code[i] == '>')
                        {
                            i++;
                            Value_part += code[i];
                        }
                    }

                }

                catch
                {

                }

                class_part = Operators(Value_part);


                if (class_part != "Invalid")
                {
                    Make_Token(class_part, Value_part, line_no);
                    class_part = "";
                    Value_part = "";
                }







                #endregion
                #region char
                try
                {

                    int char_count = 0;
                    if ((code[i] == '\''))
                    {
                        do
                        {
                            Value_part += code[i];
                            char_count++;
                            if ((code[i] == '\\')&&(char_count==2))
                            {
                                char_count--;
                            }
                            i++;

                            if (code.Length == i)
                            {
                                break;
                            }
                        }
                        while (char_count < 3);
                        //while (code[i] != '\'' && char_count < 3) ;
                        i--;

                        //if (code[i - 1] == '\'')
                        //{
                        //    //Value_part += "'";
                        //    i--;

                        //}
                        DFA d = new DFA();

                        if (d.Char(Value_part))
                        {
                            class_part = "character";
                        }
                        else
                        {
                            class_part = "Invalid";

                        }
                        Make_Token(class_part, Value_part, line_no);
                        class_part = "";
                        Value_part = "";

                    }

                }
                catch
                {

                }

                #endregion
                #region string
                try
                {
                    if ((code[i] == '@'))
                    {
                        Value_part += code[i];
                        i++;
                        if ((code[i] == '\"'))
                        {
                            do
                            {
                                if ((code[i] == '\\'))
                                {
                                    Value_part += code[i];
                                    i++;

                                    if ((code[i] == '\"'))
                                    {
                                        Value_part += code[i];
                                        i++;
                                        continue;
                                    }

                                }
                                else
                                {
                                    Value_part += code[i];
                                    i++;
                                }
                                if (code.Length == i)
                                {
                                    break;
                                }
                            }
                            while ((code[i] != '\"') && (code[i] != '\r'));
                            Value_part += code[i];
                            i++;
                            string result = Is_string(Value_part);
                            if (result == "String")
                            {
                                class_part = "String";
                            }
                            else
                            {
                                class_part = "Invalid";

                            }

                            Make_Token(class_part, Value_part, line_no);
                            class_part = "";
                            Value_part = "";

                        }

                    }

                    if ((code[i] == '\"'))
                    {
                        do
                        {
                            if ((code[i] == '\\'))
                            {
                                Value_part += code[i];
                                i++;

                                if ((code[i] == '\"'))
                                {
                                    Value_part += code[i];
                                    i++;
                                    continue;
                                }

                            }
                            else
                            {
                                Value_part += code[i];
                                i++;
                            }
                        }
                        while ((code[i] != '\"') && (code[i] != '\r'));
                        Value_part += code[i];
                        string result = Is_string(Value_part);
                        if (result == "String")
                        {
                            class_part = "String";
                        }
                        else
                        {
                            class_part = "Invalid";

                        }

                        Make_Token(class_part, Value_part, line_no);
                        class_part = "";
                        Value_part = "";

                    }
                }
                catch (Exception)
                {


                }
                #endregion
                #region single line Comments

                try
                {
                    if ((code[i] == '#'))
                    {
                        if (code[i + 1] != '!')
                        {
                            try
                            {
                                while (code[i] != '\n')
                                {
                                    i++;
                                }
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            i++;
                            i++;
                            try
                            {
                                while (code[i] != '!')
                                {
                                    i++;
                                }
                                if (code[i] == '!')
                                {
                                    i++;
                                    if (code[i] == '#')
                                    {
                                        //    break;
                                    }
                                    else
                                    {
                                        // continue;
                                        while (code[i] != '!')
                                        {
                                            i++;
                                        }
                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                    }
                }
                catch
                {

                }

                #endregion
                #region invalids 
                try
                {


                    if ((code[i] == '$') || (code[i] == '^') || (code[i] == '~') || (code[i] == '!') || (code[i] == '@') || (code[i] == '_'))
                    {
                        class_part = "Invalid";
                        Value_part = code[i].ToString();

                        Make_Token(class_part, Value_part, line_no);
                        class_part = "";
                        Value_part = "";


                    }
                }
                catch (Exception)
                {


                }
                #endregion
            }

                #endregion

            write_fie();
            Read_token_classpart();
            if (start_syntax())
            {
                Console.WriteLine("successfully parsed");
            }
            else
            {
                Console.WriteLine("parsed failed");

            }  


        }
        static bool  start_syntax()
        {
            CFG cfg = new CFG();
            if ((cfg.my_main()) && (index == All_Tokens.Count))
            {
                return true;
            }
            return false;
        }
        static void Read_token_classpart()
        {

            StreamReader read = new StreamReader(@"C:\Users\Muhammad Arsalan\Desktop\compiler\compiler\bin\Debug\Tokens.txt");
            while (read.EndOfStream != true)
            {
                Token token = new Token();
                string line_from_file = read.ReadLine();
                char[] Array_of_line = line_from_file.ToCharArray();

                string value_part = "";
                string class_part = "";
                string line_no = "";
                int i = 1;
                // for class part
                while (Array_of_line[i] != ',')
                {
                    class_part += Array_of_line[i];
                    i++;
                }
                if (i == 1)
                {
                    class_part += Array_of_line[i];
                    i++;
                }
                i++;
                // for value part

                while (Array_of_line[i] != ',')
                {
                    value_part += Array_of_line[i];
                    i++;
                }
                if (i == 3)
                {
                    value_part += Array_of_line[i];
                    i++;
                }
                i++;
                // for line no

                while (Array_of_line[i] != ')')
                {
                    line_no += Array_of_line[i];
                    i++;
                }
                //char[] char_of_class = Array_of_line[0].ToCharArray();
                //char[] char_of_line_no = Array_of_line[2].ToCharArray();


                //int i = 1;
                //while (true)
                //{
                //    class_part = class_part + char_of_class[i];
                //    if (i < char_of_class.Length - 1)
                //    {
                //        i++;

                //    }
                //    else
                //    {
                //        break;
                //    }
                //}


                //i = 0;
                //while (char_of_line_no[i] != ')')
                //{
                //    line = line + char_of_line_no[i];
                //    i++;
                //}

                token.class_part = class_part;
                token.Value_part = value_part;
                token.line_no = Convert.ToInt32(line_no);

                All_Tokens.Add(token);
            }


        }


        static string Is_string(string Value_part)
        {
            return saqib_DFA_string.Result(Value_part);
        }
        static string Key_word(string Value_part)
        {
            string[,] KW = { { "class", "class" }, { "line", "Datatype" }, { "num", "Datatype" }, { "char", "Datatype" }, { "bignum", "Datatype" }, { "bool", "Datatype" }, { "repeat", "repeat" }, { "either", "either" }, { "or", "or" }, { "repeatwhile", "repeatwhile" }, { "bound", "bound" }, { "default", "default" }, { "shift", "shift" }, { "void", "void" }, { "Main", "Main" }, { "Define", "Define" }, { "return", "return" } };
            for (int i = 0; i < KW.Length / 2; i++)
            {
                if (Value_part.ToLower() == KW[i, 0].ToLower())
                {
                    return KW[i, 1];
                    break;
                }
            }
            return "Invalid";
        }

        static string Operators(string Value_part)
        {
            string[,] Operat = { { "=", "=" }, { "+=", "Asg-Operator" }, { "-=", "Asg-Operator" }, { "/=", "Asg-Operator" }, { "%=", "Asg-Operator" }, { "*=", "Asg-Operator" }, 
                               { "<", "Relation-Operator" }, { ">", "Relation-Operator" }, { "<=", "Relation-Operator" }, { ">=", "Relation-Operator" }, { "!=", "Relation-Operator" }, { "==", "Relation-Operator" },
                               { "+", "P-M" }, { "-", "P-M" }, { "*", "*" }, { "/", "D-M" }, { "%", "D-M" },
                               { "++", "Inc-Dec" }, { "--", "Inc-Dec" } ,
                                { "&&", "&&" }, { "||", "||" } ,
                                { "&", "&" }, { "!", "!" } ,{ "|", "|" }, { "~", "~" },{ "<<", "Insert" },{ ">>", "Insert" } 
                               
                               
                               
                               };
            for (int i = 0; i < Operat.Length / 2; i++)
            {
                if (Value_part.ToLower() == Operat[i, 0].ToLower())
                {
                    return Operat[i, 1];
                }
            }
            return "Invalid";
        }

        static bool Is_integer(string Value_part)
        {

            string integer = @"^[+-]?[0-9]+([eE][-+]?\d+)?$";
            return (Regex.IsMatch(Value_part, integer));
        }
        static bool Is_identifier(string Value_part)
        {
            string identifier = @"^[_A-Za-z][$_A-Za-z0-9]*$";
            return (Regex.IsMatch(Value_part, identifier));
        }
        //static bool Is_Array(string Value_part)
        //{
        //    string array = @"^[_A-Za-z][$_A-Za-z0-9]*[\[][0-9]+[\]]$";
        //    return (Regex.IsMatch(Value_part, array));
        //}
        static void Make_Token(string class_part, string Value_part, int line_no)
        {
            Token token = new Token(class_part, Value_part, line_no);
            All_tokens.Add(token);
        }

        static void write_fie()
        {
            StreamWriter writer = new StreamWriter("Tokens.txt");

            foreach (Token token in All_tokens)
            {
                writer.WriteLine("(" + token.class_part + "," + token.Value_part + "," + token.line_no + ")");
            }
            writer.Flush();
            writer.Close();

        }

        static bool Is_float(string Value_part)
        {
            string Float = @"^[-+]?\d*\.\d+([eE][-+]?\d+)?$";
            return (Regex.IsMatch(Value_part, Float));
        }
        static bool Is_char(string Value_part)
        {
            string character = @"^'[\w\s]'$";
            string a =saqib_DFA_character.Result(Value_part);
            return (Regex.IsMatch(Value_part, character));
        }
    }
}
