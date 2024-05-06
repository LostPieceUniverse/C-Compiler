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
      
      //.text
      static public void InitVariables(StringBuilder sb, Dictionary<string, string> integerVariables)
      {
        sb.AppendFormat("\n;init variables");
        foreach(var variable in integerVariables)
        {
          sb.AppendFormat("\nmov dword [{0}], {1}", variable.Key, variable.Value);
        }
        sb.AppendFormat("\n");
      }

      static public void CalcEquation(StringBuilder sb, Dictionary<string, string> integerVariables)
      {

      }

      static public void ConsoleOutputString(StringBuilder sb, string str)
      {
        string variableName = str.Replace(" ", "").ToLower();
        
        string asmString = " ;print\nmov eax, 4 \nmov ebx, 1\nmov ecx, " + variableName + "\nmov edx, " + str.Length + " \nint 0x80\n\n";
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
