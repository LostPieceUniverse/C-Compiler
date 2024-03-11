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
    
    public Node( List<Token> tokens)
    { 
      Console.WriteLine("Trying to define nodeType");
      bool containsIntegerLiteral = tokens.Any(token => token.Type == Token.TokenType.IntegerLiteral);
      if(containsIntegerLiteral)
      {
        Console.WriteLine("true");
      }
      else
      {
        Console.WriteLine("false");
      }
      if(tokens.Any(token => token.Type == Token.TokenType.IntegerLiteral))
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
      /*
      foreach (Token token in tokens)
      {
        Console.WriteLine(token.Type.ToString());
        if(token.Type == Token.TokenType.IntegerLiteral)
        {
          Type = NodeType.IntegerExpression;
        }
        else if(token.Type == Token.TokenType.StringLiteral)
        {
          Type = NodeType.StringExpression;
        }
        else
        {
          Type = NodeType.Statement;
        }
      }
      */
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
    public ExpressionNode( List<Token> tokens) : base( tokens)
    {
      RootNode = ExpressionTree.Build(Type, tokens);
    }
    public ExpressionTree RootNode { get; set; }
  }


  public class StatementNode : Node
  {
    public StatementNode( List<Token> tokens) : base( tokens)
    {
    }
  }
}

