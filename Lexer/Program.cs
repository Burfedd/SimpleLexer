using System;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            StringReader sr = new StringReader(input);

            Tokenizer t = new Tokenizer(sr);
            Parser p = new Parser(t);

            Node result = p.ParseExpression();

            Console.WriteLine(result.Eval());
        }
    }
}
