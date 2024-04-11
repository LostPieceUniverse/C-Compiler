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
      Literal, 
      Operand,
      OpenParenthesis, 
      CloseParenthesis,
      OpenBrace,
      CloseBrace,
      Equals,
      Semicolon,
      muffin
    }
    public enum LiteralType
    {
      StringLiteral,
      IntegerLiteral,
      muffin
    }
    public TokenType Type { get; set; } = TokenType.muffin;
    public string Value { get; set; } = "memi";
    public LiteralType Literal { get; set; } = LiteralType.muffin;

  }
}
