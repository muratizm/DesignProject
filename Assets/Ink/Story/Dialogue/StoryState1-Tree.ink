INCLUDE ../globals.ink

-> main

=== main ===
So, you are our new adventurer tasked with solving the secret of our lost civilization. #voice:voice1
Embarking on this adventure requires great courage. Welcome to our civilization, Alduin! #voice:voice2
I hope that by completing this adventure, you not only save our civilization but also reunite with your parents. #voice:voice3
    + [Ready for the adventure! ]
        -> adventure    
    + [Where are my parents? (Attack!)]
        -> parents
    + [Request exit. ]
        -> coward

=== parents ===
What did you mean by reuniting me with my parents? Where are they?
Answer to me or I am going to kill you!
-> parentsAnswer

=== adventure ===
Ready for the adventure! How do I begin this journey?
-> adventureAnswer

=== coward ===
I want to leave this place right away. Let me go back now!
-> cowardAnswer

===parentsAnswer===
Kill me? hahaha. Here, attack this!
~ curstate = "attack_to_tree"
-> END

===adventureAnswer===
Here, young man, I am clearing the way for you. 
If you need anything, you can come to me. 
But be careful, when you try to come back, the labyrinth may give unexpected reactions.
~ curstate = "respect_to_tree"
-> END

===cowardAnswer===
Don't be afraid, young man, don't be afraid.
If you are afraid, you cannot fulfill your potential.
You are currently holding within you the energy brought by all your ancestors. I believe you will pass this difficult journey.
I give you a gold leaf to help you.
I hope it works for you. Good luck!
~ curstate = "scared_of_adventure"
-> END