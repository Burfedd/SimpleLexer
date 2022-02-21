using System;

namespace Lexer
{
    public class SampleContext : IContext
    {
        public double ResolveFunction(string name, double[] arguments)
        {
            switch ( name )
            {
                case "Perimeter":
                    return (arguments[0] + arguments[1]) * 2;
                case "Area":
                    return (arguments[0] * arguments[1]);
            }

            throw new Exception("Invalid function name!");
        }

        public double ResolveVariable(string name)
        {
            switch ( name )
            {
                case "pi":
                    return 3.14;
                case "e":
                    return 2.72;
            }

            throw new Exception("Invalid variable name!");
        }
    }
}
