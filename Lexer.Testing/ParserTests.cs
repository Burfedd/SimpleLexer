using Xunit;
using NSubstitute;
using System;
using Lexer.Context;
using Lexer.Parsing;

namespace Lexer.Testing
{
    public class ParserTests
    {
        private IContext _context;
        public ParserTests()
        {
            _context = Substitute.For<IContext>();
            _context.ResolveVariable(Arg.Any<string>()).Returns(1.5);
            _context.ResolveFunction(Arg.Any<string>(), Arg.Any<double[]>()).Returns(1.5);
        }

        [Fact]
        public void Parse_NumbersAndUnaryOperators()
        {
            Assert.Equal(0, Parser.Parse("0", _context));
            Assert.Equal(1, Parser.Parse("1", _context));
            Assert.Equal(1, Parser.Parse("   1   ", _context));
            Assert.Equal(12, Parser.Parse("12", _context));
            Assert.Equal(123, Parser.Parse("123", _context));
            Assert.Equal(1234, Parser.Parse("1234", _context));
            Assert.Equal(1.5, Parser.Parse("1.5", _context));
            Assert.Equal(5.15, Parser.Parse("5.15", _context));
            Assert.Equal(-1, Parser.Parse("-1", _context));
            Assert.Equal(1, Parser.Parse("+1", _context));
        }

        [Fact]
        public void Parse_AdditionAndSubtraction()
        {
            Assert.Equal(2, Parser.Parse("1+1", _context));
            Assert.Equal(2, Parser.Parse("   1   +   1   ", _context));
            Assert.Equal(1, Parser.Parse("2-1", _context));
            Assert.Equal(1, Parser.Parse("   2   -   1   ", _context));
            Assert.Equal(5, Parser.Parse("1 + 2 + 1 + 0.5 + 0.5", _context));
        }

        [Fact]
        public void Parse_MultiplicationAndDivision()
        {
            Assert.Equal(2, Parser.Parse("1*2", _context));
            Assert.Equal(2, Parser.Parse("   1   *   2   ", _context));
            Assert.Equal(1024, Parser.Parse("2*2*2*2*2*2*2*2*2*2", _context));
            Assert.Equal(6, Parser.Parse("1.5 * 4", _context));
            Assert.Equal(2, Parser.Parse("4/2", _context));
            Assert.Equal(2, Parser.Parse("   4   /   2   ", _context));
            Assert.Equal(2, Parser.Parse("1024/2/2/2/2/2/2/2/2/2", _context));
            Assert.Equal(4, Parser.Parse("6 / 1.5", _context));
        }

        [Fact]
        public void Parse_PrecedenceWithoutParentheses()
        {
            Assert.Equal(6, Parser.Parse("2+2*2", _context));
            Assert.Equal(3, Parser.Parse("2+2/2", _context));
            Assert.Equal(-7, Parser.Parse("5-3*4", _context));
            Assert.Equal(3.5, Parser.Parse("5-3/2", _context));
        }

        [Fact]
        public void Parse_PrecedenceWithParentheses()
        {
            Assert.Equal(8, Parser.Parse("(2+2)*2", _context));
            Assert.Equal(8, Parser.Parse("2*(2+2)", _context));
            Assert.Equal(130, Parser.Parse("5 * (2 + (3 * (7 + 1)))", _context));
            Assert.Equal(3, Parser.Parse("4.5 / (2 - (3 / (7 - 1)))", _context));
        }

        [Fact]
        public void Parse_Variables()
        {
            Assert.Equal(1.5, Parser.Parse("a", _context));
            Assert.Equal(2.5, Parser.Parse("a + 1", _context));
            Assert.Equal(2.5, Parser.Parse("1 + a", _context));
            Assert.Equal(3, Parser.Parse("2 * a", _context));
            Assert.Equal(2, Parser.Parse("3 / a", _context));
            Assert.Equal(3.75, Parser.Parse("a + a * a", _context));
            Assert.Equal(4.5, Parser.Parse("(a + a) * a", _context));
            Assert.Equal(1.5, Parser.Parse("b", _context));
            Assert.Equal(2.5, Parser.Parse("b + 1", _context));
            Assert.Equal(2.5, Parser.Parse("1 + b", _context));
            Assert.Equal(3, Parser.Parse("2 * b", _context));
            Assert.Equal(2, Parser.Parse("3 / b", _context));
            Assert.Equal(3.75, Parser.Parse("b + b * b", _context));
            Assert.Equal(4.5, Parser.Parse("(b + b) * b", _context));
        }

        [Fact]
        public void Parse_Functions()
        {
            Assert.Equal(1.5, Parser.Parse("a(0)", _context));
            Assert.Equal(3, Parser.Parse("1.5 + a(0)", _context));
            Assert.Equal(3, Parser.Parse("a(0) + 1.5", _context));
            Assert.Equal(0, Parser.Parse("a(0) - 1.5", _context));
            Assert.Equal(0, Parser.Parse("1.5 - a(0)", _context));
            Assert.Equal(1.5, Parser.Parse("a(0, 1, 2)", _context));
            Assert.Equal(1.5, Parser.Parse("a(1.5, 1.2)", _context));
            Assert.Equal(3, Parser.Parse("a(0) * 2", _context));
            Assert.Equal(0.5, Parser.Parse("a(0) / 3", _context));
            Assert.Equal(3.75, Parser.Parse("a(0) + a(0) * a(0)", _context));
            Assert.Equal(4.5, Parser.Parse("(a(0) + a(0)) * a(0)", _context));
        }

        [Fact]
        public void Parse_Factorials()
        {
            Assert.Equal(1, Parser.Parse("0!", _context));
            Assert.Equal(1, Parser.Parse("1!", _context));
            Assert.Equal(3, Parser.Parse("2!", _context));
            Assert.Equal(6, Parser.Parse("3!", _context));
        }
    }
}
