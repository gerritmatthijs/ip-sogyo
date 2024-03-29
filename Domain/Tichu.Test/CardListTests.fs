module CardListTests

open Xunit
open Tichu

let RemoveCardsStringVersion(hand: string, set: string): string = 
    hand |> CardList.StringToCardList |> CardList.RemoveCards(CardList.StringToCardList set) |> CardList.CardListToString

[<Fact>]
let ``Remove a single card from a hand`` () = 
    let handstring = "23679TTK"
    let newHand = RemoveCardsStringVersion(handstring, "T")
    Assert.Equal("23679TK", newHand)

[<Fact>]
let ``Remove multiple cards from a hand`` () = 
    let handstring = "2234556779TKA"
    let set = "234567"
    let newHand = RemoveCardsStringVersion(handstring, set)
    Assert.Equal("2579TKA", newHand)

[<Fact>]
let ``Removing a card that is not in the hand gives error`` () = 
    let handstring = "223556779TKA"
    let set = "234567"
    Assert.Throws<System.Exception>(fun () -> RemoveCardsStringVersion(handstring, set) :> obj)

[<Fact>]
let ``Remove full house from hand`` () =
    let handstring = "333477889KK"
    let newHand = RemoveCardsStringVersion(handstring, "333KK")
    Assert.Equal("477889", newHand)

[<Fact>]
let ``Convert string to card list`` () = 
    let handstring = "24TD"
    let convertedHand = CardList.StringToCardList(handstring)
    Assert.Equal<Card list>([Card.Card('2'); Card.Card('4'); Card.Card('T'); Card.Card('D')], convertedHand)

[<Fact>]
let ``Convert card list to string`` () =
    let hand = [Card.Card('2'); Card.Card('4'); Card.Card('T'); Card.Card('D')]
    let handstring = CardList.CardListToString(hand)
    Assert.Equal("24TD", handstring)
