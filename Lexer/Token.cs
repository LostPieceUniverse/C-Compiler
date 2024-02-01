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
  
  //keywordss
  public class Int :IToken
  {
    public  GetValue()
    {
      return '{';
    }
  }
  public class CloseBrace :IToken
  {
    public object GetValue()
    {
      return '}';
    }
  }
  public class OpenParenthesis :IToken
  {
    public object GetValue()
    {
      return '(';
    }
  }
  public class CloseParenthesis :IToken
  {
    public object GetValue()
    {
      return ')';
    }
  }
  public class Semicolon :IToken
  {
    public object GetValue()
    {
      return ';';
    }
  }
  
  //operators
  public class Plus :IToken
  {
    public object GetValue()
    {
      return _value;
    }
  }
  public class Minus :IToken
  {
    public object GetValue()
    {
      return _value;
    }
  }
  public class Equals :IToken 
  {
    public object GetValue()
    {
      return _value;
    }
  }

  //keywords
  public class Int :IToken
  {
    public object GetValue()
    {
      return _value;
    }
  }
  public class Return :IToken
  {
    public object GetValue()
    {
      return _value;
    }
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
