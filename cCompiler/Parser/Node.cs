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
    public Node(NodeType type, List<Token> tokens)
    {
      Type = type;
      Tokens = tokens;
    }
    public NodeType Type { get; private set; } = NodeType.muffin;
    public List<Token> Tokens { get; private set; } = new List<Token>();

    public Node Left { get; set; } = null;
    public Node Right { get; set; } = null;

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
    public ExpressionNode(NodeType type, List<Token> tokens) : base(type, tokens)
    {
      Root = ExpressionTree.Build(tokens);
    }
    public ExpressionTree Root { get; set; } = null;
  }
  public class StatementNode : Node
  {
    public StatementNode(NodeType type, List<Token> tokens) : base(type, tokens)
    {
      
    }
  }
}

