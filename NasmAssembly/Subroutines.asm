; .data create variables
SECTION .data
msg db  'Hello, brave new World!', 0Ah ; assign msg variable with string + Line Feed Character (0Ah)

; .text contains label with entry point
SECTION .text 
global _start

_start: 
  mov  eax, msg       ; move the address of our message string into EAX
  call strlen         ; call our function to calculate the length of the string
 
  mov edx, eax        ; our function leaves the result in EAX
  mov ecx, msg        ; this is all the same as before
  mov ebx, 1
  mov eax, 4
  int 80h
 
  mov ebx, 0
  mov eax, 1
  int 80h
 
strlen:                ; this is our first function declaration
  push ebx             ; push the value in EBX onto the stack to preserve it while we use EBX in this function
  mov  ebx, eax        ; move the address in EAX into EBX (Both point to the same segment in memory)
 
nextchar:
  cmp byte [eax], 0
  jz  finished
  inc eax
  jmp nextchar
 
finished:
  sub eax, ebx
  pop ebx             ; pop the value on the stack back into EBX
  ret                 ; return to where the function was called
