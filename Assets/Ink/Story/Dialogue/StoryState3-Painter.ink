INCLUDE ../globals.ink

-> main

=== main ===
can you help me alduin?
    + [(yes)]
        -> helpful_adventurer
    + [(no boomer) ]
        -> bad
    + [(for what) ]
        -> sly

=== helpful_adventurer ===
of courseee
~ curstate = "minigame"
-> painter_ty

=== bad ===
no boomerrr
-> youarebad

=== sly ===
what will you give me
-> agift

=== painter_ty ===
thank you boy ty.
-> END

=== youarebad ===
youare a bad person. you missed the chance.
    + [(thinkful)]
        -> ohiamsorry
    + [(sorry) ]
        -> ohiamsorry
    + [(missed a chance??) ]
        -> sly


=== agift ===
a surprise gift!!
    + [(no boomerrr)]
        -> bad
    + [(oh thank youu) ]
        -> ohiamsorry
    + [(you are the greatest person ontheworld) ]
        -> greatpainter


=== ohiamsorry ===
thank you very much men of course i can help you 
~ curstate = "minigame"
-> END

=== greatpainter ===
ohhh. what a great painter are you
~ curstate = "minigame"
-> END

