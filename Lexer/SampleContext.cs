using System;

namespace Lexer
{
    public class SampleContext : IContext
    {
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
