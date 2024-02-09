module ActionTests

open Xunit
open Tichu

[<Fact>]
let ``Convert string to Action`` () = 
    let playAction = "333" |> Action.ToAction
    let unwrappedSet = 
        match playAction with
        | Pass -> ""
        | Set(set) -> set |> CardSet.CardSetToString
    Assert.Equal("333", unwrappedSet)

[<Fact>]
let ``Play higher set of same type is OK`` () = 
    let lastPlay = Some("333" |> CardSet.StringToCardSet)
    let newAction = "444" |> Action.ToAction
    Assert.Equal("OK", newAction |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Play lower or equal set of same type returns alert text`` () = 
    let lastPlay = Some("44" |> CardSet.StringToCardSet)
    let lowerAction = "33" |> Action.ToAction
    let equalAction = "44" |> Action.ToAction
    Assert.Equal("Your card set has to be higher than the last played card set.", lowerAction |> Action.GetAlertTextOrOK(lastPlay))
    Assert.Equal("Your card set has to be higher than the last played card set.", equalAction |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Play wrong type of set returns alert text`` () = 
    let lastPlay = Some("44" |> CardSet.StringToCardSet)
    let action = "666" |> Action.ToAction
    Assert.Equal("You can only play sets of 2 cards of the same height in this trick.", action |> Action.GetAlertTextOrOK(lastPlay))

[<Fact>]
let ``Pass when starting a trick returns alert text`` () = 
    let lastPlay = None
    let action = "pass" |> Action.ToAction
    Assert.Equal("You cannot pass when opening a trick.", action |> Action.GetAlertTextOrOK(lastPlay))