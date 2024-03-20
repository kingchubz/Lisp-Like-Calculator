using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp_Interpreter
{
    public class Enviroment
    {
        public Enviroment()
        {
            vars = new Dictionary<String, token_node>();
        }
        public Enviroment(List<String> names, List<token_node> values, Enviroment e = null)
        {
            vars = new Dictionary<String, token_node>();

            if (names.Count == values.Count)
                for (int i = 0; i < names.Count; i++)
                    vars.Add(names[i], values[i]);
            else
                throw new InterpError("unexpected number values, expected: " + names.Count + " given:" + values.Count);

            env = e;
        }

        public bool ContainsKey(String key)
        {
            if (vars.ContainsKey(key))
                return true;
            else if (env != null)
                return env.ContainsKey(key);
            else
                return false;
        }

        public bool TryGetValue(String key, out token_node value)
        {

            if (vars.TryGetValue(key, out value))
                return true;
            else if (env != null)
                return env.TryGetValue(key, out value);
            else
                return false;
        }

        public bool Add(String key, token_node value)
        {
            try
            {
                vars.Add(key, value);
            }
            catch (ArgumentException)
            {
                vars.Remove(key);
                vars.Add(key, value);
            }

            return true;
        }

        protected Dictionary<String, token_node> vars;
        private Enviroment env;
    }
}
