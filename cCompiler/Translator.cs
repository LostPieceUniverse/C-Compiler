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
        //sb append translation
        sb.Append(node.PrintNodeType());
        sb.Append("Token Count: ");
        sb.AppendLine(node.Tokens.Count.ToString());
        //go to next node
        TranslateNode(node.Right, sb);
        TranslateNode(node.Left, sb);
      }

      static private void TranslateProgram()
      {
        
      }
      static private void TranslateExpression()
      {

      }

      static private void TranslateStatement()
      {

      }
    }
}
