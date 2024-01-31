using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Lexer 
{
  public interface IToken
  {
    public object GetValue();
  }
  
  //grouping, seoerator
  public class OpenBrace :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = '{';
  }
  public class CloseBrace :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = '}';
  }
  public class OpenParenthesis :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = '(';
  }
  public class CloseParenthesis :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = ')';
  }
  public class Semicolon :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = ';';
  }
  
  //operators
  public class Plus :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = '+';
  }
  public class Minus :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = '-';
  }
  public class Equals :IToken 
  {
    public object GetValue()
    {
      return _value;
    }
    private char _value = '=';
  }

  //keywords
  public class Int :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private string _value = "int";
  }
  public class Return :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private string _value = "return";
  }

  //identifier
  public class Identifier :IToken
  {
    public Identifier(string identifier)
    { 
      _value = identifier;
    }
    public object GetValue()
    {
      return _value;
    }
    private string _value;
  }

  //constant
  public class IntegerLiteral :IToken
  {
    public IntegerLiteral(int integerLiteral)
    {
      _value = integerLiteral;
    }
    public object GetValue()
    {
      return _value;
    }
    private int _value;
  }  

  public class Bullshit :IToken
  {
    public object GetValue()
    {
      return _value;
    }
    private string _value = "sheit";
  }
}
