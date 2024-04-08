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
      static public ExpressionTree Build(Node.NodeType nodeType, List<Token> tokenList)
      {
          //berry unpretty solution incomming...I think...
          ExpressionTree rootNode = null;
          IntegerLiteralExpressionNode obj = new IntegerLiteralExpressionNode();
          rootNode = obj.BuildAST(0, tokenList);

          /*
          foreach (Token token in tokenList)
          {
              if (nodeType == Node.NodeType.IntegerExpression)
              {
                  IntegerLiteralExpressionNode obj = new IntegerLiteralExpressionNode();
                  rootNode = obj.BuildAST(0, tokenList);
              }
              else if (nodeType == Node.NodeType.StringExpression)
              {

              }
          }
          */
          //if null then error
          return rootNode;
      }
  }

  public class IntegerLiteralExpressionNode : ExpressionTree
  {
      public IntegerLiteralExpressionNode BuildAST(int index, List<Token> tokenList)
      {
          IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();
          IntegerLiteralExpressionNode currentNode = new IntegerLiteralExpressionNode();

          while (index < tokenList.Count)
          {
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
                      break;
                  case Token.TokenType.Operand:
                      if (node.IsOperator)
                      {
                          throw new Exception("two operands ffs");
                      }
                      IntegerLiteralExpressionNode newNode = new IntegerLiteralExpressionNode();
                      switch (token.Value)
                      {
                          case "-":
                              newNode.Operand = OperatorType.Subtraction;
                              break;
                          case "+":
                              newNode.Operand = OperatorType.Addition;
                              break;
                          case "/":
                              newNode.Operand = OperatorType.Division;
                              break;
                          case "*":
                              newNode.Operand = OperatorType.Multiplication;
                              break;
                      }
                      newNode.IsOperator = true;
                      newNode.LeftNode = node;
                      node = newNode;
                      node.RightNode = BuildAST(index + 1, tokenList);
                      return node;
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

      public IntegerLiteralExpressionNode LeftNode { get; set; } = null;
      public IntegerLiteralExpressionNode RightNode { get; set; } = null;

      public OperatorType Operand { get; private set; } = OperatorType.muffin;
      public bool IsOperator { get; private set; } = false;

      public string Value { get; private set; } = string.Empty;
      public bool IsValue { get; private set; } = false;
  }

  public class StringLiteralExpressionNode : ExpressionTree
  {
      
  }
}
