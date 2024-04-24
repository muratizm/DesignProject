INCLUDE ../globals.ink

-> main

=== main ===
Welcome to our civilization!
So, you are our new adventurer assigned to solve the secret of our lost civilization.
I hope that by completing this adventure you will be able to save our civilization and be reunitd with your parents.
    + [(Ready for adventure) ]
        -> adventure    
    + [(My parents?)]
        -> parents
    + [(Coward) ]
        -> coward

=== parents ===
What do you mean by reuniting my parent? Where are they!
~ curstate = "attack_to_tree"
-> END

=== adventure ===
Dear Tree, Where can I start this adventure?
~ curstate = "respect_to_tree"
-> END

=== coward ===
I didn't like this place. Please let me free please.
~ curstate = "scared_of_adventure"
-> END

