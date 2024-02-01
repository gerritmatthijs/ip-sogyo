module HandTests

open Xunit
open Tichu

[<Fact>]
let ``Convert string to card list and back`` () = 
    let handstring = "24T"
    let convertedHand : Card list = Hand.StringToCardList(handstring)
    let doubleConvertedHand = Hand.CardListToString(convertedHand)
    Assert.Equal(handstring, doubleConvertedHand)

[<Fact>]
let ``Remove a single card from a hand`` () = 
    let handstring = "23679TTK"
    let newHand = Hand.RemoveCardsStringVersion(handstring, "T")
    Assert.Equal("23679TK", newHand)

[<Fact>]
let ``Remove multiple cards from a hand`` () = 
    let handstring = "2234556779TKA"
    let set = "234567"
    let newHand = Hand.RemoveCardsStringVersion(handstring, set)
    Assert.Equal("2579TKA", newHand)

[<Fact>]
let ``Removing a card that is not in the hand gives error`` () = 
    let handstring = "223556779TKA"
    let set = "234567"
    Assert.Throws<System.Exception>(fun () -> Hand.RemoveCardsStringVersion(handstring, set) :> obj)