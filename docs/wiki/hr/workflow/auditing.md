# Revizija

Revizija je proces praćenja promjena na podacima koji se mogu revidirati, poput
lokacija i korisnika mreže, putem revizijskih događaja. Revizijske događaje
stvara poslužitelj svaki put kada se podaci koji se mogu revidirati kreiraju,
ažuriraju ili brišu. Ovi događaji se pohranjuju u bazu podataka i mogu se
pretraživati kako bi se odredilo tko je napravio promjenu, kada je promjena
napravljena i koja je promjena bila.

Revizija se provodi putem presretača upita koji stvara revizijski događaj svaki
put kada se podaci koji se mogu revidirati kreiraju, ažuriraju ili brišu.

```plantuml
@startuml

start
:Baza je upitana;

:Presretač upita počinje čitati promijenjene entitete;

repeat :Počinje obrada entiteta;
  if (Entitet je podložan reviziji i mutiran?) then (da)
    :Izmijenite svojstva revizije na entitetu;
    :Stvorite revizijski događaj;
    :Dodajte kreiranje revizijskog događaja u upit;
  else (ne)
  endif
backward :Obradi sljedeći entitet;
repeat while (Više entiteta?);

:Upit je izvršen;

stop

@enduml
```
