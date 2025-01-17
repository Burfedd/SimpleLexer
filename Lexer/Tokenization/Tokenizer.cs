using System.Globalization;
using System.IO;
using System.Text;

namespace Lexer.Tokenization
{
    public class Tokenizer
    {
        private readonly TextReader _reader;
        private char _currentChar;
        private Token _currentToken;
        private double _number;
        private string _identifier;

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

        public string Identifier
        {
            get
            {
                return _identifier;
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

                case ',':
                    NextChar();
                    _currentToken = Token.Comma;
                    return;
            }

            if (char.IsDigit(_currentChar) || _currentChar == '.')
            {
                StringBuilder sb = new StringBuilder();
                bool hasDecimalPoint = false;

                while (char.IsDigit(_currentChar) || !hasDecimalPoint && _currentChar == '.')
                {
                    sb.Append(_currentChar);
                    if (_currentChar == '.')
                    {
                        hasDecimalPoint = true;
                    }
                    NextChar();
                }

                CultureInfo info = new CultureInfo("en-US", true);
                _number = double.Parse(sb.ToString(), info.NumberFormat);
                _currentToken = Token.Number;

                if (_currentChar == '!') 
                {
                    NextChar();
                    if (hasDecimalPoint) throw new InvalidDataException($"Invalid factorial: {_number} {_currentChar}");

                    if (double.IsInteger(_number))
                    {
                        if (_number == 0)
                        {
                            _number = 1;
                        }
                        else
                        {
                            double result = 0;
                            while (_number > 0)
                            {
                                result += _number;
                                _number--;
                            }
                            _number = result;
                        }
                    }
                }

                return;
            }

            if (char.IsLetter(_currentChar) || _currentChar == '_')
            {
                StringBuilder sb = new StringBuilder();

                while (char.IsLetterOrDigit(_currentChar) || _currentChar == '_')
                {
                    sb.Append(_currentChar);
                    NextChar();
                }

                _identifier = sb.ToString();
                _currentToken = Token.Identifier;
                return;
            }

            // If nothing was parsed
            throw new InvalidDataException($"Invalid character: {_currentChar}");
        }
    }
}
