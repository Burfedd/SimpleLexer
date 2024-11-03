using Xunit;
using NSubstitute;
using Lexer.Nodes;
using Lexer.Context;

namespace Lexer.Testing
{
    public class NodeBinaryTests
    {
        private IContext _context;
        public NodeBinaryTests()
        {
            _context = Substitute.For<IContext>();
        }

        [Fact]
        public void GivenTwoNodesAndFunc_WhenEval_ThenCorrectResult()
        {
            // Arrange
            NodeNumber leftNode = Substitute.For<NodeNumber>(1.5);
            NodeNumber rightNode = Substitute.For<NodeNumber>(2.5);
            leftNode.Eval(_context).Returns(1.5);
            rightNode.Eval(_context).Returns(2.5);
            NodeBinary node = new NodeBinary(leftNode, rightNode, (a, b) => { return a + b; });
            double expectedResult = 4;

            // Act
            double result = node.Eval(_context);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
