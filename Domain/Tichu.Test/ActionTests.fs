module ActionTests

open Xunit
open Tichu

[<Fact>]
let ``Convert string to Action`` () = 
    let playAction = "33" |> Action.ToAction
    let passAction = "pass" |> Action.ToAction
    let cardList = [Card.Card('3'); Card.Card('3')]
    Assert.Equal(Set(cardList), playAction)
    Assert.Equal(Pass, passAction)