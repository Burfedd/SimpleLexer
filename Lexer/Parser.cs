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
            Node exp = ParseFourActions();

            if (_tokenizer.Token != Token.EOF)
            {
                throw new Exception("Invalid characters at end of expression");
            }

            return exp;
        }

        private Node ParseFourActions()
        {
            Node left = ParseLeaf();

            while (true)
            {
                Func<double, double, double> op = null;
                switch ( _tokenizer.Token )
                {
                    case Token.Add:
                        op = (a, b) => a + b;
                        break;

                    case Token.Subtract:
                        op = (a, b) => a - b;
                        break;

                    case Token.Multiply:
                        op = (a, b) => a * b;
                        break;

                    case Token.Divide:
                        op = (a, b) => a / b;
                        break;

                    default:
                        return left;
                }

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
