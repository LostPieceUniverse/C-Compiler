namespace Compiler
{
  public class ExpressionTree
  {
    public enum OperatorType
    {
      Addition,
      Subtraction,
      Multiplication,
      Division,
      muffin
    }
    static public ExpressionTree Build(List<Token> tokenList)
    {
      //berry unpretty solution incomming...I think...
      ExpressionTree rootNode = null;
      foreach (Token token in tokenList)
      {
        if(token.Type == TokenType.IntegerLiteral)
        {
          IntegerLiteralExpressionNode obj = new IntegerLiteralExpressionNode();
          rootNode = obj.BuildAST(tokenList);
        }
        else if(token.Type == TokenType.StringLiteral)
        {

        }
      }
      return rootNode;
    }
  }

  public class IntegerLiteralExpressionNode : ExpressionTree
  {
    public IntegerLiteralExpressionNode BuildAST(List<Token> tokenList)
    {
      Token token = tokenList[0];
      tokenList.RemoveAt(0);

      IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();

      if(token.Type == TokenType.OpenParenthesis)
      {
        node = BuildAST(tokenList);
        //remove the closing parenthesis
        tokenList.RemoveAt(0);
        return node;
      }
      else if(this.IsOperator)
      {
        node.Operator = this.Operator;
        node.Left = BuildAST(tokenList);
        node.Right = BuildAST(tokenList);
        return node;
      }
      return null;
    }
    
    public IntegerLiteralExpressionNode Left { get; set;} = null;
    public IntegerLiteralExpressionNode Right { get; set;} = null;

    //public OperatorType Operator { get; set; } = OperatorType.muffin;
    public OperatorType Operator { get; set; } = OperatorType.muffin;//temp

    public string Operand { get; set; } = string.Empty;
    public bool IsOperator { get; set; } = false;
  }

  public class StringLiteralExpressionNode : ExpressionTree
  {

  }
}
