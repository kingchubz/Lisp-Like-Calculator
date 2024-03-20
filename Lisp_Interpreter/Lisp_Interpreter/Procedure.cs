using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp_Interpreter
{
    public class User_Procedure
    {
        token_node param;
        token_node body;
        Enviroment env;

        public User_Procedure(token_node p,token_node b,Enviroment e) 
        {
            param = p;
            body = b;
            env = e;
        }

        public token_node proc(List<token_node> args) 
        {
            List<String> names = new List<String>();
            List<token_node> param_list = (List<token_node>)param.value;

            foreach (token_node node in param_list)
                names.Add((String)node.value);

            Enviroment new_env = new Enviroment(names, args, env);

            return Interpreter.eval(body,new_env);
        }
    }

    public class Standart_Procedure
    {

        public static token_node proc(String op, List<token_node> args)
        {
            switch (op)
            {
                case "+":
                    float sum = 0;
                    foreach (token_node node in args)
                    {
                        if (node.type == token_type.number)
                        {
                            sum += float.Parse((String)node.value);
                        }
                        else
                            throw new InterpError("+: argument is not a number: " + node.value);
                    }

                    return new token_node(token_type.number, sum + "");

                case "-":
                    float dif;
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.number)
                        {
                            dif = -float.Parse((String)args[0].value);
                            return new token_node(token_type.number, dif + "");
                        }
                        else
                            throw new InterpError("-: argument is not a number:");

                    }
                    else if (args.Count > 1)
                    {
                        dif = float.Parse((String)args[0].value);
                        args.RemoveAt(0);

                        foreach (token_node node in args)
                        {
                            if (node.type == token_type.number)
                            {
                                dif -= float.Parse((String)node.value);
                            }
                            else
                                throw new InterpError("-: argument not a number:");
                        }

                        return new token_node(token_type.number, dif + "");
                    }
                    else
                        throw new InterpError("-: 0 arguments");

                case "*":
                    float mul = 1;
                    foreach (token_node node in args)
                    {
                        if (node.type == token_type.number)
                        {
                            mul *= float.Parse((String)node.value);
                        }
                        else
                            throw new InterpError("+: argument is not a number: " + node.value);
                    }

                    return new token_node(token_type.number, mul + "");
                case "/":
                    float div;
                    float divisor;
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.number)
                        {
                            divisor = float.Parse((String)args[0].value);
                            if (divisor != 0)
                            {
                                div = 1 / divisor;
                                return new token_node(token_type.number, div + "");
                            }
                            else
                                throw new InterpError("/: division by zero:");
                        }
                        else
                            throw new InterpError("-: argument is not a number:");

                    }
                    else if (args.Count > 1)
                    {
                        div = float.Parse((String)args[0].value);
                        args.RemoveAt(0);

                        foreach (token_node node in args)
                        {
                            if (node.type == token_type.number)
                            {
                                divisor = float.Parse((String)node.value);
                                if (divisor != 0)
                                    div /= float.Parse((String)node.value);
                                else
                                    throw new InterpError("/: division by zero:");
                            }
                            else
                                throw new InterpError("/: argument not a number:");

                        }

                        return new token_node(token_type.number, div + "");
                    }
                    else
                        throw new InterpError("/: 0 arguments");
                case "begin":
                    if (args.Count > 0)
                        return args[args.Count - 1];
                    else
                        return new token_node(token_type.empty,"");
            }

            throw new InterpError("unknown standart procedure");
        }
    }
}
