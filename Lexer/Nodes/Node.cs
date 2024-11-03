using Lexer.Context;

namespace Lexer.Nodes
{
    public abstract class Node
    {
        public abstract double Eval(IContext context);
    }
}
