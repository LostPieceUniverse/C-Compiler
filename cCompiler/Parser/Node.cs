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
      else if(tokens[1].Value == "main")
      {
        Type = NodeType.Program;
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
      //string- or integerliteral
      if (Type == NodeType.IntegerExpression)
      {
        //assuming tokens[0] is type && tokens[1] is the identifier
        ExpressionIdentifier = tokens[1].Value;
        tokens.RemoveRange(0, Math.Min(3, tokens.Count));

        //build expressionTree
        IntegerLiteralExpressionNode obj = new IntegerLiteralExpressionNode();
        ExpressionRootNode = obj.BuildAST(0, tokens);

        if (ExpressionRootNode == null)
        {
          throw new Exception("rootnode is null");
        }

        //check if tree can be calced
        ExpressionRootNode.TreeNodeOptimizing();
      }
      else if (Type == NodeType.StringExpression)
      {
        //printf or variable
        StringLiteralExpressionNode obj = new StringLiteralExpressionNode();
        if (tokens[0].Value == "printf")
        {
          ExpressionIdentifier = tokens[0].Value;
          tokens.RemoveAt(0);

          ExpressionRootNode = obj.BuildAST(tokens);
        }
        else
        {
          ExpressionIdentifier = tokens[1].Value;
          tokens.RemoveRange(0, Math.Min(3, tokens.Count));

          ExpressionRootNode = obj.BuildAST(tokens);
        }
      }
    }
    public ExpressionTree ExpressionRootNode { get; private set; } //equation or string contents
    public String ExpressionIdentifier { get; private set; } //variable
  }


  public class StatementNode : Node
  {
    public StatementNode(List<Token> tokens) : base(tokens)
    {
    }
  }

}

