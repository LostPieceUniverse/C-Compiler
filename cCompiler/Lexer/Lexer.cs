using System.Text.RegularExpressions;

namespace Compiler
{
  public class Lexer
  {
    public static List<Token> Lexing(string code)
    {
      List<Token> tokens = new List<Token>();
      List<Token> identifierTokens = new List<Token>();
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
            token.Type = Token.TokenType.Literal;
            token.Literal = Token.LiteralType.StringLiteral;
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
            tokens.Add(HandleString(tempStr, tokens, identifierTokens));
            tempStr = string.Empty;
          }

          Token token = new Token();
          switch (character)
          {
            case " ":
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
        tokens.Add(HandleString(tempStr, tokens, identifierTokens));
      }

      return tokens;
    }

    public static Token HandleString(string str, List<Token> tokens, List<Token> identifiers)
    {
      /*
       * the following only works if no functions but main is used
       * because an identifier atm is either main or 
       * a variable...
       */
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
        case "main":
          token.Type = Token.TokenType.Identifier;
          token.Value = str;
          break;
        default:
          if (int.TryParse(str, out _))
          {
            token.Type = Token.TokenType.Literal;
            token.Literal = Token.LiteralType.IntegerLiteral;
          }
          else
          {
            token.Type = Token.TokenType.Identifier;
            if(identifiers.Any(token => token.Value == str))
            {
              int index = identifiers.FindIndex(a => a.Value == str);
              token.Literal = identifiers[index].Literal;
            }
            else if (tokens[tokens.Count - 1].Type == Token.TokenType.Int)
            {
              token.Literal = Token.LiteralType.IntegerLiteral;
              identifiers.Add(token);
            }
            else if(tokens[tokens.Count - 1].Type == Token.TokenType.String)
            {
              token.Literal = Token.LiteralType.StringLiteral;
              identifiers.Add(token);
            }
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
        _ => throw new Exception("Invalid character"),
      };
    }
  }
}
