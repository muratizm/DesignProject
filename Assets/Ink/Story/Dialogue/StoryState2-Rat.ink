INCLUDE ../globals.ink

Hello, little mouse! Could you guide me on my journey? I'd appreciate your help. #voice:voice17
-> main

=== main ===
Give me some gold, and I'll gladly guide you on your way. #voice:voice18
    + [(Give the gold)]
        -> gold
    + [(Trade the crystal) ]
        -> crystal
    + [(Get mad at the mouse) ]
        -> angry

=== gold ===
Here, I've brought you a golden leaf. #voice:voice19
~ curstate = "gave_gold"
-> END

=== crystal ===
I don't have gold, but I do have a beautiful crystal. It's yours if you guide me. #voice:voice20
~ curstate = "gave_crystal"
-> END

=== angry ===
That's not fair! I'm not giving you anything. #voice:voice21
-> END

