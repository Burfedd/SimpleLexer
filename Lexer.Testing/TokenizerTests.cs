using Xunit;
using System.IO;
using Lexer.Tokenization;

namespace Lexer.Testing
{
    public class TokenizerTests
    {
        [Fact]
        public void GivenInputString_WhenNextToken_TokenChanges()
        {
            // Arrange
            string input = "1+1";
            Token expected = Token.Add;
            Tokenizer t = new Tokenizer(new StringReader(input));

            // Act
            t.NextToken();
            Token actual = t.Token;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
