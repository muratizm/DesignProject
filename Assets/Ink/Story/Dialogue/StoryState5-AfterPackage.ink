INCLUDE ../globals.ink

-> main

=== main ===
Ah, there it is, the long-awaited package! Thank you, Alduin. #voice:voice46
Let me offer you some guidance on your journey. There are many ways to uncover the new totem's secrets. #voice:voice47
-> choose

=== choose ===
Which path would you like me to reveal? #voice:voice48
    + [(Unknown path) ]
        -> a    
    + [(Fast but risky path)]
        -> b
    + [(Safe but long path) ]
        -> c


=== a ===
The wizard path is unpredictable, nobody knowes what are there. Are you sure you want this? #voice:voice49
    + [(Yes, I'm sure.) ]
        -> aChosen
    + [(No, I don't want this.)]
        -> choose
    + [(Let's ask the AI.) ]
        -> choose

=== b ===
Are you prepared to face the treacherous path that lies ahead, fraught with danger and uncertainty? #voice:voice50
This path could be extremely dangerous. #voice:voice51

    + [(Yes, I'm sure.) ]
        -> bChosen
    + [(No, I don't want this.)]
        -> choose
    + [(Let's ask the AI.) ]
        -> choose

=== c ===
This route is said to be safe, but extremely long. #voice:voice52
Many of our friends have entered this path and never returned for years. #voice:voice53
Are you sure you want to take this long road just to avoid risks? #voice:voice54
    + [(Yes, I'm sure.) ]
        -> cChosen
    + [(No, I don't want this.)]
        -> choose
    + [(Let's ask the AI.) ]
        -> choose


=== aChosen ===
Very well, adventurer, your task is to take the unkown path. Good luck to you. #voice:voice55
~ curstate = "a"
-> END

=== bChosen ===
Alright, adventurer, your mission is to follow the parkour route, dark and very dangerous. Best of luck. #voice:voice56
~ curstate = "b"
-> END

=== cChosen===
Okay, adventurer, your journey is to take the safe path, a slow but loooong route without conflict. #voice:voice57
Wishing you all the best. #voice:voice58
~ curstate = "c"
-> END

