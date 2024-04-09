namespace Compiler
{
  public abstract class ExpressionTree
  {
    public enum OperatorType
    {
      Addition,
      Subtraction,
      Multiplication,
      Division,
      muffin
    }
    public enum ValueType
    {
      Numeric,
      EAX,
      muffin
    }
  }

  public class IntegerLiteralExpressionNode : ExpressionTree
  {
    public IntegerLiteralExpressionNode BuildAST(int index, List<Token> tokenList)
    {
      IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();
      IntegerLiteralExpressionNode nextNode = new IntegerLiteralExpressionNode();
      IntegerLiteralExpressionNode tempNode = new IntegerLiteralExpressionNode();

      if(index == tokenList.Count)
      {
        return null;
      }
      
      Token token = tokenList[index];
      switch (token.Type)
      {
        case Token.TokenType.IntegerLiteral:
            if (node.IsValue)
            {
              throw new Exception("two values ffs");
            }
            node.IsValue = true;
            node.Value = token.Value;
            nextNode = BuildAST(index + 1, tokenList);
            if(nextNode == null)
            {
              return node;
            }
            else if (nextNode.IsOperator)
            {
              tempNode = node;
              node = nextNode;
              if(node.LeftNode == null)
              {
                node.LeftNode = tempNode;
              }
              else
              {
                node.RightNode = node.LeftNode;
                node.LeftNode = tempNode;
              }
              tempNode = null;
              nextNode = null;
            }
            else
            {
              throw new Exception("switch: integerliteral");
            }
            break;
        case Token.TokenType.Operand:
          if (node.IsOperator)
          {
            throw new Exception("two operands ffs");
          }
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
          IntegerLiteralExpressionNode parenthesisNode = new IntegerLiteralExpressionNode();
          if (tokenList[index + 1].Type == Token.TokenType.OpenParenthesis)
          {
            parenthesisNode = BuildAST(index + 1, tokenList);
            int indexclosing = FindClosingParenthesis(index + 2, tokenList);
            nextNode = BuildAST(indexclosing + 1, tokenList);

            if (nextNode.LeftNode == null)
            {
              nextNode.LeftNode = parenthesisNode;
            }
            else
            {
              nextNode.RightNode = nextNode.LeftNode;
              nextNode.LeftNode = parenthesisNode;
            }
          }
          else
          {
            nextNode = BuildAST(index + 1, tokenList);
          }
          
          
          
          if (node.LeftNode == null)
          {
            node.LeftNode = nextNode;
          }
          else
          {
            node.RightNode = nextNode;
          }
          tempNode = null;
          nextNode = null;
          break;
        case Token.TokenType.OpenParenthesis:
          int closingIndex = FindClosingParenthesis(index + 1, tokenList);
          node = BuildAST(0, tokenList.GetRange(index + 1, closingIndex - index - 1));
          index = closingIndex + 1;
          if (!node.IsOperator)
          {
            throw new Exception("switch: OpenParenthesis");
          }

          break;
        case Token.TokenType.CloseParenthesis:
          return node;
        default:
          break;
      }
      //if node is null error
      if (node == null)
      {
        throw new Exception("node is null");
      }
      return node;
    }

    private int FindClosingParenthesis(int startIndex, List<Token> tokenList)
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
      return 0;//error
    }

    public IntegerLiteralExpressionNode LeftNode { get; set; } = null;
    public IntegerLiteralExpressionNode RightNode { get; set; } = null;

    public OperatorType Operand { get; private set; } = OperatorType.muffin;
    public bool IsOperator { get; private set; } = false;

    public string Value { get; private set; } = string.Empty;
    public bool IsValue { get; private set; } = false;
  }

  public class StringLiteralExpressionNode : ExpressionTree
  {
    public StringLiteralExpressionNode BuildAST(List<Token> tokenList)
    {
      StringLiteralExpressionNode node = new StringLiteralExpressionNode();
      for(int i = 0; i < tokenList.Count; i++)
      {
        if(tokenList[i].Type == Token.TokenType.StringLiteral)
        {
          node.Value += tokenList[i].Value;
        }
      }
      return node;
    }

    public string Value { get; private set; } = string.Empty;
  }

}

