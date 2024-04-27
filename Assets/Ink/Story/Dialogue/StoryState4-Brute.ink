INCLUDE ../globals.ink

-> main

=== main ===
meraba yolcu,
benim balta körelmiş de bu baltayı nasıl düzeltiriz be.
şu ağacı kesemedim bi türlü
    + [(baltanı ver) ]
        -> help    
    + [(ağaci koru)]
        -> protect_tree
    + [(umursama) ]
        -> donotcare

=== help ===
al buyur kardesim benim baltami kullan
~ curstate = "give_axe"
-> END

=== protect_tree ===
bu agaci sana kestirmem ihtiyar. biz dogayi koruruz
~ curstate = "protect_tree"
-> END

=== donotcare ===
ben bilmem kardesim naparsan yap
~ curstate = "donotcare"
-> END

