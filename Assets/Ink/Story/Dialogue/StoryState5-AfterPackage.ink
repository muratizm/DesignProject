INCLUDE ../globals.ink

-> main

=== main ===
aah, işte beklediğim paket
çok teşekkürler dost
sana biraz yol göstermek isterim delikanlı bi adama benzyosun
yeni totemin keşfedilmesi için farklı farklı yollar var 
-> choose

=== choose ===
sana hangisinden bahsetmemi istersin
    + [(a yolu) ]
        -> a    
    + [(b yolu)]
        -> b
    + [(c yolu) ]
        -> c


=== a ===
a yolu böyle böyledir
bunu istediğine emin misin
    + [(evet eminim) ]
        -> aChosen
    + [(hayır)]
        -> choose
    + [(diger) ]
        -> choose

=== b ===
b yolu böyle böyledir
bunu istediğine emin misin
    + [(evet eminim) ]
        -> bChosen
    + [(hayır)]
        -> choose
    + [(diger) ]
        -> choose

=== c ===
c yolu böyle böyledir
bunu istediğine emin misin
    + [(evet eminim) ]
        -> cChosen
    + [(hayır)]
        -> choose
    + [(diger) ]
        -> choose


=== aChosen ===
tamamdir maceracı, görevin a bu budur.
sana bol şans.
~ curstate = "path_with_magicians"
-> END

=== bChosen ===
tamamdir maceracı, görevin b şu şudur.
sana bol şans.
~ curstate = "path_with_bats"
-> END

=== cChosen===
tamamdir maceracı, görevin c du dudur.
sana bol şans.
~ curstate = "path_slow_safe"
-> END

