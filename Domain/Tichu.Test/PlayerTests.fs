module PlayerTests

open Xunit
open Tichu

[<Fact>]
let ``Player creation`` () = 
    let henk = {name = "Henk"; hand = "2233" |> CardList.StringToCardList}
    Assert.Equal("Henk", henk.name)
    Assert.Equal("2233", henk.hand |> CardList.CardListToString)

[<Fact>]
let ``Remove cards from hand`` () = 
    let henk = {name = "Henk"; hand = "2233" |> CardList.StringToCardList}
    let piet = {name = "Piet"; hand = "66667" |> CardList.StringToCardList}
    let newHenk = henk |> Player.PlayCards("22" |> CardList.StringToCardList)
    let newPiet = piet |> Player.PlayCards("666" |> CardList.StringToCardList)
    Assert.Equal("33", newHenk.hand |> CardList.CardListToString)
    Assert.Equal("67", newPiet.hand |> CardList.CardListToString)

