using Xunit;

namespace Lexer.Testing
{
    public class NodeNumberTests
    {
        [Fact]
        public void GivenNumberNode_WhenEval_ThenNumberIsReturned()
        {
            // Arrange
            NodeNumber num = new NodeNumber(1.5);
            double expected = 1.5;

            // Act
            double actual = num.Eval();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
