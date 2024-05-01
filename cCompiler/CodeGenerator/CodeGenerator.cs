using System.Text;
namespace Compiler
{
    internal class AssemblyGenerator
    {
      static public void Generate(Node node)
      {
        GenerateNode(node);
      }
      
      static private void GenerateNode(Node node)
      {
        if(node == null)
        {
          return;
        }
        //call corresponding function
        switch (node.Type)
        {
          
          case Node.NodeType.Program:
            GenerateProgram(node);
            break;
          case Node.NodeType.FuncDecl:
            GenerateFuncDecl(node);
            break;
          case Node.NodeType.Statement:
            GenerateStatement(node);
            break;
          case Node.NodeType.IntegerExpression:
            GenerateIntegerExpression(node);
            break;
          case Node.NodeType.StringExpression:
            GenerateStringExpression(node);
            break;
          default:
            throw new Exception("NodeType error or w/e");
        }
        
        //go to next node
        GenerateNode(node.Right);
        GenerateNode(node.Left);
      }

      //ProgramNode
      static private void GenerateProgram(Node node)
      {
        
      }

      //FuncDeclNodes
      static private void GenerateFuncDecl(Node node)
      {

      }
      
      //StatementNodes
      static private void GenerateStatement(Node node)
      {

      }
      
      //ExpressionNodes
      static private void GenerateIntegerExpression(Node node)
      {

      }

      static private void GenerateStringExpression(Node node)
      {
        foreach(var n in node.Tokens)
        {
          Console.WriteLine(n.Value);
          Console.WriteLine(n.Type);
          Console.WriteLine(n.Literal);
          Console.WriteLine(newLine);
        }
      }

      private const string newLine = "\n"; //windows -> \r\n

      public StringBuilder SECTIONdata { get; set; } = new StringBuilder("SECTION .data" + newLine);

      public StringBuilder SECTIONtext { get; set; } = new StringBuilder("SECTION .text" + newLine + "global _start" + newLine + newLine + "_start:");

      public StringBuilder SECTIONbss { get; set; } = new StringBuilder("SECTION .bss");
    }
}
