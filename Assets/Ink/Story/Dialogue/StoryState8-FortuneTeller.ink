INCLUDE ../globals.ink

-> main

=== main ===
Greetings, handsome young one! I am the fortune teller of these lands, the one who knows all that can be known. 
I have gazed into your future, and your path is entwined with mysteries yet to be unraveled.
-> choose

=== choose ===
What truths do you wish to uncover? Ask your question.
    + [(Refuse.) ]
        -> no    
    + [(Ask about family.)]
        -> aile_question
    + [(Ask about civilization.]
        -> medeniyet_question


=== no ===
I do not believe in the visions of fortune teller. I choose not to ask you any questions.
-> END

=== aile_question ===
Tell me about my family—where are they? What fate has befallen them?
~ curstate = "aile"
-> aile_answer

=== medeniyet_question ===
What can you tell me about the ancient civilization of this land? I'm wondering, is it possible to save this civilization?
~ curstate = "medeniyet"
-> medeniyet_answer

=== aile_answer ===
Do you seek the truth about your parents, or would you rather uncover the story of the grandfather who set you on this path?
    + [(Ask about parents.) ]
        -> annebaba_question   
    + [(Ask about grandfather.)]
        -> dede_question
    + [(Doesn't matter.) ]
        -> dede_question

=== medeniyet_answer ===
This question can only be answered by the master of the labyrinth. I'm afraid this is beyond my knowledge—my apologies.
-> choose

=== dede_question ===
What happened to my grandfather?
-> dede_answer

=== annebaba_question ===
What happened to my parents?
-> annebaba_answer   

=== dede_answer ===
Your grandfather's story is a tale of tragedy and mystery. He vanished without a trace, his fate entangled with the secrets of the labyrinth. 
His knowledge of the lost civilization haunted him, leading him down a dangerous path from which he never returned.
-> choose

=== annebaba_answer ===
Your parents vanished after a tragic car accident, and their bodies were never found. 
Your grandfather believed their disappearance was linked to the mysteries of the labyrinth, but he couldn't convince others of his suspicions.
-> choose 

