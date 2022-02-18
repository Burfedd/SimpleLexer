
namespace Lexer
{
    public class NodeNumber : Node
    {
        private double _number;

        public NodeNumber(double number)
        {
            _number = number;
        }

        public override double Eval(IContext context)
        {
            return _number;
        }
    }
}
