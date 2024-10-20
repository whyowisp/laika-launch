# Laika Launch - Loppuraportti

## 1. Asennus

GitLab:in palautetussa projektissa ei ole release -versiota mukana. 
Ohjelma käyttää SQLite -tietokantaa ja ensimmäisen käynnistyksen yhteydessä ohjelma luo itse tarvittavan tietokantatiedoston.


## 2. Tietoa ohjelmasta (mitä tekee, miksi etc)

### Toteutetut toiminnalliset vaatimukset:

1. Kaksiulotteinen gravitaatiovaikutus
2. Gravitaatiokeskuksen osalta toteutus on viimeistellympi, mutta lähtöpaikka ja kohde eivät ole 
liikuteltavissa uusien pelikenttien luomiseksi.
3. Pelaajan highscore voidaan lisätä tietokantaan pelaajan nimen kanssa. 

### Toteuttamatta jääneet toiminnalliset vaatimukset

1. Pelissä on vähemmän taivaankappaleita kuin oli tarkoitus sillä niiden toteutus jäi keskeneräiseksi 
(gravitationalObject -luokkaa olisi ollut tarkoitus käyttää, tai tehdä toinen luokka hitObject erikseen)
2. Pelissä ei kirjauduta sisään.

### Toiminnallisuus joka toteuttiin ohi/yli alkuperäisten vaatimusten

1. Laikalle lisättiin thrust -ominaisuus jolla voidaan vaikuttaa lentorataan
2. Toteutettiin hiiriohjaus.

### Ei-toiminnalliset vaatimukset sekä mahdolliset reunaehdot/rajoitukset

1. Pelialue ei ole skaalautuva vaan vaatii riittävän suuren näytön ja tarkkuuden.


## 3. Kuvaruutukaappaukset tärkeimmistä käyttöliittymistä + lyhyet käyttöohjeet jollei "ilmiselvää"

## 4. Ohjelman tarvitsemat /mukana tulevat tiedostot/tietokannat

Projektin mukana tulee kaikki tarvittava. 


## 5. Tiedossa olevat ongelmat ja bugit sekä jatkokehitysideat

1. Highscore -lista ei osaa korvata olemassa olevaa pelaajan nimeä.
2. Taivaankappaleista täytyy tehdä oma luokka, tai lisätä ne gravitationalObject -luokkaan ja pääohjelman täytyy osata instantioda ja käsitellä niitä.
Tällä mahdollistetaan uuden pelikentän luominen kun edellinen on läpäisty. 
 
## 6. Mitä opittu, mitkä olivat suurimmat haasteet, mitä kannattaisi tutkia/opiskella lisää jne

Koin ongelmien jäsentelyn luokiksi haasteelliseksi. Refaktoroin paljon koodia, yritin mm. kirjoittaa fysiikkamallin kokonaisuudessaan omaan luokkaan, tulokseksi sain kuitenkin
tökkivän epäjohdonmukaisesti toimivan mallin, josta jouduin palaamaan edelliseen versioon, joka kieltämättä toimii liukkaasti ja on yksinkertainen. Koska alkuperäinen koodaustaustani
on proseduraalisessa tavassa (Basic) ja tein sitä aika nuorena niin oma ajatteluni on ehkä luonnostaan virittynyt proseduraaliseksi. gravitationalObject luokkaa aion kesällä
kehitellä eteenpäin, jotta saan sen avulla toteutettua uudet pelikentät. Teen ehkä listan erilaisia taivaankappaleita jotka tuodaan randomilla pelikentälle. Hienoa olisi antaa osalle,
tai kaikille gravitaatio -ominaisuus ja ruudunpäivityksessä laskettaisiin voimien summa.

Itselleni uutta oli myös tilakoneen käyttö eri pelitilanteiden käsittelyyn. Ratkaisu on mielestäni erittäin toimiva.

Peter Wentworthin teos How to think like computer scientist, oli suuri apu ja innostava tiedonlähde monessa vaikeassa paikassa. https://www.ict.ru.ac.za/Resources/ThinkSharply/ThinkSharply/index.html
Luonnollisesti ilman kurssimateriaaleja en olisi päässyt alkua pidemmälle. Tietokantoja tuskin olisin saanut lainkaan tehtyä ilman. Joten kiitokset tekijöille!

## 7. Tekijät, vastuiden ja työmäärän jakautuminen

### Ajankäyttösuunnitelman toteuma

| Pvm. | Puuha |
| -- | -- |
| | Aiemmin suunniteltu gravitaatiomalli (2 pv) |
|5.5 | Pelin graafisen ulkoasun demoversio |
|6.5 | Gravitaatiomallin siirtäminen koodiin |
|7.5 | Gravitaatiomallin siirtäminen koodiin |
|8.5 | Fysiikkaa fysiikkaa |
|12.5 | Toimintoja lisätty |
|13.5 | Progress barit ym. lisätty |
|14.5 | Alkuperäinen näppäimistöohjaus siirretty hiirelle. Sulavampi käyttö. |
|15.5 | Hiirellä ohjausta kehitetty eteenpäin. Pelattavuuden osalta valmis. |
|17.5 | Fysiikkamallin refaktorointia olio-paradigman mukaiseksi. Hukkapäivä. Tökkivä, huonosti toimiva koodi. |
|18.5 | Palattiin edelliseen toimivaan versioon. Hiiriohjaus toteutettu kokonaan. |
|19.5 | Pienempimuotoinen refaktorointi. Ominaisuuksien ja toiminnallisuuksien hienosäätöä. |
|20.5 | Käyttöliittymän siistimistä. Tietokannan suunnittelua. |
|21.5 | Tietokantoja |
|22.5 | Tietokantoja |
|23.5 | Tietokantoja ja pelin viimeistely näyttöä varten. |

Allekirjoittaneen harteille kaikki työ ja siitä seuranneet virheet. Tunteja kertyi yli vaaditun varmaankin 150% suunnitellusta.

## 8. Tekijöiden ehdotus arvosanaksi, ja perustelut sille

Ehdottaisin arvosanaksi 4. En oikein usko, että ihan kaikki kurssin tavoitteet ovat täyttyneet, joten vitonen ei tulisi kyseeseen.
kolmonen taas tuntuisi olevan vähän alakanttiin, koska tässä opittiin todella monta uutta juttua, oli ne sitten kurssin tavoitteissa tai ei. Ehkä jos
tätä olisi tehnyt kaverin kanssa, niin olisi voinut jakaa vastuita ja käytettyjä tunteja projektin hiomiseen olisi ollut tietysti tuplasti enemmmän.