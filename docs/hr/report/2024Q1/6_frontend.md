# Frontend

<div style="display: none;">
  \page izvjesce-2024-q1-frontend Frontend
</div>

Frontend OZDS-a je Blazor SSR (Server-Side Rendering) projekt baziran na
OrchardCore, koji je trenutno u intenzivnom razvoju.

Do sada su implementirani osnovni izgled, navigacija, lista korisnika, lista
uređaja, detaljni pregled uređaja i agregacija mjerenja.

## Prijava

Na stranici za prijavu korisnici započinju svoju sesiju. Nakon prijave, korisnik
je preusmjeren na stranicu prilagođenu njihovim potrebama, na temelju njihove
vrste korisnika, privilegija te lokacija i mrežnih korisnika za koje su
odgovorni.

![Prijava](docs/en/assets/login.png) _/login_

## Admin

Ovaj odjeljak opisuje stranice koje su dostupne samo administratorima (obično
programerima).

### Korisnici

Na ovom sučelju mogu se mijenjati svi aspekti korisnika, mogu se stvarati novi
korisnici ili brisati postojeći. Ti korisnici kasnije se povezuju s
predstavnicima mrežnih korisnika i lokacija.

![Korisnici](docs/en/assets/users.png) _/admin/users_

## Operator

Ovaj odjeljak opisuje stranice koje su dostupne samo operatorima.

### Predstavnici

Predstavnici su povezani s korisnicima OrchardCore i omogućuju nam dodavanje
više podataka korisnicima. Ovi podaci mogu se pregledavati i uređivati na ovoj
stranici.

![Predstavnici](docs/en/assets/representatives.png) _/app/representatives_

## Mrežni korisnik

Ovaj odjeljak opisuje stranice koje su dostupne samo mrežnim korisnicima.

### Nadzorna ploča

Ovdje korisnik može vidjeti agregirane podatke o mjerenju potrošnje za tekući
mjesec, prethodni mjesec i grafikon potrošnje tijekom dužih razdoblja. Ispod
agregiranih vrijednosti nalazi se tablica s lokacijama mjerenja koje prikazuju
potrošnju pojedinačnih mjernih lokacija u tekućem i prethodnom mjesecu, te
obračune ukupno u prethodnom mjesecu.

Klikom na imena lokacija, mrežnih korisnika ili mjernih lokacija, korisnici se
preusmjeravaju na detaljan pregled tih entiteta. Lokacije i mrežni korisnici
preusmjeravaju na pregled podataka o korisniku i privilegijama. Klik na mjerna
mjesta preusmjerava na detaljan pregled pojedinačnih mjernih lokacija.

![Nadzorna ploča](docs/en/assets/dashboard.png) _/app/dashboard_

## Ostalo

Ovaj odjeljak opisuje stranice koje su dostupne svim korisnicima ovisno o
navigaciji i privilegijama. Drugim riječima, stranice koje nisu ograničene na
specifičnu ulogu.

### Mjerni uređaj

Kada se klikne na jednu od mjernih lokacija, postaju vidljivi detaljni podaci o
toj mjernoj lokaciji. Prikazuju se početna i krajnja očitanja na toj mjernoj
lokaciji po mjesecu, zajedno s ukupnom potrošnjom i maksimalnom snagom tijekom
tog mjeseca. Grafikon s desne strane pokazuje mjerenja za posljednjih četvrt
sata, pola sata, sat i šest sati, i može prikazivati vrijednosti za napon,
struju, aktivnu snagu, reaktivnu snagu i prividnu snagu. Pokazivač na lijevoj
strani pokazuje trenutnu snagu i uspoređuje je s priključnom snagom.

![Mjerni uređaj](docs/en/assets/meter.png) _/app/meters_
