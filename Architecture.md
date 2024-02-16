# Individual Project - Architecture

The architecture of the project is roughly described by the following diagram:

![image](architecture-diagram.drawio.png)

This diagram shows how the project is divided into a Client layer, API layer, domain layer and persistence layer. The front-end server deals with the Client layer, and the back-end server deals with the rest. The start entity is the place where the project is executed. The entry point of the user into the front-end server is represented by the user entity. 

# Domain layout

The ITichuFacade and ITichuFactory are the only parts of the domain exposed to the other layers. The interface ITichuFacade consists of several information getters and a DoTurn funtion. The data communicated by these methods are simple string and integer formats (and the interface itself), so that C# can call these methods without issues. The TichuFacade object's main job is to implement these interface members and convert between basic formats and F#-specific objects such as records (or product types) and discrimated unions (or sum types). 

In particular, the TichuGame object defined in the Tichu file is nothing but a record containing the game state. Since F# is a functional programming language, the TichuGame object does not have mutable state. Consequentely, it also has no member methods changing state. As such, the main 'DoTurn' function is not a member function of the TichuGame object but rather accepts a TichuGame object together with an 'Action' object representing the player action. It outputs a completely new TichuGame object, containing the updated game state (each of whose components are also created anew).

The logic of the DoTurn function is spread over several modules, each representing some smaller object of the game state. For example, the logic for determing the type of a set of cards (pair, straight, full house, etc) as well as the logic for comparing these types is contained in the 'CardSet' module. The logic for removing a set of cards from a hand (i.e. a list of cards) and sorting a list of cards is contained in the CardList module. 

The cards themselves are represented by a discrimanted union type 'Card'. A Card is one of the special cases or the 'Normal' case. The Normal case contains a char value representing the card ('2' for two, 'T' for ten, 'J' for jack, etc.). If I get around to implementing suits, it will also have a suit value. 

The Phoenix case is a bit special: it contains 2 values to represent in what way it is being played. The 'isSingle' value represents if it is played as a single or as part of a set. The 'valueCopied' value represents what card it is being declared as. If it is played as a single, it instead represents the previous card of the trick. The logic for this declaration is contained in a separate module 'Phoenix'.

The Card type has a member function returning the numeric value of the card: 11 for jack, 12 for queen, etc. This is used to compare the height of sets, and to order cards in a card list. The dragon is given a value of 100, since it's the highest single card. The hound/dog does not really have a numeric value, but since my personal preference is to order it all the way to the right of my hand, I gave it a very high value. The phoenix has a value of 99 before being declared for ordering purposes. After being declared, it takes on the numeric value of the card it is being declared as, or 1/2 higher than the previous card if it is played as a single. 

Let us walk through what happens when the DoTurn function is called. 