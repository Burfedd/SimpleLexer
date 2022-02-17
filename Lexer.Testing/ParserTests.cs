using Xunit;

namespace Lexer.Testing
{
    public class ParserTests
    {
        [Fact]
        public void GivenDifferentMathsExpressions_WhenParse_ThenEvaluatedCorrectly()
        {
            // Digits, numbers, fractions and unary operators
            Assert.Equal(0, Parser.Parse("0"));
            Assert.Equal(1, Parser.Parse("1"));
            Assert.Equal(1, Parser.Parse("   1   "));
            Assert.Equal(12, Parser.Parse("12"));
            Assert.Equal(123, Parser.Parse("123"));
            Assert.Equal(1234, Parser.Parse("1234"));
            Assert.Equal(1.5, Parser.Parse("1,5"));
            Assert.Equal(5.15, Parser.Parse("5,15"));
            Assert.Equal(-1, Parser.Parse("-1"));
            Assert.Equal(1, Parser.Parse("+1"));

            // Addition and subtraction
            Assert.Equal(2, Parser.Parse("1+1"));
            Assert.Equal(2, Parser.Parse("   1   +   1   "));
            Assert.Equal(1, Parser.Parse("2-1"));
            Assert.Equal(1, Parser.Parse("   2   -   1   "));
            Assert.Equal(5, Parser.Parse("1 + 2 + 1 + 0,5 + 0,5"));

            // Multiplication and division
            Assert.Equal(2, Parser.Parse("1*2"));
            Assert.Equal(2, Parser.Parse("   1   *   2   "));
            Assert.Equal(1024, Parser.Parse("2*2*2*2*2*2*2*2*2*2"));
            Assert.Equal(6, Parser.Parse("1,5 * 4"));
            Assert.Equal(2, Parser.Parse("4/2"));
            Assert.Equal(2, Parser.Parse("   4   /   2   "));
            Assert.Equal(2, Parser.Parse("1024/2/2/2/2/2/2/2/2/2"));
            Assert.Equal(4, Parser.Parse("6 / 1,5"));
        }
    }
}
