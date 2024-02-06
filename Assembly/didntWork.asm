section .data

section .text
  global _start

_start:
  ;mov 2 to the variable a
  mov eax, 2
  mov dword [a], eax

  ;add 3 to the value a and store it in b
  mov eax, dword [a]
  add eax, 3
  mov dword [b], eax

  ;mov b value to eax for return
  mov eax, dword [b]

  ;exit with return value in eax
  mov ebx, 0
  mov eax, 1
  int 0x80

section .bss
  a resd 1
  b resd 1
