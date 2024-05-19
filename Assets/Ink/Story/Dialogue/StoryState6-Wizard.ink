INCLUDE ../globals.ink

-> main

=== main ===
Greetings, young advennturer. I am the oldest wizard in this land. #voice:voice62
I've seen countless adventures come and go, and I sense you are on a journey of great importance. #voice:voice63
Many moons have passed since I first set foot in this realm, and I've witnessed its many transformations. #voice:voice64
-> choose

=== choose ===
(But enough about me—how are you faring?) #voice:voice65
    + [(Well!) ]
        -> good    
    + [(Tough!)]
        -> bad
    + [(Frightening!) ]
        -> coward


=== good ===
I'm doing well, thank you for asking! The journey has been good to me so far. #voice:voice66
-> wizard_answer1

=== bad ===
It's been tough, wizard. I've faced so many trials already, and I'm struggling with the weight of them all. #voice:voice67
~ curstate = "aile"
-> wizard_answer2

=== coward ===
I must admit, there have been moments of great fear along the way. Please, get me out of here! #voice:voice68
~ curstate = "medeniyet"
-> wizard_answer2

=== wizard_answer1 ===
My, what a fearless adventurer you are! #voice:voice69
It's been centuries since this realm has seen someone as bold and daring as you. #voice:voice70
Your courage is truly remarkable! #voice:voice71
    + [(Much appreciated!) ]
        -> ty   
    + [(Ask the wizard how he feels.)]
        -> you
    + [(Request aid.) ]
        -> help

=== wizard_answer2 ===
That saddens me to hear. Is there anything I can do to help you? #voice:voice72
    + [(Show gratitude.) ]
        -> grateful   
    + [(Ask for aid.)]
        -> help
    + [(Plead for assistance.) ]
        -> beg



=== ty ===
Your generosity is greatly appreciated! Thank you for your kindness and support on my journey! #voice:voice73
-> wizard_help

=== you ===
Thanks, friend. How are you faring in this strange place? #voice:voice74
The air here feels thick with mystery and danger. #voice:voice75
-> how_is_wizard

=== help ===
Can you offer me guidance? I'm trying to navigate my way. #voice:voice76
-> wizard_help



=== grateful ===
May your kindness be rewarded, my friend! It would be most helpful, and I would be truly grateful!  #voice:voice77
-> wizard_help

=== how_is_wizard ===
Me? It's a rarity for anyone to ask about my well-being. #voice:voice78
Your character is a marvel—compassionate and courageous. For that, I will offer you my guidance. #voice:voice79
-> wizard_help

=== beg ===
Please, I beg you! I've reached my limit—I can't go on without your help! #voice:voice80
-> wizard_help



=== wizard_help ===
I'd be happy to help you, young one. #voice:voice81
I'm giving you this item—you're very close to finding your way out of the labyrinth, and this will bring you even closer.  #voice:voice82
I trust you; may your path be clear! #voice:voice83
~ curstate = "give_item"
-> END








