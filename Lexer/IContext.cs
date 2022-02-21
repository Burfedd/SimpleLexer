
namespace Lexer
{
    public interface IContext
    {
        double ResolveVariable(string name);
        double ResolveFunction(string name, double[] arguments);
    }
}
