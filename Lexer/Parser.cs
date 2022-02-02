using System;

namespace Lexer
{
    public class Parser
    {
        private Tokenizer _tokenizer;
        public Parser(Tokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public Node ParseExpression()
        {
            Node exp = ParseAddSubtract();

            if (_tokenizer.Token != Token.EOF)
            {
                throw new Exception("Invalid characters at end of expression");
            }

            return exp;
        }

        private Node ParseAddSubtract()
        {
            Node left = ParseLeaf();

            while (true)
            {
                Func<double, double, double> op = null;

                if (_tokenizer.Token == Token.Add)
                {
                    op = (a, b) => a + b;
                } 
                else if (_tokenizer.Token == Token.Subtract)
                {
                    op = (a, b) => a - b;
                }

                // If token isn't a binary operator
                if (op == null)
                {
                    return left;
                }

                // If token is a binary operator, parse the other side
                _tokenizer.NextToken();

                Node right = ParseLeaf();
                left = new NodeBinary(left, right, op);
            }
        }

        private Node ParseLeaf()
        {
            if (_tokenizer.Token == Token.Number)
            {
                NodeNumber node = new NodeNumber(_tokenizer.Number);
                _tokenizer.NextToken();
                return node;
            }

            // Invalid token
            throw new Exception($"Invalid token: {_tokenizer.Token}");
        }
    }
}
