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
      if (tokens[1].Value == "main")
      {
        Type = NodeType.Program;
      }
      else if (tokens[0].Type == Token.TokenType.Return)
      {
        Type = NodeType.Statement;
      }
      else if(tokens.Any(token => token.Type == Token.TokenType.Literal))
      {
        if (tokens.Any(token => token.Literal == Token.LiteralType.IntegerLiteral))
        {
          Type = NodeType.IntegerExpression;
        }
        else if (tokens.Any(token => token.Literal == Token.LiteralType.StringLiteral))
        {
          Type = NodeType.StringExpression;
        }
      }
      else if(tokens.Any(token => token.Type == Token.TokenType.Identifier))
      {
        if(tokens.Any(token => token.Literal == Token.LiteralType.StringLiteral))
        {
          Type = NodeType.StringExpression;
        }
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
    /*
    public static void OutputNode(Node rootNode, string indent = "")
    {
      if (rootNode != null)
      {
        Console.Write(indent);
        foreach(var t in rootNode.Tokens)
        {
          Console.Write(t.Value);
        }
        Console.WriteLine("");
        if (rootNode.Left != null)
        {
          Console.WriteLine(indent + "|");
          Console.WriteLine(indent + "|- Left:");
          OutputNode(rootNode.Left, indent + "|  ");
        }

        if (rootNode.Right != null)
        {
          Console.WriteLine(indent + "|");
          Console.WriteLine(indent + "|- Right:");
          OutputNode(rootNode.Right, indent + "|  ");
        }
      }
    }
    */
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
        bool hasVariable = false;
        ExpressionRootNode.TreeNodeOptimizing(ref hasVariable, 0);
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
    //kept very simple for now
    public StatementNode(List<Token> tokens) : base(tokens)
    {
      if (tokens[0].Type == Token.TokenType.Return)
      {
          
      }
    }
  }
}

