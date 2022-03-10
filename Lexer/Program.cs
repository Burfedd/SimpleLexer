using System;

namespace Lexer
{
    class Program
    {
        static void Main(string[] args)
        {

            string input = Console.ReadLine();
            Console.WriteLine(Parser.Parse(input, new SampleContext()));
        }
    }
}
