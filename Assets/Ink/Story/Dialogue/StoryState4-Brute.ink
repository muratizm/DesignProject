INCLUDE ../globals.ink

-> main

=== main ===
Greetings, adventurer.
Exhaustion weighs me down, and my axe has lost its edge. This stubborn tree won't fall.
    + [(Give the man your axe.) ]
        -> help    
    + [(Protect the tree.)]
        -> protect_tree
    + [(Ignore him.) ]
        -> donotcare

=== help ===
Take my axe—it's sharp enough to fell the tree.
~ curstate = "give_axe"
-> END

=== protect_tree ===
This tree won't fall. I'll stand guard and make sure you don't harm it.
~ curstate = "protect_tree"
-> END

=== donotcare ===
I don't care what you do; I need to keep going.
~ curstate = "donotcare"
-> END

