INCLUDE ../globals.ink

-> choices

=== choices ===
Medeniyetimize hoş geldin
* [A ! (x) ]  -> chosen ("yokettik")
* [B (c)] -> chosen("actik")
* [C (v)] -> chosen("osurduk")

=== chosen(option) ===
 ~ curstate = option
You choosed '{option}'!  #end3
-> END
