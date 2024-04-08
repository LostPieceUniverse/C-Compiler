namespace Compiler
{
  public class Parser
  {
    static public Node Parsing(List<Token> tokenList)
    {
      Node rootNode = null;
      Node currentNode = null;
      List<Token> tempTokens = new List<Token>();

      for (int i = 0; i < tokenList.Count; i++)
      {
        switch (tokenList[i].Type)
        {
          case Token.TokenType.OpenBrace:
            if (rootNode == null)
            {
                rootNode = new Node(GetTokens(tempTokens));//to fix: make type type.programm aka main
                currentNode = rootNode;
            }
            tempTokens.Clear();
            continue;
          case Token.TokenType.Semicolon:
            List<Token> list = GetTokens(tempTokens);
            ExpressionNode expNode = new ExpressionNode(list);
            currentNode.Left = expNode;
            currentNode = expNode;
            tempTokens.Clear();
            continue;
          case Token.TokenType.Return:
            StatementNode statNode = new StatementNode(GetTokens(tokenList.Where((value, index) => index >= i && index <= tokenList.Count).ToList()));
            currentNode.Right = statNode;
            tempTokens.Clear();
            return rootNode;
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
