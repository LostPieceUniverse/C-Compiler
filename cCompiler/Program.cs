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

      Console.WriteLine("************LEXER***********************");
      List<Token> tokenList = Lexer.Lexing(code);

      Console.WriteLine("************PARSER********************");

      Node node = Parser.Parsing(tokenList);
      OutputNode(node);

      Console.WriteLine("********************************");

      CodeGenerator.Generate(node);

      Console.ReadLine();
    }

    static void OutputNode(Node node)
    {
      if (node == null)
      {
        return;
      }
      Console.WriteLine("********************************");
      Console.WriteLine(node.Type.ToString());
      foreach (var token in node.Tokens)
      {
        List<Token> temp = new List<Token>();
        temp.Add(token);
      }
      OutputNode(node.Right);
      OutputNode(node.Left);
    }

  }
}
