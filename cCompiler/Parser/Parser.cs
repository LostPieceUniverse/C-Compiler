namespace Compiler
{
  public class Parser
  {
    static public Node Parsing(List<Token> tokenList)
    {
      Node rootNode = null;
      Node currentNode = null;
      Stack<Node> nodeStack = new Stack<Node>();
      
      List<Token> tempTokens = new List<Token>();

      for (int i = 0; i < tokenList.Count; i++)
      {
        switch (tokenList[i].Type)
        {
          case Token.TokenType.OpenBrace:
            if (rootNode == null)
            {
              rootNode = new Node(GetTokens(tempTokens));
              currentNode = rootNode;
            }
            else
            {
              Node newNode = new Node(GetTokens(tempTokens));
              currentNode.Left = newNode;
              nodeStack.Push(currentNode);
              currentNode = newNode;
            }
            tempTokens.Clear();
            break;

          case Token.TokenType.CloseBrace:
            if (nodeStack.Count > 0)
            {
              currentNode = nodeStack.Pop();
            }
            tempTokens.Clear();
            break;

          case Token.TokenType.Semicolon:
            List<Token> list = GetTokens(tempTokens);
            ExpressionNode expNode = new ExpressionNode(list);
            currentNode.Left = expNode;
            currentNode = expNode;
            tempTokens.Clear();
            break;

          case Token.TokenType.Return:
            StatementNode statNode = new StatementNode(GetTokens(tokenList.GetRange(i, (tokenList.Count - 1) - i)));
            currentNode.Right = statNode;
            tempTokens.Clear();
            i = tokenList.Count - 1;
            break;

          default:
            tempTokens.Add(tokenList[i]);
            break;
        }
      }

      return rootNode;
    }

    static private List<Token> GetTokens(List<Token> tokens)
    {
      List<Token> tempList = new List<Token>();
      foreach (Token token in tokens)
      {
        tempList.Add(token);
      }
      return tempList;
    }
  }
}
