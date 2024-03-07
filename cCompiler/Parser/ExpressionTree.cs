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
    static public ExpressionTree Build(Node.NodeType nodeType, List<Token> tokenList)
    {
      //berry unpretty solution incomming...I think...
      ExpressionTree rootNode = null;
      foreach (Token token in tokenList)
      {
        if(nodeType == Node.NodeType.IntegerExpression)
        {
          IntegerLiteralExpressionNode obj = new IntegerLiteralExpressionNode();
          rootNode = obj.BuildAST(0,tokenList);
        }
        else if(nodeType == Node.NodeType.StringExpression)
        {

        }
      }
      return rootNode;
    }
  }

  public class IntegerLiteralExpressionNode : ExpressionTree
  {
    public IntegerLiteralExpressionNode BuildAST(int index, List<Token> tokenList)
    {
      Token token = tokenList[index];

      IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();
      IntegerLiteralExpressionNode tempNode = new IntegerLiteralExpressionNode();
      
      for(;;)
      {
        //if paranthesis
        if(token.Type == Token.TokenType.OpenParenthesis)
        {
          node = BuildAST(index++, tokenList);
          //remove the closing parenthesis
          tokenList.RemoveAt(0);
          return node;
        }
        //if operand
        else if(token.IsOperator())
        {
          switch (token.Value)
          {
            case "-":
              node.Operand = ExpressionTree.OperatorType.Subtraction;
              break;
            case "+":
              node.Operand = ExpressionTree.OperatorType.Addition;
              break;
            case "/":
              node.Operand = ExpressionTree.OperatorType.Division;
              break;
            case "*":
              node.Operand = ExpressionTree.OperatorType.Multiplication;
              break;
              default:
              break;
          }
          node.IsOperator = true;
        
          node.Left = BuildAST(tokenList);
          node.Right = BuildAST(tokenList);
          return node;
        }
        //if value
        else
        {
          tempNode.IsValue = true;
          tempNode.Value = token.Value;
        }
        return node;
      }
    }
    
    public IntegerLiteralExpressionNode LeftNode { get; set;} = null;
    public IntegerLiteralExpressionNode RightNode { get; set;} = null;

    //public OperatorType Operator { get; private set; } = OperatorType.muffin;
    public OperatorType Operand { get; private set; } = OperatorType.muffin;//temp
    public bool IsOperator { get; private set; } = false;

    public string Value { get; private set; } = string.Empty;
    public bool IsValue { get; private set; } = false;
  }

  public class StringLiteralExpressionNode : ExpressionTree
  {

  }
}
