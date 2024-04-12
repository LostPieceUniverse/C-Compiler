using System.Text;
namespace Compiler
{
    internal class AssemblyGenerator
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
      static private void GenerateIntegerExpression(Node node)
      {

      }

      static private void GenerateStringExpression(Node node)
      {

      }
    }
}
