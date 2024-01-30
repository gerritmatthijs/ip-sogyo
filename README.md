# Individueel project - Presidenten/tichu

Deze repository bevat mijn individuele project als afsluiting van het trainee traject bij Sogyo. 

# Doel van het project

Dit project heeft als doelstelling het ontwikkelen van een full-stack web-applicatie voor het spelen van het spel presidenten of tichu. Tichu is een uitgebreidere variant van het spel presidenten, dus het doel is in eerste instantie om het spel presidenten te implementeren en daarna extra tichu regels toe te voegen, voor zover de tijd dat toelaat. 

**Persoonlijke leerdoelen**
* consistenter TDD toepassen
* Handiger worden met nieuwe frameworks aan de praat krijgen

**Technische leerdoelen**
* Een nieuwe domeintaal met het dotnet framework gebruiken (C#/F#)
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

# Framework

Het voorlopige idee is om C# (misschien F#) te gebruiken voor de backend, React of Vue voor de front-end, en MySQL voor de database.

Om de back-end server op te starten run je het volgende commando vanuit de API folder:
```bash
dotnet run
```
Deze luistert vanuit poort 5036. 

De front-end server start je op door eerst naar de Client folder te gaan. Om de dependencies te installeren (alleen de eerste keer) run je het volgende commando:
```bash
npm install
```
Vervolgens start je de server als volgt:
```bash
npm run dev
```
De front-end server luistert op poort 5173.

# Planning

Het doel voor week 1 is om de front-end en back-end draaiende te krijgen en met elkaar te laten praten. Daarna kunnen de volgende 'vertical slices' stap voor stap worden toegevoegd:
* Vanaf de Front-end een fetch naar de back-end sturen (bijv. haal kaarten op)
* Een POST request sturen en het effect zien in de UI
* Checken of een set te spelen kaarten aan de regels voldoet
* Meerdere spelers die na elkaar spelen
* Gespeelde set moet hoger zijn dan de vorige set

# References

* A tutorial for setting up a basic controller API in C#: [link](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code)
* A basic tutorial for XUnit: [link](https://xunit.net/docs/getting-started/netcore/cmdline)
* F# documentation: [link](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/)

# Trivia

My favourite piece in F# (minor) is Rachmaninoff's 1st piano concerto - especially the [cadenza](https://youtu.be/y6EX3t2Mdnw?t=650)!