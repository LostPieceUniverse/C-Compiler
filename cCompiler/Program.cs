namespace Compiler
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //string path = @"/home/haru/test.c";
      string path = @"/home/runin/dev/c#/Compiler/test.c";
      //string path = @"C:\Users\sam.zgraggen\Desktop\test.c";
      string code = File.ReadAllText(path);
      Console.WriteLine(code);
      Console.WriteLine("************LEXER***********************");

      List<Token> tokenList = Lexer.Lexing(code);
      OutputToken(tokenList);

      Console.WriteLine("************PARSER********************");
      
      Node node = Parser.Parsing(tokenList);
      OutputToken(tokenList);
      OutputNode(node);

      Console.WriteLine("********************************");

      CodeGenerator.Generate(node);

      Console.ReadLine();
    }

    static void OutputToken(List<Token> list)
    {
      foreach (var token in list)
      {
        switch (token.Type)
        {
          case Token.TokenType.Int:
              Console.WriteLine("int");
              break;
          case Token.TokenType.String:
              Console.WriteLine("string");
              break;
          case Token.TokenType.Return:
              Console.WriteLine("return");
              break;
          case Token.TokenType.If:
              Console.WriteLine("if");
              break;
          case Token.TokenType.Else:
              Console.WriteLine("else");
              break;
          case Token.TokenType.IntegerLiteral:
              Console.WriteLine(token.Value);
              break;
          case Token.TokenType.StringLiteral:
              Console.WriteLine(token.Value);
              break;
          case Token.TokenType.Identifier:
              Console.WriteLine(token.Value);
              break;
          case Token.TokenType.OpenParenthesis:
              Console.WriteLine("(");
              break;
          case Token.TokenType.CloseParenthesis:
              Console.WriteLine(")");
              break;
          case Token.TokenType.OpenBrace:
              Console.WriteLine("{");
              break;
          case Token.TokenType.CloseBrace:
              Console.WriteLine("}");
              break;
          case Token.TokenType.Equals:
              Console.WriteLine("=");
              break;
          case Token.TokenType.Semicolon:
              Console.WriteLine(";");
              break;
          default:
              break;
        }
      }
    }

    static void OutputNode(Node node)
    {
      if(node == null)
      {
        return;
      }
      Console.WriteLine("********************************");
      Console.WriteLine(node.Type.ToString());
      foreach(var token in node.Tokens)
      {
        List<Token> temp = new List<Token>();
        temp.Add(token);
        OutputToken(temp);
      }
      OutputNode(node.Right);
      OutputNode(node.Left);
    }
  }
}
