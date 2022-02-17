using System.IO;
using System.Text;

namespace Lexer
{
    public class Tokenizer
    {
        private TextReader _reader;
        private char _currentChar;
        private Token _currentToken;
        private double _number;

        public Tokenizer(TextReader reader)
        {
            _reader = reader;
            NextChar();
            NextToken();
        }

        public Token Token
        {
            get
            {
                return _currentToken;
            }
        }

        public double Number
        {
            get
            {
                return _number;
            }
        }

        public void NextChar()
        {
            int c = _reader.Read();
            if (c == -1)
            {
                _currentChar = '\0';
            }
            else
            {
                _currentChar = (char)c;
            }
        }

        public void NextToken()
        {
            while (char.IsWhiteSpace(_currentChar))
            {
                NextChar();
            }

            switch (_currentChar)
            {
                case '\0':
                    _currentToken = Token.EOF;
                    return;

                case '+':
                    NextChar();
                    _currentToken = Token.Add;
                    return;

                case '-':
                    NextChar();
                    _currentToken = Token.Subtract;
                    return;

                case '*':
                    NextChar();
                    _currentToken = Token.Multiply;
                    return;

                case '/':
                    NextChar();
                    _currentToken = Token.Divide;
                    return;

                case '(':
                    NextChar();
                    _currentToken = Token.OpeningParenthesis;
                    return;

                case ')':
                    NextChar();
                    _currentToken = Token.ClosingParenthesis;
                    return;
            }

            if (char.IsDigit(_currentChar) || _currentChar == ',')
            {
                StringBuilder sb = new StringBuilder();
                bool haveDecimalPoint = false;

                while(char.IsDigit(_currentChar) || (!haveDecimalPoint && _currentChar == ','))
                {
                    sb.Append(_currentChar);
                    if (_currentChar == ',')
                    {
                        haveDecimalPoint = true;
                    }
                    NextChar();
                }

                _number = double.Parse(sb.ToString());
                _currentToken = Token.Number;
                return;
            }

            // If nothing was parsed
            throw new InvalidDataException($"Invalid character: {_currentChar}");
        }
    }

    public enum Token
    {
        EOF,
        Add,
        Subtract,
        Multiply,
        Divide,
        Number,
        OpeningParenthesis,
        ClosingParenthesis
    }
}
