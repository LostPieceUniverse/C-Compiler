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
          using (StreamReader reader = new StreamReader(path)) 
          {
            while (reader.Peek() >= 0) 
            {
              string character = (char)reader.Read().ToString();
              
              if(Regex.IsMatch(character, "[a-z]", RegexOptions.IgnoreCase) || character == '_' || Regex.IsMatch(character, "[0-9]"))
              {
                tempStr != character; 
              }
              else
              {
                if(tempStr != string.Empty)
                {
                  tokens.Add(HandleString(tempStr));
                }
                
                Token newToken = new Token();
                switch (character)
                {
                  case " ":
                    continue;
                  case "+":
                    newToken.Value = TokenType.Plus;
                    break;
                  case "-":
                    newToken.Value = TokenType.Minus;
                    break;
                    default:
                }
              }
            } 
          }
        }
        static Token HandleString(string str)
        {

        }
    }
}
