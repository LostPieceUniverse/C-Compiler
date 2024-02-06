using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
          //string path = @"/home/haru/test.c";
          string path = @"/home/fedora/dev/Compiler/test.c";
          string code = File.ReadAllText(path);
          List<Token> tokenList = Lexer.Lexing(code);
          Output(tokenList);
        }

        static void Output(List<Token> list)
        {
          foreach(var token in list)
          {
            switch(token.Type)
            {
              case TokenType.Int:
                Console.WriteLine("int");
                break;
              case TokenType.String:
                Console.WriteLine("string");
                break;
              case TokenType.Return:
                Console.WriteLine("return");
                break;
              case TokenType.If:
                Console.WriteLine("if");
                break;
              case TokenType.Else:
                Console.WriteLine("else");
                break;
              case TokenType.IntegerLiteral:
                Console.WriteLine(token.Value);
                break;
              case TokenType.StringLiteral:
                Console.WriteLine(token.Value);
                break;
              case TokenType.Identifier:
                Console.WriteLine(token.Value);
                break;
              case TokenType.Plus:
                Console.WriteLine("+");
                break;
              case TokenType.Minus:
                Console.WriteLine("-");
                break;
              case TokenType.Multiplication:
                Console.WriteLine("*");
                break;
              case TokenType.Divition:
                Console.WriteLine("/");
                break;
              case TokenType.Modulo:
                Console.WriteLine("%");
                break;
              case TokenType.OpenParenthesis:
                Console.WriteLine("(");
                break;
              case TokenType.CloseParenthesis:
                Console.WriteLine(")");
                break;
              case TokenType.OpenBrace:
                Console.WriteLine("{");
                break;
              case TokenType.CloseBrace:
                Console.WriteLine("}");
                break;
              case TokenType.Equals:
                Console.WriteLine("=");
                break;
              case TokenType.Semicolon:
                Console.WriteLine(";");
                break;
              default:
                break;
            }
          }
        }
    }
}
