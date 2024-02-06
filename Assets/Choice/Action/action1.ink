INCLUDE ../globals.ink

-> main

=== main ===
gizemli bir mavi nesne ile karşılaştın, seçimini yap!
-> choices

=== choices ===
* [Yok ET ! (x)]  
  ~ curstate = "yokettik"
  -> END 

* [Yavasca acmayi dene (c)] 
  ~ curstate = "actik"
  -> END

* [Osur! (v)] 
  ~ curstate = "osurduk"
  -> END
