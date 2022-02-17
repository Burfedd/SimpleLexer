using System;

namespace Lexer
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

        public override double Eval()
        {
            double rightEval = _right.Eval();
            double result = _op(rightEval);
            return result;
        }
    }
}
