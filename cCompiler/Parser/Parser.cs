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
      List<Token> tempTokens = new List<Token>();

      for(int i = 0; i < tokenList.Count; i++)
      {
        switch (tokenList[i].Type)
        {
          case TokenType.OpenBrace:
            if(root == null)
            {
              root = new Node(NodeType.Program, GetTokens(tempTokens));
              currentNode = root;
            }
            tempTokens.Clear();
            continue;
          case TokenType.Semicolon:
              ExpressionNode expNode = new ExpressionNode(NodeType.Expression, GetTokens(tempTokens));
              currentNode.Left = expNode;
              currentNode = expNode;
              tempTokens.Clear();
              continue;
          case TokenType.Return:
              StatementNode statNode = new StatementNode(NodeType.Statement, GetTokens(tokenList.Where((value, index) => index >= i && index <= tokenList.Count).ToList()));
              currentNode.Right = statNode;
              tempTokens.Clear();
            return root;
          default:
            tempTokens.Add(tokenList[i]);
            break;
        }
      }
      return root;
    }

    static private List<Token> GetTokens(List<Token> tokens)
    {
      List<Token> tempList = new List<Token>();
      foreach(Token token in tokens)
      {
        tempList.Add(token);
      }
      return tempList;
    }
  }
}
