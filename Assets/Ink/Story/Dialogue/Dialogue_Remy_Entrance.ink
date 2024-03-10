INCLUDE ../globals.ink

-> main

=== main ===
Welcome to our civilization!
So, you are our new adventurer assigned to solve the secret of our lost civilization.
It takes great courage to go on this adventure. I hope that by completing this adventure you will be able to save our civilization and be reunited with your parents.
    + [(Attaaaacck!!)]
        -> stupidlybrave
    + [(Curious and Brave) ]
        -> adventurerbrave
    + [(Coward) ]
        -> coward

=== stupidlybrave ===
Sen ne diyon uşağım kelleni alirum ha! 
-> END

=== adventurerbrave ===
Bu maceraya nerden başlayabilirim ey yüce Remy!
~ curstate = "attack_to_tree"
-> END

=== coward ===
Ben burayı sevmedim beni evime götürün
-> END

