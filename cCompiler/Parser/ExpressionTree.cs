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
      return null;
    }
    
    public IntegerLiteralExpressionNode BuildAST(int index, List<Token> tokenList)
    {
      var node = new IntegerLiteralExpressionNode();
      Token token = tokenList[index];
      index++;

      switch(token.Type)
      {
        case Token.TokenType.Identifier:
        case Token.TokenType.Literal:
          if(token.Literal != Token.LiteralType.IntegerLiteral)
          {
            Console.WriteLine("Expected IntegerLiteral is StringLiteral");
            Console.WriteLine(token.Value + " -- " + token.Type + " -- " + token.Literal);
          }
          else
          {
            node.IsValue = true;
            node.Value = token.Value;
          }
          break;
        case Token.TokenType.Operand:
          switch(token.Value)
          {
            case "-":
              node.Operand = OperatorType.Subtraction;
              break;
            case "+":
              node.Operand = OperatorType.Addition;
              break;
            case "*":
              node.Operand = OperatorType.Multiplication;
              break;
            case "/":
              node.Operand = OperatorType.Division;
              break;
          }
          break;
        default:
          break;
      }
      
      var nextNode = BuildAST(index+1, tokenList);
      return node; 
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

