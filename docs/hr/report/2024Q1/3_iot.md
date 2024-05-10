# IOT

Uređaji Raspberry PI 4 (posrednici) sadrže Rust program koji kontinuirano čita
mjerenja s mjerača, grupira ih svakim određenim intervalom i šalje ih na cloud
server.

Rust program se pokreće putem Systemd servisa koji radi na NixOS-u. Servis je
konfiguriran da se pokrene pri pokretanju sustava i ponovno pokrene u slučaju
neuspjeha. Mjerenja se pohranjuju u lokalno instaliranu Postgresql bazu podataka
s TimescaleDB ekstenzijom.
