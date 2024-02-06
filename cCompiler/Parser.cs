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
              root = new Node(NodeType.Program);
              TransmitTokens(root.Tokens, tempTokens);
              currentNode = root;
            }
            tempTokens.Clear();
            continue;
          case TokenType.Semicolon:
              ExpressionNode expNode = new ExpressionNode(NodeType.Expression);
              TransmitTokens(expNode.Tokens, tempTokens);
              currentNode.ChildNodes.Add(expNode);
              tempTokens.Clear();
              continue;
          case TokenType.Return:
              StatementNode statNode = new StatementNode(NodeType.Statement);
              TransmitTokens(statNode.Tokens, tokenList.Where((value, index) => index >= i && index <= tokenList.Count).ToList());
              currentNode.ChildNodes.Add(statNode);
              currentNode = statNode;
              tempTokens.Clear();
            return root;
            break;
          default:
            tempTokens.Add(tokenList[i]);
            break;
        }
      }
      return root;
    }

    static private void TransmitTokens(List<Token> nodeTokens, List<Token> tokens)
    {
        foreach(Token token in tokens)
        {
          nodeTokens.Add(token);
        }
    }
  }
}
