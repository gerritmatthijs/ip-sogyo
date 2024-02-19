module TichuFactoryTests

open Xunit
open Tichu

[<Fact>]
let ``Create new game through factory`` () = 
    let factory = new TichuFactory() :> ITichuFactory
    let tichuGame = factory.CreateNewGame(["Gerrit"; "Daniel"; "Wesley"; "Hanneke"])
    Assert.Equal(14, tichuGame.GetPlayerHand(0).Length)
    Assert.Equal(14, tichuGame.GetPlayerHand(1).Length)
    Assert.NotEqual<string>(tichuGame.GetPlayerHand(0), tichuGame.GetPlayerHand(1))
    Assert.NotEqual<string>("12222333344445", tichuGame.GetPlayerHand(0))

[<Fact>]
let ``Create existing game through factory`` () =
    let factory = new TichuFactory() :> ITichuFactory
    let names = ["Gerrit"; "Daniel"; "Wesley"; "Hanneke"]
    let hands = ["44557JJ"; "3356789A"; "229TQK"; "66699K"]
    let tichuGame = factory.CreateExistingGame(names, hands, "Gerrit", "3", 1)
    Assert.Equal("44557JJ", tichuGame.GetPlayerHand(0))
    Assert.Equal("229TQK", tichuGame.GetPlayerHand(2))
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