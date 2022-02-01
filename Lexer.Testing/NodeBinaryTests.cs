using Xunit;
using Moq;

namespace Lexer.Testing
{
    public class NodeBinaryTests
    {
        [Fact]
        public void GivenTwoNodesAndFunc_WhenEval_ThenCorrectResult()
        {
            // Arrange
            Mock<Node> leftNode = new Mock<Node>();
            Mock<Node> rightNode = new Mock<Node>();
            leftNode.Setup(l => l.Eval()).Returns(1.5);
            rightNode.Setup(r => r.Eval()).Returns(2.5);
            NodeBinary node = new NodeBinary(leftNode.Object, rightNode.Object, (a, b) => { return a + b; });
            double expectedResult = 4;

            // Act
            double result = node.Eval();

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
