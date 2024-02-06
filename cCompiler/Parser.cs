using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
  public class Parser
  {
    static public Node Parsing(List<Token> tokenList)
    {
      Node root = null;
      Node currentNode = null;
      List<Token> tokens = new List<Token>();

      for(int i = 0; i < tokenList.Count; i++)
      {
        Console.WriteLine("token");
        switch (tokenList[i].Type)
        {
            case TokenType.OpenBrace:
            if(root == null)
            {
              root = new Node(NodeType.Program);
              root.Tokens = tokens;
              currentNode = root;
              Console.WriteLine("*********************************");
              Console.WriteLine("Program");
              foreach (var item in tokens)
              {
                  Console.WriteLine("root token");
              }
            }
            tokens.Clear();
            continue;
            break;
          case TokenType.Semicolon:
              ExpressionNode expNode = new ExpressionNode(NodeType.Expression);
              expNode.Tokens = tokens;
              currentNode.ChildNodes.Add(expNode);
              Console.WriteLine("*********************************");
              Console.WriteLine("Expression");
              foreach (var item in tokens)
              {
                  Console.WriteLine("Expression token");
              }
              tokens.Clear();
              continue;
            break;
          case TokenType.Return:
              StatementNode statNode = new StatementNode(NodeType.Statement);
              statNode.Tokens = tokens.GetRange(i + 1, (tokenList.Count - 1));
              currentNode.ChildNodes.Add(statNode);
              currentNode = statNode;
              Console.WriteLine("*********************************");
              Console.WriteLine("Statement");
              foreach (var item in tokens)
              {
                  Console.WriteLine("Statement token");
              }
              tokens.Clear();
              break;
          default:
              tokens.Add(tokenList[i]);
            break;
        }
      }
      return root;
    }
  }
}
