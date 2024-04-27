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
        -> ohthankyou
    + [(you are the greatest person ontheworld) ]
        -> greatpainter


=== ohiamsorry ===
i am sorry man! i didnt mean it 
~ curstate = "minigame"
-> END

=== ohthankyou ===
ooh ty man
-> END

=== greatpainter ===
ohhh. what a great painter are you
~ curstate = "minigame"
-> END

