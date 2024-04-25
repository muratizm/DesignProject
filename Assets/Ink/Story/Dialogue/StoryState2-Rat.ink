INCLUDE ../globals.ink

-> main

=== main ===
RAT dialogue
    + [(Attaaaacck!!)]
        -> stupidlybrave
    + [(Curious and Brave) ]
        -> adventurerbrave
    + [(Coward) ]
        -> coward

=== stupidlybrave ===
Sen ne diyon uşağım kelleni alirum ha! 
~ curstate = "attack_to_tree"
-> END

=== adventurerbrave ===
Bu maceraya nerden başlayabilirim ey yüce Remy!
~ curstate = "respect_to_tree"
-> END

=== coward ===
Ben burayı sevmedim beni evime götürün
~ curstate = "scared_of_adventure"
-> END

