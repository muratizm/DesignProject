INCLUDE ../globals.ink

-> main

=== main ===
Hey, welcome to the Realm of Colors! Come, help me out, Alduin. #voice:voice22
    + [(The painter needs help; accept to assist.)]
        -> helpful_adventurer
    + [(Refuse to offer help.) ]
        -> bad
    + [(Ask why you should help.) ]
        -> sly

=== helpful_adventurer ===
Absolutely, painter! I'm here to help you out! #voice:voice23
~ curstate = "minigame"
-> painter_ty

=== bad ===
Looks like you’re on your own this time! #voice:voice24
-> youarebad

=== sly ===
Painter, what do I stand to gain if I help you? #voice:voice25
-> agift

=== painter_ty ===
Thank you so much! I'm grateful for your help! #voice:voice26
-> END

=== youarebad ===
You're not who I thought you were. You missed your chance. #voice:voice27
    + [(thinkful)]
        -> ohiamsorry
    + [(sorry) ]
        -> ohiamsorry
    + [(missed a chance??) ]
        -> sly


=== agift ===
If you agree to help me, I'll give you a surprise gift. #voice:voice28
    + [(Refuse!)]
        -> bad
    + [(Give thanks and continue.) ]
        -> ohthankyou
    + [(Accept the task and praise the painter.) ]
        -> greatpainter


=== ohiamsorry ===
No thanks, you can keep your gift! #voice:voice29
~ curstate = "minigame"
-> END

=== ohthankyou ===
Oh, thank you! But I can't waste time on that. #voice:voice30
-> END

=== greatpainter ===
You're amazing, and I'm glad I found you for this task! #voice:voice31
~ curstate = "minigame"
-> END


