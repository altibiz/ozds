# Lažiranje

Lažiranje mjerenja se vrši u dva načina: slanjem i sjemenjenjem. U načinu
slanja, mjerenja se kontinuirano lažiraju i šalju na poslužitelj. U načinu
sjemenjenja, mjerenja se lažiraju za određeni interval (npr. prošli tjedan) i
šalju na poslužitelj u velikim serijama što je brže moguće.

## Slanje

Slanje je kontinuirani proces koji šalje mjerenja na poslužitelj svakog
određenog intervala (npr. zadnja minuta) za mjerenja u tom intervalu. Zatraženi
interval se prvo projicira u vrijeme CSV datoteke, a zatim se zapisi projiciraju
u budućnost.

```plantuml
@startuml

start

repeat
  :Pročitaj određeni interval
  projiciran u vrijeme
  CSV datoteke;

  :Projiciraj zapise u budućnost;

  :Pretvori zapise u zahtjev za slanje;

  :Pošalji zahtjev za slanje na poslužitelj;

  :Pričekaj određeni interval;
repeat while ()

stop

@enduml
```

## Sjemenjenje

Sjemenjenje je jednokratna operacija koja šalje mjerenja na poslužitelj za
određeni interval (npr. prošli tjedan) u velikim serijama što je brže moguće. To
se postiže na isti način kao i slanje, ali se zahtjevi šalju jednom bez čekanja
na sljedeći interval.

```plantuml
@startuml

start

:Određuje se početak trenutnog intervala;

repeat
  :Pročitaj CSV zapise za dan
  od početka trenutnog intervala
  projiciran u vrijeme CSV datoteke;

  :Projiciraj zapise u budućnost;

  :Pretvori zapise u zahtjeve za slanje mjerenja;

  repeat :Uzmi određenu seriju mjerenja;
    :Ukloni seriju s popisa mjerenja;

    :Spakiraj zahtjeve za mjerenja u zahtjev;

    :Pošalji zahtjev na poslužitelj;
  backward :Obradi sljedeću seriju mjerenja;
  repeat while (Više mjerenja?);
backward :Početak trenutnog intervala pomaknut je na sljedeći dan;
repeat while (Početak trenutnog intervala < sada?)

stop

@enduml
```
