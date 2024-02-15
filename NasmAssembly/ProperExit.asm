; .data create variables
SECTION .data
msg db  'Hello World!', 0Ah ; assign msg variable with string + Line Feed Character (0Ah)

; .text contains label with entry point
SECTION .text 
global _start

_start:
  mov edx, 13   ; number of bytes to write -> one per letter plus LF
  mov ecx, msg  ; move memory address of message string into ecx
  mov ebx, 1    ; write to the STDOUT file
  mov eax, 4    ; invoke SYS_WRITE -> output to console
  int 80h       

  mov ebx, 0    ; return 0 status
  mov eax, 1    ; invoke SYS_Exit 
  int 80h
