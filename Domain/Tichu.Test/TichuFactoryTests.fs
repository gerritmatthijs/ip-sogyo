module TichuFactoryTests

open Xunit
open Tichu

[<Fact>]
let ``Create new game through factory`` () = 
    let factory = new TichuFactory() :> ITichuFactory
    let tichuGame = factory.CreateNewGame(["Gerrit"; "Daniel"; "Wesley"; "Hanneke"])
    Assert.Equal(13, tichuGame.GetPlayerHand("Gerrit").Length)
    Assert.Equal(13, tichuGame.GetPlayerHand("Daniel").Length)
    Assert.NotEqual<string>(tichuGame.GetPlayerHand("Gerrit"), tichuGame.GetPlayerHand("Daniel"))
    Assert.NotEqual<string>("2222333344445", tichuGame.GetPlayerHand("Gerrit"))

[<Fact>]
let ``Create existing game through factory`` () =
    let factory = new TichuFactory() :> ITichuFactory
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["44557JJ"; "3356789A"; "229TQK"; "66699K"]
    let tichuGame = factory.CreateExistingGame(names, hands, "Gerrit", "3", 1)
    Assert.Equal("44557JJ", tichuGame.GetPlayerHand("Gerrit"))
    Assert.Equal("229TQK", tichuGame.GetPlayerHand("Wesley"))
    Assert.Equal("Gerrit", tichuGame.GetCurrentLeader())
    Assert.Equal("3", tichuGame.GetLastPlayed())
    Assert.Equal(1, tichuGame.GetTurn())

[<Fact>]
let ``Create existing game at start of trick through factory `` () =
    let factory = new TichuFactory() :> ITichuFactory
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["44557JJ"; "3356789A"; "229TQK"; "66699K"]
    let tichuGame = factory.CreateExistingGame(names, hands, "", "", 2)
    Assert.Equal("", tichuGame.GetCurrentLeader())
    Assert.Equal("", tichuGame.GetLastPlayed())
    Assert.Equal(2, tichuGame.GetTurn())