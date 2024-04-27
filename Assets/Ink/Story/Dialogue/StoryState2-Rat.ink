INCLUDE ../globals.ink

Hey fare dost! Bana bir yol göster, biraz yardımcı ol
-> main

=== main ===
eğer altın verirsen yardım ederim
    + [(give gold!!)]
        -> gold
    + [(give crystal!!) ]
        -> crystal
    + [(kız) ]
        -> angry

=== gold ===
Al buyur sana altın yaprak getirdim
~ curstate = "gave_gold"
-> END

=== crystal ===
al buyur sana crystal getirdim
~ curstate = "gave_crystal"
-> END

=== angry ===
sana ne para vericem lan köylü
-> END

