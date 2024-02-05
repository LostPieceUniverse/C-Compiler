---
geometry: margin=2cm
---
# C Compiler

## Writing a C compiler

1. [Lexer](##Lexer) -> done!
2. [Parser](##Parser)

## Lexer 
-> lexer breaks up sourcecode into tokens

### Lexical elements
Here are all the tokens my lexer should recognize, and the regular expression defining each of them:

- keyword<br>
All lowercase, for fixed keywords<br>(e.g. basic datatypes, if - else, ect.)
```
int
return
...
```
- identifier<br>
Sequence of nondigit characters (lower- & uppercase) including underscore<br>(e.g. function- & variablenames )
```
[a-zA-Z]
[0-9]
```
- constant<br>
A constant holds a value (string- / integer-literal)
```
string-literal
integer-literal
```

- string-literal<br>
Char-Sequence enclosed in double-quotes
```
[a-zA-Z]
[0-9]
...
```
- integer-literal<br>
numeric-Sequence 
```
[0-9]
```
- operator<br>
```
Arithmetic
Relational
Logical
Bitwise
Assignment
```
- punctuator<br>
```
()
{}
[]
=
;
:
,
.
->
...
```
### Example
int variableName = 5;

**keyword** -> *int*<br>
**identifier** -> *variableName*<br>
**punctuator** - > *=*<br>
**constant** -> **integer-literal** -> *5*<br>
**punctuator** - > *;*

### What to do

Implement a *lex* function **input**: file **output**: list of tokens.

## Parser
-> parser builds AST with tokens and check Syntax
### AST
-> Abstract Syntax Tree<br>
A data structure representing the structure of a program (or code snippet).

### What to do
Implement a *pars* function **input**: list of tokens **output**: AST.<br>
AST should be rooted at a Program node and raise an error on invalid syntax;



# Quellen

![Nora Sandler](https://norasandler.com/2017/11/29/Write-a-Compiler.html)

![lexer tutorial](https://stlab.cc/legacy/how-to-write-a-simple-lexical-analyzer-or-parser.html)

![c bible](https://web.archive.org/web/20200909074736if_/https://www.pdf-archive.com/2014/10/02/ansi-iso-9899-1990-1/ansi-iso-9899-1990-1.pdf)

