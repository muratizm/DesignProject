INCLUDE ../globals.ink

-> main

=== main ===
Hey, welcome to the Realm of Colors! Come, help me out, Alduin. 
    + [(The painter needs help; accept to assist.)]
        -> helpful_adventurer
    + [(Refuse to offer help.) ]
        -> bad
    + [(Ask why you should help.) ]
        -> sly

=== helpful_adventurer ===
Absolutely, painter! I'm here to help you out!
~ curstate = "minigame"
-> painter_ty

=== bad ===
Looks like you’re on your own this time!
-> youarebad

=== sly ===
Painter, what do I stand to gain if I help you?
-> agift

=== painter_ty ===
Thank you so much! I'm grateful for your help!
-> END

=== youarebad ===
You're not who I thought you were. You missed your chance.
    + [(thinkful)]
        -> ohiamsorry
    + [(sorry) ]
        -> ohiamsorry
    + [(missed a chance??) ]
        -> sly


=== agift ===
If you agree to help me, I'll give you a surprise gift.
    + [(Refuse!)]
        -> bad
    + [(Give thanks and continue.) ]
        -> ohthankyou
    + [(Accept the task and praise the painter.) ]
        -> greatpainter


=== ohiamsorry ===
No thanks, you can keep your gift!
~ curstate = "minigame"
-> END

=== ohthankyou ===
Oh, thank you! But I can't waste time on that.
-> END

=== greatpainter ===
You're amazing, and I'm glad I found you for this task!
~ curstate = "minigame"
-> END


