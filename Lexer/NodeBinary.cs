using System;

namespace Lexer
{
    public class NodeBinary : Node
    {
        private Node _left;
        private Node _right;
        private Func<double, double, double> _op;

        public NodeBinary(Node left, Node right, Func<double, double, double> op)
        {
            _left = left;
            _right = right;
            _op = op;
        }

        public override double Eval(IContext context)
        {
            double leftEval = _left.Eval(context);
            double rightEval = _right.Eval(context);

            double result = _op(leftEval, rightEval);
            return result;
        }
    }
}
