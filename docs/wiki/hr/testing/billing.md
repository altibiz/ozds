# Naplata

Kao što je prikazano u workflow-u naplate, naplata je orkestrirani proces koji
uključuje cloud server, bazu podataka i Altibiz ERP server. Imajući to na umu,
možemo identificirati sljedeće točke kvara:

- **Server -> Baza podataka**: Server šalje podatke u bazu podataka. Ako je baza
  podataka nedostupna ili nema veze s bazom podataka ili postoje problemi s
  upitima, server neće moći pohraniti podatke.

- **Server**: Sam server može prestati raditi ili imati bug koji uzrokuje prekid
  komunikacije s drugim servisima.

- **Server -> Altibiz ERP**: Cloud server šalje podatke o naplati na Altibiz ERP
  server. Ako je Altibiz ERP server nedostupan ili nema veze s Altibiz ERP
  serverom ili postoje problemi s podacima, cloud server neće moći pohraniti
  odgovor. Ova točka kvara bit će proširena kada se implementira pravilna
  integracija s Altibiz ERP serverom.

## Kvarovi

Ovdje je popis kvarova koji se mogu pojaviti u procesu naplate podijeljenih po
područjima:

- **Server -> Baza podataka**:

  - Baza podataka nije povezana na mrežu
  - Baza podataka baca iznimku (softverski bug)

- **Server**:

  - Server nije povezan na mrežu
  - Server baca iznimku (softverski bug)

- **Server -> Altibiz ERP**:

  - Altibiz ERP server nije povezan na mrežu
  - Altibiz ERP server šalje netočne podatke
  - Altibiz ERP server baca iznimku (softverski bug)

Napomena: Svaki kvar može se pojaviti u različitim fazama procesa naplate.

## Testiranje

Da bismo testirali otpornost u procesu naplate, možemo simulirati kvarove na
sljedeće načine:

- Baza podataka nije povezana na mrežu: isključite bazu podataka i ponovo je
  uključite nakon nekog vremena. Server bi trebao ponovno početi raditi i svi
  podaci trebaju biti ispravno pohranjeni. Ako je neka operacija završena,
  server bi trebao nastaviti s tim operacijama.

- Baza podataka baca iznimku: stvorite bug na serveru koji uzrokuje prestanak
  obrade dolaznih upita u bazi podataka. Server ne bi trebao prestati raditi i
  trebao bi generirati alarm.

- Server nije povezan na mrežu: isključite server i ponovo ga uključite nakon
  nekog vremena. Server bi trebao ponovno početi raditi i svi podaci trebaju
  biti ispravno pohranjeni, a alarm bi trebao biti generiran. Ako je neka
  operacija završena, server bi trebao nastaviti s tim operacijama.

- Server baca iznimku: stvorite bug na serveru koji uzrokuje prestanak obrade
  dolaznih upita. Server ne bi trebao prestati raditi i trebao bi generirati
  alarm.

- Altibiz ERP server nije povezan na mrežu: isključite lažni Altibiz server i
  ponovo ga uključite nakon nekog vremena. Server bi trebao ponovno početi
  raditi i svi podaci trebaju biti ispravno pohranjeni, a alarm bi trebao biti
  generiran.

- Altibiz ERP server baca iznimku
