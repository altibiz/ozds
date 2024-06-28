# Ozds.Server

Ovaj projekt sadrži ulaznu točku za projekt `Ozds.Client` i sve potrebne API
kontrolere. Kontroleri se nalaze u imenskom prostoru `Controller`. Svaka akcija
kontrolera je autorizirana i trebala bi samo obuhvatiti ulaze i izlaze određenih
funkcija unutar `Ozds.Business`.

## Ozds.Server.Controllers

Postoji nekoliko marker sučelja koja se koriste za implementaciju korisničkog
sučelja i API-ja:

- App: ovo je ulazna točka za korisničko sučelje aplikacije i služi za
  posluživanje stranica iz projekta `Ozds.Client`. Koristi se za posluživanje
  datoteke `index.html` i svih drugih statičkih datoteka koje su potrebne za
  prikaz korisničkog sučelja.

- IOT: ulazna točka za akcije `push` i `poll` koje su potrebne za slanje
  mjerenja u bazu podataka i dohvaćanje konfiguracije za messengere.
