INCLUDE ../globals.ink

-> main

=== main ===
Hey, hold up there, young man! Not just anyone can wander into this forest. Who are you?"
-> choose

=== choose ===
Introduce yourself.
    + [(I'm Alduin (scared)) ]
        -> alduin    
    + [(I'm here to save my family.)]
        -> family
    + [(None of your business.) ]
        -> arrogant


=== alduin ===
W-Who's there? I'm Alduin, please don't hurt me! 
I don't even know how I got here. Can you show me the way? I'm not here to cause harm, just trying to save my family."
-> alduin_answer

=== family ===
I'm here to rescue my family. You can either help me or face me. The choice is yours.
-> family_answer

=== arrogant ===
What's it to you, old man? I'm here for my own reasons.
-> arrogant_answer

=== family_answer ===
You've come to rescue your family? Many have tried for centuries, but none have succeeded. 
No one who enters ever returns. Still, we'll help you, brave soul.
    + [(Thank you!)) ]
        -> thanks   
    + [(Offer help.)]
        -> how_are_you
    + [(I don't need your help.) ]
        -> arrogant2

=== alduin_answer ===
Nice to meet you, Alduin. Your courage to come here is admirable.
-> family_answer

=== arrogant_answer ===
Watch your tone, disrespectful one! 
You won't pass through this forest without showing respect, or you might just become one with the trees. Threats won't serve you well here.
-> sorry

=== sorry ===
My apologies, please, please don't hurt me! 
I don't want to act like this—I'm just trying to save my parents. This place scares me, please forgive me.
-> family_answer

=== arrogant2 ===
Who are you to help me? I can handle myself. Just leave me be!
-> arrogant2_answer

=== arrogant2_answer===
Do as you wish, but beware: any trouble you cause here will come back to haunt you. Be on your way!
-> END

=== how_are_you ===
Thank you, but your help isn't necessary; I can handle this on my own. Is there something you need from me?
-> help

=== thanks===
Thank you so much for your help. I'll never forget this kindness.
-> help 

===help===
You're welcome, kind soul. Let me show you the way out. Follow the forest trail, and when you reach the end, avoid the left path. Continue straight ahead to find your guide. As for who it is... I can't reveal too much. But trust me, they're expecting you. I have faith in you.
-> END
