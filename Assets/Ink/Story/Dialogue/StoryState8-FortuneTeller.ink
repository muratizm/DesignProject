INCLUDE ../globals.ink

-> main

=== main ===
Greetings, handsome young one! #voice:voice84
I am the fortune teller of these lands, the one who knows all that can be known. #voice:voice85
I have gazed into your future, and your path is entwined with mysteries yet to be unraveled. #voice:voice86
-> choose

=== choose ===
What truths do you wish to uncover? Ask your question. #voice:voice87
    + [(Refuse.) ]
        -> no    
    + [(Ask about family.)]
        -> aile_question
    + [(Ask about civilization.]
        -> medeniyet_question


=== no ===
I do not believe in the visions of fortune teller. I choose not to ask you any questions. #voice:voice88
-> END

=== aile_question ===
Tell me about my family—where are they? What fate has befallen them? #voice:voice89
~ curstate = "aile"
-> aile_answer

=== medeniyet_question ===
What can you tell me about the ancient civilization of this land? #voice:voice90
I'm wondering, is it possible to save this civilization? #voice:voice91
~ curstate = "medeniyet"
-> medeniyet_answer

=== aile_answer ===
Do you seek the truth about your parents, or would you rather uncover the story of the grandfather who set you on this path? #voice:voice92
    + [(Ask about parents.) ]
        -> annebaba_question   
    + [(Ask about grandfather.)]
        -> dede_question
    + [(Doesn't matter.) ]
        -> dede_question

=== medeniyet_answer ===
This question can only be answered by the master of the labyrinth. #voice:voice93
I'm afraid this is beyond my knowledge—my apologies. #voice:voice94
-> choose

=== dede_question ===
What happened to my grandfather? #voice:voice95
-> dede_answer

=== annebaba_question ===
What happened to my parents? #voice:voice96
-> annebaba_answer   

=== dede_answer ===
Your grandfather's story is a tale of tragedy and mystery. #voice:voice97
He vanished without a trace, his fate entangled with the secrets of the labyrinth. #voice:voice98
His knowledge of the lost civilization haunted him, leading him down a dangerous path from which he never returned. #voice:voice99
-> choose

=== annebaba_answer ===
Your parents vanished after a tragic car accident, and their bodies were never found. #voice:voice100 
Your grandfather believed their disappearance was linked to the mysteries of the labyrinth, but he couldn't convince others of his suspicions. #voice:voice101
-> choose 

