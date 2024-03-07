INCLUDE ../globals.ink

-> choices

=== choices ===
Medeniyetimize hoş geldin
* [Yok ET ! (x) ]  -> chosen ("yokettik")
* [Yavasca acmayi dene (c)] -> chosen("actik")
* [Osur! (v)] -> chosen("osurduk")

=== chosen(option) ===
 ~ curstate = option
You choosed "{option}"!  #end3
-> END
