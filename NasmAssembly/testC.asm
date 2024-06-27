SECTION .data
msg db 'HelloWorld!', 0Ah ;assign msg variable with string + Line Feed Character (0Ah)
newline db 0xA ;newline character for formatting output

SECTION .text 
global _start

_start:
;init variables
    mov dword [a], 6
    mov dword [b], 5 ; 2 + 3
    mov dword [c], 21 ; 3 + (8 - 2) * 3

    mov eax, dword [a]
    sub eax, dword [b]
    imul eax, 3
    add eax, 3
    mov dword [d], eax

;print "HelloWorld"
    mov eax, 4 ;set eax register to 4 (sys_write)
    mov ebx, 1 ;indicating to write the data to the standard output (usually console)
    mov ecx, msg
    mov edx, 10 ;len of HelloWorld
    int 0x80 ;trigger system-call in eax

;print newline character for formatting
    mov eax, 4 ;set eax register to 4 (sys_write)
    mov ebx, 1 ;indicating to write the data to the standard output (usually console)
    mov ecx, newline
    mov edx, 1 ;len of newline character
    int 0x80 ;trigger system-call in eax

;exit
    ;on linux 1 in eax indicates upcoming invocation of exit system-call
    ;xor ebx, ebx -> xor with itself to clear out the ebx register ensuring exit system-call with 0
    mov eax, 1 ;set eax register to 1 (sys_exit)
    xor ebx, ebx
    int 0x80 ;trigger system-call in eax

;variable sheit
    ;.bss typically used for variables that need memory allocation but dont require initialization
    ;resd stands for reserve double-word and allocates specific number of double-words(32 bits or 4 bytes) of memory
    ;The 1 after resd specifies how many double-words of memory to reserve for each variable so each variable (a, b, c, d) will occupy 4 bytes of memory
section .bss 
    a resd 1 
    b resd 1
    c resd 1
    d resd 1
