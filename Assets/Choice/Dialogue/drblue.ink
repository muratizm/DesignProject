INCLUDE ../globals.ink

-> main

=== main ===
hi how are you? #speaker:dr blue #layout:right
-> choices

=== choices ===
+[good ty] -> good
+[bad] -> bad
+[sorry. i couldn't hear you.] -> repeat

==good==
oh, i am great. Thanks for asking! #speaker:you #layout:left
-> DONE
==bad==
not good. #speaker:you #layout:left
and. dont ask me why! 
if you dare. i will kill you
-> DONE 
==repeat==
oh. you didnt hear me? i said: -> main

