INCLUDE ../globals.ink

-> main

=== main ===
Medeniyetimize hoş geldin
Demek sen kayıp medeniyetimizin sırrını çözmek üzere görevlendirilen yeni maceracımızsın.
Bu maceraya çıkmak büyük cesaret ister. Dilerim ki bu macerayı tamamlayarak hem medeniyetimizi kurtarır hem de ebeveynlerinle yeniden bir araya gelebilirsin.
    + [(saldırrrr)]
        -> stupidlybrave
    + [(İlgili ve Cesur) ]
        -> adventurerbrave
    + [(Korkak) ]
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

