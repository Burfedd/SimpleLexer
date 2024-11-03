using System;
using Lexer.Context;

namespace Lexer.Nodes
{
    public class NodeUnary : Node
    {
        private Node _right;
        private Func<double, double> _op;

        public NodeUnary(Node right, Func<double, double> op)
        {
            _right = right;
            _op = op;
        }

        public override double Eval(IContext context)
        {
            double rightEval = _right.Eval(context);
            double result = _op(rightEval);
            return result;
        }
    }
}
