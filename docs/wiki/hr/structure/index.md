# Struktura projekta OZDS

C# rješenje OZDS repozitorija sadrži nekoliko projekata, od kojih svaki služi
različitoj svrsi. Ovdje je pregled svakog projekta

## Ozds.Data

Ovaj projekt sadrži shemu baze podataka i upite, i ništa više. Klasa
`OzdsDbContext` sadrži sve tablice baze podataka, a `OzdsDbClient` sadrži sve
upite potrebne za OZDS poslužitelj. Svi entiteti (tablice) nalaze se u imenskom
prostoru `Ozds.Data.Entities`.

## Ozds.Business

Ovaj projekt je sloj koji se nalazi između `Ozds.Server` i `Ozds.Data`. Bilo
koja logika koja mora biti između akcija poslužitelja i upita nalazi se ovdje i
organizirana je u usluge koje se dodaju u DI spremnik. Svaka funkcija definirana
u ovom projektu testirana je jediničnim testovima u projektu
`Ozds.Business.Test`.

## Ozds.Server

Ovaj projekt je početni projekt i sadrži sve API kontrolere i ulaznu točku za
projekt `Ozds.Client`. Složenost u ovom projektu bi trebala dolaziti samo iz
korisničkog sučelja, a sva logika pozadinskog sustava bi trebala biti smještena
u projektu `Ozds.Business`.

## Ozds.Client

Ovaj projekt sadrži sve UI Razor datoteke koje će prikazivati podatke krajnjim
korisnicima. Složenost u ovom projektu treba biti minimalna, a sva dodatna
logika treba biti smještena u projektu `Ozds.Business`.

## Ozds.Business.Test

Ovaj projekt testira sve u projektu `Ozds.Business` i oponaša njegovu strukturu.

## Ozds.Fake

Ovaj projekt se tretira kao skripta koja generira lažna mjerenja za poslužitelj.
Složenost u ovom projektu treba biti minimalna i ovaj projekt bi trebao
koristiti projekt `Ozds.Server` za oponašanje strukture mjerenja.
