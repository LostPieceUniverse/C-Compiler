using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using System.Text.RegularExpressions;
 
namespace Compiler
{
  public class Lexer
  {
    
    static public List<Token> LexAndParse(string code)
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
              token.Type = TokenType.IntegerLiteral;
            }
            else
            {
              token.Type = TokenType.StringLiteral;
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
              token.Type = TokenType.Plus;
              break;
            case "-":
              token.Type = TokenType.Minus;
              break;
            case "(":
              token.Type = TokenType.OpenParenthesis;
              break;
            case ")":
              token.Type = TokenType.CloseParenthesis;
              break;
            case "{":
              token.Type = TokenType.OpenBrace;
              break;
            case "}":
              token.Type = TokenType.CloseBrace;
              break;
            case "=":
              token.Type = TokenType.Equals;
              break;
            case ";":
              token.Type = TokenType.Semicolon;
              break;
            default:
              break;
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
          token.Type = TokenType.Int;
          break;
        case "string":
          token.Type = TokenType.String;
          break;
        case "return":
          token.Type = TokenType.Return;
          break;
        default:
          token.Type = TokenType.Identifier;
          token.Value = str;
          break;
      }
      return token;
    }
  }
}
