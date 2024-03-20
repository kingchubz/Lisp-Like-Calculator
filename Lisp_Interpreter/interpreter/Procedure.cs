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
                case ">":
                    if (args.Count == 1)
                        return new token_node(token_type.constant, "#t");
                    else if (args.Count > 1) 
                    {
                        foreach(token_node arg in args)
                            if(arg.type != token_type.number)
                                throw new InterpError(">:argument is not a number");
                        float x = float.Parse((String)args[0].value);
                        float y = float.Parse((String)args[1].value);

                        return new token_node(token_type.constant, x>y ? "#t" : "#f");
                    }
                    else
                        throw new InterpError(">:no args");
                case "<":
                    if (args.Count == 1)
                        return new token_node(token_type.constant, "#t");
                    else if (args.Count > 1)
                    {
                        foreach (token_node arg in args)
                            if (arg.type != token_type.number)
                                throw new InterpError("<:argument is not a number");
                        float x = float.Parse((String)args[0].value);
                        float y = float.Parse((String)args[1].value);

                        return new token_node(token_type.constant, x < y ? "#t" : "#f");
                    }
                    else
                        throw new InterpError("<:no args");
                case ">=":
                    if (args.Count == 1)
                        return new token_node(token_type.constant, "#t");
                    else if (args.Count > 1)
                    {
                        foreach (token_node arg in args)
                            if (arg.type != token_type.number)
                                throw new InterpError(">=:argument is not a number");
                        float x = float.Parse((String)args[0].value);
                        float y = float.Parse((String)args[1].value);

                        return new token_node(token_type.constant, x >= y ? "#t" : "#f");
                    }
                    else
                        throw new InterpError(">=:no args");
                case "<=":
                    if (args.Count == 1)
                        return new token_node(token_type.constant, "#t");
                    else if (args.Count > 1)
                    {
                        foreach (token_node arg in args)
                            if (arg.type != token_type.number)
                                throw new InterpError("<=:argument is not a number");
                        float x = float.Parse((String)args[0].value);
                        float y = float.Parse((String)args[1].value);

                        return new token_node(token_type.constant, x <= y ? "#t" : "#f");
                    }
                    else
                        throw new InterpError("<=:no args");
                case "=":
                    if (args.Count == 1)
                        return new token_node(token_type.constant, "#t");
                    else if (args.Count > 1)
                    {
                        foreach (token_node arg in args)
                            if (arg.type != token_type.number)
                                throw new InterpError("=:argument is not a number");
                        float x = float.Parse((String)args[0].value);
                        float y = float.Parse((String)args[1].value);

                        return new token_node(token_type.constant, x == y ? "#t" : "#f");
                    }
                    else
                        throw new InterpError("=:no args");
                case "cons":
                    if (args.Count == 2)
                    {
                        List<token_node> tokens = args.ToList();
                        token_node pair = new token_node(token_type.list,tokens);

                        return pair;
                    }
                    else
                        throw new InterpError("cons: expected 2 args given: " + args.Count);
                case "car":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.list) 
                        {
                            List<token_node> list = (List<token_node>)args[0].value;
                            if(list.Count != 0)
                                return list[0];
                            else
                                throw new InterpError("car: empty list");
                        }
                        else
                            throw new InterpError("car: expected list given: " + args[0].type);
                    }
                    else
                        throw new InterpError("car: expected 1 args given: " + args.Count);
                case "cdr":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.list)
                        {
                            List<token_node> list = (List<token_node>)args[0].value;
                            
                            if (list.Count > 2)
                            {
                                List<token_node> cdr_list = list.ToList();
                                cdr_list.RemoveAt(0);
                                token_node cdr = new token_node(token_type.list, cdr_list);
                                return cdr;
                            }
                            else if (list.Count == 2)
                                return list[1];
                            else if (list.Count == 1)
                                return new token_node(token_type.empty, "");
                            else
                                throw new InterpError("cdr: empty list");
                        }
                        else
                            throw new InterpError("cdr: expected list given: " + args[0].type);
                    }
                    else
                        throw new InterpError("cdr: expected 1 args given: " + args.Count);
                case "empty?":
                    if (args.Count == 1) 
                    {
                        if (args[0].type == token_type.empty)
                            return new token_node(token_type.constant, "#t");
                        else
                            return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("empty?: expected 1 args given: " + args.Count);
                case "constant?":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.constant)
                            return new token_node(token_type.constant, "#t");
                        else
                            return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("constant?: expected 1 args given: " + args.Count);
                case "symbol?":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.symbol)
                            return new token_node(token_type.constant, "#t");
                        else
                            return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("symbol?: expected 1 args given: " + args.Count);
                case "number?":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.number)
                            return new token_node(token_type.constant, "#t");
                        else
                            return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("number?: expected 1 args given: " + args.Count);
                case "list?":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.list)
                            return new token_node(token_type.constant, "#t");
                        else
                            return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("list?: expected 1 args given: " + args.Count);
                case "procedure?":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.procedure)
                            return new token_node(token_type.constant, "#t");
                        else
                            return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("procedure?: expected 1 args given: " + args.Count);
                case "eq?":
                    if (args.Count == 2)
                    {
                        if (args[0].type == args[1].type)
                            if(args[0].value.Equals(args[1].value))
                                return new token_node(token_type.constant, "#t");

                        return new token_node(token_type.constant, "#f");
                    }
                    else
                        throw new InterpError("eq?: expected 2 args given: " + args.Count);
                case "round":
                    if (args.Count == 1)
                    {
                        if (args[0].type == token_type.number) {
                            float num = float.Parse((String)args[0].value);
                            return new token_node(token_type.number, Math.Round(num)+"");
                        }
                            
                       
                        throw new InterpError("round: expected number given: " + args[0].type);
                    }
                    else
                        throw new InterpError("round: expected 1 args given: " + args.Count);
            }

            throw new InterpError("unknown standart procedure: " + op);
        }
    }
}
