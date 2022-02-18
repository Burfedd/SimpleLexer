
namespace Lexer
{
    public interface IContext
    {
        double ResolveVariable(string name);
    }
}
