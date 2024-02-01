using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Lexer 
{
  public enum TokenType
  {
    Int, 
    String, 
    Return, 
    Identifier, 
    StringLiteral, 
    IntegerLiteral, 
    Plus, 
    Minus, 
    OpenParenthesis, 
    CloseParenthesis,
    OpenBrace,
    CloseBrace,
    Equals,
    Semicolon,
    muffin
  }
  public class Token
  {
    public TokenType Type { get; set; } = TokenType.muffin;
    public string Value { get; set; } = "memi";
  }
}
