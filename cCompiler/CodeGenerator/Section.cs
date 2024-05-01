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
      }
      
      //.text
      static public void FillText(StringBuilder sb, List<string> integerVariables)
      {
        Section.InitVariables(sb, integerVariables);
      }

      static private void InitVariables(StringBuilder sb, List<string> integerVariables)
      {
        sb.AppendFormat("\n;init variables");
        foreach(string variable in integerVariables)
        {
          sb.AppendFormat("\nmov dword [{0}],", variable);
        }
      }

      //.bss
      static public void FillBss(StringBuilder sb)
      {

      }
    }
}
