INCLUDE ../globals.ink

-> main

=== main ===
Ah, there it is, the long-awaited package! Thank you, Alduin. 
Let me offer you some guidance on your journey. There are many ways to uncover the new totem's secrets.
-> choose

=== choose ===
Which path would you like me to reveal?
    + [(Unknown path) ]
        -> a    
    + [(Fast but risky path)]
        -> b
    + [(Safe but long path) ]
        -> c


=== a ===
The wizard path is unpredictable, nobody knowes what are there. Are you sure you want this?
    + [(Yes, I'm sure.) ]
        -> aChosen
    + [(No, I don't want this.)]
        -> choose
    + [(Let's ask the AI.) ]
        -> choose

=== b ===
bu rota çok tehlikeli işte çok zorlu bir parkur oldugu söyleniyor falan
    + [(Yes, I'm sure.) ]
        -> bChosen
    + [(No, I don't want this.)]
        -> choose
    + [(Let's ask the AI.) ]
        -> choose

=== c ===
bu rotanın güvenli oldugu fakat çook uzun oldugu söyleniyor
o rotaya girip yıllarca gelmeyen dostlarımız oldu
sırf risk almamak içinbu uzunyolu seçmek istediğine emin misin
    + [(Yes, I'm sure.) ]
        -> cChosen
    + [(No, I don't want this.)]
        -> choose
    + [(Let's ask the AI.) ]
        -> choose


=== aChosen ===
Very well, adventurer, your task is to take the unkown path. Good luck to you.
~ curstate = "a"
-> END

=== bChosen ===
Alright, adventurer, your mission is to follow the parkour route, dark and very dangerous. Best of luck.
~ curstate = "b"
-> END

=== cChosen===
Okay, adventurer, your journey is to take the safe path, a slow but loooong route without conflict. Wishing you all the best.
~ curstate = "c"
-> END

