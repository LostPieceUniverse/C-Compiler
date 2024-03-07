using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Compiler
{
  public class Token
  {
    public enum TokenType
    {
      Int, 
      String, 
      Return,
      If,
      Else,
      Identifier, 
      StringLiteral, 
      IntegerLiteral, 
      Addition, 
      Subtraction, 
      Multiplication,
      Division,
      Modulo,
      OpenParenthesis, 
      CloseParenthesis,
      OpenBrace,
      CloseBrace,
      Equals,
      Semicolon,
      muffin
    }
    public TokenType Type { get; set; } = TokenType.muffin;
    public string Value { get; set; } = "memi";
  }
}
