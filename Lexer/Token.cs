using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Lexer 
{
  public interface IToken
  {
    //idk
  }
  public class Token
  {
    public object Value;
  }
  
  //grouping, seoerator
  public class OpenBrace : Token
  {
    public char Value { get; } = '{';
  }
  public class CloseBrace : Token
  {
    public char Value { get; } = '}';
  }
  public class OpenParenthesis : Token
  {
    public char Value { get; } = '(';
  }
  public class CloseParenthesis : Token
  {
    public char Value { get; } = ')';
  }
  public class Semicolon : Token
  {
    public char Value { get; } = ';';
  }
  
  //operators
  public class Plus : Token
  {
    public char Value { get; } = '+';
  }
  public class Minus : Token
  {
    public char Value { get; } = '-';
  }
  public class Equals : Token 
  {
    public char Value { get; } = '=';
  }

  //keywords
  public class Int : Token
  {
    public string Value { get; } = "int";
  }
  public class Return : Token
  {
    public string Value { get; } = "return";
  }

  //identifier
  public class Identifier : Token
  {
    public Identifier(string identifier)
    { 
      Value = identifier;
    }
    public string Value { get; private set; }
  }

  //constant
  public class IntegerLiteral : Token
  {
    public IntegerLiteral(int integerLiteral)
    {
      Value = integerLiteral;
    }
    public int Value { get; private set; }
  }  

  public class Bullshit : Token
  {
    public string Value { get; } = "sheit";
  }
}
