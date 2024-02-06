INCLUDE globals.ink

{poke_name == "" : -> main | -> already_chosen}


=== main ===
Which poke do you choose?
    + [ahar]
        -> chosen("ahar")
    + [bhar]
        -> chosen("bhar")
    + [char]
        -> chosen("char")

=== chosen(poke) ===
~ poke_name = poke
You choosed {poke}!
-> END

=== already_chosen ===
You already chose {poke_name}
-> END