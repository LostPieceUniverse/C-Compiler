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
    public abstract void print(IntegerLiteralExpressionNode obj);
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
        IntegerLiteralExpressionNode ob3j = new IntegerLiteralExpressionNode();

        Console.WriteLine("*******************print*********************");
        if(rootNode != null)
        {
          ob3j.print(((IntegerLiteralExpressionNode)rootNode));
        }
      //if null then error
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
            Console.WriteLine("index:{0}, closingIndex{1}",index, closingIndex);
            index = closingIndex;
            break;
          case Token.TokenType.CloseParenthesis:
            return node;
          default:
            break;
        }
        index++;
      }
      //if node is null error
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

    public override void print(IntegerLiteralExpressionNode obj)
    {
      Console.WriteLine("Operand: " + obj.Operand.ToString());
      Console.WriteLine("Value: " + obj.Value);
      if(obj.LeftNode != null)
      {
        print(obj.LeftNode);
      }
      if(obj.RightNode != null)
      {
        print(obj.RightNode);
      }
    }
    public IntegerLiteralExpressionNode LeftNode { get; set;} = null;
    public IntegerLiteralExpressionNode RightNode { get; set;} = null;

    public OperatorType Operand { get; private set; } = OperatorType.muffin;
    public bool IsOperator { get; private set; } = false;

    public string Value { get; private set; } = string.Empty;
    public bool IsValue { get; private set; } = false;
  }

  public class StringLiteralExpressionNode : ExpressionTree
  {
    public override void print(IntegerLiteralExpressionNode obj)
    {

    }
  }
}
