using System.Text.RegularExpressions;
 
namespace Compiler
{
  public class Lexer
  {
    
    static public List<Token> Lexing(string code)
    {
      List<Token> tokens = new List<Token>();
      string tempStr = string.Empty;
      bool isString = false;

      for(int i = 0; i < code.Length; i++)
      {
        string character = (code[i]).ToString();
        if(isString)
        {
          if(character == "\"")
          {
            Token token = new Token();
            token.Value = tempStr;
            if(int.TryParse(tempStr, out int num))
            {
              token.Type = Token.TokenType.IntegerLiteral;
            }
            else
            {
              token.Type = Token.TokenType.StringLiteral;
            }
            tokens.Add(token);
            isString = false;
            tempStr = string.Empty;
          }
          else
          {
            tempStr += character; 
          }
        }
        else if(Regex.IsMatch(character, "[a-z]", RegexOptions.IgnoreCase) || character == "_" || Regex.IsMatch(character, "[0-9]"))
        {
          tempStr += character; 
        }
        else
        {
          if(tempStr != string.Empty)
          {
            tokens.Add(HandleString(tempStr));
            tempStr = string.Empty;
          }

          Token token = new Token();
          switch (character)
          {
            case " ":
              continue;
            case "\"":
              if(!isString)
              {
                isString = true;
              }
              break;
            case "+":
              //token.Type = TokenType.Addition;
              //break;
            case "-":
              //token.Type = TokenType.Subtraction;
              //break;
            case "*":
              //token.Type = TokenType.Multiplication;
              //break;
            case "/":
              //token.Type = TokenType.Division;
              //break;
            case "%":
              //token.Type = TokenType.Modulo;
              token.Type = Token.TokenType.Operand;
              break;
            case "(":
              token.Type = Token.TokenType.OpenParenthesis;
              break;
            case ")":
              token.Type = Token.TokenType.CloseParenthesis;
              break;
            case "{":
              token.Type = Token.TokenType.OpenBrace;
              break;
            case "}":
              token.Type = Token.TokenType.CloseBrace;
              break;
            case "=":
              token.Type = Token.TokenType.Equals;
              break;
            case ";":
              token.Type = Token.TokenType.Semicolon;
              break;
            default:
              continue;
          }
          tokens.Add(token);
        }
      }
      return tokens;
    }

    static public Token HandleString(string str)
    {
      Token token = new Token();
      switch(str)
      {
        case "int":
          token.Type = Token.TokenType.Int;
          break;
        case "string":
          token.Type = Token.TokenType.String;
          break;
        case "return":
          token.Type = Token.TokenType.Return;
          break;
        case "if":
          token.Type = Token.TokenType.If;
          break;
        case "else":
          token.Type = Token.TokenType.Else;
          break;
        default:
          token.Type = Token.TokenType.Identifier;
          token.Value = str;
          break;
      }
      return token;
    }
  }
}
