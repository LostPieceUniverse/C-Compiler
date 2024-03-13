using System.Text.RegularExpressions;

namespace Compiler
{
  public class Lexer
  {
    public static List<Token> Lexing(string code)
    {
      List<Token> tokens = new List<Token>();
      string tempStr = string.Empty;
      bool isString = false;

      for (int i = 0; i < code.Length; i++)
      {
        string character = (code[i]).ToString();
        if (isString)
        {
          if (character == "\"")
          {
            Token token = new Token();
            token.Value = tempStr;
            token.Type = Token.TokenType.StringLiteral;
            tokens.Add(token);
            isString = false;
            tempStr = string.Empty;
          }
          else
          {
            tempStr += character;
          }
        }
        else if (Regex.IsMatch(character, "[a-z]", RegexOptions.IgnoreCase) || character == "_" || Regex.IsMatch(character, "[0-9]"))
        {
          tempStr += character;
        }
        else
        {
          if (tempStr != string.Empty)
          {
              tokens.Add(HandleString(tempStr));
              tempStr = string.Empty;
          }

          Token token = new Token();
          switch (character)
          {
            case " ":
              //if (double.TryParse(tempStr, out _))
              //{
                //token.Value = tempStr;
                //tempStr = string.Empty;
                //token.Type = Token.TokenType.IntegerLiteral;
              //}
              continue;
            case "\"":
              if (!isString)
              {
                  isString = true;
              }
              break;
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
            case "(":
            case ")":
            case "{":
            case "}":
            case "=":
            case ";":
              if (!isString)
              {
                tokens.Add(new Token
                {
                    Value = character,
                    Type = GetTokenType(character)
                });
              }
              break;
            default:
              continue;
          }
        }
    }

      if (tempStr != string.Empty)
      {
          tokens.Add(HandleString(tempStr));
      }

      return tokens;
  }

    public static Token HandleString(string str)
    {
        Token token = new Token();
        switch (str)
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
                if (int.TryParse(str, out _))
                {
                    token.Type = Token.TokenType.IntegerLiteral;
                }
                else
                {
                    token.Type = Token.TokenType.Identifier;
                }
                token.Value = str;
                break;
        }
        return token;
    }

    private static Token.TokenType GetTokenType(string character)
    {
        return character switch
        {
            "+" => Token.TokenType.Operand,
            "-" => Token.TokenType.Operand,
            "*" => Token.TokenType.Operand,
            "/" => Token.TokenType.Operand,
            "%" => Token.TokenType.Operand,
            "(" => Token.TokenType.OpenParenthesis,
            ")" => Token.TokenType.CloseParenthesis,
            "{" => Token.TokenType.OpenBrace,
            "}" => Token.TokenType.CloseBrace,
            "=" => Token.TokenType.Equals,
            ";" => Token.TokenType.Semicolon,
            _ => throw new ArgumentException("Invalid character"),
        };
    }
  }
}
