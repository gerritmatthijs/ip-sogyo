module PlayerTests

open Xunit
open Tichu

[<Fact>]
let ``Player creation`` () = 
    let henk = {name = "Henk"; hand = "2233" |> Card.StringToCardList}
    Assert.Equal("Henk", henk.name)
    Assert.Equal("2233", henk.hand |> Card.CardListToString)

[<Fact>]
let ``Remove cards from hand`` () = 
    let henk = {name = "Henk"; hand = "2233" |> Card.StringToCardList}
    let piet = {name = "Piet"; hand = "66667" |> Card.StringToCardList}
    let newHenk = henk |> Player.PlayCards("22" |> Card.StringToCardList)
    let newPiet = piet |> Player.PlayCards("666" |> Card.StringToCardList)
    Assert.Equal("33", newHenk.hand |> Card.CardListToString)
    Assert.Equal("67", newPiet.hand |> Card.CardListToString)

