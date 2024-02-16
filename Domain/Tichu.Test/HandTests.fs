module HandTests

open Xunit
open Tichu

let RemoveCardsStringVersion(hand: string, set: string): string = 
    hand |> Card.StringToCardList |> CardList.RemoveCards(Card.StringToCardList set) |> Card.CardListToString

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