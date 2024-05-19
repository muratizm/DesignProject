INCLUDE ../globals.ink

-> main

=== main ===
Welcome, stranger. Who are you, and what brings you here? #voice:voice128
-> alduin1

=== alduin1 ===
I'm Alduin. May I ask who you are, sir? Can you help me? #voice:voice129
->lord1

===lord1===
I am Abe, the master of this labyrinth. #voice:voice130
It's been centuries since anyone has ventured here. What is it that you seek from me? #voice:voice131
-> ask_to_lord


===ask_to_lord===
Ask me your questions, and I shall provide answers. #voice:voice132
    + [(Ask exit.) ]
        -> exit    
    + [(Ask wish to know.)]
        -> learnAnswer
    + [(No question.) ]
        -> no

===learnAnswer===
What knowledge do you seek? #voice:voice133
    + [(Tell me about the tree at the start.) ]
        -> tree    
    + [(I want to know about my parents.)]
        -> aileQuestion
    + [(Tell me about the lost civilization.) ]
        -> medeniyetQuestion

===exit===
Please, show me the way out. #voice:voice134
I've traveled far and faced many dangers to reach this point. #voice:voice135
My journey depends on finding the path to freedom. #voice:voice136
->exit_answer

===exit_answer===

Follow the path ahead. Turn left when you reach the clearing. #voice:voice137
Run bravely, and you will find your way out. Good luck. #voice:voice138

->END

===no===
I must take my leave now. My journey calls, and there's no time to waste. #voice:voice139
->END

=== tree ===
Ah, the tree you speak of was once mine. It stood proudly as my own, but it dared to oppose me. #voice:voice140
It rebelled against my plans for growing a certain plant that I desired, so I cast it out of the civilization.  #voice:voice141
Now it remains a solitary sentinel at the entrance, bearing the mark of its rebellion. #voice:voice142
-> ask_to_lord

=== aileQuestion ===
What happened to my parents? #voice:voice143
-> aileAnswer

=== medeniyetQuestion ===
What became of the lost civilization? Can it be saved? #voice:voice144
-> medeniyetAnswer

=== aileAnswer ===
Do you seek the truth about your parents, or would you rather uncover the story of the grandfather who set you on this path? #voice:voice145
    + [(Ask about parents.) ]
        -> annebabaQuestion   
    + [(Ask about grandfather.)]
        -> dedeQuestion
    + [(Doesn't matter.) ]
        -> dedeQuestion

=== medeniyetAnswer ===
Ah, the fate of the civilization weighs heavily on me. #voice:voice146
I let my people grow a plant that promised them great wealth but caused untold harm to humanity. #voice:voice147
In their quest for selfish gain, they fled and vanished into the labyrinth, taking their secrets with them. #voice:voice148
Yet there may still be hope. #voice:voice149
Your grandfather's research uncovered the perilous plant responsible for our downfall. #voice:voice150
If it can be tracked down and eradicated wherever it grows, perhaps the civilization can be saved. #voice:voice151
Alduin, you've proven yourself by reaching me here. #voice:voice152
I trust you now, and I believe you are the one who can complete this mission.#voice:voice153
-> ask_to_lord

=== dedeQuestion ===
What happened to my grandfather? #voice:voice154
-> dedeAnswer

=== annebabaQuestion ===
What happened to my parents? #voice:voice155
-> annebabaAnswer   

=== dedeAnswer ===
Your grandfather's story is a tale of tragedy and mystery. #voice:voice156
He vanished without a trace, his fate entangled with the secrets of the labyrinth. #voice:voice157
His knowledge of the lost civilization haunted him, leading him down a dangerous path from which he never returned. #voice:voice158
-> ask_to_lord

=== annebabaAnswer ===
Your parents vanished after a tragic car accident, and their bodies were never found. #voice:voice159
Your grandfather believed their disappearance was linked to the mysteries of the labyrinth, but he couldn't convince others of his suspicions. #voice:voice160
-> ask_to_lord 


