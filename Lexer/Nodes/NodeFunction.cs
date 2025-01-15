using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lexer.Context;

namespace Lexer.Nodes
{
    public class NodeFunction : Node
    {
        private string _functionName;
        private Node[] _arguments;
        
        public NodeFunction(string functionName, Node[] arguments)
        {
            _functionName = functionName;
            _arguments = arguments;
        }

        public override double Eval(IContext context)
        {
            double[] values = new double[_arguments.Length];

            for (int i = 0; i < _arguments.Length; i++)
            {
                values[i] = _arguments[i].Eval(context);
            }

            return context.ResolveFunction(_functionName, values);
        }
    }
}
