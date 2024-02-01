using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Lexer
{
    internal class Program
    {
        static void Main(string[] args)
        {
          List<Token> tokens = new List<Token>();
          string path = @"/home/fedora/test.txt";

          string tempStr = string.Empty;
          char lastChar;
          using (StreamReader reader = new StreamReader(path)) 
          {
            while (reader.Peek() >= 0) 
            {
              char character = (char)reader.Read();

            }   
          }
        }

    }
}
