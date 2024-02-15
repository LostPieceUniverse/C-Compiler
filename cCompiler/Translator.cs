using System;
using System.Text;
namespace Compiler
{
    internal class Translator
    {
      static public void Translating(Node node)
      {
        StringBuilder sb = new StringBuilder();
        TranslateNode(node, sb);

        Console.WriteLine(sb);
      }
      
      static private void TranslateNode(Node node, StringBuilder sb)
      {
        if(node == null)
        {
          return;
        }
        //call corresponding function
        switch (node.Type)
        {
          case NodeType.Program:
            TranslateProgram(node);
            break;
          case NodeType.FuncDecl:
            TranslateFuncDecl(node);
            break;
          case NodeType.Statement:
            TranslateStatement(node);
            break;
          case NodeType.Expression:
            TranslateExpression(node);
            break;
          default:
            break;
        }
        
        //go to next node
        TranslateNode(node.Right, sb);
        TranslateNode(node.Left, sb);
      }

      //ProgramNode
      static private void TranslateProgram(Node node)
      {
        
      }

      //FuncDeclNodes
      static private void TranslateFuncDecl(Node node)
      {

      }
      
      //StatementNodes
      static private void TranslateStatement(Node node)
      {

      }
      
      //ExpressionNodes
      static private void TranslateExpression(Node node)
      {

      }
    }
}
