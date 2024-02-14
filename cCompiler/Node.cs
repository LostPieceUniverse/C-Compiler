using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
  public enum NodeType
  {
    Program,
    FuncDecl,
    Statement,
    Expression,
    muffin
  }
  public class Node
  {
    public Node(NodeType type)
    {
      Type = type;
    }
    public NodeType Type { get; private set; } = NodeType.muffin;
    public Node Left { get; set; } = null;
    public Node Right { get; set; } = null;
    //public List<Node> ChildNodes { get; set; } = new List<Node>();
    public List<Token> Tokens { get; set; } = new List<Token>();

    //for debugging purposes
    public string PrintNodeType()
    {
      switch (Type)
      {
        case NodeType.Program:
          return "Program";
        case NodeType.FuncDecl:
          return "FuncDecl";
        case NodeType.Statement:
          return "Statement";
        case NodeType.Expression:
          return "Expression";
          default:
          return "muffin";
      }
    }
  }

  public class ExpressionNode : Node
  {
    public ExpressionNode(NodeType type) : base(type)
    {
      
    }
  }
  public class StatementNode : Node
  {
    public StatementNode(NodeType type) : base(type)
    {
      
    }
  }
}

