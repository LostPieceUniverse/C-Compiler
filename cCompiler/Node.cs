using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
  public enum StatementType 
  {
    If,
    Else,
    ElseIf,
    Return,
    muffin
  }
  public enum ExpressionType
  {
    StringLiteral,
    IntegerLiteral,
    muffin
  }
  public class Node
  {
    public List<Token> Tokens { get; set; } = null;
    
    public Node Left { get; set; } = null;
    public Node Right { get; set; } = null;
  }
  public class FunctionNode : Node
  {
    public FunctionNode(string name)
    {
      Name = name;
    }
    public string Name { get; private set; } = string.Empty;
  }
  public class StatementNode : Node
  {
    public StatementNode(StatementType type)
    {
      Type = type;
    }
    public StatementType Type { get; private set; } = StatementType.muffin;
  }
  public class ExpressionNode : Node
  {
    public ExpressionNode(ExpressionType type)
    {
      Type = type;
    }
    public ExpressionType Type { get; private set; } = ExpressionType.muffin;
  }
}

