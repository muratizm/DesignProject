INCLUDE ../globals.ink

-> main

=== main ===
Fareden görevini aldın.
-> choices

=== choices ===
* [Yoluna git (-) ]  -> chosen ("go away")
* [Fareyi öldür (x)] -> chosen("kill")
* [Soru Sor (e)] -> chosen("Dialogue With Rat")

=== chosen(option) ===
 ~ storystate2_rat = option
You choosed {option}!  #end3
-> END