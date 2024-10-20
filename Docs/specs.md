# Laika launch -speksi

Tekijä: Mikko Sipola - Jyväskylän ammattikorkeakoulu

## Yleiskuvaus

Tyyppi - peli

Laika launchissa tavoitteena on ampua kanuunalla Laika-koira toiselle taivaankappaleelle aidossa Soviet union -hengessä. 
Pelin tausta on liikkumaton ja "Maa" sijaitsee ruudun alareunassa ja muut taivaankappaleet ruudulla sen yllä. Peliin toteutetaan
painovoimavaikutus ja erilaisia taivaankappaleita johon Laikaa kuljettava ammus voi matkallaan törmätä. 

Painovoimavaikutus on olennainen osa pelin haastetta ja sen fysiikka yritetään toteuttaa ensiksi niin, että kuvaruudun keskelle sijoitetaan
gravitaatiokenttä, jonka ohi Laika tähdätään, painovoimakenttä kaareuttaa rataa ja sitä voidaan käyttää ammuksen ohjaamiseen takaisinpäin. Tällöin
toteutetaan muitakin taivaankappaleita "maaliksi", joista osa voi olla piilossa toisen takana niin ettei sitä voida lähestyä suoraa reittiä.

Mikäli toivottu kaksiulotteinen gravitaatiovaikutus osoittautuu liian haasteelliseksi B -suunnitelmana toteutetaan suora alaspäin vetävä 
gravitaatiokenttä, mutta pelin idea on edelleen sama: Osua toivotulle taivaankappaleelle jolle ei voida vetää suoraa janaa lähtöpisteestä, 
vaan gravitaatiota täytyy osata hyödyntää.

Käyttäjät voivat kirjautua omilla tunnuksilla ja lisätä omia tietoja, jotka tallennetaan SQLite -tietokantaan. Lisäksi pidetään tilastoa parhaista pisteistä.

## Kohdeyleisö

9-99 vuotiaat nuoret tai ikinuoret. 

## Käyttöympäristö ja teknologiat

Omalle laitteelle ladattava freeware -peli. C#, xaml, WPF, SQLite

## Ajankäyttösuunnitelma

Varataan reilusti aikaa kolmen viikon ajan välille 4.5.2020 - 24.5.2020 ma-pe.

| Viikko | Ajankäyttö |
|:-:|:-:|
| 19 | Alkuviikosta alustava MainWindow.xaml -suunnittelu ja loppuviikkoa kohti siirrytään fysiikkamallin suunniteluun|
| 20 | Pelin fysiikkamallin valmiiksisaattaminen, loppuviikko käyttöliittymän ulkoasun tekeminen. |
| 21 | Lisätään MSQLite -toiminnot ja viimeistellään kaikki osa-alueet, sekä loppuraportin kirjoittaminen.|

### Ajankäyttösuunnitelman toteuma

Aiemmin suunniteltu gravitaatiomalli (2 pv)

5.5 Pelin graafisen ulkoasun demoversio
6.5 Gravitaatiomallin siirtäminen koodiin
7.5 Gravitaatiomallin siirtäminen koodiin
8.5 Fysiikkaa fysiikkaa
12.5 Toimintoja lisätty
13.5 Progress barit ym. lisätty
14.5 Alkuperäinen näppäimistöohjaus siirretty hiirelle. Sulavampi käyttö.
15.5 Hiirellä ohjaus toteutettu kokonaan. Pelattavuuden osalta valmis.
17.5 Fysiikkamallin refaktorointia olio-paradigman mukaiseksi. Hukkapäivä. Tökkivä, huonosti toimiva koodi.
18.5 Palattiin edelliseen toimivaan versioon. Hiiriohjaus toteutettu kokonaan.
19.5 Pienempimuotoinen refaktorointi. Ominaisuuksien ja toiminnallisuuksien hienosäätöä.
20.5 Käyttöliittymän siistimistä. Tietokannan suunnittelua.
21.5 Tietokantoja
22.5 Tietokantoja
23.5 Tietokantoja ja pelin viimeistely näyttöä varten.
