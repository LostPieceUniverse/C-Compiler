---
geometry: margin=2cm
---
# C Compiler

## Writing a C compiler

[comment]: <> (thanks for lettin me copy ur doc memiXD)

1. [Lexing](##Lexer) and [Parsing](##Parser)
2. [Type and Scope checking](##TypeandScopechecking)

## Lexer

-> lexter breaks up sourcecode into tokens

Here are all the tokens your lexer needs to recognize, and the regular expression defining each of them:

- Open brace ```{```
- Close brace ```}```
- Open parenthesis ```(```
- Close parenthesis ```)```
- Semicolon ```;```
- Int keyword ```int```
- Return keyword ```return ```
- Identifier ```[a-zA-Z]```
- Integer literal ```[0-9]```

### What to do

Implement a *lex* function **input**: file **output**: list of tokens.

## Parser

## Type and Scope checking

# Quellen

![Nora Sandler](https://norasandler.com/2017/11/29/Write-a-Compiler.html)

![lexer tutorial](https://stlab.cc/legacy/how-to-write-a-simple-lexical-analyzer-or-parser.html)

