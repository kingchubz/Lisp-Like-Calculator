using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace Lisp_Interpreter
{
    public class Interpreter
    {
        
        public static void init_enviroment(ref Enviroment env)
        {
            env = new Enviroment();
            env.Add("+", new token_node(token_type.procedure, "+"));
            env.Add("-", new token_node(token_type.procedure, "-"));
            env.Add("*", new token_node(token_type.procedure, "*"));
            env.Add("begin", new token_node(token_type.procedure, "begin"));
        }

        public static String go(String code, ref Enviroment env) {
            Queue<String> segments = segmentator(code);

            token_node tree = parser(ref segments);


            token_node result = eval(tree, env);
            return show_tree(result);
        }

        private static String show_tree(token_node tree)
        {
            if (tree.type == token_type.list)
            {
                List<token_node> l = (List<token_node>)tree.value;
                String str = "";
                foreach (token_node e in l)
                {
                    if (e.type == token_type.list)
                        str = str + show_tree(e);
                    else
                        str = str + "\'" + (String)e.value + "\'";     
                }
                return str;
            }
            else
                return "\'" + (String)tree.value + "\'";
        }
        private static Queue<String> segmentator(String text)
        {

            if (text.Equals(""))
                return null;

            text = text.Replace("(", " ( ");
            text = text.Replace(")", " ) ");

            char[] separator = new char[] { ' ', '\n', '\r' };
            List<String> splits = new List<String>(text.Split(separator));

            Regex none = new Regex(@"^\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            Queue<String> segments = new Queue<String>();

            foreach (String split in splits)
                if (!none.IsMatch(split))
                    segments.Enqueue(split);
            return segments;
        }

        private static token_node parser(ref Queue<String> segments)
        {

            if (segments == null)
                return new token_node(token_type.empty, "");

            Regex number = new Regex(@"^[\+-]?([0-9]+[.])?[0-9]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex symbol = new Regex(@"^[\S][\S]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            String segment = segments.Dequeue();

            if (segment.Equals("("))
            {

                List<token_node> subtree = new List<token_node>();
                
                try
                {
                    while (!segments.Peek().Equals(")"))
                    {
                        subtree.Add(parser(ref segments));
                    }
                }
                catch (Exception) 
                {
                        throw new InterpError("error no closing bracket");
                    }
                
                    
                segments.Dequeue();

                token_node tree = new token_node(token_type.list, subtree);

                return tree;
            }
            else if (segment.Equals(")"))
                throw new InterpError("unexpected token: )");
            else if (number.IsMatch(segment))
                return new token_node(token_type.number, segment);
            else if (symbol.IsMatch(segment))
                return new token_node(token_type.symbol, segment);
            else
                throw new InterpError("unexpected token: " + "\'" + segment + "\'");



        }

        public static token_node eval(token_node tree, Enviroment env)
        {
            switch(tree.type)
            {
                case token_type.empty:
                    throw new InterpError("unexpected empty input");

                case token_type.number:
                    return tree;

                case token_type.symbol:
                    String key = (String)tree.value;

                    token_node result;
                    if (env.TryGetValue(key, out result))
                        return result;
                    else
                        throw new InterpError("undefined: " + key);
                case token_type.procedure:
                    return tree;
            }
            
            List<token_node> subtree = (List<token_node>)tree.value;
            token_node oper = subtree[0];
            
            subtree.RemoveAt(0);
            List<token_node> args = subtree;
            

            if (oper.type == token_type.symbol) 
            {
                switch ((String)oper.value) 
                {
                    case "quote":
                        break;
                    //case "if":
                    //    if (args.Count == 3) {
                    //        if (eval(args[0], env))
                    //            return eval(args[1], env);
                    //        else
                    //            return eval(args[2], env);
                    //    }    
                    //    break;
                    case "define":
                        if (args.Count == 2)
                            if (args[0].type == token_type.symbol)
                            {
                                token_node val = eval(args[1], env);
                                env.Add((String)args[0].value, val);
                                return new token_node(token_type.empty,"");
                            }
                        throw new InterpError("bad syntax: define");
                    case "set!":
                        break;
                    case "lambda":
                        if (args.Count == 2)
                        {
                            if (args[0].type == token_type.list && args[1].type == token_type.list)
                            {
                                token_node proc = new token_node(token_type.procedure, new User_Procedure(args[0], args[1], env));
                                return proc;
                            }
                        }

                        throw new InterpError("bad syntax: lambda");

                }
            }
            
            oper = eval(oper, env);
            for (int i = 0; i < args.Count; i++)
                args[i] = eval(args[i], env);

            if(typeof(String).IsInstanceOfType(oper.value))
                return Standart_Procedure.proc((String)oper.value, args);
            User_Procedure procedure;
            //if (oper.type == token_type.procedure)
            procedure = (User_Procedure)oper.value;
            //else;
            //trow
            return procedure.proc(args);
            
            //return new token_node(token_type.error, "fuck off");
        }

    }
    
    public enum token_type
    {
        empty,
        symbol,
        number,
        list,
        procedure
    }
    public struct token_node
    {
        public token_node(token_type t, String v)
        {
            type = t;
            value = v;
        }

        public token_node(token_type t, List<token_node> v)
        {
            type = t;
            value = v;
        }

        public token_node(token_type t, User_Procedure v)
        {
            type = t;
            value = v;
        }

        public token_type type;
        public Object value;
    }

}
