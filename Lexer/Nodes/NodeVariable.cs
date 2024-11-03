using System;
using Lexer.Context;

namespace Lexer.Nodes
{
    public class NodeVariable : Node
    {
        private string _variable;

        public NodeVariable(string name)
        {
            _variable = name;
        }

        public override double Eval(IContext context)
        {
            return context.ResolveVariable(_variable);
        }
    }
}
