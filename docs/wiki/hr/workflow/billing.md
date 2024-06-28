# Naplata

<!-- FIXME: messaging -->

Naplata je proces izdavanja računa korisnicima mreže i lokacijama. Trenutno je
implementirana samo naplata za korisnike mreže. Operateri trenutno mogu izdavati
račune na zahtjev za posljednje obračunsko razdoblje (prošli mjesec).

Naplata se provodi putem skupa klasa koje izračunavaju različite dijelove računa
ovisno o mjernim mjestima i tarifama.

```plantuml
@startuml

start

:Predstavnik operatera klikne na gumb za izdavanje računa za korisnika mreže;

:Baza je upitana za osnovu izdavanja računa za korisnika mreže;

:Kalkulator računa korisnika mreže počinje čitati osnove izračuna odgovarajuće svakom njihovom mjernom mjestu;

repeat :Počinje obrada osnove izračuna;
  :Odaberite kalkulator ovisno o tarifi:
    - bijela srednja,
    - bijela niska,
    - crvena niska,
    - plava niska;

  :Kalkulator počinje čitati osnove stavki izračuna;

  repeat :Počinje obrada osnove stavki izračuna;
    :Odaberite kalkulator ovisno o vrsti stavke izračuna:
      - aktivna energija T0/T1/T2,
      - jalova energija,
      - vršna aktivna snaga,
      - naknada za brojilo,
      - naknada za poslovnu uporabu,
      - naknada za obnovljivu energiju;

    :Izračunajte vrijednosti stavki;
  backward :Obradi sljedeću osnovu stavke izračuna;
  repeat while (Više osnova stavki izračuna?);

  :Izračunajte ukupni trošak izračuna;
backward :Obradi sljedeću osnovu izračuna;
repeat while (Više osnova izračuna?);

:Izračunajte zbroj stavki izračuna koji se protežu kroz sve izračune;

:Izračunajte ukupni iznos, porez i ukupni iznos s porezom;

:Spremite račun u bazu podataka i dobijte stvoreni ID računa;

:Počnite čitati izračunate izračune;

repeat :Izračun se čita;
  :Povežite izračun s računom;

  :Pratite izračun kao stvoren;
backward :Obradi sljedeći izračun;
repeat while (Više izračuna?);

:Spremite izračune u bazu podataka;

stop

@enduml
```
