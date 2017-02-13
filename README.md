# DrusProjekat
Projekat iz predmeta distribuirani upravljacki sistemi

Uputstvo:
1.U projektu MeasureService nalazi se sql skripta: Model1.edmx.sql.
Potrebno je nju pokrenuti da bi se kreirala baza podataka.
(u txt fajlu skripta_sql.txt se nalazi ista skripta,moze se i ona pokrenuti)

2.Pokrenuti projekat Host

3.Pokrenuti projekat MeasureClient i uneti ID meraca koji zelimo da kreiramo
(moguce je pokrenuti vise puta,tj.kreirati vise meraca)

4.Pokrenuti projekat ObserverClient koji pretstavlja posmatraca.
 Zatim mozemo izabrati da li zelimo da pratimo trenutno stanje meraca,ili da pregledamo izvestaje.
Na merac se mozemo prijaviti/odjaviti preko njegovog ID-a.

5.Kod izvestaja mozemo dobiti sledece informacije:
	1-Izvestaj 1, Sva merenja sa odredjenog meraca u intervalu od-do.
	2-Izvestaj 2, Vlaznost ili Temperatura sa odredjenog meraca u intervalu od-do.
	3-Izvestaj 3, Svi trenuci kada je vrednost merenja bila iznad/ispod zadate granice.
	4-Izvestaj 4, Prosek temperature/vlaznosti za odredjeni period i lokaciju.
	5-Izvestaj 5, Svi trenuci kada je vrednost merenja bila iznad/ispod zadate granice na odredjenoj lokaciji.
