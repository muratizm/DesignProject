INCLUDE ../globals.ink

-> main

=== main ===
meraba genç yakışıklı!
ben bu diyarın MAGİCİIAN'I MURAT
-> choose

=== choose ===
sorun varsa alabilirim
    + [(no) ]
        -> no    
    + [(aile)]
        -> aile_question
    + [(medeniyet) ]
        -> medeniyet_question


=== no ===
no
-> END

=== aile_question ===
aileme noldu
~ curstate = "aile"
-> aile_answer

=== medeniyet_question ===
medeniyete noldu
~ curstate = "medeniyet"
-> medeniyet_answer

=== aile_answer ===
anne babanı mı örenmek istersin yoksa macerayi baslatan dedeni mi
    + [(annebaba) ]
        -> annebaba_question   
    + [(dede)]
        -> dede_question
    + [(farketmez) ]
        -> dede_question

=== medeniyet_answer ===
medeniyete böyle böyle oldu
-> choose

=== dede_question ===
dedeme noldu
-> dede_answer

=== annebaba_question ===
annebabama noldu
-> annebaba_answer   

=== dede_answer ===
dedene bu oldu
-> choose

=== annebaba_answer ===
annebabana bu bu oldu
-> choose 
