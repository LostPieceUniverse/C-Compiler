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
    public abstract void TreeNodeOptimizing( ref bool hasVariable, int depth);
    public abstract string[] OutputTree();

  }

  public class IntegerLiteralExpressionNode : ExpressionTree
  {

    private void SetPrecedence()
    {
      Precedence += Operand switch
      {
          OperatorType.Addition => 1,
          OperatorType.Subtraction => 1,
          OperatorType.Multiplication => 2,
          OperatorType.Division => 2,
          _ => 0
      };
    }

    private IntegerLiteralExpressionNode NewNode(Token token)
    {
      IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();
      switch (token.Type)
      {
        case Token.TokenType.Identifier:
        case Token.TokenType.Literal:
          node.Value = token.Value;
          node.IsValue = true;
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
          node.SetPrecedence();
          break;
      }
      return node;
    }
      public IntegerLiteralExpressionNode BuildAST(int index, List<Token> tokenList)
      {
        IntegerLiteralExpressionNode node = new IntegerLiteralExpressionNode();
        IntegerLiteralExpressionNode nextNode = new IntegerLiteralExpressionNode();
        IntegerLiteralExpressionNode tempNode = new IntegerLiteralExpressionNode();

        if (index == tokenList.Count)
        {
          return null;
        }

        Token token = tokenList[index];
        switch (token.Type)
        {
          case Token.TokenType.Identifier:
          case Token.TokenType.Literal:
            if(token.Literal == Token.LiteralType.IntegerLiteral)
            {
              if (node.IsValue)
              {
                throw new Exception("two values ffs");
              }
              node.IsValue = true;
              node.Value = token.Value;
              nextNode = BuildAST(index + 1, tokenList);
              if (nextNode == null)
              {
                return node;
              }
              else if (nextNode.IsOperator)
              {
                tempNode = node;
                node = nextNode;
                if (node.LeftNode == null)
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
            }
            else
            {
              throw new Exception("is StringLiteral");
            }
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

      public override void TreeNodeOptimizing( ref bool hasVariable, int depth)
      {
        if (LeftNode != null)
        {
          if (LeftNode.IsValue && !int.TryParse(LeftNode.Value, out _))
          {
            hasVariable = true;
          }
          else
          {
            LeftNode.TreeNodeOptimizing( ref hasVariable, depth + 1);
          }
        }
        else
        {
          //Console.WriteLine("LeftNode is null");
        }

        if (RightNode != null)
        {
          if (RightNode.IsValue && !int.TryParse(RightNode.Value, out _))
          {
            hasVariable = true;
          }
          else
          {
            RightNode.TreeNodeOptimizing( ref hasVariable, depth + 1);
          }
        }
        else
        {
          //Console.WriteLine("RightnNode is null");
        }

        if(depth == 0 && !hasVariable)
        {
          int result = TreeNodeSolver();
          LeftNode = null;
          RightNode = null;

          IsOperator = false;
          Operand = OperatorType.muffin;

          IsValue = true;
          Value = result.ToString();

          TreeGotOptimized = true;
        }
      }
      public int TreeNodeSolver()
      {
        if (IsOperator)
        {
          switch (Operand)
          {
            case OperatorType.Addition:
                return LeftNode.TreeNodeSolver() + RightNode.TreeNodeSolver();
            case OperatorType.Subtraction:
                return LeftNode.TreeNodeSolver() - RightNode.TreeNodeSolver();
            case OperatorType.Multiplication:
                return LeftNode.TreeNodeSolver() * RightNode.TreeNodeSolver();
            case OperatorType.Division:
                return LeftNode.TreeNodeSolver() / RightNode.TreeNodeSolver();
            default:
                throw new Exception("idfk");
          }
        }
        else if (IsValue)
        {
          int number = 0;
          int.TryParse(Value, out number);
          return number;
        }
        return 0;
      }

    //output tree
    public override string[] OutputTree()
    {
      int depth = GetTreeDepth(this);
      int width = (int)Math.Pow(2, depth) - 1; 

      string[] output = new string[depth];

      for (int i = 0; i < depth; i++)
      {
        output[i] = new string(' ', width * 2);      
      }


      FillOutput(this, output, 0, 0, width);

      return output;
    }

    private void FillOutput(IntegerLiteralExpressionNode node, string[] output, int depth, int left, int right)
    {
      if (node == null) return;

      int mid = (left + right) / 2; 
      output[depth] = output[depth].Remove(mid * 2, 1).Insert(mid * 2, node.IsOperator ? OperandToString(node.Operand) : node.Value); // Insert node value.

      if (node.LeftNode != null)
      {
        FillOutput(node.LeftNode, output, depth + 1, left, mid - 1); 
      }
      if (node.RightNode != null)
      {
        FillOutput(node.RightNode, output, depth + 1, mid + 1, right); 
      }
    }

    private int GetTreeDepth(IntegerLiteralExpressionNode node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(GetTreeDepth(node.LeftNode), GetTreeDepth(node.RightNode));
    }

    private string OperandToString(OperatorType operand)
    {
        return operand switch
        {
            OperatorType.Addition => "+",
            OperatorType.Subtraction => "-",
            OperatorType.Multiplication => "*",
            OperatorType.Division => "/",
            _ => " "
        };
    }
      public IntegerLiteralExpressionNode LeftNode { get; set; } = null;
      public IntegerLiteralExpressionNode RightNode { get; set; } = null;

      public OperatorType Operand { get; private set; } = OperatorType.muffin;
      public bool IsOperator { get; private set; } = false;//return operator ffs ------------------------------------------- !!

      public string Value { get; private set; } = string.Empty;
      public bool IsValue { get; private set; } = false;//return value ffs ------------------------------------------------- !!

      public int Precedence { get; private set; } = 0;

      public bool TreeGotOptimized { get; private set; } = false;
  }

  public class StringLiteralExpressionNode : ExpressionTree
  {
    public StringLiteralExpressionNode BuildAST(List<Token> tokenList)
    {
      StringLiteralExpressionNode node = new StringLiteralExpressionNode();
      for (int i = 0; i < tokenList.Count; i++)
      {
        if (tokenList[i].Type == Token.TokenType.Literal)
        {
          if(tokenList[i].Literal == Token.LiteralType.StringLiteral)
          {
            node.Value += tokenList[i].Value;
          }
          else
          {
            throw new Exception("is Integerliteral");
          }
        }
        else if (tokenList[i].Type == Token.TokenType.Identifier)
        {
          ///assuming stings cant be built
          node.Value = tokenList[i].Value;
        }
      }
      return node;
    }

    public override void TreeNodeOptimizing(ref bool hasVariable, int depth)
    {
        
    }
    public override string[] OutputTree()
    {
      return null;
    }
    public string Value { get; private set; } = string.Empty;
  }
}

