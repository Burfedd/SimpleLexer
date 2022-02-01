using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer
{
    public class Tokenizer
    {
        public Tokenizer(TextReader reader)
        {

        }

        public Token Token { get; set; }
        public double Number { get; set; }

        public void NextToken()
        {

        }
    }

    public enum Token
    {
        EOF,
        Add,
        Subtract,
        Multiply,
        Divide,
        Number
    }
}
