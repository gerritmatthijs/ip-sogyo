module TichuTests

open Xunit
open Tichu

let SetUpGame () = 
    new TichuGame("Gerrit", "2346TTK", None) :> ITichu

[<Fact>]
let ``Tichu Creation`` () = 
    let tichu = SetUpGame()
    Assert.Equal("2346TTK", tichu.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Get player name`` () = 
    let tichu = SetUpGame()
    Assert.Equal("Gerrit", tichu.GetPlayerName(0))

[<Fact>]
let ``Get player hand`` () = 
    let tichu = SetUpGame()
    Assert.Equal("2346TTK", tichu.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Play single card removes it from hand`` () = 
    let tichu = SetUpGame()
    let newTichu = tichu.DoTurn("Gerrit", "T")
    // let hand = 
    //     match tryNewTichu with 
    //     | Ok newTichu -> newTichu.GetPlayerHand("Gerrit")
    //     | Error e -> handstring
    // Assert.True(Result.isOk tryNewTichu)
    Assert.Equal("2346TK", newTichu.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Get last played card before and after a turn`` () = 
    let tichu = SetUpGame()
    let newTichu = tichu.DoTurn("Gerrit", "T")
    Assert.Equal("", tichu.GetLastPlayed())
    Assert.Equal("T", newTichu.GetLastPlayed())

[<Fact>]
let ``Play higher card is allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "T")
    Assert.Equal("OK", tichu1.CheckAllowed "K")

[<Fact>]
let ``Play lower card is not allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "K")
    Assert.Equal("Your card has to be higher than the last played card.", tichu1.CheckAllowed("T"))

[<Fact>]
let ``DoTurn throws exception if move is not allowed`` () = 
    let tichu = SetUpGame()
    let tichu1 = tichu.DoTurn("Gerrit", "4")
    Assert.Throws<System.Exception>(fun () -> tichu1.DoTurn("Gerrit", "3") :> obj)