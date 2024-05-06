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
bilinmeyen yol
sana bol şans.
~ curstate = "a"
-> END

=== bChosen ===
tamamdir maceracı, görevin b şu şudur.
parkur ve ölüm tehlikeli yol
sana bol şans.
~ curstate = "b"
-> END

=== cChosen===
tamamdir maceracı, görevin c du dudur.
güvenli ama korkunç uzun yol
sana bol şans.
~ curstate = "c"
-> END

