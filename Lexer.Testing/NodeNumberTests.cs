using Xunit;
using NSubstitute;

namespace Lexer.Testing
{
    public class NodeNumberTests
    {
        private IContext _context;

        public NodeNumberTests()
        {
            _context = Substitute.For<IContext>();
        }

        [Fact]
        public void GivenNumberNode_WhenEval_ThenNumberIsReturned()
        {
            // Arrange
            NodeNumber num = new NodeNumber(1.5);
            double expected = 1.5;

            // Act
            double actual = num.Eval(_context);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
