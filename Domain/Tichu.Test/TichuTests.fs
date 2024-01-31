module TichuTests

open Xunit
open Tichu

[<Fact>]
let PassingTest () =
    Assert.True(true)

[<Fact>]
let TichuCreation() = 
    let tichu = new TichuGame("2357TK") :> ITichu
    Assert.Equal("2357TK", tichu.getPlayerHand("Gerrit"))