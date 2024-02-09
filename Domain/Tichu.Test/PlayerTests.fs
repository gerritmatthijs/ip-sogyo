module PlayerTests

open Xunit
open Tichu

[<Fact>]
let ``Player creation`` () = 
    let henk = {name = "Henk"; hand = "2233" |> Hand.StringToCardList}
    Assert.Equal("Henk", henk.name)
    Assert.Equal("2233", henk.hand |> Hand.CardListToString)

[<Fact>]
let ``Remove cards from hand`` () = 
    let henk = {name = "Henk"; hand = "2233" |> Hand.StringToCardList}
    let piet = {name = "Piet"; hand = "66667" |> Hand.StringToCardList}
    let newHenk = henk |> Player.PlayCards({card = {value = '2'}; number = 2})
    let newPiet = piet |> Player.PlayCards({card = {value = '6'}; number = 3})
    Assert.Equal("33", newHenk.hand |> Hand.CardListToString)
    Assert.Equal("67", newPiet.hand |> Hand.CardListToString)

