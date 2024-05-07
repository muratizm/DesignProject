INCLUDE ../globals.ink

-> main

=== main ===
meraba genç yolcu!
ben, ben bu diyarın en eski büyücüsüyüm.
cart curt
-> choose

=== choose ===
(hal hatır sorar)
    + [(iyi) ]
        -> good    
    + [(kötü)]
        -> bad
    + [(korkak) ]
        -> coward


=== good ===
iyiyim hamdolsun
-> wizard_answer1

=== bad ===
perperişan oldum sorma
~ curstate = "aile"
-> wizard_answer2

=== coward ===
yeter artık çıkarın beni burdan
~ curstate = "medeniyet"
-> wizard_answer2

=== wizard_answer1 ===
ne kadar korkusuz bir maceracıymışsın sen böyle
yüzyıllardır bu diyara senin kadar cesur ve maceracı birisi gelmedi cart curt
    + [(teşekkür) ]
        -> ty   
    + [(sen nasılsın)]
        -> you
    + [(yardım iste) ]
        -> help

=== wizard_answer2 ===
bunu duyduğuma üzüldüm
senin için yapabileceğim bir şey var midur
    + [(teşekkür) ]
        -> grateful   
    + [(yardım iste)]
        -> help
    + [(yalvar) ]
        -> beg



=== ty ===
teşekkür ederim wizard kardeş.
-> wizard_help

=== you ===
saol dost. sen nasılsın iyi misin. buralar tekin değil gibi görünüyor
-> how_is_wizard

=== help ===
bana yardımcı olabilir misin. yolumu bulmaya çalışıyorum
-> wizard_help



=== grateful ===
allah senden razı olsun ustam çok iyi olur valla çok mutlu olruum.
-> wizard_help

=== how_is_wizard ===
ben mi? 
benim nasıl olduğumu hayatım boyunca kimse sormadı
sen yüce bir insansın
içinde hem şefkatli bir kalp
hem de korkusuz bir yürek var
işte bu yüzden...
-> wizard_help

=== beg ===
yalvarırım bana yardım et bıktım artık bıktım
sana muhtacım
-> wizard_help



=== wizard_help ===
sana yardımcı olmayı çok isterim genç.
sana bu itemi veriyorum
buradan kurtulmaya çok yakınsın ve bu item seni daha da yaklaştıracak
sana güveniyorum, yolun açık olsun
~ curstate = "give_item"
-> END








