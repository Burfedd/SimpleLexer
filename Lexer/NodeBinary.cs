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

        public override double Eval()
        {
            double leftEval = _left.Eval();
            double rightEval = _right.Eval();

            double result = _op(leftEval, rightEval);
            return result;
        }
    }
}
