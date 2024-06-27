SECTION .data
newline db 0xA ;newline character for formatting output
anotherprint db 'another print'
helloworld db 'HelloWorld'


SECTION .text
global _start

_start:
;init variables
mov dword [a], 6
mov dword [b], 5
mov dword [c], 21
;print
mov eax, 4 
mov ebx, 1
mov ecx, anotherprint
mov edx, 13 
int 0x80

;print newline character for formatting 
 mov eax, 4 
 mov ebx, 1 
 mov ecx, newline 
 mov edx, 1 
 int 0x80 

;calc
mov eax, 3
add eax, mov eax, dword[a]
sub eax, mov eax, dword[b]

imul eax, mov eax, 3


;print
mov eax, 4 
mov ebx, 1
mov ecx, helloworld
mov edx, 10 
int 0x80

;print newline character for formatting 
 mov eax, 4 
 mov ebx, 1 
 mov ecx, newline 
 mov edx, 1 
 int 0x80 


;exit
mov eax, 1 ;set eax register to 1 (sys_exit)
xor ebx, ebx
int 0x80 ;trigger system-call in eax

SECTION .bss
a resd 1
b resd 1
c resd 1