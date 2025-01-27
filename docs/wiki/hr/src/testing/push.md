# Push

Prema arhitekturi, s gledišta cloud servera, postoje više točaka s kojih mogu
nastati pogreške. Počevši od lokacija pa do baze podataka, možemo očekivati
pogreške u ovim područjima:

- **Raspberry PI -> Server**: Raspberry PI je odgovoran za slanje podataka s
  mjerača na server. Ako Raspberry PI izgubi vezu sa serverom, server neće
  primati nikakve podatke. Raspberry PI također može slati netočne podatke ili
  ne biti ovlašten za slanje podataka.

- **Server**: Sam server može prestati raditi ili imati bug koji uzrokuje
  prestanak primanja podataka s Raspberry PI.

- **Server -> Baza podataka**: Server šalje podatke u bazu podataka. Ako je baza
  podataka nedostupna ili nema veze s bazom podataka ili postoje problemi s
  upitima, server neće moći pohraniti podatke.

## Kvarovi

Ovdje je popis kvarova koji se mogu pojaviti u procesu push podijeljenih po
područjima:

- **Raspberry PI -> Server**:

  - Raspberry PI ne šalje podatke
  - Raspberry PI šalje netočne podatke

- **Server**:

  - Server nije povezan na mrežu
  - Server baca iznimku (softverski bug)

- **Server -> Baza podataka**:
  - Baza podataka nije povezana na mrežu
  - Baza podataka baca iznimku (softverski bug)

## Testiranje

Da bismo testirali otpornost u procesu push, možemo simulirati kvarove na
sljedeće načine:

- Raspberry PI ne šalje podatke: isključite fake push skriptu i provjerite kako
  se server ponaša. Server ne bi trebao prestati raditi i trebao bi generirati
  alarm nakon nekog vremena.

- Raspberry PI šalje netočne podatke: stvorite CSV datoteke s netočnim podacima
  iz CSV datoteka s točnim podacima i fake push skriptom pošaljite te mjere na
  server. Server ne bi trebao prestati raditi i trebao bi generirati alarm.

- Server nije povezan na mrežu: isključite server i ponovo ga uključite nakon
  nekog vremena. Server bi trebao ponovno početi raditi, niti jedno mjerenje ne
  bi trebalo biti izgubljeno (fake skripta bi trebala ponovno pokušati kao i
  aplikacija), i alarm bi trebao biti generiran.

- Server baca iznimku: stvorite bug na serveru koji uzrokuje prestanak obrade
  dolaznih mjera. Server ne bi trebao prestati raditi i trebao bi generirati
  alarm.

- Baza podataka nije povezana na mrežu: isključite bazu podataka i ponovo je
  uključite nakon nekog vremena. Server bi trebao ponovno početi raditi, niti
  jedno mjerenje ne bi trebalo biti izgubljeno (fake skripta bi trebala ponovno
  pokušati kao i aplikacija), i alarm bi trebao biti generiran.

- Baza podataka baca iznimku: stvorite bug na serveru koji uzrokuje prestanak
  obrade dolaznih mjera u bazi podataka. Server ne bi trebao prestati raditi i
  trebao bi generirati alarm.
