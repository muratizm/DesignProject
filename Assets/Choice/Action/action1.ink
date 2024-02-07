INCLUDE ../globals.ink

-> main

=== main ===
gizemli bir mavi nesne ile karşılaştın, seçimini yap!
-> choices

=== choices ===
* [Yok ET ! (x) ]  -> chosen ("yokettik")
* [Yavasca acmayi dene (c)] -> chosen("actik")
* [Osur! (v)] -> chosen("osurduk")

=== chosen(option) ===
 ~ curstate = option
You choosed "{option}"!  #end3
-> END