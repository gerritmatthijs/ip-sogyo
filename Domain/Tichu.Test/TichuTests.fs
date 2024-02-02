module TichuTests

open Xunit
open Tichu

[<Fact>]
let ``Passing Test`` () =
    Assert.True(true)

[<Fact>]
let ``Tichu Creation`` () = 
    let tichu = new TichuGame("2357TK", None) :> ITichu
    Assert.Equal("2357TK", tichu.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Play single card removes it from hand`` () = 
    let handstring = "2346TTK"
    let tichu = new TichuGame(handstring, None) :> ITichu
    let newTichu = tichu.DoTurn("Gerrit", "T")
    // let hand = 
    //     match tryNewTichu with 
    //     | Ok newTichu -> newTichu.GetPlayerHand("Gerrit")
    //     | Error e -> handstring
    // Assert.True(Result.isOk tryNewTichu)
    Assert.Equal("2346TK", newTichu.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Get last played card before and after a turn`` () = 
    let handstring = "2346TTK"
    let tichu = new TichuGame(handstring, None) :> ITichu
    let newTichu = tichu.DoTurn("Gerrit", "T")
    Assert.Equal("", tichu.GetLastPlayed())
    Assert.Equal("T", newTichu.GetLastPlayed())