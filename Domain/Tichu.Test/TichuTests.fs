module TichuTests

open Xunit
open Tichu

[<Fact>]
let ``Passing Test`` () =
    Assert.True(true)

[<Fact>]
let ``Tichu Creation`` () = 
    let tichu = new TichuGame("2357TK") :> ITichu
    Assert.Equal("2357TK", tichu.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Play single card removes it from hand`` () = 
    let handstring = "2346TTK"
    let tichu = new TichuGame(handstring) :> ITichu
    let tryNewTichu = tichu.DoTurn("Gerrit", "T")
    let hand = 
        match tryNewTichu with 
        | Ok newTichu -> newTichu.GetPlayerHand("Gerrit")
        | Error e -> handstring
    Assert.True(Result.isOk tryNewTichu)
    Assert.Equal("2346TK", hand)
