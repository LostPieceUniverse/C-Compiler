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
      static public void CalcEquation(StringBuilder sb, Dictionary<string, string> integerVariables, IntegerLiteralExpressionNode equation)
      {
        //travers through calcTree
        sb.Append(Traverse(equation, 'r'));
        
      }
      
      static private string Traverse(IntegerLiteralExpressionNode node, char dir)
      {
         if (node.LeftNode == null && node.RightNode == null)
        {
          // If value is numeric or variable
          if (int.TryParse(node.Value, out _))
          {
            return "mov eax, " + node.Value + "\n";
          }
          else
          {
            return "mov eax, dword[" + node.Value + "]\n";
          }
        }
        else
        {
          string leftCode = string.Empty;
          string rightCode = string.Empty;

          if(dir == 'l')
          {
            rightCode = Traverse(node.RightNode, 'r');
            leftCode = Traverse(node.LeftNode, 'l');
          }
          else
          {
            leftCode = Traverse(node.LeftNode, 'l');
            rightCode = Traverse(node.RightNode, 'r');
          }
          
          switch(node.Operand)
          {
            case ExpressionTree.OperatorType.Addition:
              return leftCode + "add eax, " + rightCode + "\n";
            case ExpressionTree.OperatorType.Subtraction:
              return leftCode + "sub eax, " + rightCode + "\n";
            case ExpressionTree.OperatorType.Multiplication:
              return leftCode + "imul eax, " + rightCode + "\n";
            case ExpressionTree.OperatorType.Division:
              return leftCode + "idiv eax, " + rightCode + "\n";
            default:
              throw new Exception("idfk");
          }
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
