using System;
using System.Text;
namespace Compiler
{
    internal class CodeGenerator
    {
      static public void Generate(Node node)
      {
        StringBuilder sb = new StringBuilder();
        GenerateNode(node, sb);

        Console.WriteLine(sb);
      }
      
      static private void GenerateNode(Node node, StringBuilder sb)
      {
        if(node == null)
        {
          return;
        }
        //call corresponding function
        switch (node.Type)
        {
          case NodeType.Program:
            GenerateProgram(node);
            break;
          case NodeType.FuncDecl:
            GenerateFuncDecl(node);
            break;
          case NodeType.Statement:
            GenerateStatement(node);
            break;
          case NodeType.Expression:
            GenerateExpression(node);
            break;
          default:
            break;
        }
        
        //go to next node
        GenerateNode(node.Right, sb);
        GenerateNode(node.Left, sb);
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
      static private void GenerateExpression(Node node)
      {

      }
    }
}
