using System.Text;
namespace Compiler
{
    internal class AssemblyGenerator
    {
      static public string Generate(Node node)
      {
        AssemblyGenerator generator = new AssemblyGenerator();
        generator.GenerateNode(node);
        
        Section.FillData(generator.SECTIONdata, generator.stringVariables);

        Section.InitVariables(generator.SECTIONtextTop, generator.integerVariables);

        Section.FillBss(generator.SECTIONbss, generator.integerVariables);

        string returnString = string.Concat(generator.SECTIONdata, generator.SECTIONtextTop, generator.SECTIONtextBody, generator.SECTIONbss);

        return returnString;
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
        Section.Exit(SECTIONtextBody);
      }

      //ExpressionNodes
      private void GenerateIntegerExpression(Node node)
      {
        ExpressionNode expNode = node as ExpressionNode;
        IntegerLiteralExpressionNode intExprNode = expNode.ExpressionRootNode as IntegerLiteralExpressionNode;
        if(intExprNode.TreeGotOptimized)
        {
          integerVariables.Add(expNode.ExpressionIdentifier, intExprNode.Value);
        }
        else
        {
          //calc equation
          SECTIONtextBody.Append(";calc\n");
          Section.CalcEquation(SECTIONtextBody, integerVariables, intExprNode, expNode.ExpressionIdentifier);
        }
      }

      private void GenerateStringExpression(Node node)
      {
        ExpressionNode expNode = node as ExpressionNode;
        switch(expNode.ExpressionIdentifier)
        {
          case "printf":
            StringLiteralExpressionNode strExprNode = expNode.ExpressionRootNode as StringLiteralExpressionNode;
            stringVariables.Add(strExprNode.Value);
            Section.ConsoleOutputString(SECTIONtextBody, strExprNode.Value);
            break;
          default:
            break;
        }
      }

      private const string newLine = "\n"; //windows -> \r\n
      
      //lists to remember
      public List<string> stringVariables { get; set; } = new List<string>();
      
      public Dictionary<string, string> integerVariables { get; set; } = new Dictionary<string, string>();


      //stringbuilders
      public StringBuilder SECTIONdata { get; set; } = new StringBuilder("SECTION .data" + newLine + "newline db 0xA ;newline character for formatting output");

      public StringBuilder SECTIONtextTop { get; set; } = new StringBuilder("\n\nSECTION .text" + newLine + "global _start" + newLine + newLine + "_start:");

      public StringBuilder SECTIONtextBody { get; set; } = new StringBuilder();

      public StringBuilder SECTIONbss { get; set; } = new StringBuilder("\n\nSECTION .bss");

    }
}
