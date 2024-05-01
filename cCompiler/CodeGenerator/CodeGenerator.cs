using System.Text;
namespace Compiler
{
    internal class AssemblyGenerator
    {
      static public void Generate(Node node)
      {
        AssemblyGenerator generator = new AssemblyGenerator();
        generator.GenerateNode(node);
        
        foreach(var a in generator.integerVariables)
        {
          Console.WriteLine(a + "----------------------------------------------------------------------");
        }
        Section.FillData(generator.SECTIONdata, generator.stringVariables);
        Section.FillText(generator.SECTIONtext, generator.integerVariables);

        Console.WriteLine(generator.SECTIONdata);
        Console.WriteLine(generator.SECTIONtext);
        Console.WriteLine(generator.SECTIONbss);
      }
      
      private void GenerateNode(Node node)
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
      private void GenerateProgram(Node node)
      {
        
      }

      //FuncDeclNodes
      private void GenerateFuncDecl(Node node)
      {

      }
      
      //StatementNodes
      private void GenerateStatement(Node node)
      {

      }
      
      //ExpressionNodes
      private void GenerateIntegerExpression(Node node)
      {
        ExpressionNode expNode = node as ExpressionNode;
        integerVariables.Add(expNode.ExpressionIdentifier);
        
      }

      private void GenerateStringExpression(Node node)
      {
        ExpressionNode expNode = node as ExpressionNode;
        switch(expNode.ExpressionIdentifier)
        {
          case "printf":
            //class section add to .data 
            StringLiteralExpressionNode strExprNode = expNode.ExpressionRootNode as StringLiteralExpressionNode;
            stringVariables.Add(strExprNode.Value);
            break;
          default:
            break;
        }
      }

      private const string newLine = "\n"; //windows -> \r\n
      
      //lists to remember
      public List<string> stringVariables { get; set; } = new List<string>();
      
      public List<string> integerVariables { get; set; } = new List<string>();

      //stringbuilders
      public StringBuilder SECTIONdata { get; set; } = new StringBuilder("SECTION .data" + newLine + "newline db 0xA ;newline character for formatting output");

      public StringBuilder SECTIONtext { get; set; } = new StringBuilder("\n\nSECTION .text" + newLine + "global _start" + newLine + newLine + "_start:");

      public StringBuilder SECTIONbss { get; set; } = new StringBuilder("\n\nSECTION .bss");
    }
}
