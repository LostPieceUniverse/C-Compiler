using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Compiler
{
  internal class Program
  {
    static void Main(string[] args)
    {
      string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/dev/c#/C-Compiler/test.c";
      
      
      string fileName = "test";
      string code = File.ReadAllText(path);

      //Console.WriteLine("************LEXER***********************");
      List<Token> tokenList = Lexer.Lexing(code);

      //Console.WriteLine("************PARSER********************");
      Node node = Parser.Parsing(tokenList);

      //Console.WriteLine("***************Generate Assembly*****************");
      string assembly = AssemblyGenerator.Generate(node);
      
      string tempDir = Path.GetTempPath();

      WriteToFile(assembly, tempDir, fileName);
    }

    static void WriteToFile(string code, string directory, string fileName)
    {
      fileName = fileName + ".asm";
      string filePath = Path.Combine(directory, fileName);

      File.WriteAllText(filePath, code);

      Console.WriteLine("File has been created at: " + filePath);
    }

    static string ReadFile(string[] args)
    {
      string directory = AppDomain.CurrentDomain.BaseDirectory;
      string fileName = args[0]; 
      string code = File.ReadAllText(Path.Combine(directory, fileName));
      return code;
    }

    static void GenerateExecutable(string fileName) //not correctly implemented yet
    {
      // Run the nasm command
      Process nasmProcess = new Process();
      nasmProcess.StartInfo.FileName = "nasm";
      nasmProcess.StartInfo.Arguments = $"-f elf {fileName}.asm";
      nasmProcess.StartInfo.UseShellExecute = false;
      nasmProcess.StartInfo.RedirectStandardOutput = true;
      nasmProcess.Start();
      nasmProcess.WaitForExit();

      // Run the ld command
      Process ldProcess = new Process();
      ldProcess.StartInfo.FileName = "ld";
      ldProcess.StartInfo.Arguments = $"-m elf_i386 {fileName}.o -o {fileName}";
      ldProcess.StartInfo.UseShellExecute = false;
      ldProcess.StartInfo.RedirectStandardOutput = true;
      ldProcess.Start();
      ldProcess.WaitForExit();

      Console.WriteLine("Commands executed successfully.");
    }
  }
}
