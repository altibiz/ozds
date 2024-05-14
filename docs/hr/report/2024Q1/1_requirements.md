# Zahtjevi

<div style="display: none;">
  \page izvjesce-2024-q1-zahtjevi Zahtjevi
</div>

Trenutno su podržani samo uređaji Abb B2x i Schneider iEM3xxx. Priručnici za te
uređaje analizirani su kako bi se odredila strategija sučeljavanja.

Konačno rješenje uključuje korištenje Raspberry PI Model 4B nazvanog posrednik
na lokaciji za čitanje podataka mjerenja s uređaja ili gatewaya. Za sada se
planira koristiti jedan posrednik za svaku lokaciju, ali to se može proširiti na
više uređaja po lokaciji ako je potrebno radi redundancije.

Posrednik čita podatke s uređaja koristeći Modbus TCP i šalje podatke na server
putem jednostavnog REST API-ja. Server je cloud-based server koji je smješten na
Azure-u.

Registri Modbusa koji se čitaju sa svakog tipa uređaja određeni su iz njihovih
priručnika kao i registri potrebni za konfiguriranje trenutne tarife uređaja.

Za Abb B2x uređaje čitaju se sljedeći registri:

| Adresa | Vrsta                  | Naziv                                  |
| ------ | ---------------------- | -------------------------------------- |
| 0x5B00 | u32 (pomnoženo s 0.1)  | Napon L1 (Volti)                       |
| 0x5B02 | u32 (pomnoženo s 0.1)  | Napon L2 (Volti)                       |
| 0x5B04 | u32 (pomnoženo s 0.1)  | Napon L3 (Volti)                       |
| 0x5B0C | u32 (pomnoženo s 0.01) | Struja L1 (Amperi)                     |
| 0x5B0E | u32 (pomnoženo s 0.01) | Struja L2 (Amperi)                     |
| 0x5B10 | u32 (pomnoženo s 0.01) | Struja L3 (Amperi)                     |
| 0x5B16 | s32 (pomnoženo s 0.01) | Aktivna snaga L1 (Wati)                |
| 0x5B18 | s32 (pomnoženo s 0.01) | Aktivna snaga L2 (Wati)                |
| 0x5B1A | s32 (pomnoženo s 0.01) | Aktivna snaga L3 (Wati)                |
| 0x5B1E | s32 (pomnoženo s 0.01) | Reaktivna snaga L1 (VAr)               |
| 0x5B20 | s32 (pomnoženo s 0.01) | Reaktivna snaga L2 (VAr)               |
| 0x5B22 | s32 (pomnoženo s 0.01) | Reaktivna snaga L3 (VAr)               |
| 0x5460 | u64 (pomnoženo s 10)   | Uvoz aktivne snage L1 (Wh)             |
| 0x5464 | u64 (pomnoženo s 10)   | Uvoz aktivne snage L2 (Wh)             |
| 0x5468 | u64 (pomnoženo s 10)   | Uvoz aktivne snage L3 (Wh)             |
| 0x546C | u64 (pomnoženo s 10)   | Izvoz aktivne snage L1 (Wh)            |
| 0x5470 | u64 (pomnoženo s 10)   | Izvoz aktivne snage L2 (Wh)            |
| 0x5474 | u64 (pomnoženo s 10)   | Izvoz aktivne snage L3 (Wh)            |
| 0x5484 | u64 (pomnoženo s 10)   | Uvoz reaktivne snage L1 (VARh)         |
| 0x5488 | u64 (pomnoženo s 10)   | Uvoz reaktivne snage L2 (VARh)         |
| 0x548C | u64 (pomnoženo s 10)   | Uvoz reaktivne snage L3 (VARh)         |
| 0x5490 | u64 (pomnoženo s 10)   | Izvoz reaktivne snage L1 (VARh)        |
| 0x5494 | u64 (pomnoženo s 10)   | Izvoz reaktivne snage L2 (VARh)        |
| 0x5498 | u64 (pomnoženo s 10)   | Izvoz reaktivne snage L3 (VARh)        |
| 0x5000 | u64 (pomnoženo s 10)   | Ukupan uvoz aktivne energije (Wh)      |
| 0x5004 | u64 (pomnoženo s 10)   | Ukupan izvoz aktivne energije (Wh)     |
| 0x500C | u64 (pomnoženo s 10)   | Ukupan uvoz reaktivne energije (VARh)  |
| 0x5010 | u64 (pomnoženo s 10)   | Ukupan izvoz reaktivne energije (VARh) |
| 0x5170 | u64 (pomnoženo s 10)   | Ukupan uvoz aktivne energije T1 (Wh)   |
| 0x5174 | u64 (pomnoženo s 10)   | Ukupan uvoz aktivne energije T2 (Wh)   |

Za uređaje Schneider iEM3xxx koriste se sljedeći registri:

| Adresa | Vrsta                  | Naziv                                  |
| ------ | ---------------------- | -------------------------------------- |
| 0x0BD3 | f32 (pomnoženo s 1)    | Napon L1 (Volti)                       |
| 0x0BD5 | f32 (pomnoženo s 1)    | Napon L2 (Volti)                       |
| 0x0BD7 | f32 (pomnoženo s 1)    | Napon L3 (Volti)                       |
| 0x0BB7 | f32 (pomnoženo s 1)    | Struja L1 (Amperi)                     |
| 0x0BB9 | f32 (pomnoženo s 1)    | Struja L2 (Amperi)                     |
| 0x0BBB | f32 (pomnoženo s 1)    | Struja L3 (Amperi)                     |
| 0x0BED | f32 (pomnoženo s 1000) | Aktivna snaga L1 (Wati)                |
| 0x0BEF | f32 (pomnoženo s 1000) | Aktivna snaga L2 (Wati)                |
| 0x0BF1 | f32 (pomnoženo s 1000) | Aktivna snaga L3 (Wati)                |
| 0x0BFB | f32 (pomnoženo s 1000) | Ukupna reaktivna snaga (VAR)           |
| 0x0C03 | f32 (pomnoženo s 1000) | Ukupna prividna snaga (VA)             |
| 0x0DBD | u64 (pomnoženo s 1)    | Uvoz aktivne energije L1 (Wh)          |
| 0x0DC1 | u64 (pomnoženo s 1)    | Uvoz aktivne energije L2 (Wh)          |
| 0x0DC5 | u64 (pomnoženo s 1)    | Uvoz aktivne energije L3 (Wh)          |
| 0x0C83 | u64 (pomnoženo s 1)    | Ukupan uvoz aktivne energije (Wh)      |
| 0x0C87 | u64 (pomnoženo s 1)    | Ukupan izvoz aktivne energije (Wh)     |
| 0x0C93 | u64 (pomnoženo s 1)    | Ukupan uvoz reaktivne energije (VARh)  |
| 0x0C97 | u64 (pomnoženo s 1)    | Ukupan izvoz reaktivne energije (VARh) |
| 0x1063 | u64 (pomnoženo s 1)    | Ukupan uvoz aktivne energije T1 (Wh)   |
| 0x1067 | u64 (pomnoženo s 1)    | Ukupan uvoz aktivne energije T2 (Wh)   |

Tipovi korišteni u tablicama:

- `u32` - pozitivni 32-bitni cijeli broj
- `s32` - 32-bitni cijeli broj
- `u64` - pozitivni 64-bitni cijeli broj
- `f32` - 32-bitni realni broj

Registri aktivne i reaktivne energije koriste se za izračun podataka za naplatu.
Vrhovi aktivne snage izračunavaju se preko aktivne energije. Ostali registri
mjere se u svrhu praćenja i dijagnostike.
