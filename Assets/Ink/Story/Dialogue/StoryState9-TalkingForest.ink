INCLUDE ../globals.ink

-> main

=== main ===
heeyyyy,
orda dur bakalım genç adam
bu ormana öyle her adımını atan giremez
-> choose

=== choose ===
kimsin sen tanıt kendini
    + [(alduin (scared)) ]
        -> alduin    
    + [(ailemi kurtarmaya çalışıyorum)]
        -> family
    + [(sanane ihtiyar) ]
        -> arrogant


=== alduin ===
k k kim konuşuyor orda
ben alduin, lütfen bana zarar vermeyin
ben buraya nasıl geldiğimi bilmiyorum
bana yolu gösterir misiniz
amacım zarar vermek değil sadece ailemi kurtarmak
-> alduin_answer

=== family ===
ben ailemi kurtarmaya geldim buraya
ya bana yardımcı olursunuz...
ya da benimle yüzleşmek zorunda kalırsınız
-> family_answer

=== arrogant ===
sanane moruk (falan filan)
-> arrogant_answer

=== family_answer ===
aileni kurtarmaya mı geldin
yüzyıllardır bunu başarabilen kimse olmadı
buraya giren çıkamaz falan gibi cümleler
sana yardımcı olacağız cesur adam
    + [(teşekkür) ]
        -> thanks   
    + [(sizin bir isteğiniz var mı)]
        -> how_are_you
    + [(size ihtiyacım yok) ]
        -> arrogant2

=== alduin_answer ===
işte seni tanıdığımza memnun olduk falan filan 
-> family_answer

=== arrogant_answer ===
haddini bil densiz cart curt
buraya gelip bize saygı duymazsan bu ormandan geçemezsin ve burda ağaç olursun gibi iddialı söylemler falan tehdit
-> sorry

=== sorry ===
özür dilerim falan filan
ben sadece ailemi kurtmatmaya çalışyıroum
-> family_answer

=== arrogant2 ===
siz kimsiniz bana yardım edeceksiniz
ben kendi başımın çaresine bakarım
beni rahatsız etmeyin yeter
-> arrogant2_answer

=== arrogant2_answer===
ne bok yersen ye falan filan
eğer ki ormandan geçersen taşkınlık çıkarırsan gününü görürsün falan
hadi git yoluna
-> END

=== how_are_you ===
.ok teşekkür ederim
sizlerin bir yardıma ihtiyacı var mı, size yardım etmek isterim
-> help

=== thanks===
çok teşekkür ederim yardımınızı
bunu asla unutmayacağım
-> help 

===help===
ne demek güzel insan
sana çıkış yolunu göstermek istiyoruz umarımm bunu başarabilirsin
bu ormanı takip et ormanın bitiminde sola doğru ssakın girme
yolun sonuna kadar devam et. yolun sonuna gelmeden zaten seni birisi bekliyor olacak
o kişi...
o'nun hakkında çok bilgi veremem
sana güveniyorum
-> END
