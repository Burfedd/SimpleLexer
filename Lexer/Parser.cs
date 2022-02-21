using System;
using System.Collections.Generic;
using System.IO;

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
            Node left = ParseMultiplyDivide();

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

                    default:
                        return left;
                }

                _tokenizer.NextToken();

                Node right = ParseMultiplyDivide();
                left = new NodeBinary(left, right, op);
            }
        }

        private Node ParseMultiplyDivide()
        {
            Node left = ParseUnary();
            while ( true )
            {
                Func<double, double, double> op = null;
                switch ( _tokenizer.Token )
                {
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

                Node right = ParseUnary();
                left = new NodeBinary(left, right, op);
            }
        }

        private Node ParseUnary()
        {
            // + sign
            if (_tokenizer.Token == Token.Add )
            {
                _tokenizer.NextToken();
                return ParseUnary();
            }

            // - sign
            if (_tokenizer.Token == Token.Subtract )
            {
                _tokenizer.NextToken();

                var right = ParseUnary();
                return new NodeUnary(right, (x) => -x);
            }

            return ParseLeaf();
        }

        private Node ParseLeaf()
        {
            if (_tokenizer.Token == Token.OpeningParenthesis )
            {
                _tokenizer.NextToken();

                Node node = ParseAddSubtract();

                if (_tokenizer.Token == Token.ClosingParenthesis )
                {
                    _tokenizer.NextToken();
                    return node;
                }

                throw new Exception("Missing closing parenthesis!");
            }

            if (_tokenizer.Token == Token.Number)
            {
                NodeNumber node = new NodeNumber(_tokenizer.Number);
                _tokenizer.NextToken();
                return node;
            }

            if (_tokenizer.Token == Token.Identifier )
            {
                string name = _tokenizer.Identifier;
                _tokenizer.NextToken();

                if (_tokenizer.Token == Token.OpeningParenthesis )
                {
                    _tokenizer.NextToken();

                    List<Node> args = new List<Node>();
                    while ( true )
                    {
                        args.Add(ParseAddSubtract());
                        if (_tokenizer.Token == Token.Comma )
                        {
                            _tokenizer.NextToken();
                            continue;
                        }
                        break;
                    }

                    if (_tokenizer.Token == Token.ClosingParenthesis )
                    {
                        _tokenizer.NextToken();
                        return new NodeFunction(name, args.ToArray());
                    }
                    throw new Exception($"Invalid expression: missing closing parenthesis");

                }

                NodeVariable node = new NodeVariable(_tokenizer.Identifier);
                _tokenizer.NextToken();
                return node;
            }

            // Invalid token
            throw new Exception($"Invalid token: {_tokenizer.Token}");
        }

        public static double Parse(string s, IContext context)
        {
            StringReader sr = new StringReader(s);
            Tokenizer t = new Tokenizer(sr);
            Parser p = new Parser(t);
            return p.ParseExpression().Eval(context);
        }
    }
}
