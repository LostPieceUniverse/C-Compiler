---
geometry: margin=2cm
---
# C Compiler

## Writing a C compiler

[comment]: <> (thanks for lettin me copy ur doc memiXD)

1. [Lexing and Parsing](##LexerandParsing)
2. [Type and Scope checking](##TypeandScopechecking)

## Lexing and Parsing

### Lexer 
-> lexer breaks up sourcecode into tokens

#### Lexical elements
Here are all the tokens my lexer should recognize, and the regular expression defining each of them:

- keyword
All lowercase, for fixed keywords<br>(e.g. basic datatypes, if - else, ect.)
```
int
return
...
```
- identifier
Sequence of nondigit characters (lower- & uppercase) including underscore<br>(e.g. function- & variablenames )
```
[a-zA-Z]
[0-9]
```
- constant
A constant holds a value (string- / integer-literal)
```
string-literal
integer-literal
```

- string-literal
Char-Sequence enclosed in double-quotes
```
[a-zA-Z]
[0-9]
...
```
- integer-literal
numeric-Sequence 
```
[0-9]
```
- operator
```
Arithmetic
Relational
Logical
Bitwise
Assignment
```
- punctuator
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
#### Example
int variableName = 5;

**keyword** -> *int*<br>
**identifier** -> *variableName*<br>
**punctuator** - > *=*<br>
**constant** -> **integer-literal** -> *5*<br>
**punctuator** - > *;*

#### What to do

Implement a *lex* function **input**: file **output**: list of tokens.

### Parser
-> parser interpretes tokens as for example identifier

#### What to do
Combine Parsing with Lexer.

## Type and Scope checking

# Quellen

![Nora Sandler](https://norasandler.com/2017/11/29/Write-a-Compiler.html)

![lexer tutorial](https://stlab.cc/legacy/how-to-write-a-simple-lexical-analyzer-or-parser.html)

![c bible](https://web.archive.org/web/20200909074736if_/https://www.pdf-archive.com/2014/10/02/ansi-iso-9899-1990-1/ansi-iso-9899-1990-1.pdf)

