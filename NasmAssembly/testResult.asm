; Disassembly of file: test.o
; Mon Apr 15 09:38:36 2024
; Type: ELF64
; Syntax: NASM
; Instruction set: 8086, x64

default rel

global main: function

extern printf                                           ; near


SECTION .text   align=1 exec                            ; section number 1, code

main:   ; Function begin
        push    rbp                                     ; 0000 _ 55
        mov     rbp, rsp                                ; 0001 _ 48: 89. E5
        sub     rsp, 16                                 ; 0004 _ 48: 83. EC, 10
        mov     dword [rbp-0CH], 2                      ; 0008 _ C7. 45, F4, 00000002
        mov     dword [rbp-8H], 5                       ; 000F _ C7. 45, F8, 00000005
        mov     dword [rbp-4H], 21                      ; 0016 _ C7. 45, FC, 00000015
        lea     rax, [rel ?_001]                        ; 001D _ 48: 8D. 05, 00000000(rel)
        mov     rdi, rax                                ; 0024 _ 48: 89. C7
        mov     eax, 0                                  ; 0027 _ B8, 00000000
        call    printf                                  ; 002C _ E8, 00000000(PLT r)
        mov     eax, 0                                  ; 0031 _ B8, 00000000
        leave                                           ; 0036 _ C9
        ret                                             ; 0037 _ C3
; main End of function


SECTION .data   align=1 noexec                          ; section number 2, data


SECTION .bss    align=1 noexec                          ; section number 3, bss


SECTION .rodata align=1 noexec                          ; section number 4, const

?_001:                                                  ; byte
        db 48H, 65H, 6CH, 6CH, 6FH, 57H, 6FH, 72H       ; 0000 _ HelloWor
        db 6CH, 64H, 00H                                ; 0008 _ ld.


SECTION .note.gnu.property align=8 noexec               ; section number 5, const

        db 04H, 00H, 00H, 00H, 20H, 00H, 00H, 00H       ; 0000 _ .... ...
        db 05H, 00H, 00H, 00H, 47H, 4EH, 55H, 00H       ; 0008 _ ....GNU.
        db 02H, 00H, 01H, 0C0H, 04H, 00H, 00H, 00H      ; 0010 _ ........
        db 00H, 00H, 00H, 00H, 00H, 00H, 00H, 00H       ; 0018 _ ........
        db 01H, 00H, 01H, 0C0H, 04H, 00H, 00H, 00H      ; 0020 _ ........
        db 01H, 00H, 00H, 00H, 00H, 00H, 00H, 00H       ; 0028 _ ........


