using System.Data.Common;
using System.Text;
namespace Compiler
{
    internal class Section
    {

      //.data
      static public void FillData(StringBuilder sb, List<string> stringVariables)
      {
        foreach(string variable in stringVariables)
        {
          sb.AppendFormat("\n{0} db '{1}'", variable.Replace(" ", "").ToLower(), variable);
        }
        sb.AppendFormat("\n");
      }
      
      //.textTop
      static public void InitVariables(StringBuilder sb, Dictionary<string, string> integerVariables)
      {
        sb.AppendFormat("\n;init variables");
        foreach(var variable in integerVariables)
        {
          sb.AppendFormat("\nmov dword [{0}], {1}", variable.Key, variable.Value);
        }
        sb.AppendFormat("\n");
      }

      //.textBody
      static public void CalcEquation(StringBuilder sb, Dictionary<string, string> integerVariables, IntegerLiteralExpressionNode equation, string identifier)
      {
          // Clear the string builder
          StringBuilder sb2 = new StringBuilder();
          
          // Traverse the equation tree to generate assembly code
          Traverse(equation, integerVariables, sb2);
          sb2.Append("  pop dword [" + identifier + "]");
          sb2.Append("\r\n");
          // Append the generated code to the main StringBuilder
          sb.Append(sb2);
          sb.Append("\r\n");
      }

      static private void Traverse(IntegerLiteralExpressionNode node, Dictionary<string, string> integerVariables, StringBuilder sb)
      {
          if (node == null)
          {
              return;
          }

          // Traverse left subtree
          if (node.LeftNode != null)
          {
              Traverse(node.LeftNode, integerVariables, sb);
          }

          // Traverse right subtree
          if (node.RightNode != null)
          {
              Traverse(node.RightNode, integerVariables, sb);
          }

          // Generate assembly for operators and values/variables
          if (node.IsOperator)
          {
              switch (node.Operand)
              {
                  case ExpressionTree.OperatorType.Addition:
                      sb.AppendLine("  pop rbx");
                      sb.AppendLine("  pop rax");
                      sb.AppendLine("  add rax, rbx");
                      sb.AppendLine("  push rax");
                      break;
                  case ExpressionTree.OperatorType.Subtraction:
                      sb.AppendLine("  pop rbx");
                      sb.AppendLine("  pop rax");
                      sb.AppendLine("  sub rax, rbx");
                      sb.AppendLine("  push rax");
                      break;
                  case ExpressionTree.OperatorType.Multiplication:
                      sb.AppendLine("  pop rbx");
                      sb.AppendLine("  pop rax");
                      sb.AppendLine("  imul rax, rbx");
                      sb.AppendLine("  push rax");
                      break;
                  case ExpressionTree.OperatorType.Division:
                      sb.AppendLine("  pop rbx");
                      sb.AppendLine("  pop rax");
                      sb.AppendLine("  cqo"); // convert rax to rdx:rax
                      sb.AppendLine("  idiv rbx");
                      sb.AppendLine("  push rax");
                      break;
                  default:
                      throw new InvalidOperationException("Unknown operator type");
              }
          }
          else if (node.IsValue)
          {
              // Check if the value is a variable
              if (integerVariables.ContainsKey(node.Value))
              {
                  sb.AppendLine($"  push dword [{node.Value}]");
              }
              else
              {
                  sb.AppendLine($"  push {node.Value}");
              }
          }
          else
          {
              throw new InvalidOperationException("Node is neither a value nor an operator");
          }
      }

      static public void ConsoleOutputString(StringBuilder sb, string str)
      {
        string variableName = str.Replace(" ", "").ToLower();
        
        string asmString = ";print\nmov eax, 4 \nmov ebx, 1\nmov ecx, " + variableName + "\nmov edx, " + str.Length + " \nint 0x80\n\n";
        sb.AppendFormat(asmString);
        sb.AppendFormat(newLine);
      }

      static public void Exit(StringBuilder sb)
      {
        string strExit = "\n;exit\nmov eax, 1 ;set eax register to 1 (sys_exit)\nxor ebx, ebx\nint 0x80 ;trigger system-call in eax";

        sb.AppendFormat(strExit);
      }

      //.bss
      static public void FillBss(StringBuilder sb, Dictionary<string, string> integerVariables)
      {
        foreach(var variable in integerVariables)
        {
          sb.AppendFormat("\n{0} resd 1", variable.Key);
        }

      }

      static public string newLine { get; private set; } = ";print newline character for formatting \n mov eax, 4 \n mov ebx, 1 \n mov ecx, newline \n mov edx, 1 \n int 0x80 \n\n";
    }
}
