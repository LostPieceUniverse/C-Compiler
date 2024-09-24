<!-- TOC start (generated with https://github.com/derlin/bitdowntoc) -->

- [C Compiler NOT UP TO DATE but still usable](#c-compiler-not-up-to-date-but-still-usable)

<!-- TOC end -->

<!-- TOC --><a name="c-compiler-not-up-to-date-but-still-usable"></a>
# C Compiler NOT UP TO DATE but still usable

the code I am testing with:
```c
int main() {
  int a = 6;
  int b = 2 + 3;
  printf("another string");
  int c = 3 + (8 - 2) * 3;
  int d = 3 + (a - b) * 3;
  printf("HelloWorld");
  return 0;
}
```
```muffin``` and ```memi``` are ment as default values.
yes I know one could ```#include <stdio.h>``` but I wanned to implement the printf function myself in the Assembygenerator for now.
## Writing a C compiler

1. [Lexer](#Lexer) -> done!
2. [Parser](#Parser) -> done 
3. [CodeGenerator](#CodeGenerator) -> work in progress
4. [Glossar](#Glossar)
5. [References](#References)

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

if...<br> 
...an ```Expression``` is an equation I will have to form an AST for said equation -> seperate class *ExpressionTree*<br>
...an ```Expression``` is a string literal it must be in the root node since those go ontop of everything in assembly(also handled in *ExpressionTree*)<br>

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

As I traverse through the AST I'll have ```StringBuilder```s creating the different sections.
One for the ```.data```, ```.text``` and ```.bss``` section.

### Memory Handling
-> define how variables and data will be allocated and accessed in memory, considering  stack frames, heap memory ect.<br>

Memory will either be allocated on a stack or heap.

#### Stack
The size of memory to be allocated is known to the compiler and whenever a function is called, its variables get memoru allocated on the stack.
Is the functioncall over the memory for those variables will be de-allocated. This means any value stored in stack memory is accesible
as long as the function has not completed its execution. Stack memory allocation is considered safer as compared to heap memory allocation
because the data stored can only be accessed by the owner thread. Altho memory allocation and de-allocation are faster than heap memory allocation
stack memroy has less storage space than heap memory.

#### Heap
later...to compilcated and not needed atm.

##### key differences
|| Stack | Heap |
|--------------|:-----------:|:------------:|
| Memory is allocated | ...in a contiguous block |...in a random order|
| allocation / deallocation | by compiler | by programmer |
| cost | less | more |
| implementation | easy | hard |
| access time | faster | slower |
| issue | memory shortage | memory fragmentation |
| flexibility | fixed size | resizing is possible |
| Size | smaller than heap | larger than stack |

### Assembly 
#### stack operations
Stack operations are fundamental in assembly programming and used for data managing, functioncalls and memory allocation.

**Push**<br>
-> is used to add an element on the top of the stack<br>
If a value gets ```push```ed onto the stack, it becomes the new top element and all other elements are shifted down.<br>
```push ax ; Pushes the value of the AX register onto the stack```

**Pop**<br>
-> is used to remove the top element from the stack<br>
If a value gets ```pop```ed from the stack, the top element is removed and the next element becomes the new op element.<br>
```pop ax ; Pops the top value from the stack into the AX register```

**Peek**<br> 
-> is used to view the top element of the stack without removing it<br>
If the top value needs to be checked without altering the stack's contents ```peek``` is used.<br>
```mov ax, [esp] ; Copies the top value of the stack into the AX register without popping it```

**IsEmpty**<br> 
-> is used to check if the stack is empty<br>
If operations are performed such as ```pop``` or ```peek``` its essential to verify the stack's status first to avoid errors<br>
```cmp esp, ebp ; Compares the stack pointer (ESP) with the base pointer (EBP) to check if the stack is empty```

#### Registers

|||
|--------------|:-----------:|
| rax | 64 bit 'long' register |
| eax | 32 bit 'int' register |
| rdx | scratch register |
| rbx | preserved register |
| cqo |  converts rdx to rax |

# Glossar

*Translation Unit*<br>
-> The whole code file without any sort of preprocessor directives ('#xyz')

*Scratch register*<br>
-> Refers to a register that is used for temporary storage of data during computations or function calls

*Preserved register*<br>
-> A register that must maintain its value across function calls, meaning it must be saved and restored if used within a function

# References

![Nora Sandler write a compiler](https://norasandler.com/2017/11/29/Write-a-Compiler.html)

![lexer tutorial](https://stlab.cc/legacy/how-to-write-a-simple-lexical-analyzer-or-parser.html)

![c bible](https://web.archive.org/web/20200909074736if_/https://www.pdf-archive.com/2014/10/02/ansi-iso-9899-1990-1/ansi-iso-9899-1990-1.pdf)

![compiler explorer](https://godbolt.org/)

![assembly totorial](https://asmtutor.com/#lesson1)

![memory sheit](https://www.geeksforgeeks.org/stack-vs-heap-memory-allocation/)

![stack stuff](https://marketsplash.com/tutorials/assembly/assembly-stack-operations/)

![GG Simple Code Generator](https://www.geeksforgeeks.org/simple-code-generator/)

