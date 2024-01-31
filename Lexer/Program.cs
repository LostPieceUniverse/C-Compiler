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
          List<IToken> tokens = new List<IToken>();
          string path = @"/home/fedora/test.txt";

          string tempStr = string.Empty;
          char lastChar;
          using (StreamReader reader = new StreamReader(path)) 
          {
            while (reader.Peek() >= 0) 
            {
              char character = (char)reader.Read();

              switch(character)
              {
                case ' ':
                  switch (tempStr)
                  {
                    case "":
                      continue;
                    case "int":
                      tokens.Add(new Int());
                      tempStr = string.Empty;
                      break;
                    case "return":
                      tokens.Add(new Return());
                      tempStr = string.Empty;
                      break;
                    default:
                      if(int.TryParse(tempStr, out int integer))
                      {
                        tokens.Add(new IntegerLiteral(integer));
                      }
                      else
                      {
                        tokens.Add(new Identifier(tempStr));
                      }
                      tempStr = string.Empty;
                      break;
                  }
                  break;
                case '(':
                  if(tempStr != Empty)
                  {
                      if(int.TryParse(tempStr, out int integer))
                      {
                        tokens.Add(new IntegerLiteral(integer));
                      }
                      else
                      {
                        tokens.Add(new Identifier(tempStr));
                      }
                      tempStr = string.Empty;
                  }
                  tokens.Add(new OpenParenthesis());
                  break;
                case ')':
                  tokens.Add(new CloseParenthesis());
                  break;
                case '{':
                  tokens.Add(new OpenBrace());
                  break;
                case '}':
                  tokens.Add(new CloseBrace());
                  break;
                case ';':
                  tokens.Add(new Semicolon());
                  break;
                case '+':
                  tokens.Add(new Plus());
                  break;
                case '-':
                  tokens.Add(new Minus());
                  break;
                case '=':
                  tokens.Add(new Equals());
                  break;
                default:
                  tempStr += character.ToString();
                  break;
              }
              lastChar = character;
              Console.Write(character.ToString());
              //tokens.Add((char).sr.Read());
            }   
          }
          foreach(var item in tokens)
          {
            Console.WriteLine(item.GetValue().ToString());
          }
        }

    }
}
