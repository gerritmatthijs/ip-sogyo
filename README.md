<!-- ### (Nederlandse versie onderaan) -->

# Individual project - Presidents/Tichu

This repository contains my individual project concluding my traineeship program at Sogyo.

# Project goal

The goal of this project is the development of a full-stack web application for the game presidents or Tichu. Tichu is (almost) a more complicated version of the game presidents, hence the goal is to first implement Presidents and subsequently add Tichu rules as far as time allows. 

**Personal learning goals**
* Apply TDD more consistently
* Get new frameworks working more easily

**Technical learning goals**
* Using a new domain language with the .NET framework (C#/F#)
* Better understand how servers communicate
* (If time allows) learn how to host an actual website

# Moscow requirements

**Must**
* UI with clickable cards
* API layer
* Domain layer
* Working game flow (someone plays a set of cards, the next person can play a higher version of the same set of cards or pass)
* If everyone passes, the last person to play starts the next round
* A player who has played all their cards does not get turns anymore
* End of game when all players except 1 played all their cards

**Should**
* Persistence via a database
* More possible sets (subsequent pairs, street, full house)
* 4 special cards: mahjong (simplified), dog, dragon (simplified), phoenix
* Cards in different colors & set 'street of the same color'

**Could**
* Host game on a site, multiplayer functionality
* Bomb (4 of a kind or street of the same color) can be played on any trick (at any moment)
* Points (5 for 5, 10 for 10 & king, 25 for dragon, -25 for phoenix)
* Teams (being the first 2 out = 200 points & 0 points for the opponent)
* Special rules dragon & mahjong
* Small & great tichu
* Last player out with cards: hand goes to opponent, tricks to winner
* Passing cards around at the start of each round

**Won't**
* Fancy UI design
* Player ratings
* AI

# Framework & Set-up

The back-end of this project runs on .NET; the domain layer is programmed in F# and the API- and persistence layers in C#. The front-end uses Node.js in combination with React, Typescript and a Vite server. The database is not currently implemented (instead a simple in-memory dictionary is being used). When it is implemented, it will be MySQL. 

To start the back-end server, make sure you have .NET 8.0 installed. Run the following command from the API folder: 
```bash
dotnet run
```
The server listens from port 5036. 

To start the front-end server, you need to have the [Node.js webserver](https://nodejs.org/en/) installed. First go to the Client folder. To install the dependencies (only the first time), run the following command:
```bash
npm install
```
Then start the server by running:
```bash
npm run dev
```
The front-end server listens on port 5173. Go to http://localhost:5173/ in the browser to play the game. 

# Planning

The goal for week 1 was to get the front-end and back-end working and communicating. Then I added the following vertical slices one by one: 
* Send a GET request from the front-end to the back-end server (e.g. get starting hands)
* Send a POST request and view the effect in the UI
* Create clickable cards that are removed from hand upon clicking
* Display last played card in the middle
* A card can only be played if it is higher than the previous card.

The planning for week 2 was to finish the rest of the MUSTS. I divided the work among the following vertical slices: 
* Multiple players giving the turn to each other. Display the hand of the active player
* Pass button
* End of a trick
* A player who finished all of their cards does not get another turn
* The game is over when 3 players played all their cards
* Add sets of 2, 3 or 4 of the same card. 

The planning for week 3 is to finish the SHOULDS and get as far as possible with the COULDS. 

# References

* A tutorial for setting up a basic controller API in C#: [link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code)
* A basic tutorial for XUnit: [link](https://xunit.net/docs/getting-started/netcore/cmdline)
* F# documentation: [link](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/)

# Trivia

My favourite piece in F# (minor) is Rachmaninoff's 1st piano concerto - especially the [cadenza](https://youtu.be/y6EX3t2Mdnw?t=650)!

<!-- 
## ----- Dutch version -----


# Individueel project - Presidenten/tichu

Deze repository bevat mijn individuele project als afsluiting van het trainee-traject bij Sogyo. 

# Doel van het project

Dit project heeft als doelstelling het ontwikkelen van een full-stack web-applicatie voor het spelen van het spel presidenten of tichu. Tichu is een uitgebreidere variant van het spel presidenten, dus het doel is in eerste instantie om het spel presidenten te implementeren en daarna extra tichu regels toe te voegen, voor zover de tijd dat toelaat. 

**Persoonlijke leerdoelen**
* Consistenter TDD toepassen
* Handiger worden met nieuwe frameworks aan de praat krijgen

**Technische leerdoelen**
* Een nieuwe domeintaal met het .NET framework gebruiken (C#/F#)
* Beter leren begrijpen hoe servers communiceren
* (Eventueel) leren hoe je een echte website host

# Moscow requirements

**Must**
* UI met klikbare kaarten
* API laag
* Domeinlaag 
* Werkende game flow (iemand speelt een set kaarten, de volgende persoon kan een hogere set van dezelfde hoeveelheid spelen of passen)
* Als iedereen past mag de speler die de hoogste set heeft gespeeld de volgende ronde beginnen
* Een speler die al zijn kaarten kwijt is speelt niet meer mee
* Einde spel als alle spelers op 1 na hun kaarten kwijt zijn

**Should**
* Persistence via database
* Meer mogelijke setjes (opeenvolgede paren, straat, full house)
* 4 speciale kaarten: mahjong (vereenvoudigd), hond, draak (vereenvoudigd), feniks
* Kaarten in verschillende kleuren & setje 'straat van dezelfde kleur'

**Could**
* Spel hosten op een site, multiplayer functionaliteit
* Bom (4 of a kind of straat van dezelfde kleur) mag op elk moment op elke slag
* Puntentelling (5 voor 5, 10 voor 10 & koning, 25 voor draak, -25 voor feniks)
* Teams (als eerste 2 uit = 200 punten & 0 punten voor de tegenstander)
* Speciale regels draak & mahjong
* Kleine & grote tichu
* Laatste uit: handkaarten naar tegenstander, slagen naar winnaar
* Doorgeven van kaarten aan het begin van elke ronde

**Won't**
* Fancy UI design
* Player ratings
* AI

# Framework & Installatie

De back-end van dit project draait op .NET; de domeinlaag is geprogrammeerd in F# en de API- en persistence lagen staan in C#. Voor de front-end wordt Node.js gebruikt in combinatie met React, Typescript en een Vite server. De database is momenteel nog niet geïmplementeerd (in plaats daarvan wordt nu een simpele in-memory dictionary gebruikt). Dit wordt uiteindelijk MySQL.

Om de back-end server op te starten run je het volgende commando vanuit de API folder:
```bash
dotnet run
```
Deze server luistert vanuit poort 5036. 

De front-end server start je op door eerst naar de Client folder te gaan. Om de dependencies te installeren (alleen de eerste keer) run je het volgende commando:
```bash
npm install
```
Vervolgens start je de server als volgt:
```bash
npm run dev
```
De front-end server luistert op poort 5173. Ga in de browser naar http://localhost:5173/ om het spel te spelen.

# Planning

Het doel voor week 1 was om de front-end en back-end draaiende te krijgen en met elkaar te laten praten. Daarna zijn de volgende 'vertical slices' stap voor stap  toegevoegd:
* Vanaf de front-end een fetch naar de back-end sturen (bijv. haal kaarten op)
* Een POST request sturen en het effect zien in de UI
* Klikbare kaarten creëren die verdwijnen als je erop klikt
* Laatst gespeelde kaart zichtbaar in het midden
* Gespeelde set moet hoger zijn dan de vorige set

De planning voor week 2 was om de rest van de MUSTS af te krijgen. Dit heb ik onderverdeeld in de volgende vertical slices:
* Meerdere spelers die de beurt aan elkaar doorgeven. Hand van de actieve speler is zichtbaar.
* Een pas-knop
* Einde van een slag 
* Een speler die al zijn kaarten kwijt is krijgt geen beurten meer
* Het spel is klaar als 3 spelers al hun kaarten kwijt zijn
* Er kunnen ook setjes van 2, 3 of 4 van dezelfde kaart worden gespeeld

De planning voor week 3 is om de SHOULDS af te maken en zo ver mogelijk te komen met de COULDS.

# Links

* Een tutorial voor het opzetten van een standaard controller API in C#: [link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code)
* Een basistutorial voor XUnit: [link](https://xunit.net/docs/getting-started/netcore/cmdline)
* F# documentatie: [link](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/)

# Trivia

Mijn favoriete stuk in F# (klein) is Rachmaninoff's 1e pianoconcert - vooral de [cadens](https://youtu.be/y6EX3t2Mdnw?t=650)! -->
