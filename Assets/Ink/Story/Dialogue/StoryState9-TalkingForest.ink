INCLUDE ../globals.ink

-> main

=== main ===
Hey, hold up there, young man! #voice:voice102
Not just anyone can wander into this forest. Who are you? #voice:voice103
-> choose

=== choose ===
Introduce yourself. #voice:voice104
    + [(I'm Alduin (scared)) ]
        -> alduin    
    + [(I'm here to save my family.)]
        -> family
    + [(None of your business.) ]
        -> arrogant


=== alduin ===
W-Who's there? I'm Alduin, please don't hurt me! #voice:voice105
I don't even know how I got here. Can you show me the way? #voice:voice106
I'm not here to cause harm, just trying to save my family. #voice:voice107
-> alduin_answer

=== family ===
I'm here to rescue my family. You can either help me or face me. The choice is yours. #voice:voice108
-> family_answer

=== arrogant ===
What's it to you, old man? I'm here for my own reasons. #voice:voice109
-> arrogant_answer

=== family_answer ===
You've come to rescue your family? #voice:voice110
Many have tried for centuries, but none have succeeded. #voice:voice111
No one who enters ever returns. Still, we'll help you, brave soul. #voice:voice112
    + [(Thank you!)) ]
        -> thanks   
    + [(Offer help.)]
        -> how_are_you
    + [(I don't need your help.) ]
        -> arrogant2

=== alduin_answer ===
Nice to meet you, Alduin. Your courage to come here is admirable. #voice:voice113
-> family_answer

=== arrogant_answer ===
Watch your tone, disrespectful one! #voice:voice114
You won't pass through this forest without showing respect, or you might just become one with the trees. #voice:voice115
Threats won't serve you well here. #voice:voice116
-> sorry

=== sorry ===
My apologies, please, please don't hurt me! #voice:voice117
I don't want to act like this—I'm just trying to save my parents. #voice:voice118
This place scares me, please forgive me. #voice:voice119
-> family_answer

=== arrogant2 ===
Who are you to help me? I can handle myself. Just leave me be! #voice:voice120
-> arrogant2_answer

=== arrogant2_answer===
Do as you wish, but beware: any trouble you cause here will come back to haunt you. Be on your way! #voice:voice121
-> END

=== how_are_you ===
Thank you, but your help isn't necessary; I can handle this on my own. Is there something you need from me? #voice:voice122
-> help

=== thanks===
Thank you so much for your help. I'll never forget this kindness. #voice:voice123
-> help 

===help===
You're welcome, kind soul. Let me show you the way out. #voice:voice124
Follow the forest trail, and when you reach the end, avoid the left path. #voice:voice125
Continue straight ahead to find your guide. As for who it is... #voice:voice126
I can't reveal too much. But trust me, they're expecting you. I have faith in you. #voice:voice127
-> END
