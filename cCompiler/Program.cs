using System.Diagnostics;
namespace Compiler
{
  internal class Program
  {
    static void Main(string[] args)
    {
      //string path = @"/home/haru/test.c";
      string path = @"/home/runin/dev/c#/Compiler/test.c";
      //string path = @"C:\Users\sam.zgraggen\Desktop\test.c";

      //read filename from args
      //string directory = AppDomain.CurrentDomain.BaseDirectory;
      //string filename = args[0] //check if args[0] has filename
      //string code = File.ReadAllText(Path.Combine(directory, filename));

      string directory = @"/home/runin/dev/c#/Compiler/NasmAssembly/testing/";
      string filename = "test";
      string code = File.ReadAllText(path);//path.combine

      //Console.WriteLine("************LEXER***********************");
      List<Token> tokenList = Lexer.Lexing(code);
      //Console.WriteLine(string.Join(Environment.NewLine, tokenList));

      //Console.WriteLine("************PARSER********************");

      Node node = Parser.Parsing(tokenList);
      //Node.OutputNode(node);

      //Console.WriteLine("***************Generate Assembly*****************");

      string assembly = AssemblyGenerator.Generate(node);
      
      string tempDir = Path.GetTempPath();

      WriteToFile(assembly, tempDir, filename);

      /*
      // Run the nasm command
        Process nasmProcess = new Process();
        nasmProcess.StartInfo.FileName = "nasm";
        nasmProcess.StartInfo.Arguments = $"-f elf {filename}.asm";
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
*/
      Console.ReadLine();
    }

    static void WriteToFile(string code, string directory, string filename)
    {
      filename = filename + ".asm";
      string filePath = Path.Combine(directory, filename);

      File.WriteAllText(filePath, code);

      Console.WriteLine("File has been created at: " + filePath);
    }



    /*
    static void OutputNode(Node node)
    {
      if (node == null)
      {
        return;
      }
      Console.WriteLine("********************************");
      Console.WriteLine(node.Type.ToString());
      foreach (var token in node.Tokens)
      {
        List<Token> temp = new List<Token>();
        temp.Add(token);
      }
      OutputNode(node.Right);
      OutputNode(node.Left);
    }
    */
  }
}
