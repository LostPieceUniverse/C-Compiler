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
      IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();
      
      while(index < tokenList.Count)
      {
        Token token = tokenList[index];
        
        switch (token.Type)
        {
          case Token.TokenType.IntegerLiteral:
            node.IsValue = true;
            node.Value = token.Value;
            break;
          case Token.TokenType.Operand:
            switch (token.Value)
            {
              case "-":
                node.Operand = OperatorType.Subtraction;
                break;
              case "+":
                node.Operand = OperatorType.Addition;
                break;
              case "/":
                node.Operand = OperatorType.Division;
                break;
              case "*":
                node.Operand = OperatorType.Multiplication;
                break;
            }
            node.IsOperator = true;
            node.LeftNode = BuildAST(index + 1, tokenList);
            node.RightNode = BuildAST(index + 2, tokenList);
            break;
          case Token.TokenType.OpenParenthesis:
            int closingIndex = FindClosingParenthesis(index + 1, tokenList);
            node = BuildAST(index + 1, tokenList.GetRange(index + 1, closingIndex - index - 1));
            index = closingIndex;
            break;
          case Token.TokenType.CloseParenthesis:
            return node;
          default:
            break;
        }
        index++;
        /*
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
        
          node.LeftNode = tempNode;
          node.RightNode = BuildAST(index++, tokenList);
          return node;
        }
        //if value
        else
        {
          tempNode.IsValue = true;
          tempNode.Value = token.Value;
          index++;
        }
        */
      }
      return node;
    }
    
    private static int FindClosingParenthesis(int startIndex, List<Token> tokenList)
    {
      int count = 1;
      for (int i = startIndex; i < tokenList.Count; i++)
      {
        if (tokenList[i].Type == Token.TokenType.OpenParenthesis)
        {
          count++;
        }
        else if (tokenList[i].Type == Token.TokenType.CloseParenthesis)
        {
          count--;
          if (count == 0)
          {
            return i;
          }
        }
      }
      return 0;
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
