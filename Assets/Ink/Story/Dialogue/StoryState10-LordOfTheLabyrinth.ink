INCLUDE ../globals.ink

-> main

=== main ===
buyur sen kimsin vesaire
-> alduin1

=== alduin1 ===
ben alduin, siz kimsiniz efendim
bana yardımcı olabilir misiniz
->lord1

===lord1===
ben labirentin efendisi ABE
yüzyıllardır buralara kimse gelmemişti
ne istiyorsun bakalım benden
-> ask_to_lord


===ask_to_lord===
sor bana ve ben de cevaplıyim
    + [(çıkış yolu?) ]
        -> exit    
    + [(öğrenmek istediklerim var)]
        -> learnAnswer
    + [(yok) ]
        -> no

===learnAnswer===
neyi öğrenmek istersin
    + [(baştaki ağaç?) ]
        -> tree    
    + [(aile)]
        -> aileQuestion
    + [(medeniyet) ]
        -> medeniyetQuestion

===exit===
bana nolur çıkışı göster
->exit_answer

===exit_answer===

yolu takip et
ilerden sola dön
korkmadan koşarak dümdüz devam et, çıkışa ulaşacaksın
bol şans
->END

===no===
benim gitmem lazım
->END

=== tree ===
ağaç şu bu 
şöyle böyle
-> ask_to_lord

=== aileQuestion ===
aileme noldu
-> aileAnswer

=== medeniyetQuestion ===
medeniyete noldu, medeniyeti kurtmak mümkün mü
-> medeniyetAnswer

=== aileAnswer ===
anne babanı mı örenmek istersin yoksa macerayi baslatan dedeni mi
    + [(annebaba) ]
        -> annebabaQuestion   
    + [(dede)]
        -> dedeQuestion
    + [(farketmez) ]
        -> dedeQuestion

=== medeniyetAnswer ===
medeniyete böyle böyle oldu
hayır mümkün değil
veya evet mümkün falan filan
konuşma konuşma
-> ask_to_lord

=== dedeQuestion ===
dedeme noldu
-> dedeAnswer

=== annebabaQuestion ===
annebabama noldu
-> annebabaAnswer   

=== dedeAnswer ===
dedene bu oldu
-> ask_to_lord

=== annebabaAnswer ===
annebabana bu bu oldu
-> ask_to_lord 

