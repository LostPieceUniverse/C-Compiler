---
geometry: margin=2cm
---
# C Compiler

## Writing a C compiler

1. [Lexer](##Lexer) -> done!
2. [Parser](##Parser) -> done *(still kinda work in progress)*
3. [Stuff](##Stuff) -> work in progress

## Lexer 
-> lexer breaks up sourcecode into tokens

### What to do
Implement a *lex* function **input**: file **output**: list of tokens.

### My Plan
I have a class *Token* holding two attributes<br>
-> *Type* enum(TokenType)<br>
-> *Value* string<br>

After Lexing I get a ```List<Token>``` holding the sourcecode broken up into tokens.<br>

### Lexical elements
Here are all the tokens this lexer could recognize, and the regular expression defining each of them:

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
#### Example
int variableName = 5;

**keyword** -> *int*<br>
**identifier** -> *variableName*<br>
**punctuator** - > *=*<br>
**constant** -> **integer-literal** -> *5*<br>
**punctuator** - > *;*

## Parser
-> parser builds AST with tokens and check Syntax.

### What to do
Implement a *pars* function **input**: list of tokens **output**: AST.<br>
AST should be rooted at a Program node and raise an error on invalid syntax.

### My Plan
*(I am ignoring scope/syntac/ect checking)*<br>
I have a class *Node* holding three attributes<br>
-> *Type* enum(NodeType)<br>
-> *Left* Node<br>
-> *Right* Node<br>
-> *Tokens* List<Token><br>

Left Nodes are the main strain going through the program ```Expressions```<br>
Right Nodes are sidetracks ```Statement```<br>

Right now everythign is in one function. If I'd add statements other than ```Return``` there would have to be some sort of recursion.<br>

### AST
-> Abstract Syntax Tree<br>
A data structure representing the structure of a program (or code snippet).

## Code Generator
-> code generator translates an AST into assemblycode.

### What to do
Implement a *translate* function **input**: AST **output**: assembly code.<br>

### My Plan
I have a class *Translator* with<br>
-> Translate function for each NodeType ```Program```,```FuncDecl```,```Statement```,```Expression```<br>

While recursivly iterating over the AST-Nodes, each node gets fed one of the corresponding translatefunctions.

I have to learn about proper memory handling.

### Understand Memory Handling
-> define how variables and data will be allocated and accessed in memory, considering  stack frames, heap memory ect.


## Stuff

### Translation Unit
-> The whole code file without any sort of preprocessor directives ('#xyz')

# Quellen

![Nora Sandler](https://norasandler.com/2017/11/29/Write-a-Compiler.html)

![lexer tutorial](https://stlab.cc/legacy/how-to-write-a-simple-lexical-analyzer-or-parser.html)

![c bible](https://web.archive.org/web/20200909074736if_/https://www.pdf-archive.com/2014/10/02/ansi-iso-9899-1990-1/ansi-iso-9899-1990-1.pdf)

![compiler explorer](https://godbolt.org/)

