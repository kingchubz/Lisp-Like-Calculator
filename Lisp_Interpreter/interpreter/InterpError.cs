using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lisp_Interpreter
{
    [Serializable]
    class InterpError : Exception
    {
            public InterpError() : base() { }
            public InterpError(string message) : base(message) { }
            public InterpError(string message, Exception inner) : base(message, inner) { }

    }
}
