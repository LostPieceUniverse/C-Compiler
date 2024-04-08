namespace Compiler
{
  public class Node
  {
    public enum NodeType
    {
      Program,
      FuncDecl,
      Statement,
      IntegerExpression,
      StringExpression,
      muffin
    }

    public Node(List<Token> tokens)
    {
      bool containsIntegerLiteral = tokens.Any(token => token.Type == Token.TokenType.IntegerLiteral);
      if (tokens.Any(token => token.Type == Token.TokenType.IntegerLiteral))
      {
        Type = NodeType.IntegerExpression;
      }
      else if (tokens.Any(token => token.Type == Token.TokenType.StringLiteral))
      {
        Type = NodeType.StringExpression;
      }
      else
      {
        Type = NodeType.Statement;
      }
      Tokens = tokens;
    }
    public NodeType Type { get; private set; } = NodeType.muffin;
    public List<Token> Tokens { get; private set; } = new List<Token>();

    public Node Left { get; set; }
    public Node Right { get; set; }

    //for debugging purposes
    public string PrintNodeType()
    {
      switch (Type)
      {
        case NodeType.Program:
          return "Program";
        case NodeType.FuncDecl:
          return "FuncDecl";
        case NodeType.Statement:
          return "Statement";
        case NodeType.IntegerExpression:
          return "IntegerExpression";
        case NodeType.StringExpression:
          return "StringExpression";
        default:
          return "muffin";
      }
    }
  }

  public class ExpressionNode : Node
  {
    public ExpressionNode(List<Token> tokens) : base(tokens)
    {
      //assuming tokens[0] is type && tokens[1] is the identifier
      ExpressionIdentifier = tokens[1].Value;
      tokens.RemoveRange(0, Math.Min(3, tokens.Count));
      if(ExpressionIdentifier == "c")
      {
          int asdasd = 98;
      }
      //build expressionTree
      ExpressionRootNode = ExpressionTree.Build(Type, tokens);

      //check if tree can be calced
    }
    public ExpressionTree ExpressionRootNode { get; private set; } //quation
    public String ExpressionIdentifier { get; private set; } //variable
  }


  public class StatementNode : Node
  {
    public StatementNode(List<Token> tokens) : base(tokens)
    {
    }
  }

}

