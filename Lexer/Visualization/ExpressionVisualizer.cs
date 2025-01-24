using System.Collections.Generic;
using Lexer.Tokenization;

public class ExpressionVisualizer
{
    private readonly Tokenizer _tokenizer;
    private readonly Stack<string> _lines;

    public ExpressionVisualizer(Tokenizer tokenizer)
    {
        _tokenizer = tokenizer;
        _lines = new Stack<string>();
    }

    public void GenerateTree()
    {
        // a + b            5 * (a + b) / 12
        //   +                          /
        //  / \                        / \
        // a   b                      *   12
        //                           / \
        //                          5   +
        //                             / \
        //                            a   b
    }

    public void Print()
    {

    }
}