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

[<Fact>]
let ``Play higher set of same type is OK`` () = 
    let lastPlay = Some("333" |> Card.StringToCardList)
    let newAction = "444" |> Action.ToAction
    Assert.Equal("OK", newAction |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Play lower or equal set of same type returns alert text`` () = 
    let lastPlay = Some("44" |> Card.StringToCardList)
    let lowerAction = "33" |> Action.ToAction
    let equalAction = "44" |> Action.ToAction
    Assert.Equal("Your card set has to be higher than the last played card set.", lowerAction |> Action.GetAlertTextOrOK(lastPlay))
    Assert.Equal("Your card set has to be higher than the last played card set.", equalAction |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Play wrong type of set returns alert text`` () = 
    let lastPlay = Some("44" |> Card.StringToCardList)
    let action = "666" |> Action.ToAction
    Assert.Equal("You can only play sets of the same type as the leading set.", action |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Pass when starting a trick returns alert text`` () = 
    let lastPlay = None
    let action = "pass" |> Action.ToAction
    Assert.Equal("You cannot pass when opening a trick.", action |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Play invalid set returns alert text`` () =
    let lastPlay = None
    let action = "23" |> Action.ToAction
    Assert.Equal("Invalid card set.", action |> Action.GetAlertTextOrOK(lastPlay))