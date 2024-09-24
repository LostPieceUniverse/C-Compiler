# C Compiler

- [1 Objective](#1-objective)
- [2 Procedure](#2-procedure)
    - [2.1 Technology selection](#2.1-technology-selection)
    - [2.2 Modular structure](#2.2-modular-structure)
        - [2.2.1 Lexer](#2.2.1-lexer)
        - [2.2.2 Parser](#2.2.2-parser)
        - [2.2.3 Code generator](#2.2.3-code-generator) 
        - [2.2.4 Test strategy](#2.2.4-test-strategy)
- [3 Current status](#3-current-status)
    - [3.1 Milestones achieved](#3.1-milestones-achieved) 
    - [3.2 Problems](#3.2-problems)
    - [3.3 Example](#3.3-example)
        - [3.3.1 Works](#3.3.1-works) 
        - [3.3.2 Faulty](#3.3.2-faulty)
- [4 Outlook]](#4-outlook)
- [5 Sources](#5-sources)

# 1 Objective

The goal is to create a simple C compiler. The compiler should be able to read a basic C file, convert it into NASM assembly, and generate an executable file from it.

C is a straightforward language, which is why the compiler was chosen to be a C compiler.

Through this project, I will learn how a compiler is structured and gain a deeper understanding of the theory behind it. Additionally, I will become familiar with and use assembly language. Patterns like Lexer and Parser will be applied, giving me practical experience.

The compiler will be developed according to the Open-Closed Principle, allowing for incremental extensions over time. It should be able to output strings without preprocessor directives and handle basic integer calculations, as well as initialize and use variables. A semantic analyzer and optimizer will not be implemented initially. However, calculations that don’t involve variables will be computed directly.

# 2 Procedure

## 2.1 Technology selection
The compiler is being developed using .NET 6.0 and C#. I chose the .NET framework and C# because I have the most experience with them, and the project itself is already very complex. However, I am aware that a functional programming language (e.g., Haskell) would likely be more suitable.

[Lunarvim](https://www.lunarvim.org/) is my preferred development environment on the Linux distribution EndeavourOS. Since I work on this project in my free time, it was advantageous to stay in my familiar environment. This allowed me to focus entirely on the complexity of the project.

## 2.2 Modular structure

### 2.2.1 Lexer

The lexer analyzes source code and divides it into tokens.

**To Do:**<br>
Implement a ```lex``` method.<br>
**Input:** C source code **Output:** List of Tokens ```List<Token>```<br>

**Structure:**<br>

There is a ```Token``` class that includes the following elements:<br>

two enumerations:<br> 

    - ```TokenType``` 
    - ```LiteralType```

three attributes:<br>

    - ```TokenType Type```
    - ```string Value```
    - ```LiteralType Literal```

Two Enumerations:<br>

    - ```TokenType: {Int, Return, Literal, Semicolon, …}```
    - ```LiteralType: {StringLiteral, IntegerLiteral, …}```

Example: For the statement ```Int a = 5;```<br>

    - Type      =   Identifier
    - Value     =   “a”
    - Literal   =   IntegerLiteral

**Lexical Elements**

Here are all the tokens that this lexer can recognize, along with the regular expressions that define each one:

| Term | Definition	| Example |
|:------------|:------------:|:------------:|
| Keyword |	All lowercase letters used for reserved keywords (e.g., basic data types, if-else statements). |	int, return, ... |
| Identifier |	A sequence of non-numeric characters (both uppercase and lowercase) including underscores (e.g., function and variable names). |	[a-zA-Z], [0-9] |
| Constant |	A constant holds a value (either a string or an integer). |	string-literal, integer-literal |
| String Literal |	A sequence of characters enclosed in double quotation marks. |	"[a-zA-Z]", "[0-9]", ... |
| Integer Literal |	A sequence of numeric characters. |	[0-9] |
| Operator |	Used for arithmetic operations. |	arithmetic |
| Punctuation |	Symbols used for grouping and assignment. |	(), =, :, ... |

**Example:** ```int variableName = 5;```<br>

- Keyword: int<br>
- Identifier: variableName<br>
- Punctuator: =<br>
- Constant: Integer Literal: 5<br>
- Punctuator: ;<br>

### 2.2.2 Parser

The parser processes the token list to create an Abstract Syntax Tree (AST) and performs a partial syntax check. This verification occurs automatically, as, for instance, a variable name is expected after a data type.

**To Do:**<br>
Implement a ```pars``` method.<br>
**Input:** List of Tokens ```List<Token>``` **Output:** Abstract Syntax Tree (AST)<br> 

**Structure:**<br>

The program consists of two main classes: ```Node``` and ```ExpressionTree```, along with their subclasses.

***Class: Node***<br>

The Node class represents the nodes that form the Abstract Syntax Tree (AST).<br> 

- Constructor:<br> 
    - This constructor checks whether the sequence of tokens represents an expression or a statement, and initializes the ```Type```and ```Tokens```variables accordingly.<br>

- Attributes:<br>
    - ```NodeType Type```
    - ```List<Token> Tokens```
    - ```Node Left```
    - ```Node Right```

- Enum:<br>
    - ```NodeType {Program, FuncDecl, Statement, IntegerExpression, StringExpression}```

*Subclass: ExpressionNode*<br>

This subclass creates ```string``` or ```integer literal``` objects from the ```ExpressionTree``` class and constructs an AST for handling those expressions.

*Subclass: StatementNode*<br>

This subclass is designed to handle statements such as ```return``` and ```if-else```. However, its functionality will not be implemented in this project. It exists to support simple statements like ```return 0;```.

***Class: ExpressionTree***<br>

- Enum:<br>
    - ```OperatorType {Addition, Subtraction, Multiplication, Division}```

*Subclass: IntegerLiteralExpressionTree*<br>

This subclass recursively forms an equation AST and implements the method ```TreeNodeSolver()```, which computes equations that do not contain variables directly.<br>

- Attributes:<br>
    - ```OperatorType Operand```<br>
    - ```bool IsOperator```<br>
    - ```string Value```<br>
    - ```bool IsValue```<br>
    - ```int Precedence```<br>
    - ```bool TreeGotOptimized```<br>

*Subclass: StringLiteralExpressionTree*<br>

This subclass constructs an AST with a node that contains the string being processed.<br>

- Attribute:<br>
    - ```string Value```<br>

**AST:**<br>

A data structure that represents the structure of a program (or code snippet). The Abstract Syntax Tree (AST) should be rooted in a program node and trigger an error in the event of invalid syntax. The left nodes form the main trunk that runs through the program, while the right nodes represent branches (expressions that are handled by the ```ExpressionTree class```).

