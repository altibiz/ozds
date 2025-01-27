# Backend

Backend OZDS-a je ASP.NET Core web aplikacija. Podijeljena je u pakete:

- `Ozds.Server`: početni projekt
- `Ozds.Data`: sloj pristupa podacima
- `Ozds.Business`: poslovni sloj
- `Ozds.Client`: sloj korisničkog sučelja

Poslovni sloj je odgovoran za rukovanje administrativnim zadacima u web
aplikaciji. Sljedeći odjeljci objašnjavaju potrebne radnje za obavljanje
administrativnih zadataka.

## Naplata

Naplata je proces kreacije računa korisnicima mreže i lokacijama. Trenutno je
implementirana samo naplata korisnika mreže. Trenutno, operateri mogu izdati
račune na zahtjev za zadnje razdoblje naplate (prošli mjesec).

Naplata je implementirana putem skupa klasa koje izračunavaju različite dijelove
računa ovisno o lokacijama mjerenja i tarifama.

```plantuml
@startuml

start

:Predstavnik operatera klikne na gumb za izdavanje računa za korisnika mreže;

:Baza podataka se upituje za osnovu za izdavanje računa za korisnika mreže;

:Kalkulator računa korisnika mreže počinje čitati osnove za obračune
koje odgovaraju svakoj od njihovih lokacija mjerenja;

repeat :Početak obrade osnove za obračun;
  :Odaberi kalkulator ovisno o tarifi:
    - bijela srednja,
    - bijela niska,
    - crvena niska,
    - plava niska;

  :Kalkulator počinje čitati osnove za obračun stavki;

  repeat :Početak obrade osnove za obračun stavke;
    :Odaberi kalkulator ovisno o vrsti stavke obračun:
      - aktivna energija T0/T1/T2,
      - reaktivna energija,
      - vrhunac aktivne snage,
      - naknada za mjerilo,
      - naknada za poslovnu upotrebu,
      - naknada za obnovljivu energiju;

    :Obračunaj vrijednosti stavki;
  backward :Obradi sljedeću osnovu za obračun stavke;
  repeat while (Ima više osnova za obračun stavki?);

  :Obračunaj ukupne troškove obračuna;
backward :Obradi sljedeću osnovu za obračun;
repeat while (Ima više osnova za obračun?);

:Izračunaj ukupne vrijednosti stavki obračuna koje se protežu kroz sve obračune;

:Izračunaj ukupno, porez i ukupno s porezom;

:Spremi račun u bazu podataka i dobij identifikator izdanog računa;

:Počni čitati obračunate obračune;

repeat :Obračun je pročitan;
  :Poveži obračun s računom;

  :Prati obračun kao kreiran;
backward :Obradi sljedeći obračun;
repeat while (Ima više obračuna?);

:Spremi obračune u bazu podataka;

stop

@enduml
```
