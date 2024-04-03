# Business math

naming:

price -> spanning -> tariff -> duplex -> phasic

price: null | price? spanning: null | minmax (Min, Max) | average(Avg) tariff:
null | unary(T0) | binary(T1, T2) duplex: null | any(Any) | net(Net) |
importexport(Import, Export) phasic: null | single(Total) | tri(L1, L2, L3)

[ActiveEnergy][Total][Import][T1][Price]\_EUR (cijena potrosnje aktivne energije
u dnevnoj tarifi)

[ReactiveEnergy][Total][Import][T0][Price]\_EUR (cijena potrosnje reaktivne
energije u jednoj tarifi)

[Voltage][L1][Any][T0]\_V

[ReactivePower][L1][Net][T0][Avg]\_VAR

[ActiveEnergy][Total][Import][T1][Min]\_Wh

math:

phasic: sum (zbroj svih faza) | average (prosjek svih faza) | peak (najvisa
faza) | trough (najniza faza) | l1 | l2 | l3 duplex: net (import - export) | any
(kada je n/a - znaci za struju i napon) | import (potrosnja) | export
(proizvodnja) tariff: unary (cijelo vrijeme) | binary (po danu ili noci)
spanning: min (minimum u intervalu vremena) | max (max u intervalu vremena) |
diff (max - min)

npr ukupna potrosnja u mjesecu u tablicama
ActiveEnergy_Wh.SpanDiff.TariffUnary.DuplexImport.PhaseSum

napon na grafu average faza Voltage_V.TariffUnary.DuplexAny.PhaseAverage

napon na grafu jedna faza Voltage_V.TariffUnary.DuplexAny.PhaseL1
